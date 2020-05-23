// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.BattleConfigVMBase`1
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ObjectSystem;

namespace VirtualBattlegrounds
{
  public abstract class BattleConfigVMBase<T> : ViewModel where T : BattleConfigBase<T>
  {
    private CharacterSelectionView _selectionView;
    private MissionMenuView _missionMenuView;
    private List<MultiplayerClassDivisions.MPHeroClass> _allMpHeroClasses;
    private Dictionary<string, Dictionary<string, List<MultiplayerClassDivisions.MPHeroClass>>> _allMpHeroClassesMap;
    private string _playerName;
    private string _enemyName;
    private BattleConfigVMBase<T>.TroopInfo[] _playerTroopInfos;
    private BattleConfigVMBase<T>.TroopInfo[] _enemyTroopInfos;
    private string _combatAI;

    protected T CurrentConfig { get; set; }

    public MultiplayerClassDivisions.MPHeroClass PlayerHeroClass
    {
      get
      {
        return this.CurrentConfig.PlayerHeroClass;
      }
      set
      {
        this.CurrentConfig.PlayerHeroClass = value;
        this.PlayerName = ((object) value?.get_HeroName()).ToString();
      }
    }

    public string ControlTroopTipString { get; }

    public string SwitchTeamTipString { get; }

    public string SwitchFreeCameraTipString { get; }

    public string DisableDeathTipString { get; }

    public string ResetMissionTipString { get; }

    public string MoreOptionTipString { get; }

    public string PauseTipString { get; }

    public string LeaveMissionTip { get; }

    public string ReadPositionTip { get; }

    public string TeleportTip { get; }

    public string PlayerCharacterString { get; }

    public string SpawnPlayerString { get; }

    public string IsPlayerAttackerString { get; }

    public string PlayerTroop1String { get; }

    public string PlayerTroop2String { get; }

    public string PlayerTroop3String { get; }

    public string PlayerTroop1CountString { get; }

    public string PlayerTroop2CountString { get; }

    public string PlayerTroop3CountString { get; }

    public string EnemyTroop1String { get; }

    public string EnemyTroop2String { get; }

    public string EnemyTroop3String { get; }

    public string EnemyTroop1CountString { get; }

    public string EnemyTroop2CountString { get; }

    public string EnemyTroop3CountString { get; }

    public string EnemyCommanderString { get; }

    public string SpawnEnemyCommanderString { get; }

    public string SoldiersPerRowString { get; }

    public string FormationPositionString { get; }

    public string FormationDirectionString { get; }

    public string DistanceString { get; }

    public string SkyBrightnessString { get; }

    public string RainDensityString { get; }

    public string MoreOptionsString { get; }

    public string NoFriendlyBannerString { get; }

    public string NoKillNotificationString { get; }

    public string MakeGruntVoiceString { get; }

    public string HasBoundaryString { get; }

    public string ChangeCombatAIString { get; }

    public string CombatAIString { get; }

    public string SaveAndStartString { get; }

    public string SaveString { get; }

    public string LoadConfigString { get; }

    public string ExitString { get; }

    [DataSourceProperty]
    public bool SpawnPlayer
    {
      get
      {
        return this.CurrentConfig.SpawnPlayer;
      }
      set
      {
        this.CurrentConfig.SpawnPlayer = value;
        this.OnPropertyChanged(nameof (SpawnPlayer));
      }
    }

    [DataSourceProperty]
    public bool IsPlayerAttacker
    {
      get
      {
        return this.CurrentConfig.isPlayerAttacker;
      }
      set
      {
        if (this.CurrentConfig.isPlayerAttacker == value)
          return;
        this.CurrentConfig.isPlayerAttacker = value;
        this.OnPropertyChanged(nameof (IsPlayerAttacker));
      }
    }

    [DataSourceProperty]
    public string PlayerTroopCount1
    {
      get
      {
        return this._playerTroopInfos[0].count;
      }
      set
      {
        if (value == this._playerTroopInfos[0].count)
          return;
        this._playerTroopInfos[0].count = value;
        this.OnPropertyChanged(nameof (PlayerTroopCount1));
      }
    }

    [DataSourceProperty]
    public string PlayerTroopCount2
    {
      get
      {
        return this._playerTroopInfos[1].count;
      }
      set
      {
        if (value == this._playerTroopInfos[1].count)
          return;
        this._playerTroopInfos[1].count = value;
        this.OnPropertyChanged(nameof (PlayerTroopCount2));
      }
    }

    [DataSourceProperty]
    public string PlayerTroopCount3
    {
      get
      {
        return this._playerTroopInfos[2].count;
      }
      set
      {
        if (value == this._playerTroopInfos[2].count)
          return;
        this._playerTroopInfos[2].count = value;
        this.OnPropertyChanged(nameof (PlayerTroopCount3));
      }
    }

    [DataSourceProperty]
    public string PlayerTroopCount4
    {
      get
      {
        return this._playerTroopInfos[3].count;
      }
      set
      {
        if (value == this._playerTroopInfos[3].count)
          return;
        this._playerTroopInfos[3].count = value;
        this.OnPropertyChanged(nameof (PlayerTroopCount4));
      }
    }

    [DataSourceProperty]
    public string PlayerTroopCount5
    {
      get
      {
        return this._playerTroopInfos[4].count;
      }
      set
      {
        if (value == this._playerTroopInfos[4].count)
          return;
        this._playerTroopInfos[4].count = value;
        this.OnPropertyChanged(nameof (PlayerTroopCount5));
      }
    }

    [DataSourceProperty]
    public string PlayerTroopCount6
    {
      get
      {
        return this._playerTroopInfos[5].count;
      }
      set
      {
        if (value == this._playerTroopInfos[5].count)
          return;
        this._playerTroopInfos[5].count = value;
        this.OnPropertyChanged(nameof (PlayerTroopCount6));
      }
    }

