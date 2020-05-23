// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.SwitchTeamLogic
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using TaleWorlds.InputSystem;
using TaleWorlds.MountAndBlade;

namespace VirtualBattlegrounds
{
  internal class SwitchTeamLogic : MissionLogic
  {
    public event SwitchTeamLogic.SwitchTeamDelegate PreSwitchTeam;

    public event SwitchTeamLogic.SwitchTeamDelegate PostSwitchTeam;

    public virtual void OnMissionTick(float dt)
    {
      ((MissionBehaviour) this).OnMissionTick(dt);
      if (!((MissionBehaviour) this).get_Mission().get_InputManager().IsKeyPressed((InputKey) 67))
        return;
      this.SwapTeam();
    }

    public void SwapTeam()
    {
      Agent agent = !Utility.IsAgentDead((Agent) ((MissionBehaviour) this).get_Mission().get_PlayerEnemyTeam().get_PlayerOrderController().Owner) ? (Agent) ((MissionBehaviour) this).get_Mission().get_PlayerEnemyTeam().get_PlayerOrderController().Owner : ((MissionBehaviour) this).get_Mission().get_PlayerEnemyTeam().get_Leader();
      if (agent == null)
      {
        Utility.DisplayLocalizedText("str_enemy_wiped_out", (string) null);
      }
      else
      {
        if (!Utility.IsPlayerDead())
        {
          ((MissionBehaviour) this).get_Mission().get_MainAgent().set_Controller((Agent.ControllerType) 1);
          AgentComponentExtensions.SetWatchState(((MissionBehaviour) this).get_Mission().get_MainAgent(), (AgentAIStateFlagComponent.WatchState) 2);
        }
        Utility.DisplayLocalizedText("str_switch_to_enemy_team", (string) null);
        SwitchTeamLogic.SwitchTeamDelegate preSwitchTeam = this.PreSwitchTeam;
        if (preSwitchTeam != null)
          preSwitchTeam();
        ((MissionBehaviour) this).get_Mission().set_PlayerTeam(((MissionBehaviour) this).get_Mission().get_PlayerEnemyTeam());
        agent.set_Controller((Agent.ControllerType) 2);
        SwitchTeamLogic.SwitchTeamDelegate postSwitchTeam = this.PostSwitchTeam;
        if (postSwitchTeam == null)
          return;
        postSwitchTeam();
      }
    }

    public SwitchTeamLogic()
    {
      base.\u002Ector();
    }

    public delegate void SwitchTeamDelegate();
  }
}
