// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.MissionMenuView
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Engine.Screens;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View.Missions;

namespace VirtualBattlegrounds
{
  public class MissionMenuView : MissionView
  {
    private static bool _oldGameStatusDisabledStatus = false;
    private MissionMenuVM _dataSource;
    private GauntletLayer _gauntletLayer;
    private GauntletMovie _movie;
    private BattleConfigBase _config;

    public bool IsActivated { get; set; }

    public MissionMenuView(BattleConfigBase config)
    {
      this.\u002Ector();
      this._config = config;
      this.ViewOrderPriorty = (__Null) 24;
    }

    public virtual void OnMissionScreenFinalize()
    {
      base.OnMissionScreenFinalize();
      this._gauntletLayer = (GauntletLayer) null;
      this._dataSource?.OnFinalize();
      this._dataSource = (MissionMenuVM) null;
      this._movie = (GauntletMovie) null;
      this._config = (BattleConfigBase) null;
    }

    public void ToggleMenu()
    {
      if (this.IsActivated)
        this.DeactivateMenu();
      else
        this.ActivateMenu();
    }

    public void ActivateMenu()
    {
      this.IsActivated = true;
      this._dataSource = new MissionMenuVM(this._config, (Func<BattleSideEnum, TacticOptionEnum, bool, bool>) ((side, tacticOption, isEnabled) =>
      {
        if (side == -1 || side == 2)
          return false;
        if (this._config.battleType == BattleType.SiegeBattle)
        {
          Utility.DisplayLocalizedText("str_tactic_inchangable_in_siege", (string) null);
          return false;
        }
        TacticOptionInfo[] tacticOptionInfoArray = side == 1 ? this._config.attackerTacticOptions : this._config.defenderTacticOptions;
        if (((IEnumerable<TacticOptionInfo>) tacticOptionInfoArray).Sum<TacticOptionInfo>((Func<TacticOptionInfo, int>) (info => info.tacticOption != tacticOption ? (!info.isEnabled ? 0 : 1) : (!isEnabled ? 0 : 1))) == 0)
        {
          Utility.DisplayLocalizedText("str_at_least_one_tactic", (string) null);
          return false;
        }
        ((IEnumerable<TacticOptionInfo>) tacticOptionInfoArray).First<TacticOptionInfo>((Func<TacticOptionInfo, bool>) (info => info.tacticOption == tacticOption)).isEnabled = isEnabled;
        Team team = side == 1 ? ((MissionBehaviour) this).get_Mission().get_AttackerTeam() : ((MissionBehaviour) this).get_Mission().get_DefenderTeam();
        if (team == null)
          return true;
        if (isEnabled)
          TacticOptionHelper.AddTacticComponent(team, tacticOption, true);
        else
          TacticOptionHelper.RemoveTacticComponent(team, tacticOption, true);
        return true;
      }), new Action(this.DeactivateMenu));
      GauntletLayer gauntletLayer = new GauntletLayer((int) this.ViewOrderPriorty, "GauntletLayer");
      ((ScreenLayer) gauntletLayer).set_IsFocusLayer(true);
      this._gauntletLayer = gauntletLayer;
      ((ScreenLayer) this._gauntletLayer).get_InputRestrictions().SetInputRestrictions(true, (InputUsageMask) 7);
      ((ScreenLayer) this._gauntletLayer).get_Input().RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
      this._movie = this._gauntletLayer.LoadMovie(nameof (MissionMenuView), (ViewModel) this._dataSource);
      ((ScreenBase) this.get_MissionScreen()).AddLayer((ScreenLayer) this._gauntletLayer);
      ScreenManager.TrySetFocus((ScreenLayer) this._gauntletLayer);
      MissionMenuView.PauseGame();
    }

    public void DeactivateMenu()
    {
      this.IsActivated = false;
      this._dataSource = (MissionMenuVM) null;
      ((ScreenLayer) this._gauntletLayer).get_InputRestrictions().ResetInputRestrictions();
      ((ScreenBase) this.get_MissionScreen()).RemoveLayer((ScreenLayer) this._gauntletLayer);
      this._movie = (GauntletMovie) null;
      this._gauntletLayer = (GauntletLayer) null;
      MissionMenuView.UnpauseGame();
    }

    public virtual void OnMissionScreenTick(float dt)
    {
      base.OnMissionScreenTick(dt);
      if (this.IsActivated)
      {
        if (!((ScreenLayer) this._gauntletLayer).get_Input().IsKeyReleased((InputKey) 225) && !((ScreenLayer) this._gauntletLayer).get_Input().IsKeyReleased((InputKey) 24) && !((ScreenLayer) this._gauntletLayer).get_Input().IsHotKeyReleased("Exit"))
          return;
        this.DeactivateMenu();
      }
      else
      {
        if (!this.get_Input().IsKeyReleased((InputKey) 24))
          return;
        this.ActivateMenu();
      }
    }

    private static void PauseGame()
    {
      MBCommon.PauseGameEngine();
      MissionMenuView._oldGameStatusDisabledStatus = Game.get_Current().get_GameStateManager().get_ActiveStateDisabledByUser();
      Game.get_Current().get_GameStateManager().set_ActiveStateDisabledByUser(true);
    }

    private static void UnpauseGame()
    {
      MBCommon.UnPauseGameEngine();
      Game.get_Current().get_GameStateManager().set_ActiveStateDisabledByUser(MissionMenuView._oldGameStatusDisabledStatus);
    }
  }
}
