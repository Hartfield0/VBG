// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.ResetMissionLogic
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using TaleWorlds.InputSystem;
using TaleWorlds.MountAndBlade;

namespace VirtualBattlegrounds
{
  internal class ResetMissionLogic : MissionLogic
  {
    private EnhancedFreeBattleMissionController _controller;

    public virtual void OnBehaviourInitialize()
    {
      ((MissionBehaviour) this).OnBehaviourInitialize();
      this._controller = (EnhancedFreeBattleMissionController) ((MissionBehaviour) this).get_Mission().GetMissionBehaviour<EnhancedFreeBattleMissionController>();
    }

    public virtual void OnMissionTick(float dt)
    {
      ((MissionBehaviour) this).OnMissionTick(dt);
      if (!Input.IsKeyPressed((InputKey) 88))
        return;
      this.ResetMission();
    }

    public void ResetMission()
    {
      ((MissionBehaviour) this).get_Mission().ResetMission();
      ((MissionBehaviour) this).get_Mission().get_PlayerTeam().get_PlayerOrderController().Owner = null;
      ((MissionBehaviour) this).get_Mission().get_PlayerEnemyTeam().get_PlayerOrderController().Owner = null;
      this._controller?.SpawnAgents();
      Utility.DisplayLocalizedText("str_mission_reset", (string) null);
    }

    public ResetMissionLogic()
    {
      base.\u002Ector();
    }
  }
}