    [DataSourceProperty]
    public string PlayerTroopCount7
    {
      get
      {
        return this._playerTroopInfos[6].count;
      }
      set
      {
        if (value == this._playerTroopInfos[6].count)
          return;
        this._playerTroopInfos[6].count = value;
        this.OnPropertyChanged(nameof (PlayerTroopCount7));
      }
    }

    [DataSourceProperty]
    public string PlayerTroopCount8
    {
      get
      {
        return this._playerTroopInfos[7].count;
      }
      set
      {
        if (value == this._playerTroopInfos[7].count)
          return;
        this._playerTroopInfos[7].count = value;
        this.OnPropertyChanged(nameof (PlayerTroopCount8));
      }
    }

    private void UpdatePlayerSoldierCount()
    {
      try
      {
        this.PlayerTroopCount1 = this.CurrentConfig.playerTroops[0].troopCount.ToString();
        this.PlayerTroopCount2 = this.CurrentConfig.playerTroops[1].troopCount.ToString();
        this.PlayerTroopCount3 = this.CurrentConfig.playerTroops[2].troopCount.ToString();
        this.PlayerTroopCount4 = this.CurrentConfig.playerTroops[3].troopCount.ToString();
        this.PlayerTroopCount5 = this.CurrentConfig.playerTroops[4].troopCount.ToString();
        this.PlayerTroopCount6 = this.CurrentConfig.playerTroops[5].troopCount.ToString();
        this.PlayerTroopCount7 = this.CurrentConfig.playerTroops[6].troopCount.ToString();
        this.PlayerTroopCount8 = this.CurrentConfig.playerTroops[7].troopCount.ToString();
      }
      catch (Exception ex)
      {
        Console.WriteLine((object) ex);
        this.PlayerTroopCount1 = "10";
        this.PlayerTroopCount2 = "10";
        this.PlayerTroopCount3 = "10";
        this.PlayerTroopCount4 = "10";
        this.PlayerTroopCount5 = "10";
        this.PlayerTroopCount6 = "10";
        this.PlayerTroopCount7 = "10";
        this.PlayerTroopCount8 = "10";
      }
    }

    [DataSourceProperty]
    public string EnemyTroopCount1
    {
      get
      {
        return this._enemyTroopInfos[0].count;
      }
      set
      {
        if (value == this._enemyTroopInfos[0].count)
          return;
        this._enemyTroopInfos[0].count = value;
        this.OnPropertyChanged(nameof (EnemyTroopCount1));
      }
    }

    [DataSourceProperty]
    public string EnemyTroopCount2
    {
      get
      {
        return this._enemyTroopInfos[1].count;
      }
      set
      {
        if (value == this._enemyTroopInfos[1].count)
          return;
        this._enemyTroopInfos[1].count = value;
        this.OnPropertyChanged(nameof (EnemyTroopCount2));
      }
    }

    [DataSourceProperty]
    public string EnemyTroopCount3
    {
      get
      {
        return this._enemyTroopInfos[2].count;
      }
      set
      {
        if (value == this._enemyTroopInfos[2].count)
          return;
        this._enemyTroopInfos[2].count = value;
        this.OnPropertyChanged(nameof (EnemyTroopCount3));
      }
    }

    [DataSourceProperty]
    public string EnemyTroopCount4
    {
      get
      {
        return this._enemyTroopInfos[3].count;
      }
      set
      {
        if (value == this._enemyTroopInfos[3].count)
          return;
        this._enemyTroopInfos[3].count = value;
        this.OnPropertyChanged(nameof (EnemyTroopCount4));
      }
    }

    [DataSourceProperty]
    public string EnemyTroopCount5
    {
      get
      {
        return this._enemyTroopInfos[4].count;
      }
      set
      {
        if (value == this._enemyTroopInfos[4].count)
          return;
        this._enemyTroopInfos[4].count = value;
        this.OnPropertyChanged(nameof (EnemyTroopCount5));
      }
    }

    [DataSourceProperty]
    public string EnemyTroopCount6
    {
      get
      {
        return this._enemyTroopInfos[5].count;
      }
      set
      {
        if (value == this._enemyTroopInfos[5].count)
          return;
        this._enemyTroopInfos[5].count = value;
        this.OnPropertyChanged(nameof (EnemyTroopCount6));
      }
    }

    [DataSourceProperty]
    public string EnemyTroopCount7
    {
      get
      {
        return this._enemyTroopInfos[6].count;
      }
      set
      {
        if (value == this._enemyTroopInfos[6].count)
          return;
        this._enemyTroopInfos[6].count = value;
        this.OnPropertyChanged(nameof (EnemyTroopCount7));
      }
    }

    [DataSourceProperty]
    public string EnemyTroopCount8
    {
      get
      {
        return this._enemyTroopInfos[7].count;
      }
      set
      {
        if (value == this._enemyTroopInfos[7].count)
          return;
        this._enemyTroopInfos[7].count = value;
        this.OnPropertyChanged(nameof (EnemyTroopCount8));
      }
    }

    private void UpdateEnemySoldierCount()
    {
      this.EnemyTroopCount1 = this.CurrentConfig.enemyTroops[0].troopCount.ToString();
      this.EnemyTroopCount2 = this.CurrentConfig.enemyTroops[1].troopCount.ToString();
      this.EnemyTroopCount3 = this.CurrentConfig.enemyTroops[2].troopCount.ToString();
      this.EnemyTroopCount4 = this.CurrentConfig.enemyTroops[3].troopCount.ToString();
      this.EnemyTroopCount5 = this.CurrentConfig.enemyTroops[4].troopCount.ToString();
      this.EnemyTroopCount6 = this.CurrentConfig.enemyTroops[5].troopCount.ToString();
      this.EnemyTroopCount7 = this.CurrentConfig.enemyTroops[6].troopCount.ToString();
      this.EnemyTroopCount8 = this.CurrentConfig.enemyTroops[7].troopCount.ToString();
    }

