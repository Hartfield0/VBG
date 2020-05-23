// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.EnhancedSiegeBattleConfigVM
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace VirtualBattlegrounds
{
  internal class EnhancedSiegeBattleConfigVM : BattleConfigVMBase<EnhancedSiegeBattleConfig>
  {
    private Action<EnhancedSiegeBattleConfig> startAction;
    private Action<EnhancedSiegeBattleConfig> backAction;
    private int _selectedSceneIndex;
    private string _distance;
    private string _soldierXInterval;
    private string _soldierYInterval;
    private string _soldiersPerRow;
    private string _formationPosition;
    private string _formationDirection;
    private string _skyBrightness;
    private string _rainDensity;
    private string _selectedMapName;

    public string BattleModeString { get; } = "VirtualBattlegrounds e" + BattleConfigBase.ModVersion.ToString(2) + ": " + ((object) GameTexts.FindText("str_siege_battle", (string) null)).ToString();

    [DataSourceProperty]
    public string SelectedMapName
    {
      get
      {
        return this._selectedMapName;
      }
      set
      {
        this._selectedMapName = value;
        this.OnPropertyChanged(nameof (SelectedMapName));
      }
    }

    public int SelectedSceneIndex
    {
      get
      {
        return this._selectedSceneIndex;
      }
      set
      {
        if (value < 0 || value >= this.CurrentConfig.sceneList.Length || value == this._selectedSceneIndex)
          return;
        this.CurrentConfig.sceneIndex = value;
        this._selectedSceneIndex = value;
        this.UpdateSceneContent();
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
    public bool HasBoundary
    {
      get
      {
        return this.CurrentConfig.hasBoundary;
      }
      set
      {
        if (this.CurrentConfig.hasBoundary == value)
          return;
        this.CurrentConfig.hasBoundary = value;
        this.OnPropertyChanged(nameof (HasBoundary));
      }
    }

    public EnhancedSiegeBattleConfigVM(
      CharacterSelectionView selectionView,
      MissionMenuView missionMenuView,
      Action<EnhancedSiegeBattleConfig> startAction,
      Action<EnhancedSiegeBattleConfig> backAction)
      : base(selectionView, missionMenuView, EnhancedSiegeBattleConfig.Get())
    {
      this.InitializeContent();
      this.startAction = startAction;
      this.backAction = backAction;
    }

    private void PreviousMap()
    {
      if (this.SelectedSceneIndex == 0)
        return;
      --this.SelectedSceneIndex;
    }

    private void NextMap()
    {
      if (this.SelectedSceneIndex + 1 >= this.CurrentConfig.sceneList.Length)
        return;
      ++this.SelectedSceneIndex;
    }

    private new void SelectPlayerCharacter()
    {
      base.SelectPlayerCharacter();
    }

    private new void SelectEnemyCharacter()
    {
      base.SelectEnemyCharacter();
    }

    private new void SelectPlayerTroopCharacter1()
    {
      base.SelectPlayerTroopCharacter1();
    }

    private new void SelectEnemyTroopCharacter1()
    {
      base.SelectEnemyTroopCharacter1();
    }

    protected new void OpenMissionMenu()
    {
      base.OpenMissionMenu();
    }

    private void Start()
    {
      if ((uint) this.SaveConfig() > 0U)
        return;
      this.startAction(this.CurrentConfig);
    }

    private void Save()
    {
      int num = (int) this.SaveConfig();
    }

    private void LoadConfig()
    {
      this.CurrentConfig.ReloadSavedConfig();
      this.InitializeContent();
    }

    public void GoBack()
    {
      this.backAction(this.CurrentConfig);
    }

    private void InitializeContent()
    {
      this._selectedSceneIndex = this.CurrentConfig.sceneIndex;
      this.UpdateSceneContent();
      this.SoldierXInterval = this.CurrentConfig.soldierXInterval.ToString();
      this.SoldierYInterval = this.CurrentConfig.soldierYInterval.ToString();
    }

    private void UpdateSceneContent()
    {
      this.SelectedMapName = this.CurrentConfig.SceneName;
      this.SoldiersPerRow = this.CurrentConfig.SoldiersPerRow.ToString();
      this.FormationPosition = this.Vec2ToString(this.CurrentConfig.FormationPosition);
      this.FormationDirection = this.Vec2ToString(this.CurrentConfig.FormationDirection);
      this.Distance = this.CurrentConfig.Distance.ToString();
      this.SkyBrightness = this.CurrentConfig.SkyBrightness.ToString();
      this.RainDensity = this.CurrentConfig.RainDensity.ToString();
    }

    protected override void ApplyConfig()
    {
      base.ApplyConfig();
      this.CurrentConfig.sceneIndex = this.SelectedSceneIndex;
      this.CurrentConfig.SoldiersPerRow = Convert.ToInt32(this.SoldiersPerRow);
      this.CurrentConfig.FormationPosition = this.StringToVec2(this.FormationPosition);
      EnhancedSiegeBattleConfig currentConfig = this.CurrentConfig;
      Vec2 vec2_1 = this.StringToVec2(this.FormationDirection);
      Vec2 vec2_2 = ((Vec2) ref vec2_1).Normalized();
      currentConfig.FormationDirection = vec2_2;
      this.CurrentConfig.SkyBrightness = Convert.ToSingle(this.SkyBrightness);
      this.CurrentConfig.RainDensity = Convert.ToSingle(this.RainDensity);
      this.CurrentConfig.Distance = Convert.ToSingle(this.Distance);
      this.CurrentConfig.soldierXInterval = Convert.ToSingle(this.SoldierXInterval);
      this.CurrentConfig.soldierYInterval = Convert.ToSingle(this.SoldierYInterval);
    }

    private string Vec2ToString(Vec2 vec2)
    {
      return string.Format("{0},{1}", (object) (float) vec2.x, (object) (float) vec2.y);
    }

    private Vec2 StringToVec2(string str)
    {
      string[] strArray = str.Split(',');
      return new Vec2(Convert.ToSingle(strArray[0]), Convert.ToSingle(strArray[1]));
    }
  }
}
