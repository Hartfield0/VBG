// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.EnhancedMPAgentStatCalculateModel
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace VirtualBattlegrounds
{
  public class EnhancedMPAgentStatCalculateModel : AgentStatCalculateModel
  {
    private BattleConfigBase _config;

    public EnhancedMPAgentStatCalculateModel(BattleConfigBase config)
    {
      this.\u002Ector();
      this._config = config;
    }

    public virtual void InitializeAgentStats(
      Agent agent,
      Equipment spawnEquipment,
      AgentDrivenProperties agentDrivenProperties,
      AgentBuildData agentBuildData)
    {
      agentDrivenProperties.set_ArmorEncumbrance(spawnEquipment.GetTotalWeightOfArmor(agent.get_IsHuman()));
      if (!agent.get_IsHuman())
        EnhancedMPAgentStatCalculateModel.InitializeHorseAgentStats(agent, spawnEquipment, agentDrivenProperties);
      else
        agentDrivenProperties = this.InitializeHumanAgentStats(agent, agentDrivenProperties, agentBuildData);
      using (IEnumerator<DrivenPropertyBonusAgentComponent> enumerator = ((IEnumerable) agent.get_Components()).OfType<DrivenPropertyBonusAgentComponent>().GetEnumerator())
      {
        while (((IEnumerator) enumerator).MoveNext())
        {
          DrivenPropertyBonusAgentComponent current = enumerator.Current;
          if (MBMath.IsBetween((int) current.get_DrivenProperty(), 0, 56))
          {
            float num = agentDrivenProperties.GetStat(current.get_DrivenProperty()) + current.get_DrivenPropertyBonus();
            agentDrivenProperties.SetStat(current.get_DrivenProperty(), num);
          }
        }
      }
    }

    private AgentDrivenProperties InitializeHumanAgentStats(
      Agent agent,
      AgentDrivenProperties agentDrivenProperties,
      AgentBuildData agentBuildData)
    {
      MultiplayerClassDivisions.MPHeroClass classForCharacter = Utility.GetMPHeroClassForCharacter(agent.get_Character());
      if (classForCharacter != null)
      {
        this.FillAgentStatsFromData(ref agentDrivenProperties, classForCharacter, agent, agentBuildData?.get_AgentMissionPeer());
        agentDrivenProperties.SetStat((DrivenProperty) 57, this._config.useRealisticBlocking ? 1f : 0.0f);
      }
      float num1 = 0.5f;
      float num2 = 0.5f;
      if (this._config.changeCombatAI)
      {
        num1 = (float) this._config.combatAI / 100f;
        num2 = (float) this._config.combatAI / 100f;
      }
      else if (classForCharacter != null)
      {
        num1 = (float) classForCharacter.get_MeleeAI() / 100f;
        num2 = (float) classForCharacter.get_RangedAI() / 100f;
      }
      else
        Utility.DisplayLocalizedText("str_error_no_hero_class", (string) null);
      float num3 = MBMath.ClampFloat(num1, 0.0f, 1f);
      float num4 = MBMath.ClampFloat(num2, 0.0f, 1f);
      agentDrivenProperties.set_AiRangedHorsebackMissileRange((float) (0.300000011920929 + 0.400000005960464 * (double) num4));
      agentDrivenProperties.set_AiFacingMissileWatch((float) ((double) num3 * 0.0599999986588955 - 0.959999978542328));
      agentDrivenProperties.set_AiFlyingMissileCheckRadius((float) (8.0 - 6.0 * (double) num3));
      agentDrivenProperties.set_AiShootFreq((float) (0.200000002980232 + 0.800000011920929 * (double) num4));
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      agentDrivenProperties.set_AiWaitBeforeShootFactor((^(Agent.AgentPropertiesModifiers&) ref agent._propertyModifiers).resetAiWaitBeforeShootFactor != null ? 0.0f : (float) (1.0 - 0.5 * (double) num4));
      agentDrivenProperties.set_AIBlockOnDecideAbility(MBMath.Lerp(0.05f, 0.95f, MBMath.ClampFloat((float) ((Math.Pow((double) MBMath.Lerp(-10f, 10f, num3, 1E-05f), 3.0) + 1000.0) * 0.000500000023748726), 0.0f, 1f), 1E-05f));
      agentDrivenProperties.set_AIParryOnDecideAbility(MBMath.Lerp(0.05f, 0.95f, MBMath.ClampFloat((float) Math.Pow((double) MBMath.Lerp(0.0f, 10f, num3, 1E-05f), 4.0) * 0.0001f, 0.0f, 1f), 1E-05f));
      agentDrivenProperties.set_AiTryChamberAttackOnDecide((float) (((double) num3 - 0.150000005960464) * 0.100000001490116));
      agentDrivenProperties.set_AIAttackOnParryChance(0.3f);
      agentDrivenProperties.set_AiAttackOnParryTiming((float) (0.300000011920929 * (double) num3 - 0.200000002980232));
      agentDrivenProperties.set_AIDecideOnAttackChance(0.0f);
      agentDrivenProperties.set_AIParryOnAttackAbility(0.5f * MBMath.ClampFloat((float) Math.Pow((double) MBMath.Lerp(0.0f, 10f, num3, 1E-05f), 4.0) * 0.0001f, 0.0f, 1f));
      agentDrivenProperties.set_AiKick((float) (((double) num3 > 0.400000005960464 ? 0.400000005960464 : (double) num3) - 0.100000001490116));
      agentDrivenProperties.set_AiAttackCalculationMaxTimeFactor(num3);
      agentDrivenProperties.set_AiDecideOnAttackWhenReceiveHitTiming((float) (-0.25 * (1.0 - (double) num3)));
      agentDrivenProperties.set_AiDecideOnAttackContinueAction((float) (-0.5 * (1.0 - (double) num3)));
      agentDrivenProperties.set_AiDecideOnAttackingContinue(0.1f * num3);
      agentDrivenProperties.set_AIParryOnAttackingContinueAbility(MBMath.Lerp(0.05f, 0.95f, MBMath.ClampFloat((float) Math.Pow((double) MBMath.Lerp(0.0f, 10f, num3, 1E-05f), 4.0) * 0.0001f, 0.0f, 1f), 1E-05f));
      agentDrivenProperties.set_AIDecideOnRealizeEnemyBlockingAttackAbility(0.5f * MBMath.ClampFloat((float) Math.Pow((double) MBMath.Lerp(0.0f, 10f, num3, 1E-05f), 5.0) * 1E-05f, 0.0f, 1f));
      agentDrivenProperties.set_AIRealizeBlockingFromIncorrectSideAbility(0.5f * MBMath.ClampFloat((float) Math.Pow((double) MBMath.Lerp(0.0f, 10f, num3, 1E-05f), 5.0) * 1E-05f, 0.0f, 1f));
      agentDrivenProperties.set_AiAttackingShieldDefenseChance((float) (0.200000002980232 + 0.300000011920929 * (double) num3));
      agentDrivenProperties.set_AiAttackingShieldDefenseTimer((float) (0.300000011920929 * (double) num3 - 0.300000011920929));
      agentDrivenProperties.set_AiRandomizedDefendDirectionChance((float) (1.0 - Math.Log((double) num3 * 7.0 + 1.0, 2.0) * 0.333330005407333));
      agentDrivenProperties.set_AISetNoAttackTimerAfterBeingHitAbility(MBMath.ClampFloat((float) Math.Pow((double) MBMath.Lerp(0.0f, 10f, num3, 1E-05f), 2.0) * 0.01f, 0.05f, 0.95f));
      agentDrivenProperties.set_AISetNoAttackTimerAfterBeingParriedAbility(MBMath.ClampFloat((float) Math.Pow((double) MBMath.Lerp(0.0f, 10f, num3, 1E-05f), 2.0) * 0.01f, 0.05f, 0.95f));
      agentDrivenProperties.set_AISetNoDefendTimerAfterHittingAbility(MBMath.ClampFloat((float) Math.Pow((double) MBMath.Lerp(0.0f, 10f, num3, 1E-05f), 2.0) * 0.01f, 0.05f, 0.95f));
      agentDrivenProperties.set_AISetNoDefendTimerAfterParryingAbility(MBMath.ClampFloat((float) Math.Pow((double) MBMath.Lerp(0.0f, 10f, num3, 1E-05f), 2.0) * 0.01f, 0.05f, 0.95f));
      agentDrivenProperties.set_AIEstimateStunDurationPrecision(1f - MBMath.ClampFloat((float) Math.Pow((double) MBMath.Lerp(0.0f, 10f, num3, 1E-05f), 2.0) * 0.01f, 0.05f, 0.95f));
      agentDrivenProperties.set_AiRaiseShieldDelayTimeBase((float) (0.5 * (double) num3 - 0.75));
      agentDrivenProperties.set_AiUseShieldAgainstEnemyMissileProbability((float) (0.100000001490116 + (double) num3 * 0.200000002980232));
      agentDrivenProperties.set_AiCheckMovementIntervalFactor((float) (0.00499999988824129 * (1.0 - (double) num3)));
      agentDrivenProperties.set_AiMovemetDelayFactor((float) (4.0 / (3.0 + (double) num3)));
      agentDrivenProperties.set_AiParryDecisionChangeValue((float) (0.0500000007450581 + 0.699999988079071 * (double) num3));
      agentDrivenProperties.set_AiDefendWithShieldDecisionChanceValue((float) (0.300000011920929 + 0.699999988079071 * (double) num3));
      agentDrivenProperties.set_AiMoveEnemySideTimeValue((float) (0.5 * (double) num3 - 2.5));
      agentDrivenProperties.set_AiMinimumDistanceToContinueFactor((float) (2.0 + 0.300000011920929 * (3.0 - (double) num3)));
      agentDrivenProperties.set_AiStandGroundTimerValue((float) (0.5 * ((double) num3 - 1.0)));
      agentDrivenProperties.set_AiStandGroundTimerMoveAlongValue((float) (0.5 * (double) num3 - 1.0));
      agentDrivenProperties.set_AiHearingDistanceFactor(1f + num3);
      agentDrivenProperties.set_AiChargeHorsebackTargetDistFactor((float) (1.5 * (3.0 - (double) num3)));
      float num5 = 1f - MBMath.ClampFloat(0.004f * (float) agent.get_Character().GetSkillValue(DefaultSkills.get_Bow()), 0.0f, 0.99f);
      agentDrivenProperties.set_AiRangerLeadErrorMin(num5 * 0.2f);
      agentDrivenProperties.set_AiRangerLeadErrorMax(num5 * 0.3f);
      agentDrivenProperties.set_AiRangerVerticalErrorMultiplier(num5 * 0.1f);
      agentDrivenProperties.set_AiRangerHorizontalErrorMultiplier(num5 * ((float) Math.PI / 90f));
      agentDrivenProperties.set_AIAttackOnDecideChance(50f);
      agent.set_HealthLimit(classForCharacter == null ? 100f : (float) classForCharacter.get_Health());
      agent.set_Health(agent.get_HealthLimit());
      return agentDrivenProperties;
    }

    private static void InitializeHorseAgentStats(
      Agent agent,
      Equipment spawnEquipment,
      AgentDrivenProperties agentDrivenProperties)
    {
      AgentDrivenProperties drivenProperties1 = agentDrivenProperties;
      EquipmentElement equipmentElement1 = spawnEquipment.get_Item((EquipmentIndex) 10);
      HorseComponent horseComponent = ((EquipmentElement) ref equipmentElement1).get_Item().get_HorseComponent();
      int num1 = horseComponent != null ? horseComponent.get_Monster().get_FamilyType() : 0;
      drivenProperties1.set_AiSpeciesIndex(num1);
      AgentDrivenProperties drivenProperties2 = agentDrivenProperties;
      EquipmentElement equipmentElement2 = spawnEquipment.get_Item((EquipmentIndex) 11);
      double num2 = 0.800000011920929 + (((EquipmentElement) ref equipmentElement2).get_Item() != null ? 0.200000002980232 : 0.0);
      drivenProperties2.set_AttributeRiding((float) num2);
      float num3 = 0.0f;
      for (int index = 1; index < 12; ++index)
      {
        equipmentElement2 = spawnEquipment.get_Item(index);
        if (((EquipmentElement) ref equipmentElement2).get_Item() != null)
        {
          double num4 = (double) num3;
          equipmentElement2 = spawnEquipment.get_Item(index);
          double bodyArmorHorse = (double) ((EquipmentElement) ref equipmentElement2).GetBodyArmorHorse();
          num3 = (float) (num4 + bodyArmorHorse);
        }
      }
      agentDrivenProperties.set_ArmorTorso(num3);
      equipmentElement2 = spawnEquipment.get_Item((EquipmentIndex) 10);
      ItemObject itemObject = ((EquipmentElement) ref equipmentElement2).get_Item();
      if (itemObject == null)
        return;
      itemObject.get_HorseComponent();
      EquipmentElement equipmentElement3 = spawnEquipment.get_Item((EquipmentIndex) 10);
      EquipmentElement equipmentElement4 = spawnEquipment.get_Item((EquipmentIndex) 11);
      agentDrivenProperties.set_MountManeuver((float) ((EquipmentElement) ref equipmentElement3).GetBaseHorseManeuver(equipmentElement4));
      agentDrivenProperties.set_MountSpeed((float) (((EquipmentElement) ref equipmentElement3).GetBaseHorseSpeed(equipmentElement4) + 1) * 0.22f);
      agentDrivenProperties.set_MountChargeDamage((float) ((EquipmentElement) ref equipmentElement3).GetBaseHorseCharge(equipmentElement4) * 0.01f);
      agentDrivenProperties.set_MountDifficulty((float) ((EquipmentElement) ref equipmentElement3).get_Item().get_Difficulty());
      agentDrivenProperties.set_TopSpeedReachDuration(Game.get_Current().get_BasicModels().get_RidingModel().CalculateAcceleration(((EquipmentElement) ref equipmentElement3).get_Item(), agent.get_RiderAgent()?.get_Character()));
      if (agent.get_RiderAgent() == null)
        return;
      AgentDrivenProperties drivenProperties3 = agentDrivenProperties;
      drivenProperties3.set_MountSpeed(drivenProperties3.get_MountSpeed() * (float) (1.0 + (double) agent.get_RiderAgent().get_Character().GetSkillValue(DefaultSkills.get_Riding()) * 0.002));
      AgentDrivenProperties drivenProperties4 = agentDrivenProperties;
      drivenProperties4.set_MountManeuver(drivenProperties4.get_MountManeuver() * (float) (1.0 + (double) agent.get_RiderAgent().get_Character().GetSkillValue(DefaultSkills.get_Riding()) * 0.0007999999797903));
    }

    public virtual void UpdateAgentStats(Agent agent, AgentDrivenProperties agentDrivenProperties)
    {
      if (!agent.get_IsHuman())
        return;
      BasicCharacterObject character = agent.get_Character();
      MissionEquipment equipment = agent.get_Equipment();
      float totalWeightOfWeapons = equipment.GetTotalWeightOfWeapons();
      EquipmentIndex wieldedItemIndex1 = agent.GetWieldedItemIndex((Agent.HandIndex) 0);
      EquipmentIndex wieldedItemIndex2 = agent.GetWieldedItemIndex((Agent.HandIndex) 1);
      MissionWeapon missionWeapon;
      if (wieldedItemIndex1 != -1)
      {
        missionWeapon = equipment.get_Item(wieldedItemIndex1);
        ItemObject primaryItem = ((MissionWeapon) ref missionWeapon).get_PrimaryItem();
        WeaponComponent weaponComponent = primaryItem.get_WeaponComponent();
        float realWeaponLength = weaponComponent.get_PrimaryWeapon().GetRealWeaponLength();
        totalWeightOfWeapons += (weaponComponent.GetItemType() == 8 ? 4f : 1.5f) * primaryItem.get_Weight() * MathF.Sqrt(realWeaponLength);
      }
      if (wieldedItemIndex2 != -1)
      {
        missionWeapon = equipment.get_Item(wieldedItemIndex2);
        ItemObject primaryItem = ((MissionWeapon) ref missionWeapon).get_PrimaryItem();
        totalWeightOfWeapons += 1.5f * primaryItem.get_Weight();
      }
      agentDrivenProperties.set_WeaponsEncumbrance(totalWeightOfWeapons);
      EquipmentIndex wieldedItemIndex3 = agent.GetWieldedItemIndex((Agent.HandIndex) 0);
      WeaponComponentData weaponComponentData1;
      if (wieldedItemIndex3 == -1)
      {
        weaponComponentData1 = (WeaponComponentData) null;
      }
      else
      {
        missionWeapon = equipment.get_Item(wieldedItemIndex3);
        weaponComponentData1 = ((MissionWeapon) ref missionWeapon).get_CurrentUsageItem();
      }
      WeaponComponentData weaponComponentData2 = weaponComponentData1;
      float num1;
      agentDrivenProperties.set_LongestRangedWeaponSlotIndex((float) equipment.GetLongestRangedWeaponWithAimingError(ref num1, agent));
      agentDrivenProperties.set_LongestRangedWeaponInaccuracy(num1);
      agentDrivenProperties.set_SwingSpeedMultiplier((float) (0.930000007152557 + 0.000699999975040555 * (double) this.GetSkillValueForItem(character, weaponComponentData2?.get_Item())));
      agentDrivenProperties.set_ThrustOrRangedReadySpeedMultiplier(agentDrivenProperties.get_SwingSpeedMultiplier());
      agentDrivenProperties.set_ShieldBashStunDurationMultiplier(1f);
      agentDrivenProperties.set_ReloadSpeed((float) (0.930000007152557 + 0.000699999975040555 * (double) this.GetSkillValueForItem(character, weaponComponentData2?.get_Item())));
      agentDrivenProperties.set_WeaponInaccuracy(0.0f);
      agent.get_Monster().get_Weight();
      MultiplayerClassDivisions.MPHeroClass classForCharacter = Utility.GetMPHeroClassForCharacter(agent.get_Character());
      agentDrivenProperties.set_MaxSpeedMultiplier((float) (1.04999995231628 * ((double) classForCharacter.get_MovementSpeedMultiplier() * (100.0 / (100.0 + (double) totalWeightOfWeapons)))));
      if (weaponComponentData2 != null)
      {
        if (weaponComponentData2.get_IsRangedWeapon())
        {
          WeaponComponentData weaponComponentData3 = weaponComponentData2;
          agentDrivenProperties.set_WeaponStationaryAccuracyMultiplier(!Extensions.HasAnyFlag<WeaponFlags>((M0) weaponComponentData3.WeaponFlags, (M0) 2147483648L) ? (!Extensions.HasAnyFlag<WeaponFlags>((M0) weaponComponentData3.WeaponFlags, (M0) 3072L) ? 0.5f : 1f) : 2f);
          int skillValue = character.GetSkillValue(weaponComponentData2.get_RelevantSkill());
          agentDrivenProperties.set_WeaponInaccuracy(equipment.GetWeaponInaccuracy(agent, weaponComponentData3));
          agentDrivenProperties.set_WeaponMaxMovementAccuracyPenalty((float) (500 - skillValue) * 0.00025f);
          agentDrivenProperties.set_WeaponMaxUnsteadyAccuracyPenalty((float) (500 - skillValue) * 0.0002f);
          if (agent.get_MountAgent() != null)
            agentDrivenProperties.set_WeaponMaxMovementAccuracyPenalty((float) (700 - character.GetSkillValue(weaponComponentData2.get_RelevantSkill()) - character.GetSkillValue(DefaultSkills.get_Riding())) * 0.00015f);
          else if (weaponComponentData3.get_RelevantSkill() == DefaultSkills.get_Bow())
          {
            AgentDrivenProperties drivenProperties = agentDrivenProperties;
            drivenProperties.set_WeaponMaxMovementAccuracyPenalty(drivenProperties.get_WeaponMaxMovementAccuracyPenalty() * 3.5f);
          }
          else if (weaponComponentData3.get_RelevantSkill() == DefaultSkills.get_Crossbow())
          {
            AgentDrivenProperties drivenProperties = agentDrivenProperties;
            drivenProperties.set_WeaponMaxUnsteadyAccuracyPenalty(drivenProperties.get_WeaponMaxUnsteadyAccuracyPenalty() * 10f);
          }
          if (weaponComponentData3.get_WeaponClass() == 15)
          {
            agentDrivenProperties.set_WeaponUnsteadyBeginTime((float) (1.0 + (double) character.GetSkillValue(weaponComponentData2.get_RelevantSkill()) * 0.00999999977648258));
            agentDrivenProperties.set_WeaponUnsteadyEndTime(2f + agentDrivenProperties.get_WeaponUnsteadyBeginTime());
            agentDrivenProperties.set_WeaponRotationalAccuracyPenaltyInRadians(0.025f);
          }
          else
          {
            agentDrivenProperties.set_WeaponUnsteadyBeginTime(0.0f);
            agentDrivenProperties.set_WeaponUnsteadyEndTime(0.0f);
            agentDrivenProperties.set_WeaponRotationalAccuracyPenaltyInRadians(0.0f);
          }
        }
        else if (Extensions.HasAllFlags<WeaponFlags>((M0) weaponComponentData2.WeaponFlags, (M0) 64L))
        {
          int skillValue = character.GetSkillValue(DefaultSkills.get_Polearm());
          agentDrivenProperties.set_WeaponInaccuracy(MBMath.ClampFloat((float) (1.0 - (double) skillValue * 0.00999999977648258), 0.0f, 1f));
          agentDrivenProperties.set_WeaponUnsteadyBeginTime((float) (1.0 + (double) skillValue * 0.00499999988824129));
          agentDrivenProperties.set_WeaponUnsteadyEndTime((float) (3.0 + (double) skillValue * 0.00999999977648258));
        }
      }
      agentDrivenProperties.set_AttributeShieldMissileCollisionBodySizeAdder(0.3f);
      Agent mountAgent = agent.get_MountAgent();
      float num2 = mountAgent != null ? mountAgent.GetAgentDrivenPropertyValue((DrivenProperty) 71) : 1f;
      agentDrivenProperties.set_AttributeRiding((float) character.GetSkillValue(DefaultSkills.get_Riding()) * num2);
      agentDrivenProperties.set_AttributeHorseArchery(Game.get_Current().get_BasicModels().get_StrikeMagnitudeModel().CalculateHorseArcheryFactor(character));
      using (IEnumerator<DrivenPropertyBonusAgentComponent> enumerator = ((IEnumerable) agent.get_Components()).OfType<DrivenPropertyBonusAgentComponent>().GetEnumerator())
      {
        while (((IEnumerator) enumerator).MoveNext())
        {
          DrivenPropertyBonusAgentComponent current = enumerator.Current;
          if (!MBMath.IsBetween((int) current.get_DrivenProperty(), 0, 56))
          {
            float num3 = agentDrivenProperties.GetStat(current.get_DrivenProperty()) + current.get_DrivenPropertyBonus();
            agentDrivenProperties.SetStat(current.get_DrivenProperty(), num3);
          }
        }
      }
    }

    public virtual short CalculateConsumableMaxAmountAdder()
    {
      return 0;
    }

    private void FillAgentStatsFromData(
      ref AgentDrivenProperties agentDrivenProperties,
      MultiplayerClassDivisions.MPHeroClass heroClass,
      Agent agent,
      MissionPeer missionPeer)
    {
      bool flag = agent.get_Formation().FormationIndex == Utility.CommanderFormationClass();
      bool isPlayerTeam = agent.get_Team().get_IsPlayerTeam();
      ClassInfo classInfo = !flag ? (isPlayerTeam ? this._config.playerTroops[agent.get_Formation().Index] : this._config.enemyTroops[agent.get_Formation().Index]) : (isPlayerTeam ? this._config.playerClass : this._config.enemyClass);
      float armorBonusFromPerks = MPPerkObject.GetArmorBonusFromPerks((flag ? 1 : 0) != 0, (IEnumerable<MPPerkObject>) Utility.GetAllSelectedPerks(heroClass, new int[2]
      {
        classInfo.selectedFirstPerk,
        classInfo.selectedSecondPerk
      }));
      agentDrivenProperties.set_ArmorHead((float) heroClass.get_ArmorValue() + armorBonusFromPerks);
      agentDrivenProperties.set_ArmorTorso((float) heroClass.get_ArmorValue() + armorBonusFromPerks);
      agentDrivenProperties.set_ArmorLegs((float) heroClass.get_ArmorValue() + armorBonusFromPerks);
      agentDrivenProperties.set_ArmorArms((float) heroClass.get_ArmorValue() + armorBonusFromPerks);
      agentDrivenProperties.set_TopSpeedReachDuration(heroClass.get_TopSpeedReachDuration());
      float managedParameter1 = ManagedParameters.get_Instance().GetManagedParameter((ManagedParametersEnum) 12);
      float managedParameter2 = ManagedParameters.get_Instance().GetManagedParameter((ManagedParametersEnum) 13);
      agentDrivenProperties.set_CombatMaxSpeedMultiplier(managedParameter1 + (managedParameter2 - managedParameter1) * heroClass.get_CombatMovementSpeedMultiplier());
    }

    private int GetSkillValueForItem(BasicCharacterObject characterObject, ItemObject primaryItem)
    {
      return characterObject.GetSkillValue(primaryItem != null ? primaryItem.get_RelevantSkill() : DefaultSkills.get_Athletics());
    }

    public static float CalculateMaximumSpeedMultiplier(Agent agent)
    {
      return Utility.GetMPHeroClassForCharacter(agent.get_Character()).get_MovementSpeedMultiplier();
    }

    public virtual float GetDifficultyModifier()
    {
      return 1f;
    }
  }
}
