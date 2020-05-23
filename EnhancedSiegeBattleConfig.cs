// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.EnhancedSiegeBattleConfig
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System;
using System.IO;
using System.Xml.Serialization;
using TaleWorlds.Library;

namespace VirtualBattlegrounds
{
  public class EnhancedSiegeBattleConfig : BattleConfigBase<EnhancedSiegeBattleConfig>
  {
    private static EnhancedSiegeBattleConfig _instance;
    public EnhancedSiegeBattleConfig.SceneInfo[] sceneList;
    public int sceneIndex;
    public float soldierXInterval;
    public float soldierYInterval;
    public bool charge;
    public bool hasBoundary;

    protected static Version BinaryVersion
    {
      get
      {
        return new Version(1, 10);
      }
    }

    protected void UpgradeToCurrentVersion()
    {
      switch (this.ConfigVersion?.ToString())
      {
        case "1.10":
          break;
        default:
          Utility.DisplayLocalizedText("str_config_incompatible", (string) null);
          this.ResetToDefault();
          this.Serialize();
          break;
      }
    }

    [XmlIgnore]
    public int SoldiersPerRow
    {
      get
      {
        return this.sceneList[this.sceneIndex].soldiersPerRow;
      }
      set
      {
        this.sceneList[this.sceneIndex].soldiersPerRow = value;
      }
    }

    [XmlIgnore]
    public Vec2 FormationPosition
    {
      get
      {
        return this.sceneList[this.sceneIndex].formationPosition;
      }
      set
      {
        this.sceneList[this.sceneIndex].formationPosition = value;
      }
    }

    [XmlIgnore]
    public Vec2 FormationDirection
    {
      get
      {
        return this.sceneList[this.sceneIndex].formationDirection;
      }
      set
      {
        this.sceneList[this.sceneIndex].formationDirection = value;
      }
    }

    [XmlIgnore]
    public float SkyBrightness
    {
      get
      {
        return this.sceneList[this.sceneIndex].skyBrightness;
      }
      set
      {
        this.sceneList[this.sceneIndex].skyBrightness = value;
      }
    }

    [XmlIgnore]
    public float RainDensity
    {
      get
      {
        return this.sceneList[this.sceneIndex].rainDensity;
      }
      set
      {
        this.sceneList[this.sceneIndex].rainDensity = value;
      }
    }

    [XmlIgnore]
    public float Distance
    {
      get
      {
        return this.sceneList[this.sceneIndex].distance;
      }
      set
      {
        this.sceneList[this.sceneIndex].distance = value;
      }
    }

    [XmlIgnore]
    public string SceneName
    {
      get
      {
        return this.sceneList[this.sceneIndex].name;
      }
    }

    [XmlIgnore]
    public bool IsSiegeMission
    {
      get
      {
        return this.SceneName.Contains("siege");
      }
    }

    public EnhancedSiegeBattleConfig()
      : base(BattleType.SiegeBattle)
    {
    }

