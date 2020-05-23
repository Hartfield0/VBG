// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.BattleConfigBase
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ObjectSystem;

namespace VirtualBattlegrounds
{
  public abstract class BattleConfigBase
  {
    private static string ApplicationName = "Mount and Blade II Bannerlord";
    private static string ModuleName = "VirtualBattlegrounds";
    public bool isPlayerAttacker = true;
    public AIEnableType aiEnableType = AIEnableType.EnemyOnly;
    public bool useRealisticBlocking = false;
    public bool noAgentLabel = false;
    public bool noKillNotification = false;
    [XmlIgnore]
    public BattleType battleType;
    public ClassInfo playerClass;
    private bool _spawnPlayer;
    public ClassInfo enemyClass;
    private bool _spawnEnemyCommander;
    public ClassInfo[] playerTroops;
    public ClassInfo[] enemyTroops;
    public TacticOptionInfo[] attackerTacticOptions;
    public TacticOptionInfo[] defenderTacticOptions;
    public bool disableDeath;
    public bool changeCombatAI;
    public int combatAI;

    public static Version ModVersion { get; } = new Version(0, 3, 5);

    public string ConfigVersion { get; set; }

    public int GetTotalNumberForSide(BattleSideEnum side)
    {
      return this.GetTotalNumberForSide((this.isPlayerAttacker ? 1 : 0) == side);
    }

    public int GetTotalNumberForSide(bool isPlayer)
    {
      return isPlayer ? (this.SpawnPlayer ? 1 : 0) + ((IEnumerable<ClassInfo>) this.playerTroops).Sum<ClassInfo>((Func<ClassInfo, int>) (classInfo => classInfo.troopCount)) : (this.SpawnEnemyCommander ? 1 : 0) + ((IEnumerable<ClassInfo>) this.enemyTroops).Sum<ClassInfo>((Func<ClassInfo, int>) (classInfo => classInfo.troopCount));
    }

    [XmlIgnore]
    public bool ShouldCelebrateVictory
    {
      get
      {
        return this.GetTotalNumberForSide(true) != 0 && (uint) this.GetTotalNumberForSide(false) > 0U;
      }
    }

    public void ToPreviousAIEnableType()
    {
      switch (this.aiEnableType)
      {
        case AIEnableType.None:
          this.aiEnableType = AIEnableType.Both;
          break;
        case AIEnableType.EnemyOnly:
          this.aiEnableType = AIEnableType.None;
          break;
        case AIEnableType.PlayerOnly:
          this.aiEnableType = AIEnableType.EnemyOnly;
          break;
        case AIEnableType.Both:
          this.aiEnableType = AIEnableType.PlayerOnly;
          break;
      }
    }

    public void ToNextAIEnableType()
    {
      switch (this.aiEnableType)
      {
        case AIEnableType.None:
          this.aiEnableType = AIEnableType.EnemyOnly;
          break;
        case AIEnableType.EnemyOnly:
          this.aiEnableType = AIEnableType.PlayerOnly;
          break;
        case AIEnableType.PlayerOnly:
          this.aiEnableType = AIEnableType.Both;
          break;
        case AIEnableType.Both:
          this.aiEnableType = AIEnableType.None;
          break;
      }
    }

    [XmlIgnore]
    public MultiplayerClassDivisions.MPHeroClass PlayerHeroClass
    {
      get
      {
        return (MultiplayerClassDivisions.MPHeroClass) MBObjectManager.get_Instance().GetObject<MultiplayerClassDivisions.MPHeroClass>(this.playerClass.classStringId);
      }
      set
      {
        this.playerClass.classStringId = ((MBObjectBase) value).get_StringId();
      }
    }

    [XmlIgnore]
    public MultiplayerClassDivisions.MPHeroClass EnemyHeroClass
    {
      get
      {
        return (MultiplayerClassDivisions.MPHeroClass) MBObjectManager.get_Instance().GetObject<MultiplayerClassDivisions.MPHeroClass>(this.enemyClass.classStringId);
      }
      set
      {
        this.enemyClass.classStringId = ((MBObjectBase) value).get_StringId();
      }
    }

    public void SetPlayerTroopHeroClass(int i, MultiplayerClassDivisions.MPHeroClass heroClass)
    {
      this.playerTroops[i].classStringId = ((MBObjectBase) heroClass).get_StringId();
    }

