// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.MakeGruntVoiceLogic
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System;
using System.Collections.Generic;
using TaleWorlds.MountAndBlade;

namespace VirtualBattlegrounds
{
  internal class MakeGruntVoiceLogic : MissionLogic
  {
    private List<MakeGruntVoiceLogic.Pair> _formations;

    public void AddFormation(Formation formation, float timer)
    {
      if (this._formations.Exists((Predicate<MakeGruntVoiceLogic.Pair>) (pair => pair.formation == formation)))
        return;
      this._formations.Add(new MakeGruntVoiceLogic.Pair()
      {
        formation = formation,
        timer = timer
      });
    }

    public virtual void OnClearScene()
    {
      this._formations.Clear();
    }

    public virtual void OnMissionTick(float dt)
    {
      this._formations.RemoveAll((Predicate<MakeGruntVoiceLogic.Pair>) (pair =>
      {
        pair.timer -= dt;
        if ((double) pair.timer >= 0.0)
          return false;
        using (List<Agent>.Enumerator enumerator = ((Team) pair.formation.Team).get_ActiveAgents().GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            Agent current = enumerator.Current;
            if (current != ((MissionBehaviour) this).get_Mission().get_MainAgent())
              current.MakeVoice((SkinVoiceManager.SkinVoiceType) SkinVoiceManager.VoiceType.Grunt, (SkinVoiceManager.CombatVoiceNetworkPredictionType) 2);
          }
        }
        return true;
      }));
    }

    public MakeGruntVoiceLogic()
    {
      base.\u002Ector();
    }

    private class Pair
    {
      public Formation formation;
      public float timer;
    }
  }
}