    private static EnhancedSiegeBattleConfig CreateDefault()
    {
      EnhancedSiegeBattleConfig.SceneInfo[] sceneInfoArray1 = new EnhancedSiegeBattleConfig.SceneInfo[3];
      EnhancedSiegeBattleConfig.SceneInfo sceneInfo = new EnhancedSiegeBattleConfig.SceneInfo();
      sceneInfo.name = "mp_siege_map_003";
      sceneInfo.formationPosition = new Vec2(461f, 634f);
      Vec2 vec2 = new Vec2(0.55f, 0.45f);
      sceneInfo.formationDirection = ((Vec2) ref vec2).Normalized();
      sceneInfo.distance = 180f;
      sceneInfoArray1[0] = sceneInfo;
      sceneInfoArray1[1] = new EnhancedSiegeBattleConfig.SceneInfo()
      {
        name = "mp_siege_map_005",
        formationPosition = new Vec2(424f, 320f),
        formationDirection = new Vec2(0.0f, 1f),
        distance = 220f
      };
      sceneInfoArray1[2] = new EnhancedSiegeBattleConfig.SceneInfo()
      {
        name = "mp_siege_map_007_battania",
        formationPosition = new Vec2(614f, 612f),
        formationDirection = new Vec2(0.65f, 0.35f),
        distance = 140f
      };
      EnhancedSiegeBattleConfig.SceneInfo[] sceneInfoArray2 = sceneInfoArray1;
      int num = 0;
      EnhancedSiegeBattleConfig siegeBattleConfig = new EnhancedSiegeBattleConfig();
      siegeBattleConfig.ConfigVersion = EnhancedSiegeBattleConfig.BinaryVersion.ToString(2);
      siegeBattleConfig.sceneList = sceneInfoArray2;
      siegeBattleConfig.sceneIndex = num;
      siegeBattleConfig.playerClass = new ClassInfo()
      {
        classStringId = "mp_light_cavalry_vlandia",
        selectedFirstPerk = 0,
        selectedSecondPerk = 0
      };
      siegeBattleConfig.SpawnPlayer = true;
      siegeBattleConfig.enemyClass = new ClassInfo()
      {
        classStringId = "mp_light_cavalry_battania",
        selectedFirstPerk = 0,
        selectedSecondPerk = 0
      };
      siegeBattleConfig.SpawnEnemyCommander = true;
      siegeBattleConfig.playerTroops = new ClassInfo[8]
      {
        new ClassInfo()
        {
          classStringId = "mp_shock_infantry_vlandia",
          selectedFirstPerk = 0,
          selectedSecondPerk = 0,
          troopCount = 20
        },
        new ClassInfo()
        {
          classStringId = "mp_light_infantry_vlandia",
          selectedFirstPerk = 0,
          selectedSecondPerk = 0,
          troopCount = 20
        },
        new ClassInfo()
        {
          classStringId = "mp_heavy_infantry_vlandia",
          selectedFirstPerk = 0,
          selectedSecondPerk = 0,
          troopCount = 20
        },
        new ClassInfo()
        {
          classStringId = "mp_heavy_infantry_vlandia",
          selectedFirstPerk = 0,
          selectedSecondPerk = 0,
          troopCount = 20
        },
        new ClassInfo()
        {
          classStringId = "mp_shock_infantry_vlandia",
          selectedFirstPerk = 0,
          selectedSecondPerk = 0,
          troopCount = 20
        },
        new ClassInfo()
        {
          classStringId = "mp_light_infantry_vlandia",
          selectedFirstPerk = 0,
          selectedSecondPerk = 0,
          troopCount = 20
        },
        new ClassInfo()
        {
          classStringId = "mp_heavy_infantry_vlandia",
          selectedFirstPerk = 0,
          selectedSecondPerk = 0,
          troopCount = 20
        },
        new ClassInfo()
        {
          classStringId = "mp_heavy_infantry_vlandia",
          selectedFirstPerk = 0,
          selectedSecondPerk = 0,
          troopCount = 20
        }
      };
      siegeBattleConfig.enemyTroops = new ClassInfo[8]
      {
        new ClassInfo()
        {
          classStringId = "mp_shock_infantry_battania",
          selectedFirstPerk = 0,
          selectedSecondPerk = 0,
          troopCount = 20
        },
        new ClassInfo()
        {
          classStringId = "mp_light_infantry_battania",
          selectedFirstPerk = 0,
          selectedSecondPerk = 0,
          troopCount = 20
        },
        new ClassInfo()
        {
          classStringId = "mp_heavy_infantry_battania",
          selectedFirstPerk = 0,
          selectedSecondPerk = 0,
          troopCount = 20
        },
        new ClassInfo()
        {
          classStringId = "mp_heavy_infantry_battania",
          selectedFirstPerk = 0,
          selectedSecondPerk = 0,
          troopCount = 20
        },
        new ClassInfo()
        {
          classStringId = "mp_shock_infantry_battania",
          selectedFirstPerk = 0,
          selectedSecondPerk = 0,
          troopCount = 20
        },
        new ClassInfo()
        {
          classStringId = "mp_light_infantry_battania",
          selectedFirstPerk = 0,
          selectedSecondPerk = 0,
          troopCount = 20
        },
        new ClassInfo()
        {
          classStringId = "mp_heavy_infantry_battania",
          selectedFirstPerk = 0,
          selectedSecondPerk = 0,
          troopCount = 20
        },
        new ClassInfo()
        {
          classStringId = "mp_heavy_infantry_battania",
          selectedFirstPerk = 0,
          selectedSecondPerk = 0,
          troopCount = 20
        }
      };
      siegeBattleConfig.soldierXInterval = 1.5f;
      siegeBattleConfig.soldierYInterval = 1f;
      siegeBattleConfig.charge = false;
      siegeBattleConfig.hasBoundary = true;
      siegeBattleConfig.disableDeath = false;
      siegeBattleConfig.changeCombatAI = false;
      siegeBattleConfig.combatAI = 100;
      return siegeBattleConfig;
    }