    [DataSourceProperty]
    public string PlayerName
    {
      get
      {
        return this._playerName;
      }
      set
      {
        if (this._playerName == value)
          return;
        this._playerName = value;
        this.OnPropertyChanged(nameof (PlayerName));
      }
    }

    [DataSourceProperty]
    public string EnemyName
    {
      get
      {
        return this._enemyName;
      }
      set
      {
        if (this._enemyName == value)
          return;
        this._enemyName = value;
        this.OnPropertyChanged(nameof (EnemyName));
      }
    }

    public MultiplayerClassDivisions.MPHeroClass EnemyHeroClass
    {
      get
      {
        return this.CurrentConfig.EnemyHeroClass;
      }
      set
      {
        this.CurrentConfig.EnemyHeroClass = value;
        this.EnemyName = ((object) value?.get_HeroName()).ToString();
      }
    }

    public bool SpawnEnemyCommander
    {
      get
      {
        return this.CurrentConfig.SpawnEnemyCommander;
      }
      set
      {
        this.CurrentConfig.SpawnEnemyCommander = value;
        this.OnPropertyChanged(nameof (SpawnEnemyCommander));
      }
    }

    [DataSourceProperty]
    public string PlayerTroopName1
    {
      get
      {
        return this._playerTroopInfos[0].name;
      }
      set
      {
        if (this._playerTroopInfos[0].name == value)
          return;
        this._playerTroopInfos[0].name = value;
        this.OnPropertyChanged(nameof (PlayerTroopName1));
      }
    }

    [DataSourceProperty]
    public string PlayerTroopName2
    {
      get
      {
        return this._playerTroopInfos[1].name;
      }
      set
      {
        if (this._playerTroopInfos[1].name == value)
          return;
        this._playerTroopInfos[1].name = value;
        this.OnPropertyChanged(nameof (PlayerTroopName2));
      }
    }

    [DataSourceProperty]
    public string PlayerTroopName3
    {
      get
      {
        return this._playerTroopInfos[2].name;
      }
      set
      {
        if (this._playerTroopInfos[2].name == value)
          return;
        this._playerTroopInfos[2].name = value;
        this.OnPropertyChanged(nameof (PlayerTroopName3));
      }
    }

    [DataSourceProperty]
    public string PlayerTroopName4
    {
      get
      {
        return this._playerTroopInfos[3].name;
      }
      set
      {
        if (this._playerTroopInfos[3].name == value)
          return;
        this._playerTroopInfos[3].name = value;
        this.OnPropertyChanged(nameof (PlayerTroopName4));
      }
    }

    [DataSourceProperty]
    public string PlayerTroopName5
    {
      get
      {
        return this._playerTroopInfos[4].name;
      }
      set
      {
        if (this._playerTroopInfos[4].name == value)
          return;
        this._playerTroopInfos[4].name = value;
        this.OnPropertyChanged(nameof (PlayerTroopName5));
      }
    }

    [DataSourceProperty]
    public string PlayerTroopName6
    {
      get
      {
        return this._playerTroopInfos[5].name;
      }
      set
      {
        if (this._playerTroopInfos[5].name == value)
          return;
        this._playerTroopInfos[5].name = value;
        this.OnPropertyChanged(nameof (PlayerTroopName6));
      }
    }

    [DataSourceProperty]
    public string PlayerTroopName7
    {
      get
      {
        return this._playerTroopInfos[6].name;
      }
      set
      {
        if (this._playerTroopInfos[6].name == value)
          return;
        this._playerTroopInfos[6].name = value;
        this.OnPropertyChanged(nameof (PlayerTroopName7));
      }
    }

    [DataSourceProperty]
    public string PlayerTroopName8
    {
      get
      {
        return this._playerTroopInfos[7].name;
      }
      set
      {
        if (this._playerTroopInfos[7].name == value)
          return;
        this._playerTroopInfos[7].name = value;
        this.OnPropertyChanged(nameof (PlayerTroopName8));
      }
    }

    private void UpdatePlayerTroopName()
    {
      this.PlayerTroopName1 = ((object) this.PlayerTroopHeroClass1.get_TroopName()).ToString();
      this.PlayerTroopName2 = ((object) this.PlayerTroopHeroClass2.get_TroopName()).ToString();
      this.PlayerTroopName3 = ((object) this.PlayerTroopHeroClass3.get_TroopName()).ToString();
      this.PlayerTroopName4 = ((object) this.PlayerTroopHeroClass4.get_TroopName()).ToString();
      this.PlayerTroopName5 = ((object) this.PlayerTroopHeroClass5.get_TroopName()).ToString();
      this.PlayerTroopName6 = ((object) this.PlayerTroopHeroClass6.get_TroopName()).ToString();
      this.PlayerTroopName7 = ((object) this.PlayerTroopHeroClass7.get_TroopName()).ToString();
      this.PlayerTroopName8 = ((object) this.PlayerTroopHeroClass8.get_TroopName()).ToString();
    }

    public MultiplayerClassDivisions.MPHeroClass PlayerTroopHeroClass1
    {
      get
      {
        return this.CurrentConfig.GetPlayerTroopHeroClass(0);
      }
      set
      {
        this.CurrentConfig.SetPlayerTroopHeroClass(0, value);
        this.PlayerTroopName1 = ((object) value?.get_TroopName()).ToString();
      }
    }

    public MultiplayerClassDivisions.MPHeroClass PlayerTroopHeroClass2
    {
      get
      {
        return this.CurrentConfig.GetPlayerTroopHeroClass(1);
      }
      set
      {
        this.CurrentConfig.SetPlayerTroopHeroClass(1, value);
        this.PlayerTroopName2 = ((object) value?.get_TroopName()).ToString();
      }
    }

    public MultiplayerClassDivisions.MPHeroClass PlayerTroopHeroClass3
    {
      get
      {
        return this.CurrentConfig.GetPlayerTroopHeroClass(2);
      }
      set
      {
        this.CurrentConfig.SetPlayerTroopHeroClass(2, value);
        this.PlayerTroopName3 = ((object) value?.get_TroopName()).ToString();
      }
    }

