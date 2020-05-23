// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.EnhancedCustomBattleConfigVM
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace VirtualBattlegrounds
{
  public class EnhancedCustomBattleConfigVM : BattleConfigVMBase<EnhancedCustomBattleConfig>
  {
    private Action<EnhancedCustomBattleConfig> startAction;
    private Action<EnhancedCustomBattleConfig> backAction;
    private int _selectedSceneIndex;
    private string _skyBrightness;
    private string _rainDensity;
    private string _selectedMapName;

    public string BattleModeString { get; } = "VirtualBattlegrounds e" + BattleConfigBase.ModVersion.ToString(2) + ": " + ((object) GameTexts.FindText("str_custom_battle", (string) null)).ToString();

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

    public EnhancedCustomBattleConfigVM(
      CharacterSelectionView selectionView,
      MissionMenuView missionMenuView,
      Action<EnhancedCustomBattleConfig> startAction,
      Action<EnhancedCustomBattleConfig> backAction)
      : base(selectionView, missionMenuView, EnhancedCustomBattleConfig.Get())
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

    private new void SelectPlayerTroopCharacter2()
    {
      base.SelectPlayerTroopCharacter2();
    }

    private new void SelectPlayerTroopCharacter3()
    {
      base.SelectPlayerTroopCharacter3();
    }

    private new void SelectPlayerTroopCharacter4()
    {
      base.SelectPlayerTroopCharacter4();
    }

    private new void SelectEnemyTroopCharacter1()
    {
      base.SelectEnemyTroopCharacter1();
    }

    private new void SelectEnemyTroopCharacter2()
    {
      base.SelectEnemyTroopCharacter2();
    }

    private new void SelectEnemyTroopCharacter3()
    {
      base.SelectEnemyTroopCharacter3();
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
    }

    private void UpdateSceneContent()
    {
      this.SelectedMapName = this.CurrentConfig.SceneName;
      this.SkyBrightness = this.CurrentConfig.SkyBrightness.ToString();
      this.RainDensity = this.CurrentConfig.RainDensity.ToString();
    }

    protected override void ApplyConfig()
    {
      base.ApplyConfig();
      this.CurrentConfig.sceneIndex = this.SelectedSceneIndex;
      this.CurrentConfig.SkyBrightness = Convert.ToSingle(this.SkyBrightness);
      this.CurrentConfig.RainDensity = Convert.ToSingle(this.RainDensity);
    }
  }
}
