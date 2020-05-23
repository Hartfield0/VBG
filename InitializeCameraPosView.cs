// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.InitializeCameraPosView
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View.Missions;

namespace VirtualBattlegrounds
{
  public class InitializeCameraPosView : MissionView
  {
    private Vec2 _initialPosition;
    private Vec2 _initialDirection;

    public InitializeCameraPosView(Vec2 initialPosition, Vec2 initalDirection)
    {
      this.\u002Ector();
      this._initialPosition = initialPosition;
      this._initialDirection = initalDirection;
    }

    public virtual void OnMissionScreenActivate()
    {
      Utility.SetInitialCameraPos(this.get_MissionScreen().get_CombatCamera(), this._initialPosition, this._initialDirection);
    }
  }
}
