// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.MissionMenuVM
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System;
using System.Collections.ObjectModel;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace VirtualBattlegrounds
{
  public class MissionMenuVM : ViewModel
  {
    private BattleConfigBase _config;
    private Mission _mission;
    private SwitchTeamLogic _switchTeamLogic;
    private SwitchFreeCameraLogic _switchFreeCameraLogic;
    private MissionSpeedLogic _missionSpeedLogic;
    private ResetMissionLogic _resetMissionLogic;
    private string _distance;
    private string _soldierXInterval;
    private string _soldierYInterval;
    private string _soldiersPerRow;
    private string _formationPosition;
    private string _formationDirection;
    private string _skyBrightness;
    private string _rainDensity;
    private string _currentAIEnableType;
    private MBBindingList<TacticOptionVM> _attackerTacticOptions;
    private MBBindingList<TacticOptionVM> _defenderTacticOptions;
    private Action _closeMenu;
    public Func<BattleSideEnum, TacticOptionEnum, bool, bool> updateSelectedTactic;

    public string EnableAIForString { get; }

    public string AttackerTacticOptionString { get; }

    public string DefenderTacticOptionString { get; }

    public string SwitchTeamString { get; }

    public string SwitchFreeCameraString { get; }

    public string DisableDeathString { get; }

    public string ResetMissionString { get; }

    public string TogglePauseString { get; }

    public string ResetSpeedString { get; }

    public void PreviousAIEnableType()
    {
      this._config.ToPreviousAIEnableType();
      Utility.ApplyTeamAIEnabled(this._config);
      this.UpdateCurrentAIEnableType();
    }

    [DataSourceProperty]
    public string CurrentAIEnableType
    {
      get
      {
        return this._currentAIEnableType;
      }
      set
      {
        if (value == this._currentAIEnableType)
          return;
        this._currentAIEnableType = value;
        this.OnPropertyChanged(nameof (CurrentAIEnableType));
      }
    }

    public void NextAIEnableType()
    {
      this._config.ToNextAIEnableType();
      Utility.ApplyTeamAIEnabled(this._config);
      this.UpdateCurrentAIEnableType();
    }

    [DataSourceProperty]
    public MBBindingList<TacticOptionVM> AttackerAvailableTactics
    {
      get
      {
        return this._attackerTacticOptions;
      }
      set
      {
        if (value == this._attackerTacticOptions)
          return;
        this._attackerTacticOptions = value;
        this.OnPropertyChanged(nameof (AttackerAvailableTactics));
      }
    }

    [DataSourceProperty]
    public MBBindingList<TacticOptionVM> DefenderAvailableTactics
    {
      get
      {
        return this._defenderTacticOptions;
      }
      set
      {
        if (value == this._defenderTacticOptions)
          return;
        this._defenderTacticOptions = value;
        this.OnPropertyChanged(nameof (DefenderAvailableTactics));
      }
    }

    public void SwitchTeam()
    {
      this._switchTeamLogic?.SwapTeam();
      this.CloseMenu();
    }

    [DataSourceProperty]
    public bool SwitchTeamEnabled
    {
      get
      {
        return this._switchTeamLogic != null;
      }
    }

    public void SwitchFreeCamera()
    {
      this._switchFreeCameraLogic?.SwitchCamera();
      this.CloseMenu();
    }

    [DataSourceProperty]
    public bool SwitchFreeCameraEnabled
    {
      get
      {
        return this._switchFreeCameraLogic != null;
      }
    }

    [DataSourceProperty]
    public string Distance
    {
      get
      {
        return this._distance;
      }
      set
      {
        if (value == this._distance)
          return;
        this._distance = value;
        this.OnPropertyChanged(nameof (Distance));
      }
    }

    [DataSourceProperty]
    public string SoldierXInterval
    {
      get
      {
        return this._soldierXInterval;
      }
      set
      {
        if (value == this._soldierXInterval)
          return;
        this._soldierXInterval = value;
        this.OnPropertyChanged(nameof (SoldierXInterval));
      }
    }

    [DataSourceProperty]
    public string SoldierYInterval
    {
      get
      {
        return this._soldierYInterval;
      }
      set
      {
        if (value == this._soldierYInterval)
          return;
        this._soldierYInterval = value;
        this.OnPropertyChanged(nameof (SoldierYInterval));
      }
    }

    [DataSourceProperty]
    public string SoldiersPerRow
    {
      get
      {
        return this._soldiersPerRow;
      }
      set
      {
        if (value == this._soldiersPerRow)
          return;
        this._soldiersPerRow = value;
        this.OnPropertyChanged(nameof (SoldiersPerRow));
      }
    }

    [DataSourceProperty]
    public string SkyBrightness
    {
      get
      {
        return this._skyBrightness;
      }
      set
      {
        if (this._skyBrightness == value)
          return;
        this._skyBrightness = value;
        this.OnPropertyChanged(nameof (SkyBrightness));
      }
    }

    [DataSourceProperty]
    public string RainDensity
    {
      get
      {
        return this._rainDensity;
      }
      set
      {
        if (this._rainDensity == value)
          return;
        this._rainDensity = value;
        this.OnPropertyChanged(nameof (RainDensity));
      }
    }

    [DataSourceProperty]
    public string FormationPosition
    {
      get
      {
        return this._formationPosition;
      }
      set
      {
        if (this._formationPosition == value)
          return;
        this._formationPosition = value;
        this.OnPropertyChanged(nameof (FormationPosition));
      }
    }

    [DataSourceProperty]
    public string FormationDirection
    {
      get
      {
        return this._formationDirection;
      }
      set
      {
        if (this._formationDirection == value)
          return;
        this._formationDirection = value;
        this.OnPropertyChanged(nameof (FormationDirection));
      }
    }

    [DataSourceProperty]
    public bool DisableDeath
    {
      get
      {
        return this._config.disableDeath;
      }
      set
      {
        if (this._config.disableDeath == value)
          return;
        this._config.disableDeath = value;
        ((DisableDeathLogic) this._mission.GetMissionBehaviour<DisableDeathLogic>())?.SetDisableDeath(this.DisableDeath);
        this.OnPropertyChanged(nameof (DisableDeath));
      }
    }

    public void ResetMission()
    {
      this._resetMissionLogic?.ResetMission();
      this.CloseMenu();
    }

    [DataSourceProperty]
    public bool ResetMissionEnabled
    {
      get
      {
        return this._resetMissionLogic != null;
      }
    }

    public void TogglePause()
    {
      this._missionSpeedLogic?.TogglePause();
      this.CloseMenu();
    }

    [DataSourceProperty]
    public bool AdjustSpeedEnabled
    {
      get
      {
        return this._missionSpeedLogic != null;
      }
    }

    public void ResetSpeed()
    {
      this.SpeedFactor.OptionValue = 1f;
      this._missionSpeedLogic.ResetSpeed();
    }

    [DataSourceProperty]
    public NumericVM SpeedFactor { get; }

    private void CloseMenu()
    {
      Action closeMenu = this._closeMenu;
      if (closeMenu == null)
        return;
      closeMenu();
    }

    public MissionMenuVM(
      BattleConfigBase config,
      Func<BattleSideEnum, TacticOptionEnum, bool, bool> updateSelectedTactic,
      Action closeMenu)
    {
      this.\u002Ector();
      this._config = config;
      this.UpdateCurrentAIEnableType();
      this.updateSelectedTactic = updateSelectedTactic;
      this._closeMenu = closeMenu;
      this._mission = Mission.get_Current();
      this._switchTeamLogic = (SwitchTeamLogic) this._mission.GetMissionBehaviour<SwitchTeamLogic>();
      this._switchFreeCameraLogic = (SwitchFreeCameraLogic) this._mission.GetMissionBehaviour<SwitchFreeCameraLogic>();
      this._missionSpeedLogic = (MissionSpeedLogic) this._mission.GetMissionBehaviour<MissionSpeedLogic>();
      this._resetMissionLogic = (ResetMissionLogic) this._mission.GetMissionBehaviour<ResetMissionLogic>();
      this.SpeedFactor = new NumericVM(((object) GameTexts.FindText("str_slow_motion_factor", (string) null)).ToString(), this._mission.get_Scene().get_SlowMotionMode() ? this._mission.get_Scene().get_SlowMotionFactor() : 1f, 0.01f, 2f, false, (Action<float>) (factor => this._missionSpeedLogic.SetSlowMotionFactor(factor)));
      this.FillAttackerAvailableTactics();
      this.FillDefenderAvailableTactics();
    }

    private void UpdateCurrentAIEnableType()
    {
      this.CurrentAIEnableType = ((object) GameTexts.FindText("str_ai_enable_type", this._config.aiEnableType.ToString())).ToString();
    }

    private void FillAttackerAvailableTactics()
    {
      MBBindingList<TacticOptionVM> mbBindingList = new MBBindingList<TacticOptionVM>();
      foreach (TacticOptionInfo attackerTacticOption in this._config.attackerTacticOptions)
        ((Collection<TacticOptionVM>) mbBindingList).Add(new TacticOptionVM()
        {
          parent = this,
          side = (BattleSideEnum) 1,
          IsSelected = attackerTacticOption.isEnabled,
          tacticOption = attackerTacticOption.tacticOption,
          Name = ((object) GameTexts.FindText("str_tactic_option", attackerTacticOption.tacticOption.ToString())).ToString()
        });
      this.AttackerAvailableTactics = mbBindingList;
    }

    private void FillDefenderAvailableTactics()
    {
      MBBindingList<TacticOptionVM> mbBindingList = new MBBindingList<TacticOptionVM>();
      foreach (TacticOptionInfo defenderTacticOption in this._config.defenderTacticOptions)
        ((Collection<TacticOptionVM>) mbBindingList).Add(new TacticOptionVM()
        {
          parent = this,
          side = (BattleSideEnum) 0,
          IsSelected = defenderTacticOption.isEnabled,
          tacticOption = defenderTacticOption.tacticOption,
          Name = ((object) GameTexts.FindText("str_tactic_option", defenderTacticOption.tacticOption.ToString())).ToString()
        });
      this.DefenderAvailableTactics = mbBindingList;
    }
  }
}