    public MultiplayerClassDivisions.MPHeroClass PlayerTroopHeroClass4
    {
      get
      {
        return this.CurrentConfig.GetPlayerTroopHeroClass(3);
      }
      set
      {
        this.CurrentConfig.SetPlayerTroopHeroClass(3, value);
        this.PlayerTroopName4 = ((object) value?.get_TroopName()).ToString();
      }
    }

    public MultiplayerClassDivisions.MPHeroClass PlayerTroopHeroClass5
    {
      get
      {
        return this.CurrentConfig.GetPlayerTroopHeroClass(4);
      }
      set
      {
        this.CurrentConfig.SetPlayerTroopHeroClass(4, value);
        this.PlayerTroopName5 = ((object) value?.get_TroopName()).ToString();
      }
    }

    public MultiplayerClassDivisions.MPHeroClass PlayerTroopHeroClass6
    {
      get
      {
        return this.CurrentConfig.GetPlayerTroopHeroClass(5);
      }
      set
      {
        this.CurrentConfig.SetPlayerTroopHeroClass(5, value);
        this.PlayerTroopName6 = ((object) value?.get_TroopName()).ToString();
      }
    }

    public MultiplayerClassDivisions.MPHeroClass PlayerTroopHeroClass7
    {
      get
      {
        return this.CurrentConfig.GetPlayerTroopHeroClass(6);
      }
      set
      {
        this.CurrentConfig.SetPlayerTroopHeroClass(6, value);
        this.PlayerTroopName7 = ((object) value?.get_TroopName()).ToString();
      }
    }

    public MultiplayerClassDivisions.MPHeroClass PlayerTroopHeroClass8
    {
      get
      {
        return this.CurrentConfig.GetPlayerTroopHeroClass(7);
      }
      set
      {
        this.CurrentConfig.SetPlayerTroopHeroClass(7, value);
        this.PlayerTroopName8 = ((object) value?.get_TroopName()).ToString();
      }
    }

    [DataSourceProperty]
    public string EnemyTroopName1
    {
      get
      {
        return this._enemyTroopInfos[0].name;
      }
      set
      {
        if (this._enemyTroopInfos[0].name == value)
          return;
        this._enemyTroopInfos[0].name = value;
        this.OnPropertyChanged(nameof (EnemyTroopName1));
      }
    }

    [DataSourceProperty]
    public string EnemyTroopName2
    {
      get
      {
        return this._enemyTroopInfos[1].name;
      }
      set
      {
        if (this._enemyTroopInfos[1].name == value)
          return;
        this._enemyTroopInfos[1].name = value;
        this.OnPropertyChanged(nameof (EnemyTroopName2));
      }
    }

    [DataSourceProperty]
    public string EnemyTroopName3
    {
      get
      {
        return this._enemyTroopInfos[2].name;
      }
      set
      {
        if (this._enemyTroopInfos[2].name == value)
          return;
        this._enemyTroopInfos[2].name = value;
        this.OnPropertyChanged(nameof (EnemyTroopName3));
      }
    }

    [DataSourceProperty]
    public string EnemyTroopName4
    {
      get
      {
        return this._enemyTroopInfos[3].name;
      }
      set
      {
        if (this._enemyTroopInfos[3].name == value)
          return;
        this._enemyTroopInfos[3].name = value;
        this.OnPropertyChanged(nameof (EnemyTroopName4));
      }
    }

    [DataSourceProperty]
    public string EnemyTroopName5
    {
      get
      {
        return this._enemyTroopInfos[4].name;
      }
      set
      {
        if (this._enemyTroopInfos[4].name == value)
          return;
        this._enemyTroopInfos[4].name = value;
        this.OnPropertyChanged(nameof (EnemyTroopName5));
      }
    }

    [DataSourceProperty]
    public string EnemyTroopName6
    {
      get
      {
        return this._enemyTroopInfos[5].name;
      }
      set
      {
        if (this._enemyTroopInfos[5].name == value)
          return;
        this._enemyTroopInfos[5].name = value;
        this.OnPropertyChanged(nameof (EnemyTroopName6));
      }
    }

    [DataSourceProperty]
    public string EnemyTroopName7
    {
      get
      {
        return this._enemyTroopInfos[6].name;
      }
      set
      {
        if (this._enemyTroopInfos[6].name == value)
          return;
        this._enemyTroopInfos[6].name = value;
        this.OnPropertyChanged(nameof (EnemyTroopName7));
      }
    }

    [DataSourceProperty]
    public string EnemyTroopName8
    {
      get
      {
        return this._enemyTroopInfos[7].name;
      }
      set
      {
        if (this._enemyTroopInfos[7].name == value)
          return;
        this._enemyTroopInfos[7].name = value;
        this.OnPropertyChanged(nameof (EnemyTroopName8));
      }
    }

    private void UpdateEnemyTroopName()
    {
      this.EnemyTroopName1 = ((object) this.EnemyTroopHeroClass1.get_TroopName()).ToString();
      this.EnemyTroopName2 = ((object) this.EnemyTroopHeroClass2.get_TroopName()).ToString();
      this.EnemyTroopName3 = ((object) this.EnemyTroopHeroClass3.get_TroopName()).ToString();
      this.EnemyTroopName4 = ((object) this.EnemyTroopHeroClass4.get_TroopName()).ToString();
      this.EnemyTroopName5 = ((object) this.EnemyTroopHeroClass5.get_TroopName()).ToString();
      this.EnemyTroopName6 = ((object) this.EnemyTroopHeroClass6.get_TroopName()).ToString();
      this.EnemyTroopName7 = ((object) this.EnemyTroopHeroClass7.get_TroopName()).ToString();
      this.EnemyTroopName8 = ((object) this.EnemyTroopHeroClass8.get_TroopName()).ToString();
    }

