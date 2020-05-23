// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.TeamAIEnableLogic
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System;
using TaleWorlds.MountAndBlade;

namespace VirtualBattlegrounds
{
  public class TeamAIEnableLogic : MissionLogic
  {
    private BattleConfigBase _config;

    public TeamAIEnableLogic(BattleConfigBase config)
    {
      this.\u002Ector();
      this._config = config;
    }

    public virtual void OnBehaviourInitialize()
    {
      ((MissionBehaviour) this).OnBehaviourInitialize();
      ((MissionBehaviour) this).get_Mission().get_Teams().add_OnPlayerTeamChanged((Action<Team, Team>) ((oldTeam, newTeam) => Utility.ApplyTeamAIEnabled(this._config)));
      // ISSUE: method pointer
      ((SiegeMissionController) ((MissionBehaviour) this).get_Mission().GetMissionBehaviour<SiegeMissionController>())?.add_PlayerDeploymentFinish(new CampaignSiegeMissionEvent((object) this, __methodptr(OnDeploymentFinished)));
    }

    public virtual void AfterStart()
    {
      ((MissionBehaviour) this).AfterStart();
      Utility.ApplyTeamAIEnabled(this._config);
    }

    private void OnDeploymentFinished()
    {
      Utility.ApplyTeamAIEnabled(this._config);
    }
  }
}
