// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.EnhancedCustomBattleConfig
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System;
using System.IO;
using System.Xml.Serialization;

namespace VirtualBattlegrounds
{
  public class EnhancedCustomBattleConfig : BattleConfigBase<EnhancedCustomBattleConfig>
  {
    private static EnhancedCustomBattleConfig _instance;
    public EnhancedCustomBattleConfig.SceneInfo[] sceneList;
    public int sceneIndex;

    protected static Version BinaryVersion { get; } = new Version(0, 3, 5);

    protected void UpgradeToCurrentVersion()
    {
      switch (this.ConfigVersion?.ToString())
      {
        case "1.9":
          break;
        default:
          Utility.DisplayLocalizedText("str_config_incompatible", (string) null);
          this.ResetToDefault();
          this.Serialize();
          break;
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
    public string SceneName
    {
      get
      {
        return this.sceneList[this.sceneIndex].name;
      }
    }

    public EnhancedCustomBattleConfig()
      : base(BattleType.FieldBattle)
    {
    }

    private static EnhancedCustomBattleConfig CreateDefault()
    {
      EnhancedCustomBattleConfig.SceneInfo[] sceneInfoArray = new EnhancedCustomBattleConfig.SceneInfo[11]
      {
        new EnhancedCustomBattleConfig.SceneInfo()
        {
          name = "mp_sergeant_map_001"
        },
        new EnhancedCustomBattleConfig.SceneInfo()
        {
          name = "mp_sergeant_map_005"
        },
        new EnhancedCustomBattleConfig.SceneInfo()
        {
          name = "mp_sergeant_map_007"
        },
        new EnhancedCustomBattleConfig.SceneInfo()
        {
          name = "mp_sergeant_map_008"
        },
        new EnhancedCustomBattleConfig.SceneInfo()
        {
          name = "mp_sergeant_map_009"
        },
        new EnhancedCustomBattleConfig.SceneInfo()
        {
          name = "mp_sergeant_map_010"
        },
        new EnhancedCustomBattleConfig.SceneInfo()
        {
          name = "mp_sergeant_map_011"
        },
        new EnhancedCustomBattleConfig.SceneInfo()
        {
          name = "mp_sergeant_map_011s"
        },
        new EnhancedCustomBattleConfig.SceneInfo()
        {
          name = "mp_sergeant_map_012"
        },
        new EnhancedCustomBattleConfig.SceneInfo()
        {
          name = "mp_sergeant_map_013"
        },
        new EnhancedCustomBattleConfig.SceneInfo()
        {
          name = "mp_sergeant_map_vlandia_01"
        }
      };
      int num = 0;
      EnhancedCustomBattleConfig customBattleConfig = new EnhancedCustomBattleConfig();
      customBattleConfig.ConfigVersion = EnhancedCustomBattleConfig.BinaryVersion.ToString(2);
      customBattleConfig.sceneList = sceneInfoArray;
      customBattleConfig.sceneIndex = num;
      customBattleConfig.playerClass = new ClassInfo()
      {
        classStringId = "mp_light_cavalry_vlandia",
        selectedFirstPerk = 0,
        selectedSecondPerk = 0,
        troopCount = 1
      };
      customBattleConfig.SpawnPlayer = true;
      customBattleConfig.enemyClass = new ClassInfo()
      {
        classStringId = "mp_light_cavalry_battania",
        selectedFirstPerk = 0,
        selectedSecondPerk = 0,
        troopCount = 1
      };
      customBattleConfig.SpawnEnemyCommander = true;
      customBattleConfig.playerTroops = new ClassInfo[8]
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
          classStringId = "mp_heavy_ranged_vlandia",
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
      customBattleConfig.enemyTroops = new ClassInfo[8]
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
          classStringId = "mp_heavy_ranged_battania",
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
          classStringId = "mp_heavy_ranged_battania",
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
      customBattleConfig.disableDeath = false;
      customBattleConfig.changeCombatAI = false;
      customBattleConfig.combatAI = 100;
      return customBattleConfig;
    }

    public static EnhancedCustomBattleConfig Get()
    {
      if (EnhancedCustomBattleConfig._instance == null)
      {
        EnhancedCustomBattleConfig._instance = new EnhancedCustomBattleConfig();
        EnhancedCustomBattleConfig._instance.SyncWithSave();
      }
      return EnhancedCustomBattleConfig._instance;
    }

    public override bool Validate()
    {
      return base.Validate() && this.sceneIndex >= 0 && this.sceneIndex < this.sceneList.Length;
    }

    public override bool Serialize()
    {
      try
      {
        this.EnsureSaveDirectory();
        using (TextWriter textWriter = (TextWriter) new StreamWriter(this.SaveName))
          new XmlSerializer(typeof (EnhancedCustomBattleConfig)).Serialize(textWriter, (object) this);
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
          this.CopyFrom((EnhancedCustomBattleConfig) new XmlSerializer(typeof (EnhancedCustomBattleConfig)).Deserialize(textReader));
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
      EnhancedCustomBattleConfig other = EnhancedCustomBattleConfig.CreateDefault();
      if (!other.Deserialize())
        return;
      this.CopyFrom(other);
    }

    public override void ResetToDefault()
    {
      this.CopyFrom(EnhancedCustomBattleConfig.CreateDefault());
    }

    protected override void CopyFrom(EnhancedCustomBattleConfig other)
    {
      base.CopyFrom(other);
      if (other.sceneList != null)
        this.sceneList = other.sceneList;
      this.sceneIndex = other.sceneIndex;
    }

    protected override string SaveName
    {
      get
      {
        return BattleConfigBase.SavePath + "EnhancedCustomBattleConfig.xml";
      }
    }

    protected override string[] OldNames { get; } = new string[0];

    public class SceneInfo
    {
      public float skyBrightness = -1f;
      public float rainDensity = -1f;
      public string name;
    }
  }
}