    public MultiplayerClassDivisions.MPHeroClass EnemyTroopHeroClass1
    {
      get
      {
        return this.CurrentConfig.GetEnemyTroopHeroClass(0);
      }
      set
      {
        this.CurrentConfig.SetEnemyTroopHeroClass(0, value);
        this.EnemyTroopName1 = ((object) value?.get_TroopName()).ToString();
      }
    }

    public MultiplayerClassDivisions.MPHeroClass EnemyTroopHeroClass2
    {
      get
      {
        return this.CurrentConfig.GetEnemyTroopHeroClass(1);
      }
      set
      {
        this.CurrentConfig.SetEnemyTroopHeroClass(1, value);
        this.EnemyTroopName2 = ((object) value?.get_TroopName()).ToString();
      }
    }

    public MultiplayerClassDivisions.MPHeroClass EnemyTroopHeroClass3
    {
      get
      {
        return this.CurrentConfig.GetEnemyTroopHeroClass(2);
      }
      set
      {
        this.CurrentConfig.SetEnemyTroopHeroClass(2, value);
        this.EnemyTroopName3 = ((object) value?.get_TroopName()).ToString();
      }
    }

    public MultiplayerClassDivisions.MPHeroClass EnemyTroopHeroClass4
    {
      get
      {
        return this.CurrentConfig.GetEnemyTroopHeroClass(3);
      }
      set
      {
        this.CurrentConfig.SetEnemyTroopHeroClass(3, value);
        this.EnemyTroopName4 = ((object) value?.get_TroopName()).ToString();
      }
    }

    public MultiplayerClassDivisions.MPHeroClass EnemyTroopHeroClass5
    {
      get
      {
        return this.CurrentConfig.GetEnemyTroopHeroClass(4);
      }
      set
      {
        this.CurrentConfig.SetEnemyTroopHeroClass(4, value);
        this.EnemyTroopName5 = ((object) value?.get_TroopName()).ToString();
      }
    }

    public MultiplayerClassDivisions.MPHeroClass EnemyTroopHeroClass6
    {
      get
      {
        return this.CurrentConfig.GetEnemyTroopHeroClass(5);
      }
      set
      {
        this.CurrentConfig.SetEnemyTroopHeroClass(5, value);
        this.EnemyTroopName6 = ((object) value?.get_TroopName()).ToString();
      }
    }

    public MultiplayerClassDivisions.MPHeroClass EnemyTroopHeroClass7
    {
      get
      {
        return this.CurrentConfig.GetEnemyTroopHeroClass(6);
      }
      set
      {
        this.CurrentConfig.SetEnemyTroopHeroClass(6, value);
        this.EnemyTroopName7 = ((object) value?.get_TroopName()).ToString();
      }
    }

    public MultiplayerClassDivisions.MPHeroClass EnemyTroopHeroClass8
    {
      get
      {
        return this.CurrentConfig.GetEnemyTroopHeroClass(7);
      }
      set
      {
        this.CurrentConfig.SetEnemyTroopHeroClass(7, value);
        this.EnemyTroopName8 = ((object) value?.get_TroopName()).ToString();
      }
    }

    [DataSourceProperty]
    public bool UseRealisticBlocking
    {
      get
      {
        return this.CurrentConfig.useRealisticBlocking;
      }
      set
      {
        if (this.CurrentConfig.useRealisticBlocking == value)
          return;
        this.CurrentConfig.useRealisticBlocking = value;
        this.OnPropertyChanged(nameof (UseRealisticBlocking));
      }
    }

    [DataSourceProperty]
    public bool NoAgentLabel
    {
      get
      {
        return this.CurrentConfig.noAgentLabel;
      }
      set
      {
        if (this.CurrentConfig.noAgentLabel == value)
          return;
        this.CurrentConfig.noAgentLabel = value;
        this.OnPropertyChanged(nameof (NoAgentLabel));
      }
    }

    [DataSourceProperty]
    public bool NoKillNotification
    {
      get
      {
        return this.CurrentConfig.noKillNotification;
      }
      set
      {
        if (this.CurrentConfig.noKillNotification == value)
          return;
        this.CurrentConfig.noKillNotification = value;
        this.OnPropertyChanged(nameof (NoKillNotification));
      }
    }

    [DataSourceProperty]
    public bool ChangeCombatAI
    {
      get
      {
        return this.CurrentConfig.changeCombatAI;
      }
      set
      {
        if (this.CurrentConfig.changeCombatAI == value)
          return;
        this.CurrentConfig.changeCombatAI = value;
        this.OnPropertyChanged(nameof (ChangeCombatAI));
      }
    }

    [DataSourceProperty]
    public string CombatAI
    {
      get
      {
        return this._combatAI;
      }
      set
      {
        if (this._combatAI == value)
          return;
        this._combatAI = value;
        this.OnPropertyChanged(nameof (CombatAI));
      }
    }

    protected BattleConfigVMBase(
      CharacterSelectionView selectionView,
      MissionMenuView missionMenuView,
      T currentConfig)
    {
      this.\u002Ector();
      this._selectionView = selectionView;
      this._missionMenuView = missionMenuView;
      this.CurrentConfig = currentConfig;
      this._playerTroopInfos = new BattleConfigVMBase<T>.TroopInfo[8];
      this._enemyTroopInfos = new BattleConfigVMBase<T>.TroopInfo[8];
      this.InitializeContent();
    }

