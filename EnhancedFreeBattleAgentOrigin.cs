// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.EnhancedFreeBattleAgentOrigin
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace VirtualBattlegrounds
{
  public class EnhancedFreeBattleAgentOrigin : IAgentOriginBase
  {
    private EnhancedTroopSupplier _troopSupplier;
    private bool _isRemoved;
    private readonly UniqueTroopDescriptor _descriptor;

    public CustomBattleCombatant CustomBattleCombatant { get; private set; }

    IBattleCombatant IAgentOriginBase.BattleCombatant
    {
      get
      {
        return (IBattleCombatant) this.CustomBattleCombatant;
      }
    }

    public BasicCharacterObject Troop { get; private set; }

    public int Rank { get; private set; }

    public Banner Banner
    {
      get
      {
        return this.CustomBattleCombatant.get_Banner();
      }
    }

    public bool IsUnderPlayersCommand { get; }

    public uint FactionColor
    {
      get
      {
        return this.CustomBattleCombatant.get_BasicCulture().get_Color();
      }
    }

    public uint FactionColor2
    {
      get
      {
        return this.CustomBattleCombatant.get_BasicCulture().get_Color2();
      }
    }

    public int Seed
    {
      get
      {
        return !this.Troop.get_IsHero() ? this.Troop.GetDefaultFaceSeed(this.Rank) : 0;
      }
    }

    public int UniqueSeed
    {
      get
      {
        if (this.Troop.get_IsHero())
          return 0;
        UniqueTroopDescriptor descriptor = this._descriptor;
        return ((UniqueTroopDescriptor) ref descriptor).get_UniqueSeed();
      }
    }

    public bool IsCoopTroop
    {
      get
      {
        return false;
      }
    }

    public VirtualPlayer Peer
    {
      get
      {
        return (VirtualPlayer) null;
      }
    }

    public EnhancedFreeBattleAgentOrigin(
      CustomBattleCombatant customBattleCombatant,
      EnhancedTroopSupplier troopSupplier,
      BasicCharacterObject characterObject,
      int rank = -1,
      UniqueTroopDescriptor uniqueNo = null)
    {
      this.CustomBattleCombatant = customBattleCombatant;
      this._troopSupplier = troopSupplier;
      this.Troop = characterObject;
      this._descriptor = !((UniqueTroopDescriptor) ref uniqueNo).get_IsValid() ? new UniqueTroopDescriptor(Game.get_Current().get_NextUniqueTroopSeed()) : uniqueNo;
      this.Rank = rank == -1 ? MBRandom.RandomInt(10000) : rank;
      this.IsUnderPlayersCommand = Mission.get_Current().get_PlayerTeam().get_Side() == customBattleCombatant.get_Side();
    }

    public void SetWounded()
    {
      if (this._isRemoved)
        return;
      this._troopSupplier?.OnTroopWounded();
      this._isRemoved = true;
    }

    public void SetKilled()
    {
      if (this._isRemoved)
        return;
      this._troopSupplier?.OnTroopKilled();
      this._isRemoved = true;
    }

    public void SetRouted()
    {
      if (this._isRemoved)
        return;
      this._troopSupplier?.OnTroopRouted();
      this._isRemoved = true;
    }

    public void OnAgentRemoved(float agentHealth)
    {
    }

    void IAgentOriginBase.OnScoreHit(
      BasicCharacterObject victim,
      int damage,
      bool isFatal,
      bool isTeamKill,
      int weaponKind,
      int currentWeaponUsageIndex)
    {
    }

    public void SetBanner(Banner banner)
    {
    }
  }
}
