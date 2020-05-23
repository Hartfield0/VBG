// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.EnhancedFreeBattleConfig
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System;
using System.IO;
using System.Xml.Serialization;
using TaleWorlds.Library;

namespace VirtualBattlegrounds
{
  public class EnhancedFreeBattleConfig : BattleConfigBase<EnhancedFreeBattleConfig>
  {
    public bool makeGruntVoice = true;
    private static EnhancedFreeBattleConfig _instance;
    public EnhancedFreeBattleConfig.SceneInfo[] sceneList;
    public int sceneIndex;
    public float soldierXInterval;
    public float soldierYInterval;
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

    public EnhancedFreeBattleConfig()
      : base(BattleType.FieldBattle)
    {
    }

    private static EnhancedFreeBattleConfig CreateDefault()
    {
      EnhancedFreeBattleConfig.SceneInfo[] sceneInfoArray1 = new EnhancedFreeBattleConfig.SceneInfo[36];
      sceneInfoArray1[0] = new EnhancedFreeBattleConfig.SceneInfo()
      {
        name = "mp_skirmish_map_001a",
        formationPosition = new Vec2(200f, 200f),
        formationDirection = new Vec2(1f, 0.0f)
      };
      EnhancedFreeBattleConfig.SceneInfo sceneInfo1 = new EnhancedFreeBattleConfig.SceneInfo();
      sceneInfo1.name = "mp_tdm_map_001";
      sceneInfo1.formationPosition = new Vec2(385f, 570f);
      Vec2 vec2_1 = new Vec2(0.7f, -0.3f);
      sceneInfo1.formationDirection = ((Vec2) ref vec2_1).Normalized();
      sceneInfoArray1[1] = sceneInfo1;
      EnhancedFreeBattleConfig.SceneInfo sceneInfo2 = new EnhancedFreeBattleConfig.SceneInfo();
      sceneInfo2.name = "mp_tdm_map_001_spring";
      sceneInfo2.formationPosition = new Vec2(385f, 570f);
      Vec2 vec2_2 = new Vec2(0.7f, -0.3f);
      sceneInfo2.formationDirection = ((Vec2) ref vec2_2).Normalized();
      sceneInfoArray1[2] = sceneInfo2;
      EnhancedFreeBattleConfig.SceneInfo sceneInfo3 = new EnhancedFreeBattleConfig.SceneInfo();
      sceneInfo3.name = "mp_tdm_map_004";
      sceneInfo3.formationPosition = new Vec2(300f, 325f);
      Vec2 vec2_3 = new Vec2(-1f, 0.0f);
      sceneInfo3.formationDirection = ((Vec2) ref vec2_3).Normalized();
      sceneInfoArray1[3] = sceneInfo3;
      sceneInfoArray1[4] = new EnhancedFreeBattleConfig.SceneInfo()
      {
        name = "mp_duel_001",
        formationPosition = new Vec2(567f, 600f),
        formationDirection = new Vec2(0.0f, 10f)
      };
      sceneInfoArray1[5] = new EnhancedFreeBattleConfig.SceneInfo()
      {
        name = "mp_duel_001_winter",
        formationPosition = new Vec2(567f, 600f),
        formationDirection = new Vec2(0.0f, 1f)
      };
      sceneInfoArray1[6] = new EnhancedFreeBattleConfig.SceneInfo()
      {
        name = "mp_ruins_2",
        formationPosition = new Vec2(514f, 470f),
        formationDirection = new Vec2(1f, 0.0f)
      };
      sceneInfoArray1[7] = new EnhancedFreeBattleConfig.SceneInfo()
      {
        name = "mp_sergeant_map_001",
        formationPosition = new Vec2(250f, 500f),
        formationDirection = new Vec2(1f, 0.0f)
      };
      sceneInfoArray1[8] = new EnhancedFreeBattleConfig.SceneInfo()
      {
        name = "mp_sergeant_map_007",
        formationPosition = new Vec2(330f, 439f),
        formationDirection = new Vec2(1f, 0.0f)
      };
      sceneInfoArray1[9] = new EnhancedFreeBattleConfig.SceneInfo()
      {
        name = "mp_sergeant_map_008",
        formationPosition = new Vec2(471f, 505f),
        formationDirection = new Vec2(1f, 0.0f)
      };
      sceneInfoArray1[10] = new EnhancedFreeBattleConfig.SceneInfo()
      {
        name = "mp_sergeant_map_009",
        formationPosition = new Vec2(530f, 503f),
        formationDirection = new Vec2(0.0f, 1f)
      };
      sceneInfoArray1[11] = new EnhancedFreeBattleConfig.SceneInfo()
      {
        name = "mp_sergeant_map_010",
        formationPosition = new Vec2(391f, 376f),
        formationDirection = new Vec2(0.0f, 1f)
      };
      sceneInfoArray1[12] = new EnhancedFreeBattleConfig.SceneInfo()
      {
        name = "mp_sergeant_map_011",
        formationPosition = new Vec2(485f, 364f),
        formationDirection = new Vec2(0.4f, 0.6f)
      };
      sceneInfoArray1[13] = new EnhancedFreeBattleConfig.SceneInfo()
      {
        name = "mp_sergeant_map_011s",
        formationPosition = new Vec2(485f, 364f),
        formationDirection = new Vec2(0.4f, 0.6f)
      };
      EnhancedFreeBattleConfig.SceneInfo sceneInfo4 = new EnhancedFreeBattleConfig.SceneInfo();
      sceneInfo4.name = "mp_sergeant_map_012";
      sceneInfo4.formationPosition = new Vec2(493f, 312f);
      Vec2 vec2_4 = new Vec2(0.2f, 0.8f);
      sceneInfo4.formationDirection = ((Vec2) ref vec2_4).Normalized();
      sceneInfoArray1[14] = sceneInfo4;
      sceneInfoArray1[15] = new EnhancedFreeBattleConfig.SceneInfo()
      {
        name = "mp_sergeant_map_013",
        formationPosition = new Vec2(580f, 576f),
        formationDirection = new Vec2(1f, 0.0f)
      };
      sceneInfoArray1[16] = new EnhancedFreeBattleConfig.SceneInfo()
      {
        name = "mp_sergeant_map_vlandia_01",
        formationPosition = new Vec2(485f, 364f),
        formationDirection = new Vec2(0.4f, 0.6f)
      };
      EnhancedFreeBattleConfig.SceneInfo sceneInfo5 = new EnhancedFreeBattleConfig.SceneInfo();
      sceneInfo5.name = "mp_siege_map_003";
      sceneInfo5.formationPosition = new Vec2(461f, 634f);
      Vec2 vec2_5 = new Vec2(0.55f, 0.45f);
      sceneInfo5.formationDirection = ((Vec2) ref vec2_5).Normalized();
      sceneInfo5.distance = 180f;
      sceneInfoArray1[17] = sceneInfo5;
      EnhancedFreeBattleConfig.SceneInfo sceneInfo6 = new EnhancedFreeBattleConfig.SceneInfo();
      sceneInfo6.name = "mp_siege_map_004";
      sceneInfo6.formationPosition = new Vec2(502f, 472f);
      Vec2 vec2_6 = new Vec2(0.23f, 0.77f);
      sceneInfo6.formationDirection = ((Vec2) ref vec2_6).Normalized();
      sceneInfo6.distance = 200f;
      sceneInfoArray1[18] = sceneInfo6;
      EnhancedFreeBattleConfig.SceneInfo sceneInfo7 = new EnhancedFreeBattleConfig.SceneInfo();
      sceneInfo7.name = "mp_siege_map_004_bat";
      sceneInfo7.formationPosition = new Vec2(502f, 472f);
      Vec2 vec2_7 = new Vec2(0.23f, 0.77f);
      sceneInfo7.formationDirection = ((Vec2) ref vec2_7).Normalized();
      sceneInfo7.distance = 200f;
      sceneInfoArray1[19] = sceneInfo7;
      EnhancedFreeBattleConfig.SceneInfo sceneInfo8 = new EnhancedFreeBattleConfig.SceneInfo();
      sceneInfo8.name = "mp_siege_map_004_rs";
      sceneInfo8.formationPosition = new Vec2(502f, 472f);
      Vec2 vec2_8 = new Vec2(0.23f, 0.77f);
      sceneInfo8.formationDirection = ((Vec2) ref vec2_8).Normalized();
      sceneInfo8.distance = 200f;
      sceneInfoArray1[20] = sceneInfo8;
      sceneInfoArray1[21] = new EnhancedFreeBattleConfig.SceneInfo()
      {
        name = "mp_siege_map_005",
        formationPosition = new Vec2(424f, 320f),
        formationDirection = new Vec2(0.0f, 1f),
        distance = 220f
      };
      sceneInfoArray1[22] = new EnhancedFreeBattleConfig.SceneInfo()
      {
        name = "mp_siege_map_007_battania",
        formationPosition = new Vec2(614f, 612f),
        formationDirection = new Vec2(0.65f, 0.35f),
        distance = 140f
      };
      EnhancedFreeBattleConfig.SceneInfo sceneInfo9 = new EnhancedFreeBattleConfig.SceneInfo();
      sceneInfo9.name = "mp_skirmish_map_002_winter";
      sceneInfo9.formationPosition = new Vec2(415f, 490f);
      Vec2 vec2_9 = new Vec2(0.3f, 0.7f);
      sceneInfo9.formationDirection = ((Vec2) ref vec2_9).Normalized();
      sceneInfo9.soldiersPerRow = 10;
      sceneInfoArray1[23] = sceneInfo9;
      EnhancedFreeBattleConfig.SceneInfo sceneInfo10 = new EnhancedFreeBattleConfig.SceneInfo();
      sceneInfo10.name = "mp_skirmish_map_002f";
      sceneInfo10.formationPosition = new Vec2(415f, 490f);
      Vec2 vec2_10 = new Vec2(0.3f, 0.7f);
      sceneInfo10.formationDirection = ((Vec2) ref vec2_10).Normalized();
      sceneInfo10.soldiersPerRow = 10;
      sceneInfoArray1[24] = sceneInfo10;
      EnhancedFreeBattleConfig.SceneInfo sceneInfo11 = new EnhancedFreeBattleConfig.SceneInfo();
      sceneInfo11.name = "mp_skirmish_map_003_skinc";
      sceneInfo11.formationPosition = new Vec2(650f, 675f);
      Vec2 vec2_11 = new Vec2(-0.7f, 0.3f);
      sceneInfo11.formationDirection = ((Vec2) ref vec2_11).Normalized();
      sceneInfo11.soldiersPerRow = 10;
      sceneInfoArray1[25] = sceneInfo11;
      sceneInfoArray1[26] = new EnhancedFreeBattleConfig.SceneInfo()
      {
        name = "mp_skirmish_map_004",
        formationPosition = new Vec2(320f, 288f),
        formationDirection = new Vec2(0.0f, 1f),
        soldiersPerRow = 10
      };
      sceneInfoArray1[27] = new EnhancedFreeBattleConfig.SceneInfo()
      {
        name = "mp_skirmish_map_005",
        formationPosition = new Vec2(477f, 496f),
        formationDirection = new Vec2(1f, 0.0f),
        soldiersPerRow = 10
      };
      sceneInfoArray1[28] = new EnhancedFreeBattleConfig.SceneInfo()
      {
        name = "mp_skirmish_map_006",
        formationPosition = new Vec2(480f, 561f),
        formationDirection = new Vec2(1f, 0.0f),
        skyBrightness = 0.0f
      };
      sceneInfoArray1[29] = new EnhancedFreeBattleConfig.SceneInfo()
      {
        name = "mp_skirmish_map_006_nowater",
        formationPosition = new Vec2(480f, 561f),
        formationDirection = new Vec2(1f, 0.0f),
        skyBrightness = 0.0f
      };
      sceneInfoArray1[30] = new EnhancedFreeBattleConfig.SceneInfo()
      {
        name = "mp_skirmish_map_007",
        formationPosition = new Vec2(190f, 154f),
        formationDirection = new Vec2(0.0f, 1f)
      };
      sceneInfoArray1[31] = new EnhancedFreeBattleConfig.SceneInfo()
      {
        name = "mp_skirmish_map_007_winter",
        formationPosition = new Vec2(190f, 154f),
        formationDirection = new Vec2(0.0f, 1f)
      };
      sceneInfoArray1[32] = new EnhancedFreeBattleConfig.SceneInfo()
      {
        name = "mp_skirmish_map_008_skin",
        formationPosition = new Vec2(495f, 500f),
        formationDirection = new Vec2(1f, 0.0f)
      };
      sceneInfoArray1[33] = new EnhancedFreeBattleConfig.SceneInfo()
      {
        name = "mp_skirmish_map_013",
        formationPosition = new Vec2(250f, 500f),
        formationDirection = new Vec2(1f, 0.0f)
      };
      sceneInfoArray1[34] = new EnhancedFreeBattleConfig.SceneInfo()
      {
        name = "mp_skirmish_map_battania_02",
        formationPosition = new Vec2(360f, 186f),
        formationDirection = new Vec2(0.6f, -0.4f),
        soldiersPerRow = 10
      };
      sceneInfoArray1[35] = new EnhancedFreeBattleConfig.SceneInfo()
      {
        name = "mp_skirmish_map_battania_03",
        formationPosition = new Vec2(360f, 186f),
        formationDirection = new Vec2(0.6f, -0.4f),
        soldiersPerRow = 10
      };
      EnhancedFreeBattleConfig.SceneInfo[] sceneInfoArray2 = sceneInfoArray1;
      int num = 10;
      EnhancedFreeBattleConfig freeBattleConfig = new EnhancedFreeBattleConfig();
      freeBattleConfig.ConfigVersion = EnhancedFreeBattleConfig.BinaryVersion.ToString(2);
      freeBattleConfig.sceneList = sceneInfoArray2;
      freeBattleConfig.sceneIndex = num;
      freeBattleConfig.playerClass = new ClassInfo()
      {
        classStringId = "mp_heavy_cavalry_vlandia",
        selectedFirstPerk = 2,
        selectedSecondPerk = 2
      };
      freeBattleConfig.SpawnPlayer = true;
      freeBattleConfig.enemyClass = new ClassInfo()
      {
        classStringId = "mp_heavy_cavalry_vlandia",
        selectedFirstPerk = 0,
        selectedSecondPerk = 0
      };
      freeBattleConfig.SpawnEnemyCommander = true;
      freeBattleConfig.playerTroops = new ClassInfo[8]
      {
        new ClassInfo()
        {
          classStringId = "mp_heavy_infantry_vlandia",
          selectedFirstPerk = 2,
          selectedSecondPerk = 2,
          troopCount = 50
        },
        new ClassInfo()
        {
          classStringId = "mp_light_ranged_vlandia",
          selectedFirstPerk = 0,
          selectedSecondPerk = 0,
          troopCount = 20
        },
        new ClassInfo()
        {
          classStringId = "mp_heavy_ranged_vlandia",
          selectedFirstPerk = 0,
          selectedSecondPerk = 1,
          troopCount = 20
        },
        new ClassInfo()
        {
          classStringId = "mp_shock_infantry_vlandia",
          selectedFirstPerk = 1,
          selectedSecondPerk = 1,
          troopCount = 25
        },
        new ClassInfo()
        {
          classStringId = "mp_shock_infantry_vlandia",
          selectedFirstPerk = 1,
          selectedSecondPerk = 1,
          troopCount = 25
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
          classStringId = "mp_heavy_cavalry_vlandia",
          selectedFirstPerk = 2,
          selectedSecondPerk = 2,
          troopCount = 20
        },
        new ClassInfo()
        {
          classStringId = "mp_heavy_cavalry_vlandia",
          selectedFirstPerk = 2,
          selectedSecondPerk = 2,
          troopCount = 20
        }
      };
      freeBattleConfig.enemyTroops = new ClassInfo[8]
      {
        new ClassInfo()
        {
          classStringId = "mp_light_infantry_battania",
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
          classStringId = "mp_light_infantry_battania",
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
          classStringId = "mp_shock_infantry_battania",
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
      freeBattleConfig.soldierXInterval = 2.5f;
      freeBattleConfig.soldierYInterval = 2f;
      freeBattleConfig.hasBoundary = true;
      freeBattleConfig.disableDeath = false;
      freeBattleConfig.changeCombatAI = true;
      freeBattleConfig.combatAI = 100;
      return freeBattleConfig;
    }

    public static EnhancedFreeBattleConfig Get()
    {
      if (EnhancedFreeBattleConfig._instance == null)
      {
        EnhancedFreeBattleConfig._instance = new EnhancedFreeBattleConfig();
        EnhancedFreeBattleConfig._instance.SyncWithSave();
      }
      return EnhancedFreeBattleConfig._instance;
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
          new XmlSerializer(typeof (EnhancedFreeBattleConfig)).Serialize(textWriter, (object) this);
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
          this.CopyFrom((EnhancedFreeBattleConfig) new XmlSerializer(typeof (EnhancedFreeBattleConfig)).Deserialize(textReader));
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
      EnhancedFreeBattleConfig other = EnhancedFreeBattleConfig.CreateDefault();
      if (!other.Deserialize())
        return;
      this.CopyFrom(other);
    }

    public override void ResetToDefault()
    {
      this.CopyFrom(EnhancedFreeBattleConfig.CreateDefault());
    }

    protected override void CopyFrom(EnhancedFreeBattleConfig other)
    {
      base.CopyFrom(other);
      if (other.sceneList != null)
        this.sceneList = other.sceneList;
      this.sceneIndex = other.sceneIndex;
      this.soldierXInterval = other.soldierXInterval;
      this.soldierYInterval = other.soldierYInterval;
      this.makeGruntVoice = other.makeGruntVoice;
      this.hasBoundary = other.hasBoundary;
    }

    protected override string SaveName
    {
      get
      {
        return BattleConfigBase.SavePath + "EnhancedFreeBattleConfig.xml";
      }
    }

    protected override string[] OldNames { get; } = new string[2]
    {
      BattleConfigBase.SavePath + "Param.xml",
      BattleConfigBase.SavePath + "EnhancedTestBattleConfig.xml"
    };

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