    private void InitializeContent()
    {
      this.UpdatePlayerSoldierCount();
      this.UpdateEnemySoldierCount();
      this._allMpHeroClassesMap = this.GetHeroClassesMap();
      this._allMpHeroClasses = ((IEnumerable<MultiplayerClassDivisions.MPHeroClass>) this.GetHeroClasses()).ToList<MultiplayerClassDivisions.MPHeroClass>();
      if (this.PlayerHeroClass == null)
        this.PlayerHeroClass = this._allMpHeroClasses[0];
      if (this.EnemyHeroClass == null)
        this.EnemyHeroClass = this._allMpHeroClasses[0];
      if (this.PlayerTroopHeroClass1 == null)
        this.PlayerTroopHeroClass1 = this._allMpHeroClasses[0];
      if (this.PlayerTroopHeroClass2 == null)
        this.PlayerTroopHeroClass2 = this._allMpHeroClasses[0];
      if (this.PlayerTroopHeroClass3 == null)
        this.PlayerTroopHeroClass3 = this._allMpHeroClasses[0];
      if (this.PlayerTroopHeroClass4 == null)
        this.PlayerTroopHeroClass4 = this._allMpHeroClasses[0];
      if (this.PlayerTroopHeroClass5 == null)
        this.PlayerTroopHeroClass5 = this._allMpHeroClasses[0];
      if (this.PlayerTroopHeroClass6 == null)
        this.PlayerTroopHeroClass6 = this._allMpHeroClasses[0];
      if (this.PlayerTroopHeroClass7 == null)
        this.PlayerTroopHeroClass7 = this._allMpHeroClasses[0];
      if (this.PlayerTroopHeroClass8 == null)
        this.PlayerTroopHeroClass8 = this._allMpHeroClasses[0];
      if (this.EnemyTroopHeroClass1 == null)
        this.EnemyTroopHeroClass1 = this._allMpHeroClasses[0];
      if (this.EnemyTroopHeroClass2 == null)
        this.EnemyTroopHeroClass2 = this._allMpHeroClasses[0];
      if (this.EnemyTroopHeroClass3 == null)
        this.EnemyTroopHeroClass3 = this._allMpHeroClasses[0];
      if (this.EnemyTroopHeroClass4 == null)
        this.EnemyTroopHeroClass4 = this._allMpHeroClasses[0];
      if (this.EnemyTroopHeroClass5 == null)
        this.EnemyTroopHeroClass5 = this._allMpHeroClasses[0];
      if (this.EnemyTroopHeroClass6 == null)
        this.EnemyTroopHeroClass6 = this._allMpHeroClasses[0];
      if (this.EnemyTroopHeroClass7 == null)
        this.EnemyTroopHeroClass7 = this._allMpHeroClasses[0];
      if (this.EnemyTroopHeroClass8 == null)
        this.EnemyTroopHeroClass8 = this._allMpHeroClasses[0];
      this.PlayerName = ((object) this.PlayerHeroClass.get_HeroName()).ToString();
      this.EnemyName = ((object) this.EnemyHeroClass.get_HeroName()).ToString();
      this.UpdatePlayerTroopName();
      this.UpdateEnemyTroopName();
      this.ChangeCombatAI = this.CurrentConfig.changeCombatAI;
      this.CombatAI = this.CurrentConfig.combatAI.ToString();
    }

    private List<MultiplayerClassDivisions.MPHeroClass> GetHeroClasses()
    {
      return MultiplayerClassDivisions.GetMPHeroClasses().ToList<MultiplayerClassDivisions.MPHeroClass>();
    }

    private Dictionary<string, Dictionary<string, List<MultiplayerClassDivisions.MPHeroClass>>> GetHeroClassesMap()
    {
      if (MultiplayerClassDivisions.AvailableCultures == null)
        MultiplayerClassDivisions.Initialize();
      Debug.Assert(MultiplayerClassDivisions.AvailableCultures != null, "Available Cultures should not be null");
      Dictionary<string, Dictionary<string, List<MultiplayerClassDivisions.MPHeroClass>>> dictionary1 = new Dictionary<string, Dictionary<string, List<MultiplayerClassDivisions.MPHeroClass>>>();
      if (MultiplayerClassDivisions.AvailableCultures != null)
      {
        using (IEnumerator<BasicCultureObject> enumerator1 = ((IEnumerable<BasicCultureObject>) MultiplayerClassDivisions.AvailableCultures).GetEnumerator())
        {
          while (((IEnumerator) enumerator1).MoveNext())
          {
            BasicCultureObject current1 = enumerator1.Current;
            Dictionary<string, List<MultiplayerClassDivisions.MPHeroClass>> dictionary2 = new Dictionary<string, List<MultiplayerClassDivisions.MPHeroClass>>();
            using (IEnumerator<MultiplayerClassDivisions.MPHeroClass> enumerator2 = MultiplayerClassDivisions.GetMPHeroClasses(current1).GetEnumerator())
            {
              while (((IEnumerator) enumerator2).MoveNext())
              {
                MultiplayerClassDivisions.MPHeroClass current2 = enumerator2.Current;
                List<MultiplayerClassDivisions.MPHeroClass> mpHeroClassList = (List<MultiplayerClassDivisions.MPHeroClass>) null;
                if (!dictionary2.TryGetValue((string) current2.get_ClassGroup().StringId, out mpHeroClassList))
                  dictionary2[(string) current2.get_ClassGroup().StringId] = mpHeroClassList = new List<MultiplayerClassDivisions.MPHeroClass>();
                mpHeroClassList.Add(current2);
              }
            }
            dictionary1.Add(((MBObjectBase) current1).get_StringId(), dictionary2);
          }
        }
      }
      return dictionary1;
    }

    protected void SelectPlayerCharacter()
    {
      this._selectionView.Open(CharacterSelectionParams.CharacterSelectionParamsFor(this._allMpHeroClassesMap, this.CurrentConfig.playerClass, false, (Action<CharacterSelectionParams>) (param =>
      {
        this.PlayerHeroClass = param.selectedHeroClass;
        this.CurrentConfig.playerClass.selectedFirstPerk = param.selectedFirstPerk;
        this.CurrentConfig.playerClass.selectedSecondPerk = param.selectedSecondPerk;
        this._selectionView.OnClose();
      })));
    }

    protected void SelectEnemyCharacter()
    {
      this._selectionView.Open(CharacterSelectionParams.CharacterSelectionParamsFor(this._allMpHeroClassesMap, this.CurrentConfig.enemyClass, false, (Action<CharacterSelectionParams>) (param =>
      {
        this.EnemyHeroClass = param.selectedHeroClass;
        this.CurrentConfig.enemyClass.selectedFirstPerk = param.selectedFirstPerk;
        this.CurrentConfig.enemyClass.selectedSecondPerk = param.selectedSecondPerk;
        this._selectionView.OnClose();
      })));
    }

