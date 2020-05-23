// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.ControlTroopAfterPlayerDeadLogic
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace VirtualBattlegrounds
{
  internal class ControlTroopAfterPlayerDeadLogic : MissionLogic
  {
    public void ControlTroopAfterDead()
    {
      if (!Utility.IsPlayerDead() || !Utility.IsAgentDead((Agent) ((MissionBehaviour) this).get_Mission().get_PlayerTeam().get_PlayerOrderController().Owner))
        return;
      Mission mission = ((MissionBehaviour) this).get_Mission();
      Team playerTeam = ((MissionBehaviour) this).get_Mission().get_PlayerTeam();
      WorldPosition worldPosition = new WorldPosition(((MissionBehaviour) this).get_Mission().get_Scene(), ((MissionBehaviour) this).get_Mission().get_Scene().get_LastFinalRenderCameraPosition());
      Vec3 groundVec3 = ((WorldPosition) ref worldPosition).GetGroundVec3();
      Agent agent = mission.GetClosestAllyAgent(playerTeam, groundVec3, 1000f) ?? ((MissionBehaviour) this).get_Mission().get_PlayerTeam().get_Leader();
      if (agent != null)
      {
        Utility.DisplayLocalizedText("str_control_troop", (string) null);
        agent.set_Controller((Agent.ControllerType) 2);
      }
      else
        Utility.DisplayLocalizedText("str_no_troop_to_control", (string) null);
    }

    public virtual void OnMissionTick(float dt)
    {
      ((MissionBehaviour) this).OnMissionTick(dt);
      if (!((MissionBehaviour) this).get_Mission().get_InputManager().IsKeyPressed((InputKey) 33))
        return;
      this.ControlTroopAfterDead();
    }

    public ControlTroopAfterPlayerDeadLogic()
    {
      base.\u002Ector();
    }
  }
}
