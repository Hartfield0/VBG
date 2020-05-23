// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.TopState
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System;
using TaleWorlds.Core;

namespace VirtualBattlegrounds
{
  public class TopState : GameState
  {
    public static TopStateStatus status = TopStateStatus.openConfig;
    public Action openConfigMission;

    public virtual bool IsMenuState
    {
      get
      {
        return true;
      }
    }

    protected virtual void OnActivate()
    {
      base.OnActivate();
      switch (TopState.status)
      {
        case TopStateStatus.exit:
          this.get_GameStateManager().PopState(0);
          TopState.status = TopStateStatus.openConfig;
          break;
        case TopStateStatus.openConfig:
          Action openConfigMission = this.openConfigMission;
          if (openConfigMission != null)
            openConfigMission();
          TopState.status = TopStateStatus.none;
          break;
        case TopStateStatus.none:
          TopState.status = TopStateStatus.openConfig;
          break;
      }
    }

    public TopState()
    {
      base.\u002Ector();
    }
  }
}
