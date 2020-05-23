// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.Utility
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System;
using System.Collections;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ObjectSystem;

namespace VirtualBattlegrounds
{
  internal class Utility
  {
    public const string characterSufix = "_WithPerkApplied";

    public static void DisplayLocalizedText(string id, string variation = null)
    {
      InformationManager.DisplayMessage(new InformationMessage(((object) GameTexts.FindText(id, variation)).ToString()));
    }

    public static void DisplayMessage(string msg)
    {
      InformationManager.DisplayMessage(new InformationMessage(((object) new TextObject(msg, (Dictionary<string, TextObject>) null)).ToString()));
    }

    public static bool IsAgentDead(Agent agent)
    {
      return agent == null || !agent.IsActive();
    }

    public static bool IsPlayerDead()
    {
      return Utility.IsAgentDead(Mission.get_Current().get_MainAgent());
    }

    public static void SetPlayerAsCommander()
    {
      Mission current1 = Mission.get_Current();
      if (current1 == null)
        return;
      current1.get_PlayerTeam().get_PlayerOrderController().Owner = (__Null) current1.get_MainAgent();
      using (IEnumerator<Formation> enumerator = current1.get_PlayerTeam().get_FormationsIncludingEmpty().GetEnumerator())
      {
        while (((IEnumerator) enumerator).MoveNext())
        {
          Formation current2 = enumerator.Current;
          bool isAiControlled = current2.get_IsAIControlled();
          current2.set_PlayerOwner(current1.get_MainAgent());
          current2.set_IsAIControlled(isAiControlled);
        }
      }
    }

    public static void CancelPlayerCommander()
    {
    }

    public static void ApplyTeamAIEnabled(BattleConfigBase config)
    {
      Mission current = Mission.get_Current();
      Utility.DisplayLocalizedText("str_ai_enabled_for", config.aiEnableType.ToString());
      switch (config.aiEnableType)
      {
        case AIEnableType.None:
          Utility.SetTeamAIEnabled(current.get_PlayerTeam(), false);
          Utility.SetTeamAIEnabled(current.get_PlayerEnemyTeam(), false);
          break;
        case AIEnableType.EnemyOnly:
          Utility.SetTeamAIEnabled(current.get_PlayerTeam(), false);
          Utility.SetTeamAIEnabled(current.get_PlayerEnemyTeam(), true);
          break;
        case AIEnableType.PlayerOnly:
          Utility.SetTeamAIEnabled(current.get_PlayerTeam(), true);
          Utility.SetTeamAIEnabled(current.get_PlayerEnemyTeam(), false);
          break;
        case AIEnableType.Both:
          Utility.SetTeamAIEnabled(current.get_PlayerTeam(), true);
          Utility.SetTeamAIEnabled(current.get_PlayerEnemyTeam(), true);
          break;
      }
    }

    public static void SetTeamAIEnabled(Team team, bool enabled)
    {
      if (team == null)
        return;
      using (IEnumerator<Formation> enumerator = team.get_FormationsIncludingEmpty().GetEnumerator())
      {
        while (((IEnumerator) enumerator).MoveNext())
          enumerator.Current.set_IsAIControlled(enabled);
      }
    }

    public static List<MPPerkObject> GetAllSelectedPerks(
      MultiplayerClassDivisions.MPHeroClass mpHeroClass,
      int[] selectedPerks)
    {
      List<MPPerkObject> mpPerkObjectList = new List<MPPerkObject>();
      for (int index = 0; index < selectedPerks.Length; ++index)
      {
        List<MPPerkObject> perksForListIndex = mpHeroClass.GetAllAvailablePerksForListIndex(index);
        if (!Extensions.IsEmpty<MPPerkObject>((IEnumerable<M0>) perksForListIndex))
          mpPerkObjectList.Add(perksForListIndex[selectedPerks[index]]);
      }
      return mpPerkObjectList;
    }

    public static IEnumerable<PerkEffect> SelectRandomPerkEffectsForPerks(
      MultiplayerClassDivisions.MPHeroClass mpHeroClass,
      bool isPlayer,
      PerkType perkType,
      int[] selectedPerks)
    {
      List<MPPerkObject> allSelectedPerks = Utility.GetAllSelectedPerks(mpHeroClass, selectedPerks);
      return MPPerkObject.SelectRandomPerkEffectsForPerks(isPlayer, perkType, (IEnumerable<MPPerkObject>) allSelectedPerks);
    }

    public static Equipment GetNewEquipmentsForPerks(
      MultiplayerClassDivisions.MPHeroClass mpHeroClass,
      ClassInfo info,
      bool isPlayer)
    {
      BasicCharacterObject basicCharacterObject = isPlayer ? mpHeroClass.get_HeroCharacter() : mpHeroClass.get_TroopCharacter();
      Equipment equipment1 = isPlayer ? basicCharacterObject.get_Equipment().Clone(false) : Equipment.GetRandomEquipmentElements(basicCharacterObject, true, false, MBRandom.RandomInt());
      using (IEnumerator<PerkEffect> enumerator = Utility.SelectRandomPerkEffectsForPerks(mpHeroClass, (isPlayer ? 1 : 0) != 0, (PerkType) 0, new int[2]
      {
        info.selectedFirstPerk,
        info.selectedSecondPerk
      }).GetEnumerator())
      {
        while (((IEnumerator) enumerator).MoveNext())
        {
          PerkEffect current = enumerator.Current;
          Equipment equipment2 = equipment1;
          EquipmentIndex newItemIndex = current.get_NewItemIndex();
          ItemRosterElement newItem = current.get_NewItem();
          EquipmentElement equipmentElement = ((ItemRosterElement) ref newItem).get_EquipmentElement();
          equipment2.set_Item(newItemIndex, equipmentElement);
        }
      }
      return equipment1;
    }

