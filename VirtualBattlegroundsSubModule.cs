// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.VirtualBattlegroundsSubModule
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace VirtualBattlegrounds
{
  public class VirtualBattlegroundsSubModule : MBSubModuleBase
  {
    private static VirtualBattlegroundsSubModule _instance;
    private bool _initialized;

    protected virtual void OnSubModuleLoad()
    {
      base.OnSubModuleLoad();
      VirtualBattlegroundsSubModule._instance = this;
      Module.get_CurrentModule().AddInitialStateOption(new InitialStateOption("EBTFreeBattle", new TextObject("{=freebattleoption}vBattlegrounds - Free", (Dictionary<string, TextObject>) null), 3, (Action) (() => MBGameManager.StartNewGame((MBGameManager) new VirtualBattlegroundsGameManager((GameType) new EnhancedFreeBattleGame(new Func<BattleConfigBase>(EnhancedFreeBattleConfig.Get)), (Action) (() =>
      {
        TopState state = (TopState) GameStateManager.get_Current().CreateState<TopState>();
        TopState.status = TopStateStatus.openConfig;
        state.openConfigMission = (Action) (() => VirtualBattlegroundsMissions.OpenFreeBattleConfigMission());
        GameStateManager.get_Current().PushState((GameState) state, 0);
      })))), false));
      Module.get_CurrentModule().AddInitialStateOption(new InitialStateOption("EBTcustomebattle", new TextObject("{=custombattleoption}vBattlegrounds - Custom", (Dictionary<string, TextObject>) null), 3, (Action) (() => MBGameManager.StartNewGame((MBGameManager) new VirtualBattlegroundsGameManager((GameType) new EnhancedCustomBattleGame(new Func<BattleConfigBase>(EnhancedCustomBattleConfig.Get)), (Action) (() =>
      {
        TopState state = (TopState) GameStateManager.get_Current().CreateState<TopState>();
        TopState.status = TopStateStatus.openConfig;
        state.openConfigMission = (Action) (() => VirtualBattlegroundsMissions.OpenCustomBattleConfigMission());
        GameStateManager.get_Current().PushState((GameState) state, 0);
      })))), false));
      Module.get_CurrentModule().AddInitialStateOption(new InitialStateOption("EBTsiegebattle", new TextObject("{=siegebattleoption}vBattlegrounds - Siege", (Dictionary<string, TextObject>) null), 3, (Action) (() => MBGameManager.StartNewGame((MBGameManager) new VirtualBattlegroundsGameManager((GameType) new EnhancedFreeBattleGame(new Func<BattleConfigBase>(EnhancedSiegeBattleConfig.Get)), (Action) (() =>
      {
        TopState state = (TopState) GameStateManager.get_Current().CreateState<TopState>();
        TopState.status = TopStateStatus.openConfig;
        state.openConfigMission = (Action) (() => VirtualBattlegroundsMissions.OpenSiegeBattleConfigMission());
        GameStateManager.get_Current().PushState((GameState) state, 0);
      })))), false));
    }

    protected virtual void OnSubModuleUnloaded()
    {
      VirtualBattlegroundsSubModule._instance = (VirtualBattlegroundsSubModule) null;
      base.OnSubModuleUnloaded();
    }

    protected virtual void OnBeforeInitialModuleScreenSetAsRoot()
    {
      if (this._initialized)
        return;
      this._initialized = true;
    }

    protected virtual void OnApplicationTick(float dt)
    {
      base.OnApplicationTick(dt);
    }

    public VirtualBattlegroundsSubModule()
    {
      base.\u002Ector();
    }
  }
}
