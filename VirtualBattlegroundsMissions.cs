// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.VirtualBattlegroundsMissions
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace VirtualBattlegrounds
{
  public class VirtualBattlegroundsMissions
  {
    public static Mission OpenFreeBattleConfigMission()
    {
      MissionInitializerRecord initializerRecord;
      ((MissionInitializerRecord) ref initializerRecord).\u002Ector("scn_character_creation_scene");
      initializerRecord.DoNotUseLoadingScreen = (__Null) 1;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      return MissionState.OpenNew("EnhancedFreeBattleConfig", initializerRecord, VirtualBattlegroundsMissions.\u003C\u003Ec.\u003C\u003E9__0_0 ?? (VirtualBattlegroundsMissions.\u003C\u003Ec.\u003C\u003E9__0_0 = new InitializeMissionBehvaioursDelegate((object) VirtualBattlegroundsMissions.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003COpenFreeBattleConfigMission\u003Eb__0_0))), true, true, true);
    }

    public static Mission OpenFreeBattleMission(EnhancedFreeBattleConfig config)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      VirtualBattlegroundsMissions.\u003C\u003Ec__DisplayClass1_0 cDisplayClass10 = new VirtualBattlegroundsMissions.\u003C\u003Ec__DisplayClass1_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass10.config = config;
      // ISSUE: reference to a compiler-generated field
      BattleSideEnum battleSideEnum = cDisplayClass10.config.isPlayerAttacker ? (BattleSideEnum) 1 : (BattleSideEnum) 0;
      // ISSUE: reference to a compiler-generated field
      BasicCultureObject playerTeamCulture = cDisplayClass10.config.GetPlayerTeamCulture();
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      CustomBattleCombatant customBattleCombatant1 = new CustomBattleCombatant(playerTeamCulture.get_Name(), playerTeamCulture, new Banner(playerTeamCulture.get_BannerKey(), Utility.BackgroundColor(playerTeamCulture, cDisplayClass10.config.isPlayerAttacker), Utility.ForegroundColor(playerTeamCulture, cDisplayClass10.config.isPlayerAttacker)));
      customBattleCombatant1.set_Side(battleSideEnum);
      CustomBattleCombatant combatant1 = customBattleCombatant1;
      // ISSUE: reference to a compiler-generated field
      Utility.AddCharacter(combatant1, cDisplayClass10.config.playerClass, true, Utility.CommanderFormationClass(), true);
      // ISSUE: reference to a compiler-generated field
      Utility.AddCharacter(combatant1, cDisplayClass10.config.playerTroops[0], false, (FormationClass) 0, false);
      // ISSUE: reference to a compiler-generated field
      Utility.AddCharacter(combatant1, cDisplayClass10.config.playerTroops[1], false, (FormationClass) 1, false);
      // ISSUE: reference to a compiler-generated field
      Utility.AddCharacter(combatant1, cDisplayClass10.config.playerTroops[2], false, (FormationClass) 2, false);
      // ISSUE: reference to a compiler-generated field
      Utility.AddCharacter(combatant1, cDisplayClass10.config.playerTroops[3], false, (FormationClass) 3, false);
      // ISSUE: reference to a compiler-generated field
      Utility.AddCharacter(combatant1, cDisplayClass10.config.playerTroops[4], false, (FormationClass) 4, false);
      // ISSUE: reference to a compiler-generated field
      Utility.AddCharacter(combatant1, cDisplayClass10.config.playerTroops[5], false, (FormationClass) 5, false);
      // ISSUE: reference to a compiler-generated field
      Utility.AddCharacter(combatant1, cDisplayClass10.config.playerTroops[6], false, (FormationClass) 6, false);
      // ISSUE: reference to a compiler-generated field
      Utility.AddCharacter(combatant1, cDisplayClass10.config.playerTroops[7], false, (FormationClass) 7, false);
      // ISSUE: reference to a compiler-generated field
      BasicCultureObject enemyTeamCulture = cDisplayClass10.config.GetEnemyTeamCulture();
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      CustomBattleCombatant customBattleCombatant2 = new CustomBattleCombatant(enemyTeamCulture.get_Name(), enemyTeamCulture, new Banner(enemyTeamCulture.get_BannerKey(), Utility.BackgroundColor(enemyTeamCulture, !cDisplayClass10.config.isPlayerAttacker), Utility.ForegroundColor(enemyTeamCulture, !cDisplayClass10.config.isPlayerAttacker)));
      customBattleCombatant2.set_Side(Extensions.GetOppositeSide(battleSideEnum));
      CustomBattleCombatant combatant2 = customBattleCombatant2;
      // ISSUE: reference to a compiler-generated field
      Utility.AddCharacter(combatant2, cDisplayClass10.config.enemyClass, true, Utility.CommanderFormationClass(), false);
      // ISSUE: reference to a compiler-generated field
      Utility.AddCharacter(combatant2, cDisplayClass10.config.enemyTroops[0], false, (FormationClass) 0, false);
      // ISSUE: reference to a compiler-generated field
      Utility.AddCharacter(combatant2, cDisplayClass10.config.enemyTroops[1], false, (FormationClass) 1, false);
      // ISSUE: reference to a compiler-generated field
      Utility.AddCharacter(combatant2, cDisplayClass10.config.enemyTroops[2], false, (FormationClass) 2, false);
      // ISSUE: reference to a compiler-generated field
      Utility.AddCharacter(combatant2, cDisplayClass10.config.enemyTroops[3], false, (FormationClass) 3, false);
      // ISSUE: reference to a compiler-generated field
      Utility.AddCharacter(combatant2, cDisplayClass10.config.enemyTroops[4], false, (FormationClass) 4, false);
      // ISSUE: reference to a compiler-generated field
      Utility.AddCharacter(combatant2, cDisplayClass10.config.enemyTroops[5], false, (FormationClass) 5, false);
      // ISSUE: reference to a compiler-generated field
      Utility.AddCharacter(combatant2, cDisplayClass10.config.enemyTroops[6], false, (FormationClass) 6, false);
      // ISSUE: reference to a compiler-generated field
      Utility.AddCharacter(combatant2, cDisplayClass10.config.enemyTroops[7], false, (FormationClass) 7, false);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      return MissionState.OpenNew("EnhancedFreeBattle", new MissionInitializerRecord(cDisplayClass10.config.SceneName), new InitializeMissionBehvaioursDelegate((object) cDisplayClass10, __methodptr(\u003COpenFreeBattleMission\u003Eb__0)), true, true, true);
    }

    public static Mission OpenCustomBattleConfigMission()
    {
      MissionInitializerRecord initializerRecord;
      ((MissionInitializerRecord) ref initializerRecord).\u002Ector("scn_character_creation_scene");
      initializerRecord.DoNotUseLoadingScreen = (__Null) 1;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      return MissionState.OpenNew("EnhancedCustomBattleConfig", initializerRecord, VirtualBattlegroundsMissions.\u003C\u003Ec.\u003C\u003E9__2_0 ?? (VirtualBattlegroundsMissions.\u003C\u003Ec.\u003C\u003E9__2_0 = new InitializeMissionBehvaioursDelegate((object) VirtualBattlegroundsMissions.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003COpenCustomBattleConfigMission\u003Eb__2_0))), true, true, true);
    }

    public static Mission OpenCustomBattleMission(EnhancedCustomBattleConfig config)
    {
      BattleSideEnum battleSideEnum = config.isPlayerAttacker ? (BattleSideEnum) 1 : (BattleSideEnum) 0;
      BasicCultureObject playerTeamCulture = config.GetPlayerTeamCulture();
      CustomBattleCombatant customBattleCombatant1 = new CustomBattleCombatant(playerTeamCulture.get_Name(), playerTeamCulture, new Banner(playerTeamCulture.get_BannerKey(), Utility.BackgroundColor(playerTeamCulture, config.isPlayerAttacker), Utility.ForegroundColor(playerTeamCulture, config.isPlayerAttacker)));
      customBattleCombatant1.set_Side(battleSideEnum);
      CustomBattleCombatant customBattleCombatant2 = customBattleCombatant1;
      Utility.AddCharacter(customBattleCombatant2, config.playerClass, true, Utility.CommanderFormationClass(), true);
      Utility.AddCharacter(customBattleCombatant2, config.playerTroops[0], false, (FormationClass) 0, false);
      Utility.AddCharacter(customBattleCombatant2, config.playerTroops[1], false, (FormationClass) 1, false);
      Utility.AddCharacter(customBattleCombatant2, config.playerTroops[2], false, (FormationClass) 2, false);
      Utility.AddCharacter(customBattleCombatant2, config.playerTroops[3], false, (FormationClass) 3, false);
      Utility.AddCharacter(customBattleCombatant2, config.playerTroops[4], false, (FormationClass) 4, false);
      Utility.AddCharacter(customBattleCombatant2, config.playerTroops[5], false, (FormationClass) 5, false);
      Utility.AddCharacter(customBattleCombatant2, config.playerTroops[6], false, (FormationClass) 6, false);
      Utility.AddCharacter(customBattleCombatant2, config.playerTroops[7], false, (FormationClass) 7, false);
      BasicCultureObject enemyTeamCulture = config.GetEnemyTeamCulture();
      CustomBattleCombatant customBattleCombatant3 = new CustomBattleCombatant(enemyTeamCulture.get_Name(), enemyTeamCulture, new Banner(enemyTeamCulture.get_BannerKey(), Utility.BackgroundColor(enemyTeamCulture, !config.isPlayerAttacker), Utility.ForegroundColor(enemyTeamCulture, !config.isPlayerAttacker)));
      customBattleCombatant3.set_Side(Extensions.GetOppositeSide(battleSideEnum));
      CustomBattleCombatant customBattleCombatant4 = customBattleCombatant3;
      Utility.AddCharacter(customBattleCombatant4, config.enemyClass, true, Utility.CommanderFormationClass(), false);
      Utility.AddCharacter(customBattleCombatant4, config.enemyTroops[0], false, (FormationClass) 0, false);
      Utility.AddCharacter(customBattleCombatant4, config.enemyTroops[1], false, (FormationClass) 1, false);
      Utility.AddCharacter(customBattleCombatant4, config.enemyTroops[2], false, (FormationClass) 2, false);
      Utility.AddCharacter(customBattleCombatant4, config.enemyTroops[3], false, (FormationClass) 3, false);
      Utility.AddCharacter(customBattleCombatant4, config.enemyTroops[4], false, (FormationClass) 4, false);
      Utility.AddCharacter(customBattleCombatant4, config.enemyTroops[5], false, (FormationClass) 5, false);
      Utility.AddCharacter(customBattleCombatant4, config.enemyTroops[6], false, (FormationClass) 6, false);
      Utility.AddCharacter(customBattleCombatant4, config.enemyTroops[7], false, (FormationClass) 7, false);
      return VirtualBattlegroundsMissions.OpenCustomBattleMission(config, customBattleCombatant2, customBattleCombatant4, true, (BasicCharacterObject) null, "", "", 6f);
    }

    private static Mission OpenCustomBattleMission(
      EnhancedCustomBattleConfig config,
      CustomBattleCombatant playerParty,
      CustomBattleCombatant enemyParty,
      bool isPlayerGeneral,
      BasicCharacterObject playerSideGeneralCharacter,
      string sceneLevels = "",
      string seasonString = "",
      float timeOfDay = 6f)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      VirtualBattlegroundsMissions.\u003C\u003Ec__DisplayClass4_0 cDisplayClass40 = new VirtualBattlegroundsMissions.\u003C\u003Ec__DisplayClass4_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass40.config = config;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass40.playerParty = playerParty;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass40.isPlayerGeneral = isPlayerGeneral;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass40.playerSideGeneralCharacter = playerSideGeneralCharacter;
      // ISSUE: reference to a compiler-generated field
      BattleSideEnum side = cDisplayClass40.playerParty.get_Side();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass40.isPlayerAttacker = side == 1;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass40.troopSuppliers = new IMissionTroopSupplier[2];
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass40.playerTroopSupplier = new EnhancedTroopSupplier(cDisplayClass40.playerParty);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass40.troopSuppliers[cDisplayClass40.playerParty.get_Side()] = (IMissionTroopSupplier) cDisplayClass40.playerTroopSupplier;
      EnhancedTroopSupplier enhancedTroopSupplier = new EnhancedTroopSupplier(enemyParty);
      // ISSUE: reference to a compiler-generated field
      cDisplayClass40.troopSuppliers[enemyParty.get_Side()] = (IMissionTroopSupplier) enhancedTroopSupplier;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass40.isPlayerSergeant = !cDisplayClass40.isPlayerGeneral;
      AtmosphereInfo atmosphereInfo;
      if (!string.IsNullOrEmpty(seasonString))
        atmosphereInfo = new AtmosphereInfo()
        {
          AtmosphereName = (__Null) ""
        };
      else
        atmosphereInfo = (AtmosphereInfo) null;
      if (atmosphereInfo != null)
      {
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        (^(TimeInformation&) ref atmosphereInfo.TimeInfo).TimeOfDay = (__Null) (double) timeOfDay;
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass40.player = Utility.ApplyPerks(cDisplayClass40.config.playerClass, true);
      // ISSUE: reference to a compiler-generated field
      cDisplayClass40.player.set_CurrentFormationClass(Utility.CommanderFormationClass());
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass40.enemyCharacter = cDisplayClass40.config.EnemyHeroClass.get_HeroCharacter();
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass40.defenderParty = !cDisplayClass40.isPlayerAttacker ? cDisplayClass40.playerParty : enemyParty;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass40.attackerParty = cDisplayClass40.isPlayerAttacker ? cDisplayClass40.playerParty : enemyParty;
      MissionInitializerRecord initializerRecord;
      // ISSUE: reference to a compiler-generated field
      ((MissionInitializerRecord) ref initializerRecord).\u002Ector(cDisplayClass40.config.SceneName);
      initializerRecord.DoNotUseLoadingScreen = (__Null) 0;
      initializerRecord.AtmosphereOnCampaign = (__Null) atmosphereInfo;
      initializerRecord.SceneLevels = (__Null) sceneLevels;
      initializerRecord.TimeOfDay = (__Null) (double) timeOfDay;
      // ISSUE: method pointer
      return MissionState.OpenNew("EnhancedCustomBattle", initializerRecord, new InitializeMissionBehvaioursDelegate((object) cDisplayClass40, __methodptr(\u003COpenCustomBattleMission\u003Eb__0)), true, true, true);
    }

    private static Type GetSiegeWeaponType(SiegeEngineType siegeWeaponType)
    {
      if (siegeWeaponType == DefaultSiegeEngineTypes.get_Ladder())
        return typeof (SiegeLadder);
      if (siegeWeaponType == DefaultSiegeEngineTypes.get_Ballista())
        return typeof (Ballista);
      if (siegeWeaponType == DefaultSiegeEngineTypes.get_FireBallista())
        return typeof (FireBallista);
      if (siegeWeaponType == DefaultSiegeEngineTypes.get_Ram() || siegeWeaponType == DefaultSiegeEngineTypes.get_ImprovedRam())
        return typeof (BatteringRam);
      if (siegeWeaponType == DefaultSiegeEngineTypes.get_SiegeTower())
        return typeof (SiegeTower);
      if (siegeWeaponType == DefaultSiegeEngineTypes.get_Onager() || siegeWeaponType == DefaultSiegeEngineTypes.get_Catapult())
        return typeof (Mangonel);
      return siegeWeaponType == DefaultSiegeEngineTypes.get_FireOnager() || siegeWeaponType == DefaultSiegeEngineTypes.get_FireCatapult() ? typeof (FireMangonel) : (siegeWeaponType == DefaultSiegeEngineTypes.get_Trebuchet() || siegeWeaponType == DefaultSiegeEngineTypes.get_Bricole() ? typeof (Trebuchet) : (Type) null);
    }

    private static Dictionary<Type, int> GetSiegeWeaponTypes(
      Dictionary<SiegeEngineType, int> values)
    {
      Dictionary<Type, int> dictionary = new Dictionary<Type, int>();
      using (Dictionary<SiegeEngineType, int>.Enumerator enumerator = values.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          KeyValuePair<SiegeEngineType, int> current = enumerator.Current;
          dictionary.Add(VirtualBattlegroundsMissions.GetSiegeWeaponType(current.Key), current.Value);
        }
      }
      return dictionary;
    }

    public static Mission OpenSiegeBattleConfigMission()
    {
      MissionInitializerRecord initializerRecord;
      ((MissionInitializerRecord) ref initializerRecord).\u002Ector("scn_character_creation_scene");
      initializerRecord.DoNotUseLoadingScreen = (__Null) 1;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      return MissionState.OpenNew("EnhancedSiegeBattleConfig", initializerRecord, VirtualBattlegroundsMissions.\u003C\u003Ec.\u003C\u003E9__7_0 ?? (VirtualBattlegroundsMissions.\u003C\u003Ec.\u003C\u003E9__7_0 = new InitializeMissionBehvaioursDelegate((object) VirtualBattlegroundsMissions.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003COpenSiegeBattleConfigMission\u003Eb__7_0))), true, true, true);
    }

    public static Mission OpenSiegeBattleMission(EnhancedSiegeBattleConfig config)
    {
      BattleSideEnum battleSideEnum = config.isPlayerAttacker ? (BattleSideEnum) 1 : (BattleSideEnum) 0;
      BasicCultureObject playerTeamCulture = config.GetPlayerTeamCulture();
      CustomBattleCombatant customBattleCombatant1 = new CustomBattleCombatant(playerTeamCulture.get_Name(), playerTeamCulture, new Banner(playerTeamCulture.get_BannerKey(), Utility.BackgroundColor(playerTeamCulture, config.isPlayerAttacker), Utility.ForegroundColor(playerTeamCulture, config.isPlayerAttacker)));
      customBattleCombatant1.set_Side(battleSideEnum);
      CustomBattleCombatant customBattleCombatant2 = customBattleCombatant1;
      Utility.AddCharacter(customBattleCombatant2, config.playerClass, true, Utility.CommanderFormationClass(), true);
      Utility.AddCharacter(customBattleCombatant2, config.playerTroops[0], false, (FormationClass) 0, false);
      Utility.AddCharacter(customBattleCombatant2, config.playerTroops[1], false, (FormationClass) 1, false);
      Utility.AddCharacter(customBattleCombatant2, config.playerTroops[2], false, (FormationClass) 2, false);
      BasicCultureObject enemyTeamCulture = config.GetEnemyTeamCulture();
      CustomBattleCombatant customBattleCombatant3 = new CustomBattleCombatant(enemyTeamCulture.get_Name(), enemyTeamCulture, new Banner(enemyTeamCulture.get_BannerKey(), Utility.BackgroundColor(enemyTeamCulture, !config.isPlayerAttacker), Utility.ForegroundColor(enemyTeamCulture, !config.isPlayerAttacker)));
      customBattleCombatant3.set_Side(Extensions.GetOppositeSide(battleSideEnum));
      CustomBattleCombatant customBattleCombatant4 = customBattleCombatant3;
      Utility.AddCharacter(customBattleCombatant4, config.enemyClass, true, Utility.CommanderFormationClass(), false);
      Utility.AddCharacter(customBattleCombatant4, config.enemyTroops[0], false, (FormationClass) 0, false);
      Utility.AddCharacter(customBattleCombatant4, config.enemyTroops[1], false, (FormationClass) 1, false);
      Utility.AddCharacter(customBattleCombatant4, config.enemyTroops[2], false, (FormationClass) 2, false);
      return VirtualBattlegroundsMissions.OpenSiegeBattleMission(config, customBattleCombatant2, customBattleCombatant4, true, new float[1]
      {
        1f
      }, false, new Dictionary<SiegeEngineType, int>(), new Dictionary<SiegeEngineType, int>(), (config.isPlayerAttacker ? 1 : 0) != 0, 0, "", false, false, 6f);
    }

    public static Mission OpenSiegeBattleMission(
      EnhancedSiegeBattleConfig config,
      CustomBattleCombatant playerParty,
      CustomBattleCombatant enemyParty,
      bool isPlayerGeneral,
      float[] wallHitPointPercentages,
      bool hasAnySiegeTower,
      Dictionary<SiegeEngineType, int> siegeWeaponsCountOfAttackers,
      Dictionary<SiegeEngineType, int> siegeWeaponsCountOfDefenders,
      bool isPlayerAttacker,
      int sceneUpgradeLevel = 0,
      string seasonString = "",
      bool isSallyOut = false,
      bool isReliefForceAttack = false,
      float timeOfDay = 6f)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      VirtualBattlegroundsMissions.\u003C\u003Ec__DisplayClass9_0 cDisplayClass90 = new VirtualBattlegroundsMissions.\u003C\u003Ec__DisplayClass9_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass90.config = config;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass90.playerParty = playerParty;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass90.isSallyOut = isSallyOut;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass90.isReliefForceAttack = isReliefForceAttack;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass90.wallHitPointPercentages = wallHitPointPercentages;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass90.hasAnySiegeTower = hasAnySiegeTower;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass90.siegeWeaponsCountOfAttackers = siegeWeaponsCountOfAttackers;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass90.siegeWeaponsCountOfDefenders = siegeWeaponsCountOfDefenders;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass90.isPlayerAttacker = isPlayerAttacker;
      string str1;
      switch (sceneUpgradeLevel)
      {
        case 1:
          str1 = "level_1";
          break;
        case 2:
          str1 = "level_2";
          break;
        default:
          str1 = "level_3";
          break;
      }
      // ISSUE: reference to a compiler-generated field
      string str2 = str1 + (cDisplayClass90.isSallyOut ? " sally" : " siege");
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass90.playerSide = cDisplayClass90.playerParty.get_Side();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass90.troopSuppliers = new IMissionTroopSupplier[2];
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass90.playerTroopSupplier = new EnhancedTroopSupplier(cDisplayClass90.playerParty);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass90.troopSuppliers[cDisplayClass90.playerParty.get_Side()] = (IMissionTroopSupplier) cDisplayClass90.playerTroopSupplier;
      EnhancedTroopSupplier enhancedTroopSupplier = new EnhancedTroopSupplier(enemyParty);
      // ISSUE: reference to a compiler-generated field
      cDisplayClass90.troopSuppliers[enemyParty.get_Side()] = (IMissionTroopSupplier) enhancedTroopSupplier;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass90.player = Utility.ApplyPerks(cDisplayClass90.config.playerClass, true);
      // ISSUE: reference to a compiler-generated field
      cDisplayClass90.player.set_CurrentFormationClass(Utility.CommanderFormationClass());
      // ISSUE: reference to a compiler-generated field
      cDisplayClass90.isPlayerSergeant = !isPlayerGeneral;
      AtmosphereInfo atmosphereInfo;
      if (!string.IsNullOrEmpty(seasonString))
        atmosphereInfo = new AtmosphereInfo()
        {
          AtmosphereName = (__Null) ""
        };
      else
        atmosphereInfo = (AtmosphereInfo) null;
      if (atmosphereInfo != null)
      {
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        (^(TimeInformation&) ref atmosphereInfo.TimeInfo).TimeOfDay = (__Null) (double) timeOfDay;
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass90.defenderParty = !cDisplayClass90.isPlayerAttacker ? cDisplayClass90.playerParty : enemyParty;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass90.attackerParty = cDisplayClass90.isPlayerAttacker ? cDisplayClass90.playerParty : enemyParty;
      MissionInitializerRecord initializerRecord;
      // ISSUE: reference to a compiler-generated field
      ((MissionInitializerRecord) ref initializerRecord).\u002Ector(cDisplayClass90.config.SceneName);
      initializerRecord.DoNotUseLoadingScreen = (__Null) 1;
      initializerRecord.AtmosphereOnCampaign = (__Null) atmosphereInfo;
      initializerRecord.SceneLevels = (__Null) str2;
      initializerRecord.TimeOfDay = (__Null) (double) timeOfDay;
      // ISSUE: method pointer
      return MissionState.OpenNew("EnhancedSiegeBattle", initializerRecord, new InitializeMissionBehvaioursDelegate((object) cDisplayClass90, __methodptr(\u003COpenSiegeBattleMission\u003Eb__0)), true, true, true);
    }
  }
}