    protected void SelectPlayerTroopCharacter1()
    {
      this._selectionView.Open(CharacterSelectionParams.CharacterSelectionParamsFor(this._allMpHeroClassesMap, this.CurrentConfig.playerTroops[0], true, (Action<CharacterSelectionParams>) (param =>
      {
        this.PlayerTroopHeroClass1 = param.selectedHeroClass;
        this.CurrentConfig.playerTroops[0].selectedFirstPerk = param.selectedFirstPerk;
        this.CurrentConfig.playerTroops[0].selectedSecondPerk = param.selectedSecondPerk;
        this._selectionView.OnClose();
      })));
    }

    protected void SelectPlayerTroopCharacter2()
    {
      this._selectionView.Open(CharacterSelectionParams.CharacterSelectionParamsFor(this._allMpHeroClassesMap, this.CurrentConfig.playerTroops[1], true, (Action<CharacterSelectionParams>) (param =>
      {
        this.PlayerTroopHeroClass2 = param.selectedHeroClass;
        this.CurrentConfig.playerTroops[1].selectedFirstPerk = param.selectedFirstPerk;
        this.CurrentConfig.playerTroops[1].selectedSecondPerk = param.selectedSecondPerk;
        this._selectionView.OnClose();
      })));
    }

    protected void SelectPlayerTroopCharacter3()
    {
      this._selectionView.Open(CharacterSelectionParams.CharacterSelectionParamsFor(this._allMpHeroClassesMap, this.CurrentConfig.playerTroops[2], true, (Action<CharacterSelectionParams>) (param =>
      {
        this.PlayerTroopHeroClass3 = param.selectedHeroClass;
        this.CurrentConfig.playerTroops[2].selectedFirstPerk = param.selectedFirstPerk;
        this.CurrentConfig.playerTroops[2].selectedSecondPerk = param.selectedSecondPerk;
        this._selectionView.OnClose();
      })));
    }

    protected void SelectPlayerTroopCharacter4()
    {
      this._selectionView.Open(CharacterSelectionParams.CharacterSelectionParamsFor(this._allMpHeroClassesMap, this.CurrentConfig.playerTroops[3], true, (Action<CharacterSelectionParams>) (param =>
      {
        this.PlayerTroopHeroClass4 = param.selectedHeroClass;
        this.CurrentConfig.playerTroops[3].selectedFirstPerk = param.selectedFirstPerk;
        this.CurrentConfig.playerTroops[3].selectedSecondPerk = param.selectedSecondPerk;
        this._selectionView.OnClose();
      })));
    }

    protected void SelectPlayerTroopCharacter5()
    {
      this._selectionView.Open(CharacterSelectionParams.CharacterSelectionParamsFor(this._allMpHeroClassesMap, this.CurrentConfig.playerTroops[4], true, (Action<CharacterSelectionParams>) (param =>
      {
        this.PlayerTroopHeroClass4 = param.selectedHeroClass;
        this.CurrentConfig.playerTroops[4].selectedFirstPerk = param.selectedFirstPerk;
        this.CurrentConfig.playerTroops[4].selectedSecondPerk = param.selectedSecondPerk;
        this._selectionView.OnClose();
      })));
    }

    protected void SelectPlayerTroopCharacter6()
    {
      this._selectionView.Open(CharacterSelectionParams.CharacterSelectionParamsFor(this._allMpHeroClassesMap, this.CurrentConfig.playerTroops[5], true, (Action<CharacterSelectionParams>) (param =>
      {
        this.PlayerTroopHeroClass4 = param.selectedHeroClass;
        this.CurrentConfig.playerTroops[5].selectedFirstPerk = param.selectedFirstPerk;
        this.CurrentConfig.playerTroops[5].selectedSecondPerk = param.selectedSecondPerk;
        this._selectionView.OnClose();
      })));
    }

    protected void SelectPlayerTroopCharacter7()
    {
      this._selectionView.Open(CharacterSelectionParams.CharacterSelectionParamsFor(this._allMpHeroClassesMap, this.CurrentConfig.playerTroops[6], true, (Action<CharacterSelectionParams>) (param =>
      {
        this.PlayerTroopHeroClass4 = param.selectedHeroClass;
        this.CurrentConfig.playerTroops[6].selectedFirstPerk = param.selectedFirstPerk;
        this.CurrentConfig.playerTroops[6].selectedSecondPerk = param.selectedSecondPerk;
        this._selectionView.OnClose();
      })));
    }

    protected void SelectPlayerTroopCharacter8()
    {
      this._selectionView.Open(CharacterSelectionParams.CharacterSelectionParamsFor(this._allMpHeroClassesMap, this.CurrentConfig.playerTroops[7], true, (Action<CharacterSelectionParams>) (param =>
      {
        this.PlayerTroopHeroClass4 = param.selectedHeroClass;
        this.CurrentConfig.playerTroops[7].selectedFirstPerk = param.selectedFirstPerk;
        this.CurrentConfig.playerTroops[7].selectedSecondPerk = param.selectedSecondPerk;
        this._selectionView.OnClose();
      })));
    }

    protected void SelectEnemyTroopCharacter1()
    {
      this._selectionView.Open(CharacterSelectionParams.CharacterSelectionParamsFor(this._allMpHeroClassesMap, this.CurrentConfig.enemyTroops[0], true, (Action<CharacterSelectionParams>) (param =>
      {
        this.EnemyTroopHeroClass1 = param.selectedHeroClass;
        this.CurrentConfig.enemyTroops[0].selectedFirstPerk = param.selectedFirstPerk;
        this.CurrentConfig.enemyTroops[0].selectedSecondPerk = param.selectedSecondPerk;
        this._selectionView.OnClose();
      })));
    }

