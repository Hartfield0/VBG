// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.TeleportPlayerLogic
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace VirtualBattlegrounds
{
  internal class TeleportPlayerLogic : MissionLogic
  {
    public virtual void OnMissionTick(float dt)
    {
      ((MissionBehaviour) this).OnMissionTick(dt);
      Agent owner = (Agent) ((MissionBehaviour) this).get_Mission().get_PlayerTeam().get_PlayerOrderController().Owner;
      if (!((MissionBehaviour) this).get_Mission().get_InputManager().IsKeyPressed((InputKey) 38) || ((MissionBehaviour) this).get_Mission().get_MainAgent() != null || Utility.IsAgentDead(owner))
        return;
      this.TeleportAgent(owner);
    }

    public void TeleportAgent(Agent agent)
    {
      try
      {
        WorldPosition worldPosition = new WorldPosition(((MissionBehaviour) this).get_Mission().get_Scene(), ((MissionBehaviour) this).get_Mission().get_Scene().get_LastFinalRenderCameraPosition());
        Vec3 groundVec3 = ((WorldPosition) ref worldPosition).GetGroundVec3();
        Vec3 vec3 = Vec3.op_UnaryNegation((Vec3) ((Mat3) ((MissionBehaviour) this).get_Mission().get_Scene().get_LastFinalRenderCameraFrame().rotation).u);
        agent.set_LookDirection(vec3);
        agent.TeleportToPosition(groundVec3);
        agent.set_Controller((Agent.ControllerType) 2);
        Utility.DisplayMessage(string.Format("Teleport player to {0}", (object) groundVec3));
      }
      catch (Exception ex)
      {
        Utility.DisplayMessage(ex.Message);
      }
    }

    public TeleportPlayerLogic()
    {
      base.\u002Ector();
    }
  }
}