    public MultiplayerClassDivisions.MPHeroClass GetPlayerTroopHeroClass(
      int i)
    {
      return (MultiplayerClassDivisions.MPHeroClass) MBObjectManager.get_Instance().GetObject<MultiplayerClassDivisions.MPHeroClass>(this.playerTroops[i].classStringId);
    }

    public bool SpawnPlayer
    {
      get
      {
        return this._spawnPlayer;
      }
      set
      {
        this._spawnPlayer = value;
        this.playerClass.troopCount = value ? 1 : 0;
      }
    }

    public void SetEnemyTroopHeroClass(int i, MultiplayerClassDivisions.MPHeroClass heroClass)
    {
      this.enemyTroops[i].classStringId = ((MBObjectBase) heroClass).get_StringId();
    }

    public MultiplayerClassDivisions.MPHeroClass GetEnemyTroopHeroClass(
      int i)
    {
      return (MultiplayerClassDivisions.MPHeroClass) MBObjectManager.get_Instance().GetObject<MultiplayerClassDivisions.MPHeroClass>(this.enemyTroops[i].classStringId);
    }

    public bool SpawnEnemyCommander
    {
      get
      {
        return this._spawnEnemyCommander;
      }
      set
      {
        this._spawnEnemyCommander = value;
        this.enemyClass.troopCount = value ? 1 : 0;
      }
    }

    public BasicCultureObject GetPlayerTeamCulture()
    {
      if (this.SpawnPlayer)
        return this.PlayerHeroClass.get_Culture();
      for (int i = 0; i < 8; ++i)
      {
        if ((uint) this.playerTroops[i].troopCount > 0U)
          return this.GetPlayerTroopHeroClass(i).get_Culture();
      }
      return ((IEnumerable<BasicCultureObject>) MultiplayerClassDivisions.AvailableCultures).FirstOrDefault<BasicCultureObject>();
    }

    public BasicCultureObject GetEnemyTeamCulture()
    {
      if (this.SpawnEnemyCommander)
        return this.EnemyHeroClass.get_Culture();
      for (int i = 0; i < 8; ++i)
      {
        if ((uint) this.enemyTroops[i].troopCount > 0U)
          return this.GetEnemyTroopHeroClass(i).get_Culture();
      }
      return ((IEnumerable<BasicCultureObject>) MultiplayerClassDivisions.AvailableCultures).FirstOrDefault<BasicCultureObject>();
    }

    protected BattleConfigBase(BattleType t)
    {
      this.battleType = t;
      switch (t)
      {
        case BattleType.FieldBattle:
          this.attackerTacticOptions = new TacticOptionInfo[11]
          {
            new TacticOptionInfo()
            {
              tacticOption = TacticOptionEnum.Charge
            },
            new TacticOptionInfo()
            {
              tacticOption = TacticOptionEnum.FullScaleAttack
            },
            new TacticOptionInfo()
            {
              tacticOption = TacticOptionEnum.DefensiveEngagement
            },
            new TacticOptionInfo()
            {
              tacticOption = TacticOptionEnum.DefensiveLine
            },
            new TacticOptionInfo()
            {
              tacticOption = TacticOptionEnum.DefensiveRing
            },
            new TacticOptionInfo()
            {
              tacticOption = TacticOptionEnum.HoldTheHill
            },
            new TacticOptionInfo()
            {
              tacticOption = TacticOptionEnum.HoldChokePoint
            },
            new TacticOptionInfo()
            {
              tacticOption = TacticOptionEnum.ArchersOnTheHill
            },
            new TacticOptionInfo()
            {
              tacticOption = TacticOptionEnum.RangedHarassmentOffensive
            },
            new TacticOptionInfo()
            {
              tacticOption = TacticOptionEnum.FrontalCavalryCharge
            },
            new TacticOptionInfo()
            {
              tacticOption = TacticOptionEnum.CoordinatedRetreat
            }
          };
          this.defenderTacticOptions = new TacticOptionInfo[11]
          {
            new TacticOptionInfo()
            {
              tacticOption = TacticOptionEnum.Charge
            },
            new TacticOptionInfo()
            {
              tacticOption = TacticOptionEnum.FullScaleAttack
            },
            new TacticOptionInfo()
            {
              tacticOption = TacticOptionEnum.DefensiveEngagement
            },
            new TacticOptionInfo()
            {
              tacticOption = TacticOptionEnum.DefensiveLine
            },
            new TacticOptionInfo()
            {
              tacticOption = TacticOptionEnum.DefensiveRing
            },
            new TacticOptionInfo()
            {
              tacticOption = TacticOptionEnum.HoldTheHill
            },
            new TacticOptionInfo()
            {
              tacticOption = TacticOptionEnum.HoldChokePoint
            },
            new TacticOptionInfo()
            {
              tacticOption = TacticOptionEnum.ArchersOnTheHill
            },
            new TacticOptionInfo()
            {
              tacticOption = TacticOptionEnum.RangedHarassmentOffensive
            },
            new TacticOptionInfo()
            {
              tacticOption = TacticOptionEnum.FrontalCavalryCharge
            },
            new TacticOptionInfo()
            {
              tacticOption = TacticOptionEnum.CoordinatedRetreat
            }
          };
          break;
        case BattleType.SiegeBattle:
          this.attackerTacticOptions = new TacticOptionInfo[2]
          {
            new TacticOptionInfo()
            {
              tacticOption = TacticOptionEnum.Charge
            },
            new TacticOptionInfo()
            {
              tacticOption = TacticOptionEnum.BreachWalls
            }
          };
          this.defenderTacticOptions = new TacticOptionInfo[2]
          {
            new TacticOptionInfo()
            {
              tacticOption = TacticOptionEnum.Charge
            },
            new TacticOptionInfo()
            {
              tacticOption = TacticOptionEnum.DefendCastle
            }
          };
          break;
      }
    }

