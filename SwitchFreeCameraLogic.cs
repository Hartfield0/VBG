// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.SwitchFreeCameraLogic
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using TaleWorlds.InputSystem;
using TaleWorlds.MountAndBlade;

namespace VirtualBattlegrounds
{
  internal class SwitchFreeCameraLogic : MissionLogic
  {
    public virtual void OnMissionTick(float dt)
    {
      ((MissionBehaviour) this).OnMissionTick(dt);
      if (!((MissionBehaviour) this).get_Mission().get_InputManager().IsKeyPressed((InputKey) 68))
        return;
      this.SwitchCamera();
    }

    public void SwitchCamera()
    {
      if (Utility.IsPlayerDead())
        this.SwitchToAgent();
      else
        this.SwitchToFreeCamera();
    }

    private void SwitchToAgent()
    {
      Agent owner = (Agent) ((MissionBehaviour) this).get_Mission().get_PlayerTeam().get_PlayerOrderController().Owner;
      if (Utility.IsAgentDead(owner))
      {
        Utility.DisplayLocalizedText("str_player_dead", (string) null);
        ((ControlTroopAfterPlayerDeadLogic) ((MissionBehaviour) this).get_Mission().GetMissionBehaviour<ControlTroopAfterPlayerDeadLogic>())?.ControlTroopAfterDead();
      }
      else
      {
        owner.set_Controller((Agent.ControllerType) 2);
        Utility.DisplayLocalizedText("str_switch_to_player", (string) null);
      }
    }

    private void SwitchToFreeCamera()
    {
      ((MissionBehaviour) this).get_Mission().get_MainAgent().set_Controller((Agent.ControllerType) 1);
      ((MissionBehaviour) this).get_Mission().set_MainAgent((Agent) null);
      Utility.DisplayLocalizedText("str_switch_to_free_camera", (string) null);
    }

    public SwitchFreeCameraLogic()
    {
      base.\u002Ector();
    }
  }
}
