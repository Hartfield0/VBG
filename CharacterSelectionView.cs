// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.CharacterSelectionView
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System;
using System.Collections.Generic;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Engine.Screens;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View.Missions;

namespace VirtualBattlegrounds
{
  public class CharacterSelectionView : MissionView
  {
    private CharacterSelectionVM _dataSource;
    private GauntletLayer _gauntletLayer;
    private GauntletMovie _movie;
    private CharacterSelectionParams _params;
    private bool _withPerks;
    private bool _isOpen;
    private bool _toOpen;

    public CharacterSelectionView(bool withPerks)
    {
      this.\u002Ector();
      this._params = (CharacterSelectionParams) null;
      this._withPerks = withPerks;
      this.ViewOrderPriorty = (__Null) 23;
      this._isOpen = this._toOpen = false;
    }

    public virtual void OnMissionScreenInitialize()
    {
      base.OnMissionScreenInitialize();
    }

    public virtual void OnMissionScreenFinalize()
    {
      if (this._gauntletLayer != null)
      {
        ((ScreenLayer) this._gauntletLayer).get_InputRestrictions().ResetInputRestrictions();
        ((ScreenBase) this.get_MissionScreen()).RemoveLayer((ScreenLayer) this._gauntletLayer);
        this._gauntletLayer = (GauntletLayer) null;
      }
      if (this._dataSource != null)
      {
        this._dataSource.OnFinalize();
        this._dataSource = (CharacterSelectionVM) null;
      }
      base.OnMissionScreenFinalize();
    }

    public virtual void OnMissionScreenTick(float dt)
    {
      base.OnMissionScreenTick(dt);
      if (this._toOpen)
      {
        this._toOpen = false;
        this.OnOpen();
      }
      if (!this._isOpen || !((ScreenLayer) this._gauntletLayer).get_Input().IsKeyPressed((InputKey) 63))
        return;
      this._movie.get_WidgetFactory().CheckForUpdates();
      this.HandleLoadMovie();
    }

    public virtual bool OnEscape()
    {
      if (!this._isOpen)
        return base.OnEscape();
      this.OnClose();
      return true;
    }

    public void Open(CharacterSelectionParams p)
    {
      this._params = p;
      this._toOpen = true;
    }

    private void OnOpen()
    {
      if (this._isOpen)
        return;
      this._isOpen = true;
      this._dataSource = new CharacterSelectionVM(this._params);
      GauntletLayer gauntletLayer = new GauntletLayer((int) this.ViewOrderPriorty, "GauntletLayer");
      ((ScreenLayer) gauntletLayer).set_IsFocusLayer(true);
      this._gauntletLayer = gauntletLayer;
      ((ScreenBase) this.get_MissionScreen()).AddLayer((ScreenLayer) this._gauntletLayer);
      ((ScreenLayer) this._gauntletLayer).get_InputRestrictions().SetInputRestrictions(true, (InputUsageMask) 7);
      ScreenManager.TrySetFocus((ScreenLayer) this._gauntletLayer);
      this.HandleLoadMovie();
    }

    public void OnClose()
    {
      if (!this._isOpen)
        return;
      this._isOpen = false;
      ((ScreenBase) this.get_MissionScreen()).RemoveLayer((ScreenLayer) this._gauntletLayer);
      this.get_MissionScreen().SetDisplayDialog(false);
      ((ScreenLayer) this._gauntletLayer).get_InputRestrictions().ResetInputRestrictions();
      this._gauntletLayer = (GauntletLayer) null;
      this._dataSource.OnFinalize();
      this._dataSource = (CharacterSelectionVM) null;
    }

    private void HandleLoadMovie()
    {
      CharacterSelectionVM vm = this._dataSource;
      this._movie = this._gauntletLayer.LoadMovie(this._withPerks ? "CharacterSelectionViewWithPerks" : "CharacterSelectionViewWithoutPerks", (ViewModel) this._dataSource);
      ListPanel child = ((WidgetComponent) this._movie.get_RootView()).get_Target().FindChild("Cultures", true) as ListPanel;
      ListPanel groupsListPanel = ((WidgetComponent) this._movie.get_RootView()).get_Target().FindChild("Groups", true) as ListPanel;
      ListPanel charactersListPanel = ((WidgetComponent) this._movie.get_RootView()).get_Target().FindChild("Characters", true) as ListPanel;
      ListPanel firstPerkListPanel = ((WidgetComponent) this._movie.get_RootView()).get_Target().FindChild("FirstPerks", true) as ListPanel;
      ListPanel secondPerkListPanel = ((WidgetComponent) this._movie.get_RootView()).get_Target().FindChild("SecondPerks", true) as ListPanel;
      ((Container) child).set_IntValue(vm.SelectedCultureIndex);
      ((Container) groupsListPanel).set_IntValue(vm.SelectedGroupIndex);
      ((Container) charactersListPanel).set_IntValue(vm.SelectedCharacterIndex);
      if (firstPerkListPanel != null)
        ((Container) firstPerkListPanel).set_IntValue(vm.SelectedFirstPerkIndex);
      if (secondPerkListPanel != null)
        ((Container) secondPerkListPanel).set_IntValue(vm.SelectedSecondPerkIndex);
      ((List<Action<Widget>>) ((Container) child).SelectEventHandlers).Add((Action<Widget>) (w =>
      {
        vm.SelectedCultureChanged(w as ListPanel);
        ((Container) groupsListPanel).set_IntValue(vm.SelectedGroupIndex);
        ((Container) charactersListPanel).set_IntValue(vm.SelectedCharacterIndex);
        ((Container) firstPerkListPanel)?.set_IntValue(vm.SelectedFirstPerkIndex);
        ((Container) secondPerkListPanel)?.set_IntValue(vm.SelectedSecondPerkIndex);
      }));
      ((List<Action<Widget>>) ((Container) groupsListPanel).SelectEventHandlers).Add((Action<Widget>) (w =>
      {
        vm.SelectedGroupChanged(w as ListPanel);
        ((Container) charactersListPanel).set_IntValue(vm.SelectedCharacterIndex);
        ((Container) firstPerkListPanel)?.set_IntValue(vm.SelectedFirstPerkIndex);
        ((Container) secondPerkListPanel)?.set_IntValue(vm.SelectedSecondPerkIndex);
      }));
      ((List<Action<Widget>>) ((Container) charactersListPanel).SelectEventHandlers).Add((Action<Widget>) (w =>
      {
        vm.SelectedCharacterChanged(w as ListPanel);
        ((Container) firstPerkListPanel)?.set_IntValue(vm.SelectedFirstPerkIndex);
        ((Container) secondPerkListPanel)?.set_IntValue(vm.SelectedSecondPerkIndex);
      }));
      ((List<Action<Widget>>) ((Container) firstPerkListPanel)?.SelectEventHandlers).Add((Action<Widget>) (w => vm.SelectedFirstPerkChanged(w as ListPanel)));
      ((List<Action<Widget>>) ((Container) secondPerkListPanel)?.SelectEventHandlers).Add((Action<Widget>) (w => vm.SelectedSecondPerkChanged(w as ListPanel)));
    }
  }
}