    public virtual bool Validate()
    {
      return this.PlayerHeroClass != null && this.enemyClass != null && ((IEnumerable<ClassInfo>) this.playerTroops).All<ClassInfo>((Func<ClassInfo, bool>) (classInfo => MBObjectManager.get_Instance().GetObject<MultiplayerClassDivisions.MPHeroClass>(classInfo.classStringId) != null && classInfo.troopCount >= 0 && (classInfo.troopCount <= 5000 && classInfo.selectedFirstPerk >= 0) && (classInfo.selectedFirstPerk <= 2 && classInfo.selectedSecondPerk >= 0) && classInfo.selectedSecondPerk <= 2)) && (((IEnumerable<ClassInfo>) this.enemyTroops).All<ClassInfo>((Func<ClassInfo, bool>) (classInfo => MBObjectManager.get_Instance().GetObject<MultiplayerClassDivisions.MPHeroClass>(classInfo.classStringId) != null && classInfo.troopCount >= 0 && (classInfo.troopCount <= 5000 && classInfo.selectedFirstPerk >= 0) && (classInfo.selectedFirstPerk <= 2 && classInfo.selectedSecondPerk >= 0) && classInfo.selectedSecondPerk <= 2)) && this.combatAI >= 0) && this.combatAI <= 100;
    }

    public abstract bool Serialize();

    public abstract bool Deserialize();

    public abstract void ReloadSavedConfig();

    public abstract void ResetToDefault();

    protected void EnsureSaveDirectory()
    {
      Directory.CreateDirectory(BattleConfigBase.SavePath);
    }

    protected void SyncWithSave()
    {
      if (File.Exists(this.SaveName) && this.Deserialize())
      {
        this.RemoveOldConfig();
      }
      else
      {
        this.MoveOldConfig();
        if (File.Exists(this.SaveName) && this.Deserialize())
          return;
        Utility.DisplayLocalizedText("str_create_default_config", (string) null);
        this.ResetToDefault();
        this.Serialize();
      }
    }

    private void RemoveOldConfig()
    {
      foreach (string oldName in this.OldNames)
      {
        if (File.Exists(oldName))
        {
          Utility.DisplayMessage(((object) GameTexts.FindText("str_found_old_config", (string) null)).ToString() + " \"" + oldName + "\".");
          Utility.DisplayLocalizedText("str_delete_old_config", (string) null);
          File.Delete(oldName);
        }
      }
    }

    private void MoveOldConfig()
    {
      string sourceFileName = ((IEnumerable<string>) this.OldNames).FirstOrDefault<string>(new Func<string, bool>(File.Exists));
      if (sourceFileName != null && !Extensions.IsEmpty<char>((IEnumerable<M0>) sourceFileName))
      {
        Utility.DisplayLocalizedText("str_rename_old_config", (string) null);
        File.Move(sourceFileName, this.SaveName);
      }
      this.RemoveOldConfig();
    }

    protected static string SavePath
    {
      get
      {
        return Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\" + BattleConfigBase.ApplicationName + "\\Configs\\" + BattleConfigBase.ModuleName + "\\";
      }
    }

    protected abstract string SaveName { get; }

    protected abstract string[] OldNames { get; }
  }
}
