// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.MissionSpeedLogic
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using TaleWorlds.InputSystem;
using TaleWorlds.MountAndBlade;

namespace VirtualBattlegrounds
{
  internal class MissionSpeedLogic : MissionLogic
  {
    public virtual void OnMissionTick(float dt)
    {
      ((MissionBehaviour) this).OnMissionTick(dt);
      if (!Input.IsKeyPressed((InputKey) 25))
        return;
      this.TogglePause();
    }

    public void TogglePause()
    {
      MissionState.get_Current().set_Paused(!MissionState.get_Current().get_Paused());
      Utility.DisplayLocalizedText(MissionState.get_Current().get_Paused() ? "str_mission_paused" : "str_mission_continued.", (string) null);
    }

    public void ResetSpeed()
    {
      ((MissionBehaviour) this).get_Mission().get_Scene().set_SlowMotionFactor(0.2f);
      this.SetSpeedMode(MissionSpeed.Normal);
    }

    public void SetSpeedMode(MissionSpeed speed)
    {
      switch (speed)
      {
        case MissionSpeed.Slow:
          this.SetSlowMotionMode();
          break;
        case MissionSpeed.Normal:
          this.SetNormalMode();
          break;
        case MissionSpeed.Fast:
          this.SetFastForwardMode();
          break;
      }
    }

    public void SetSlowMotionFactor(float factor)
    {
      if (!((MissionBehaviour) this).get_Mission().get_Scene().get_SlowMotionMode())
        this.SetSlowMotionMode();
      ((MissionBehaviour) this).get_Mission().get_Scene().set_SlowMotionFactor(factor);
    }

    public MissionSpeed CurrentSpeed
    {
      get
      {
        if (((MissionBehaviour) this).get_Mission().get_IsFastForward())
          return MissionSpeed.Fast;
        return ((MissionBehaviour) this).get_Mission().get_Scene().get_SlowMotionMode() ? MissionSpeed.Slow : MissionSpeed.Normal;
      }
    }

    public void SetSlowMotionMode()
    {
      this.SetFastForwardModeImpl(false);
      this.SetSlowMotionModeImpl(true);
      Utility.DisplayLocalizedText("str_slow_motion_enabled", (string) null);
    }

    public void SetFastForwardMode()
    {
      this.SetSlowMotionModeImpl(false);
      this.SetFastForwardModeImpl(true);
      Utility.DisplayLocalizedText("str_fast_forward_mode_enabled", (string) null);
    }

    public void SetNormalMode()
    {
      this.SetSlowMotionModeImpl(false);
      this.SetFastForwardModeImpl(false);
      Utility.DisplayLocalizedText("str_normal_mode_enabled", (string) null);
    }

    private void SetSlowMotionModeImpl(bool enabled)
    {
      ((MissionBehaviour) this).get_Mission().get_Scene().set_SlowMotionMode(enabled);
    }

    private void SetFastForwardModeImpl(bool enabled)
    {
      ((MissionBehaviour) this).get_Mission().SetFastForwardingFromUI(enabled);
    }

    public MissionSpeedLogic()
    {
      base.\u002Ector();
    }
  }
}