    public static EnhancedSiegeBattleConfig Get()
    {
      if (EnhancedSiegeBattleConfig._instance == null)
      {
        EnhancedSiegeBattleConfig._instance = new EnhancedSiegeBattleConfig();
        EnhancedSiegeBattleConfig._instance.SyncWithSave();
      }
      return EnhancedSiegeBattleConfig._instance;
    }

    public override bool Validate()
    {
      int num;
      if (base.Validate() && this.sceneIndex >= 0 && (this.sceneIndex < this.sceneList.Length && (double) this.Distance > 0.0) && ((double) this.soldierXInterval > 0.0 && (double) this.soldierYInterval > 0.0 && this.SoldiersPerRow > 0))
      {
        Vec2 formationDirection = this.FormationDirection;
        num = (double) ((Vec2) ref formationDirection).get_Length() > 0.0 ? 1 : 0;
      }
      else
        num = 0;
      return num != 0;
    }

    public override bool Serialize()
    {
      try
      {
        this.EnsureSaveDirectory();
        using (TextWriter textWriter = (TextWriter) new StreamWriter(this.SaveName))
          new XmlSerializer(typeof (EnhancedSiegeBattleConfig)).Serialize(textWriter, (object) this);
        Utility.DisplayLocalizedText("str_saved_config", (string) null);
        return true;
      }
      catch (Exception ex)
      {
        Utility.DisplayLocalizedText("str_save_config_failed", (string) null);
        Utility.DisplayLocalizedText("str_exception_caught", (string) null);
        Utility.DisplayMessage(ex.ToString());
        Console.WriteLine((object) ex);
      }
      return false;
    }

    public override bool Deserialize()
    {
      try
      {
        this.EnsureSaveDirectory();
        using (TextReader textReader = (TextReader) new StreamReader(this.SaveName))
          this.CopyFrom((EnhancedSiegeBattleConfig) new XmlSerializer(typeof (EnhancedSiegeBattleConfig)).Deserialize(textReader));
        Utility.DisplayLocalizedText("str_loaded_config", (string) null);
        this.UpgradeToCurrentVersion();
        return true;
      }
      catch (Exception ex)
      {
        Utility.DisplayLocalizedText("str_load_config_failed", (string) null);
        Utility.DisplayLocalizedText("str_exception_caught", (string) null);
        Utility.DisplayMessage(ex.ToString());
        Console.WriteLine((object) ex);
      }
      return false;
    }

    public override void ReloadSavedConfig()
    {
      EnhancedSiegeBattleConfig other = EnhancedSiegeBattleConfig.CreateDefault();
      if (!other.Deserialize())
        return;
      this.CopyFrom(other);
    }

    public override void ResetToDefault()
    {
      this.CopyFrom(EnhancedSiegeBattleConfig.CreateDefault());
    }

    protected override void CopyFrom(EnhancedSiegeBattleConfig other)
    {
      base.CopyFrom(other);
      if (other.sceneList != null)
        this.sceneList = other.sceneList;
      this.sceneIndex = other.sceneIndex;
      this.soldierXInterval = other.soldierXInterval;
      this.soldierYInterval = other.soldierYInterval;
      this.charge = other.charge;
      this.hasBoundary = other.hasBoundary;
    }

    protected override string SaveName
    {
      get
      {
        return BattleConfigBase.SavePath + "EnhancedSiegeBattleConfig.xml";
      }
    }

    protected override string[] OldNames { get; } = new string[0];

    public class SceneInfo
    {
      public int soldiersPerRow = 20;
      public float distance = 50f;
      public float skyBrightness = -1f;
      public float rainDensity = -1f;
      public string name;
      public Vec2 formationPosition;
      public Vec2 formationDirection;
    }
  }
}