    public static void OverrideEquipment(AgentBuildData buildData, ClassInfo info, bool isPlayer)
    {
      MultiplayerClassDivisions.MPHeroClass mpHeroClass = (MultiplayerClassDivisions.MPHeroClass) MBObjectManager.get_Instance().GetObject<MultiplayerClassDivisions.MPHeroClass>(info.classStringId);
      BasicCharacterObject basicCharacterObject = isPlayer ? mpHeroClass.get_HeroCharacter() : mpHeroClass.get_TroopCharacter();
      Equipment equipmentsForPerks = Utility.GetNewEquipmentsForPerks(mpHeroClass, info, isPlayer);
      AgentBuildData agentBuildData = buildData.Equipment(equipmentsForPerks);
      EquipmentElement equipmentElement = equipmentsForPerks.get_Item((EquipmentIndex) 10);
      string randomMountKey = MountCreationKey.GetRandomMountKey(((EquipmentElement) ref equipmentElement).get_Item(), basicCharacterObject.GetMountKeySeed());
      agentBuildData.MountKey(randomMountKey);
    }

    public static BasicCharacterObject ApplyPerks(ClassInfo info, bool isHero)
    {
      MultiplayerClassDivisions.MPHeroClass mpHeroClass = (MultiplayerClassDivisions.MPHeroClass) MBObjectManager.get_Instance().GetObject<MultiplayerClassDivisions.MPHeroClass>(info.classStringId);
      BasicCharacterObject sourceCharacter = isHero ? mpHeroClass.get_HeroCharacter() : mpHeroClass.get_TroopCharacter();
      if (Extensions.IsEmpty<MPPerkObject>((IEnumerable<M0>) mpHeroClass.GetAllAvailablePerksForListIndex(0)) || Extensions.IsEmpty<MPPerkObject>((IEnumerable<M0>) mpHeroClass.GetAllAvailablePerksForListIndex(1)))
        return sourceCharacter;
      Equipment equipmentsForPerks = Utility.GetNewEquipmentsForPerks(mpHeroClass, info, isHero);
      if (equipmentsForPerks == null)
        return sourceCharacter;
      VirtualBattlegroundsCharacter battlegroundsCharacter1 = Utility.NewCharacter(sourceCharacter, isHero);
      VirtualBattlegroundsCharacter battlegroundsCharacter2 = battlegroundsCharacter1;
      List<Equipment> equipmentList = new List<Equipment>();
      equipmentList.Add(equipmentsForPerks);
      battlegroundsCharacter2.InitializeEquipmentsOnLoad(equipmentList);
      battlegroundsCharacter1.SetIsHero(isHero);
      return (BasicCharacterObject) battlegroundsCharacter1;
    }

    public static MultiplayerClassDivisions.MPHeroClass GetMPHeroClassForCharacter(
      BasicCharacterObject character)
    {
      string str = ((MBObjectBase) character).get_StringId();
      if (str.EndsWith("_WithPerkApplied"))
        str = str.Substring(0, str.Length - "_WithPerkApplied".Length);
      return MultiplayerClassDivisions.GetMPHeroClassForCharacter((BasicCharacterObject) MBObjectManager.get_Instance().GetObject<BasicCharacterObject>(str));
    }

    public static VirtualBattlegroundsCharacter NewCharacter(
      BasicCharacterObject sourceCharacter,
      bool isHero)
    {
      VirtualBattlegroundsCharacter battlegroundsCharacter1 = new VirtualBattlegroundsCharacter();
      battlegroundsCharacter1.InitializeHeroBasicCharacterOnAfterLoad(sourceCharacter, sourceCharacter.get_Name());
      battlegroundsCharacter1.UpdatePlayerCharacterBodyProperties(sourceCharacter.GetBodyPropertiesMax(), sourceCharacter.get_IsFemale());
      if (isHero)
      {
        VirtualBattlegroundsCharacter battlegroundsCharacter2 = battlegroundsCharacter1;
        StaticBodyProperties bodyPropertiesMin;
        battlegroundsCharacter1.set_StaticBodyPropertiesMin(bodyPropertiesMin = battlegroundsCharacter1.get_StaticBodyPropertiesMin());
        StaticBodyProperties staticBodyProperties = bodyPropertiesMin;
        battlegroundsCharacter2.set_StaticBodyPropertiesMax(staticBodyProperties);
      }
      else
      {
        battlegroundsCharacter1.set_StaticBodyPropertiesMax(battlegroundsCharacter1.get_StaticBodyPropertiesMax());
        battlegroundsCharacter1.set_StaticBodyPropertiesMin(battlegroundsCharacter1.get_StaticBodyPropertiesMin());
      }
      ((MBObjectBase) battlegroundsCharacter1).set_StringId(((MBObjectBase) sourceCharacter).get_StringId() + "_WithPerkApplied");
      battlegroundsCharacter1.set_Name(sourceCharacter.get_Name());
      battlegroundsCharacter1.set_Age(sourceCharacter.get_Age());
      battlegroundsCharacter1.set_FaceDirtAmount(sourceCharacter.get_FaceDirtAmount());
      battlegroundsCharacter1.set_Level(sourceCharacter.get_Level());
      return battlegroundsCharacter1;
    }