    protected void SelectEnemyTroopCharacter2()
    {
      this._selectionView.Open(CharacterSelectionParams.CharacterSelectionParamsFor(this._allMpHeroClassesMap, this.CurrentConfig.enemyTroops[1], true, (Action<CharacterSelectionParams>) (param =>
      {
        this.EnemyTroopHeroClass2 = param.selectedHeroClass;
        this.CurrentConfig.enemyTroops[1].selectedFirstPerk = param.selectedFirstPerk;
        this.CurrentConfig.enemyTroops[1].selectedSecondPerk = param.selectedSecondPerk;
        this._selectionView.OnClose();
      })));
    }

    protected void SelectEnemyTroopCharacter3()
    {
      this._selectionView.Open(CharacterSelectionParams.CharacterSelectionParamsFor(this._allMpHeroClassesMap, this.CurrentConfig.enemyTroops[2], true, (Action<CharacterSelectionParams>) (param =>
      {
        this.EnemyTroopHeroClass3 = param.selectedHeroClass;
        this.CurrentConfig.enemyTroops[2].selectedFirstPerk = param.selectedFirstPerk;
        this.CurrentConfig.enemyTroops[2].selectedSecondPerk = param.selectedSecondPerk;
        this._selectionView.OnClose();
      })));
    }

    protected void SelectEnemyTroopCharacter4()
    {
      this._selectionView.Open(CharacterSelectionParams.CharacterSelectionParamsFor(this._allMpHeroClassesMap, this.CurrentConfig.enemyTroops[3], true, (Action<CharacterSelectionParams>) (param =>
      {
        this.EnemyTroopHeroClass4 = param.selectedHeroClass;
        this.CurrentConfig.enemyTroops[3].selectedFirstPerk = param.selectedFirstPerk;
        this.CurrentConfig.enemyTroops[3].selectedSecondPerk = param.selectedSecondPerk;
        this._selectionView.OnClose();
      })));
    }

    protected void SelectEnemyTroopCharacter5()
    {
      this._selectionView.Open(CharacterSelectionParams.CharacterSelectionParamsFor(this._allMpHeroClassesMap, this.CurrentConfig.enemyTroops[4], true, (Action<CharacterSelectionParams>) (param =>
      {
        this.EnemyTroopHeroClass5 = param.selectedHeroClass;
        this.CurrentConfig.enemyTroops[4].selectedFirstPerk = param.selectedFirstPerk;
        this.CurrentConfig.enemyTroops[4].selectedSecondPerk = param.selectedSecondPerk;
        this._selectionView.OnClose();
      })));
    }

    protected void SelectEnemyTroopCharacter6()
    {
      this._selectionView.Open(CharacterSelectionParams.CharacterSelectionParamsFor(this._allMpHeroClassesMap, this.CurrentConfig.enemyTroops[5], true, (Action<CharacterSelectionParams>) (param =>
      {
        this.EnemyTroopHeroClass6 = param.selectedHeroClass;
        this.CurrentConfig.enemyTroops[5].selectedFirstPerk = param.selectedFirstPerk;
        this.CurrentConfig.enemyTroops[5].selectedSecondPerk = param.selectedSecondPerk;
        this._selectionView.OnClose();
      })));
    }

    protected void SelectEnemyTroopCharacter7()
    {
      this._selectionView.Open(CharacterSelectionParams.CharacterSelectionParamsFor(this._allMpHeroClassesMap, this.CurrentConfig.enemyTroops[6], true, (Action<CharacterSelectionParams>) (param =>
      {
        this.EnemyTroopHeroClass7 = param.selectedHeroClass;
        this.CurrentConfig.enemyTroops[6].selectedFirstPerk = param.selectedFirstPerk;
        this.CurrentConfig.enemyTroops[6].selectedSecondPerk = param.selectedSecondPerk;
        this._selectionView.OnClose();
      })));
    }

    protected void SelectEnemyTroopCharacter8()
    {
      this._selectionView.Open(CharacterSelectionParams.CharacterSelectionParamsFor(this._allMpHeroClassesMap, this.CurrentConfig.enemyTroops[7], true, (Action<CharacterSelectionParams>) (param =>
      {
        this.EnemyTroopHeroClass8 = param.selectedHeroClass;
        this.CurrentConfig.enemyTroops[7].selectedFirstPerk = param.selectedFirstPerk;
        this.CurrentConfig.enemyTroops[7].selectedSecondPerk = param.selectedSecondPerk;
        this._selectionView.OnClose();
      })));
    }

    protected void OpenMissionMenu()
    {
      this._missionMenuView.ActivateMenu();
    }

    protected BattleConfigVMBase<T>.SaveParamResult SaveConfig()
    {
      try
      {
        this.ApplyConfig();
      }
      catch
      {
        Utility.DisplayLocalizedText("str_content_illegal", (string) null);
        return BattleConfigVMBase<T>.SaveParamResult.notValid;
      }
      if (!this.CurrentConfig.Validate())
      {
        Utility.DisplayLocalizedText("str_content_outofrange", (string) null);
        return BattleConfigVMBase<T>.SaveParamResult.notValid;
      }
      this.CurrentConfig.Serialize();
      return BattleConfigVMBase<T>.SaveParamResult.success;
    }

    protected virtual void ApplyConfig()
    {
      for (int index = 0; index < 8; ++index)
        this.CurrentConfig.playerTroops[index].troopCount = Convert.ToInt32(this._playerTroopInfos[index].count);
      for (int index = 0; index < 8; ++index)
        this.CurrentConfig.enemyTroops[index].troopCount = Convert.ToInt32(this._enemyTroopInfos[index].count);
      this.CurrentConfig.combatAI = Convert.ToInt32(this._combatAI);
    }

    protected enum SaveParamResult
    {
      success,
      failed,
      notValid,
    }

    private struct TroopInfo
    {
      public string name;
      public string count;
    }
  }
}
