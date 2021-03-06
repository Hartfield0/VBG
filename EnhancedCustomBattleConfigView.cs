﻿// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.EnhancedCustomBattleConfigView
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Engine.Screens;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View.Missions;

namespace VirtualBattlegrounds
{
  public class EnhancedCustomBattleConfigView : MissionView
  {
    private GauntletLayer _gauntletLayer;
    private EnhancedCustomBattleConfigVM _dataSource;
    private CharacterSelectionView _selectionView;

    public EnhancedCustomBattleConfigView(CharacterSelectionView selectionView)
    {
      this.\u002Ector();
      this._selectionView = selectionView;
      this.ViewOrderPriorty = (__Null) 22;
    }

    public virtual void OnMissionScreenInitialize()
    {
      base.OnMissionScreenInitialize();
      this.Open();
    }

    public virtual void OnMissionScreenFinalize()
    {
      this.Close();
      base.OnMissionScreenFinalize();
    }

    public virtual bool OnEscape()
    {
      base.OnEscape();
      this._dataSource.GoBack();
      return true;
    }

    public void Open()
    {
      this._dataSource = new EnhancedCustomBattleConfigVM(this._selectionView, (MissionMenuView) ((MissionBehaviour) this).get_Mission().GetMissionBehaviour<MissionMenuView>(), (Action<EnhancedCustomBattleConfig>) (config =>
      {
        ((MissionBehaviour) this).get_Mission().EndMission();
        GameStateManager.get_Current().PopStateRPC(0);
        VirtualBattlegroundsMissions.OpenCustomBattleMission(config);
      }), (Action<EnhancedCustomBattleConfig>) (config =>
      {
        TopState.status = TopStateStatus.exit;
        ((MissionBehaviour) this).get_Mission().EndMission();
      }));
      this._gauntletLayer = new GauntletLayer((int) this.ViewOrderPriorty, "GauntletLayer");
      this._gauntletLayer.LoadMovie(nameof (EnhancedCustomBattleConfigView), (ViewModel) this._dataSource);
      ((ScreenLayer) this._gauntletLayer).get_InputRestrictions().SetInputRestrictions(true, (InputUsageMask) 7);
      ((ScreenBase) this.get_MissionScreen()).AddLayer((ScreenLayer) this._gauntletLayer);
    }

    public void Close()
    {
      if (this._gauntletLayer != null)
      {
        ((ScreenLayer) this._gauntletLayer).get_InputRestrictions().ResetInputRestrictions();
        ((ScreenBase) this.get_MissionScreen()).RemoveLayer((ScreenLayer) this._gauntletLayer);
        this._gauntletLayer = (GauntletLayer) null;
      }
      if (this._dataSource == null)
        return;
      this._dataSource.OnFinalize();
      this._dataSource = (EnhancedCustomBattleConfigVM) null;
    }
  }
}