    public static FormationClass CommanderFormationClass()
    {
      return (FormationClass) 3;
    }

    public static void AddCharacter(
      CustomBattleCombatant combatant,
      ClassInfo info,
      bool isHero,
      FormationClass formationClass,
      bool isPlayer = false)
    {
      BasicCharacterObject basicCharacterObject = Utility.ApplyPerks(info, isHero);
      basicCharacterObject.set_CurrentFormationClass(formationClass);
      if (isPlayer)
        Game.get_Current().set_PlayerTroop(basicCharacterObject);
      combatant.AddCharacter(basicCharacterObject, info.troopCount);
    }

    public static MatrixFrame ToMatrixFrame(Scene scene, Vec3 position, Vec2 direction)
    {
      Vec2 vec2;
      ((Vec2) ref vec2).\u002Ector(0.0f, 1f);
      Mat3 identity = Mat3.get_Identity();
      ((Mat3) ref identity).RotateAboutUp(((Vec2) ref vec2).AngleBetween(direction));
      return new MatrixFrame(identity, position);
    }

    public static MatrixFrame ToMatrixFrame(Scene scene, Vec2 position, Vec2 direction)
    {
      Vec2 vec2;
      ((Vec2) ref vec2).\u002Ector(0.0f, 1f);
      Mat3 identity = Mat3.get_Identity();
      ((Mat3) ref identity).RotateAboutUp(((Vec2) ref vec2).AngleBetween(direction));
      return new MatrixFrame(identity, ((Vec2) ref position).ToVec3(Utility.GetSceneHeightForAgent(scene, position)));
    }

    public static float GetSceneHeightForAgent(Scene scene, Vec2 pos)
    {
      float num = 0.0f;
      scene.GetHeightAtPoint(pos, (BodyFlags) 4305289, ref num);
      return num;
    }

    public static Tuple<float, float> GetFormationArea(
      FormationClass formationClass,
      int troopCount,
      int soldiersPerRow)
    {
      bool flag = formationClass == 2 || formationClass == 3;
      float defaultUnitDiameter = Formation.GetDefaultUnitDiameter(flag);
      int num1 = 1;
      float num2 = flag ? Formation.CavalryInterval(num1) : Formation.InfantryInterval(num1);
      int num3 = Math.Min(soldiersPerRow, troopCount);
      float num4 = (float) num3 * (defaultUnitDiameter + num2) - num2;
      if (flag)
        defaultUnitDiameter *= 1.8f;
      float num5 = (float) ((double) (int) Math.Ceiling((double) troopCount / (double) num3) * ((double) defaultUnitDiameter + (double) num2) + 1.5);
      return new Tuple<float, float>(num4, num5);
    }

    public static void SetInitialCameraPos(
      Camera camera,
      Vec2 formationPosition,
      Vec2 formationDirection)
    {
      Vec3 vec3_1 = ((Vec2) ref formationPosition).ToVec3(Utility.GetSceneHeightForAgent(Mission.get_Current().get_Scene(), formationPosition) + 5f);
      Vec3 vec3_2 = ((Vec2) ref formationPosition).ToVec3(-1f);
      Vec3 vec3_3 = ((Vec3) ref vec3_2).NormalizedCopy();
      camera.LookAt(vec3_1, Vec3.op_Addition(vec3_1, vec3_3), (Vec3) Vec3.Up);
    }

    public static IAgentOriginBase CreateOrigin(
      CustomBattleCombatant customBattleCombatant,
      BasicCharacterObject characterObject,
      int rank = -1,
      EnhancedTroopSupplier troopSupplier = null)
    {
      UniqueTroopDescriptor uniqueNo;
      ((UniqueTroopDescriptor) ref uniqueNo).\u002Ector(Game.get_Current().get_NextUniqueTroopSeed());
      return (IAgentOriginBase) new EnhancedFreeBattleAgentOrigin(customBattleCombatant, troopSupplier, characterObject, rank, uniqueNo);
    }

    public static uint BackgroundColor(BasicCultureObject culture, bool isAttacker)
    {
      return isAttacker ? culture.get_BackgroundColor1() : culture.get_BackgroundColor2();
    }

    public static uint ForegroundColor(BasicCultureObject culture, bool isAttacker)
    {
      return isAttacker ? culture.get_ForegroundColor1() : culture.get_ForegroundColor2();
    }
  }
}
