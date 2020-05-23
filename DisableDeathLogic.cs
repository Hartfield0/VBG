// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.DisableDeathLogic
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using TaleWorlds.InputSystem;
using TaleWorlds.MountAndBlade;

namespace VirtualBattlegrounds
{
  public class DisableDeathLogic : MissionLogic
  {
    private BattleConfigBase _config;

    public DisableDeathLogic(BattleConfigBase config)
    {
      this.\u002Ector();
      this._config = config;
    }

    public virtual void AfterStart()
    {
      ((MissionBehaviour) this).AfterStart();
      Mission.DisableDying = (__Null) (this._config.disableDeath ? 1 : 0);
      this.PrintDeathStatus();
    }

    public virtual void OnMissionTick(float dt)
    {
      ((MissionBehaviour) this).OnMissionTick(dt);
      if (!((MissionBehaviour) this).get_Mission().get_InputManager().IsKeyPressed((InputKey) 87))
        return;
      this._config.disableDeath = !this._config.disableDeath;
      this.SetDisableDeath(this._config.disableDeath);
    }

    public void SetDisableDeath(bool disableDeath)
    {
      Mission.DisableDying = (__Null) (disableDeath ? 1 : 0);
      this.PrintDeathStatus();
    }

    private void PrintDeathStatus()
    {
      Utility.DisplayLocalizedText(Mission.DisableDying != null ? "str_death_disabled" : "str_death_enabled", (string) null);
    }
  }
}
