// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.EnhancedTroopSupplier
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace VirtualBattlegrounds
{
  public class EnhancedTroopSupplier : IMissionTroopSupplier
  {
    private bool _anyTroopRemainsToBeSupplied = true;
    private readonly CustomBattleCombatant _customBattleCombatant;
    private PriorityQueue<float, BasicCharacterObject> _characters;
    private int _numAllocated;
    private int _numWounded;
    private int _numKilled;
    private int _numRouted;

    public EnhancedTroopSupplier(CustomBattleCombatant customBattleCombatant)
    {
      this._customBattleCombatant = customBattleCombatant;
      this.ArrangePriorities();
    }

    private void ArrangePriorities()
    {
      this._characters = new PriorityQueue<float, BasicCharacterObject>((IComparer<float>) new GenericComparer<float>());
      int[] numArray = new int[8];
      for (int i = 0; i < 8; i++)
        numArray[i] = this._customBattleCombatant.get_Characters().Count<BasicCharacterObject>((Func<BasicCharacterObject, bool>) (character => character.get_CurrentFormationClass() == i));
      int num = 1000;
      using (IEnumerator<BasicCharacterObject> enumerator = this._customBattleCombatant.get_Characters().GetEnumerator())
      {
        while (((IEnumerator) enumerator).MoveNext())
        {
          BasicCharacterObject current = enumerator.Current;
          FormationClass currentFormationClass = current.get_CurrentFormationClass();
          this._characters.Enqueue(current.get_IsHero() ? (float) num-- : (float) (numArray[currentFormationClass] / ((IEnumerable<int>) numArray).Sum()), current);
          --numArray[currentFormationClass];
        }
      }
    }

    public IEnumerable<IAgentOriginBase> SupplyTroops(
      int numberToAllocate)
    {
      List<BasicCharacterObject> basicCharacterObjectList = this.AllocateTroops(numberToAllocate);
      EnhancedFreeBattleAgentOrigin[] battleAgentOriginArray = new EnhancedFreeBattleAgentOrigin[basicCharacterObjectList.Count];
      this._numAllocated += basicCharacterObjectList.Count;
      for (int rank = 0; rank < battleAgentOriginArray.Length; ++rank)
      {
        UniqueTroopDescriptor uniqueNo;
        ((UniqueTroopDescriptor) ref uniqueNo).\u002Ector(Game.get_Current().get_NextUniqueTroopSeed());
        battleAgentOriginArray[rank] = new EnhancedFreeBattleAgentOrigin(this._customBattleCombatant, this, basicCharacterObjectList[rank], rank, uniqueNo);
      }
      if (battleAgentOriginArray.Length < numberToAllocate)
        this._anyTroopRemainsToBeSupplied = false;
      return (IEnumerable<IAgentOriginBase>) battleAgentOriginArray;
    }

    private List<BasicCharacterObject> AllocateTroops(int numberToAllocate)
    {
      if (numberToAllocate > this._characters.get_Count())
        numberToAllocate = this._characters.get_Count();
      List<BasicCharacterObject> basicCharacterObjectList = new List<BasicCharacterObject>();
      for (int index = 0; index < numberToAllocate; ++index)
        basicCharacterObjectList.Add(this._characters.DequeueValue());
      return basicCharacterObjectList;
    }

    public void OnPlayerAllocated()
    {
      ++this._numAllocated;
    }

    public void OnTroopWounded()
    {
      ++this._numWounded;
    }

    public void OnTroopKilled()
    {
      ++this._numKilled;
    }

    public void OnTroopRouted()
    {
      ++this._numRouted;
    }

    public int NumActiveTroops
    {
      get
      {
        return this._numAllocated - (this._numWounded + this._numKilled + this._numRouted);
      }
    }

    public int NumRemovedTroops
    {
      get
      {
        return this._numWounded + this._numKilled + this._numRouted;
      }
    }

    public int NumTroopsNotSupplied
    {
      get
      {
        return this._characters.get_Count() - this._numAllocated;
      }
    }

    public bool AnyTroopRemainsToBeSupplied
    {
      get
      {
        return this._anyTroopRemainsToBeSupplied;
      }
    }
  }
}
