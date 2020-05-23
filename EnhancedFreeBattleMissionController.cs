// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.EnhancedFreeBattleMissionController
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace VirtualBattlegrounds
{
  public class EnhancedFreeBattleMissionController : MissionLogic
  {
    private EnhancedFreeBattleConfig _freeBattleConfig;
    private SoundEvent _musicSoundEvent;
    public Vec3 initialFreeCameraPos;
    public Vec3 initialFreeCameraTarget;
    private AgentVictoryLogic _victoryLogic;
    private MakeGruntVoiceLogic _makeGruntVoiceLogic;

    public EnhancedFreeBattleConfig FreeBattleConfig
    {
      get
      {
        return this._freeBattleConfig;
      }
      set
      {
        this._freeBattleConfig = value;
      }
    }

    public EnhancedFreeBattleMissionController(EnhancedFreeBattleConfig config)
    {
      this.\u002Ector();
      this.FreeBattleConfig = config;
      this._victoryLogic = (AgentVictoryLogic) null;
    }

    public virtual void EarlyStart()
    {
      ((MissionBehaviour) this).get_Mission().set_MissionTeamAIType((Mission.MissionTeamAITypeEnum) 1);
      ((MissionBehaviour) this).get_Mission().SetMissionMode((MissionMode) 2, true);
      this._makeGruntVoiceLogic = (MakeGruntVoiceLogic) ((MissionBehaviour) this).get_Mission().GetMissionBehaviour<MakeGruntVoiceLogic>();
      this.AdjustScene();
      this.AddTeams(this.FreeBattleConfig.GetPlayerTeamCulture(), this.FreeBattleConfig.GetEnemyTeamCulture());
    }

    public virtual void AfterStart()
    {
      this.SpawnAgents();
    }

    public void SpawnAgents()
    {
      BasicCultureObject playerTeamCulture = this.FreeBattleConfig.GetPlayerTeamCulture();
      BasicCultureObject enemyTeamCulture = this.FreeBattleConfig.GetEnemyTeamCulture();
      this.SpawnPlayerTeamAgents(playerTeamCulture);
      this.SpawnEnemyTeamAgents(enemyTeamCulture);
      this._musicSoundEvent = SoundEvent.CreateEvent(SoundEvent.GetEventIdFromString("event:/mission/ambient/area/multiplayer/city_mp_02"), ((MissionBehaviour) this).get_Mission().get_Scene());
      this._musicSoundEvent.Play();
    }

    private void AdjustScene()
    {
      Scene scene = ((MissionBehaviour) this).get_Mission().get_Scene();
      if ((double) this.FreeBattleConfig.SkyBrightness >= 0.0)
        scene.SetSkyBrightness(this.FreeBattleConfig.SkyBrightness);
      if ((double) this.FreeBattleConfig.RainDensity < 0.0)
        return;
      scene.SetRainDensity(this.FreeBattleConfig.RainDensity);
    }

    private void AddTeams(BasicCultureObject playerTeamCulture, BasicCultureObject enemyTeamCulture)
    {
      uint num1 = Utility.BackgroundColor(playerTeamCulture, this.FreeBattleConfig.isPlayerAttacker);
      uint num2 = Utility.ForegroundColor(playerTeamCulture, this.FreeBattleConfig.isPlayerAttacker);
      Banner banner1 = new Banner(playerTeamCulture.get_BannerKey(), num1, num2);
      BattleSideEnum battleSideEnum = this.FreeBattleConfig.isPlayerAttacker ? (BattleSideEnum) 1 : (BattleSideEnum) 0;
      Team team1 = ((MissionBehaviour) this).get_Mission().get_Teams().Add(battleSideEnum, num1, num2, banner1, true, false, true);
      uint num3 = Utility.BackgroundColor(enemyTeamCulture, !this.FreeBattleConfig.isPlayerAttacker);
      uint num4 = Utility.ForegroundColor(enemyTeamCulture, !this.FreeBattleConfig.isPlayerAttacker);
      Banner banner2 = new Banner(enemyTeamCulture.get_BannerKey(), num3, num4);
      Team team2 = ((MissionBehaviour) this).get_Mission().get_Teams().Add(Extensions.GetOppositeSide(battleSideEnum), num3, num4, banner2, true, false, true);
      team1.AddTeamAI((TeamAIComponent) new TeamAIGeneral(((MissionBehaviour) this).get_Mission(), team1, 10f, 1f), false);
      team2.AddTeamAI((TeamAIComponent) new TeamAIGeneral(((MissionBehaviour) this).get_Mission(), team2, 10f, 1f), false);
      using (IEnumerator<Team> enumerator = ((IEnumerable<Team>) ((MissionBehaviour) this).get_Mission().get_Teams()).Where<Team>((Func<Team, bool>) (t => t.get_HasTeamAi())).GetEnumerator())
      {
        while (((IEnumerator) enumerator).MoveNext())
        {
          Team current = enumerator.Current;
          if (current.get_Side() == 0)
          {
            bool flag = false;
            foreach (TacticOptionInfo defenderTacticOption in this._freeBattleConfig.defenderTacticOptions)
            {
              if (defenderTacticOption.isEnabled)
              {
                flag = true;
                TacticOptionHelper.AddTacticComponent(current, defenderTacticOption.tacticOption, false);
              }
            }
            if (!flag)
              current.AddTacticOption((TacticComponent) new TacticCharge(current));
          }
          if (current.get_Side() == 1)
          {
            bool flag = false;
            foreach (TacticOptionInfo attackerTacticOption in this._freeBattleConfig.attackerTacticOptions)
            {
              if (attackerTacticOption.isEnabled)
              {
                flag = true;
                TacticOptionHelper.AddTacticComponent(current, attackerTacticOption.tacticOption, false);
              }
            }
            if (!flag)
              current.AddTacticOption((TacticComponent) new TacticCharge(current));
          }
        }
      }
      using (IEnumerator<Team> enumerator = ((ReadOnlyCollection<Team>) ((MissionBehaviour) this).get_Mission().get_Teams()).GetEnumerator())
      {
        while (((IEnumerator) enumerator).MoveNext())
        {
          Team current = enumerator.Current;
          current.ExpireAIQuerySystem();
          current.ResetTactic();
          // ISSUE: method pointer
          current.add_OnOrderIssued(new OnOrderIssuedDelegate((object) this, __methodptr(OrderIssuedDelegate)));
        }
      }
      ((MissionBehaviour) this).get_Mission().set_PlayerTeam(team1);
    }

    private void SpawnPlayerTeamAgents(BasicCultureObject playerTeamCulture)
    {
      ((MissionBehaviour) this).get_Mission().get_Scene();
      CustomBattleCombatant battleCombatant = this.CreateBattleCombatant(playerTeamCulture, (BattleSideEnum) 1);
      float soldierXinterval = this.FreeBattleConfig.soldierXInterval;
      float soldierYinterval = this.FreeBattleConfig.soldierYInterval;
      int soldiersPerRow = this.FreeBattleConfig.SoldiersPerRow;
      Vec2 formationPosition = this.FreeBattleConfig.FormationPosition;
      Vec2 formationDirection1 = this.FreeBattleConfig.FormationDirection;
      Vec2 formationDirection2 = this.FreeBattleConfig.FormationDirection;
      Vec2 vec2 = ((Vec2) ref formationDirection2).LeftVec();
      bool spawnPlayer = this.FreeBattleConfig.SpawnPlayer;
      Team playerTeam = ((MissionBehaviour) this).get_Mission().get_PlayerTeam();
      if (spawnPlayer)
      {
        BasicCharacterObject heroCharacter = this.FreeBattleConfig.PlayerHeroClass.get_HeroCharacter();
        Game.get_Current().set_PlayerTroop(heroCharacter);
        Formation formation = playerTeam.GetFormation(Utility.CommanderFormationClass());
        this.SetFormationRegion(formation, 1f, true, -2f);
        this.SpawnAgent(this.FreeBattleConfig.playerClass, true, heroCharacter, true, formation, playerTeam, battleCombatant, playerTeamCulture, true, 1, 0, new MatrixFrame?()).AllowFirstPersonWideRotation();
      }
      else
      {
        int troopCount = this.FreeBattleConfig.playerTroops[0].troopCount;
        if (troopCount <= 0)
        {
          this.initialFreeCameraTarget = ((Vec2) ref formationPosition).ToVec3(Utility.GetSceneHeightForAgent(((MissionBehaviour) this).get_Mission().get_Scene(), formationPosition));
          this.initialFreeCameraPos = Vec3.op_Subtraction(Vec3.op_Addition(this.initialFreeCameraTarget, new Vec3(0.0f, 0.0f, 10f, -1f)), ((Vec2) ref formationDirection1).ToVec3(0.0f));
        }
        else
        {
          int num = (troopCount + soldiersPerRow - 1) / soldiersPerRow;
          Vec2 pos = Vec2.op_Subtraction(Vec2.op_Addition(formationPosition, Vec2.op_Multiply((float) ((Math.Min(soldiersPerRow, troopCount) - 1) / 2) * soldierYinterval, vec2)), Vec2.op_Multiply((float) num * soldierXinterval, formationDirection1));
          this.initialFreeCameraTarget = ((Vec2) ref pos).ToVec3(Utility.GetSceneHeightForAgent(((MissionBehaviour) this).get_Mission().get_Scene(), pos));
          this.initialFreeCameraPos = Vec3.op_Subtraction(Vec3.op_Addition(this.initialFreeCameraTarget, new Vec3(0.0f, 0.0f, 10f, -1f)), ((Vec2) ref formationDirection1).ToVec3(0.0f));
        }
        Vec3 initialFreeCameraPos = this.initialFreeCameraPos;
        Vec2 formationDirection3 = this._freeBattleConfig.FormationDirection;
        Vec3 vec3 = ((Vec2) ref formationDirection3).ToVec3(0.0f);
        this.initialFreeCameraPos = Vec3.op_Subtraction(initialFreeCameraPos, vec3);
      }
      float distanceToInitialPosition = 0.0f;
      for (int index = 0; index < 8; ++index)
      {
        int troopCount = this.FreeBattleConfig.playerTroops[index].troopCount;
        if (troopCount > 0)
        {
          BasicCharacterObject troopCharacter = this.FreeBattleConfig.GetPlayerTroopHeroClass(index).get_TroopCharacter();
          Formation formation = playerTeam.GetFormation((FormationClass) index);
          float num1;
          float num2;
          this.GetInitialFormationArea(index, true, troopCharacter.get_CurrentFormationClass()).Deconstruct<float, float>(out num1, out num2);
          float width = num1;
          float num3 = num2;
          this.SetFormationRegion(formation, width, true, distanceToInitialPosition);
          distanceToInitialPosition += num3;
          BasicCultureObject culture = spawnPlayer ? playerTeamCulture : troopCharacter.get_Culture();
          for (int formationTroopIndex = 0; formationTroopIndex < troopCount; ++formationTroopIndex)
            this.SpawnAgent(this.FreeBattleConfig.playerTroops[index], false, troopCharacter, false, formation, playerTeam, battleCombatant, culture, true, troopCount, formationTroopIndex, new MatrixFrame?());
        }
      }
    }

    private void SpawnEnemyTeamAgents(BasicCultureObject enemyTeamCulture)
    {
      ((MissionBehaviour) this).get_Mission().get_Scene();
      CustomBattleCombatant battleCombatant = this.CreateBattleCombatant(enemyTeamCulture, (BattleSideEnum) 0);
      Team playerEnemyTeam = ((MissionBehaviour) this).get_Mission().get_PlayerEnemyTeam();
      Vec2 formationDirection = this.FreeBattleConfig.FormationDirection;
      if (this.FreeBattleConfig.SpawnEnemyCommander)
      {
        BasicCharacterObject heroCharacter = this.FreeBattleConfig.EnemyHeroClass.get_HeroCharacter();
        Formation formation = playerEnemyTeam.GetFormation(Utility.CommanderFormationClass());
        this.SetFormationRegion(formation, 1f, false, -2f);
        this.SpawnAgent(this.FreeBattleConfig.enemyClass, false, heroCharacter, true, formation, playerEnemyTeam, battleCombatant, enemyTeamCulture, false, 1, 0, new MatrixFrame?());
      }
      float distanceToInitialPosition = 0.0f;
      for (int index = 0; index < 8; ++index)
      {
        int troopCount = this.FreeBattleConfig.enemyTroops[index].troopCount;
        if (troopCount > 0)
        {
          BasicCharacterObject troopCharacter = this.FreeBattleConfig.GetEnemyTroopHeroClass(index).get_TroopCharacter();
          Formation formation = playerEnemyTeam.GetFormation((FormationClass) index);
          float num1;
          float num2;
          this.GetInitialFormationArea(index, false, troopCharacter.get_CurrentFormationClass()).Deconstruct<float, float>(out num1, out num2);
          float width = num1;
          float num3 = num2;
          this.SetFormationRegion(formation, width, false, distanceToInitialPosition);
          distanceToInitialPosition += num3;
          BasicCultureObject culture = this.FreeBattleConfig.SpawnEnemyCommander ? enemyTeamCulture : troopCharacter.get_Culture();
          for (int formationTroopIndex = 0; formationTroopIndex < troopCount; ++formationTroopIndex)
            this.SpawnAgent(this.FreeBattleConfig.enemyTroops[index], false, troopCharacter, false, formation, playerEnemyTeam, battleCombatant, culture, false, troopCount, formationTroopIndex, new MatrixFrame?());
        }
      }
    }

    public virtual void OnMissionTick(float dt)
    {
      this.CheckVictory();
    }

    private void CheckVictory()
    {
    }

    private void OrderIssuedDelegate(
      OrderType orderType,
      IEnumerable<Formation> appliedFormations,
      params object[] delegateParams)
    {
      using (IEnumerator<Formation> enumerator1 = appliedFormations.GetEnumerator())
      {
        while (((IEnumerator) enumerator1).MoveNext())
        {
          Formation current1 = enumerator1.Current;
          if (this._victoryLogic != null)
          {
            using (List<Agent>.Enumerator enumerator2 = ((Team) current1.Team).get_ActiveAgents().GetEnumerator())
            {
              while (enumerator2.MoveNext())
              {
                Agent current2 = enumerator2.Current;
                ((MissionBehaviour) this._victoryLogic).OnAgentDeleted(current2);
                if (!current2.get_IsPlayerControlled())
                  current2.SetActionChannel(1, ActionIndexCache.get_act_none(), true, 0UL, 0.0f, 1f, -0.2f, 0.4f, 0.0f, false, -0.2f, 0, true);
              }
            }
          }
          this._makeGruntVoiceLogic?.AddFormation(current1, 1f);
        }
      }
    }

    private CustomBattleCombatant CreateBattleCombatant(
      BasicCultureObject culture,
      BattleSideEnum side)
    {
      CustomBattleCombatant customBattleCombatant = new CustomBattleCombatant(culture.get_Name(), culture, new Banner(culture.get_BannerKey(), culture.get_BackgroundColor1(), culture.get_ForegroundColor1()));
      customBattleCombatant.set_Side(side);
      return customBattleCombatant;
    }

    private Agent SpawnAgent(
      ClassInfo classInfo,
      bool isPlayer,
      BasicCharacterObject character,
      bool isHero,
      Formation formation,
      Team team,
      CustomBattleCombatant combatant,
      BasicCultureObject culture,
      bool isPlayerSide,
      int formationTroopCount,
      int formationTroopIndex,
      MatrixFrame? matrix = null)
    {
      bool flag = isPlayerSide ? this.FreeBattleConfig.isPlayerAttacker : !this.FreeBattleConfig.isPlayerAttacker;
      AgentBuildData agentBuildData = new AgentBuildData(Utility.CreateOrigin(combatant, Utility.ApplyPerks(classInfo, isHero), -1, (EnhancedTroopSupplier) null)).Team(team).Formation(formation).FormationTroopCount(formationTroopCount).FormationTroopIndex(formationTroopIndex).Banner(team.get_Banner()).ClothingColor1(flag ? culture.get_Color() : culture.get_ClothAlternativeColor()).ClothingColor2(flag ? culture.get_Color2() : culture.get_ClothAlternativeColor2());
      if (matrix.HasValue)
        agentBuildData.InitialFrame(matrix.Value);
      Agent agent = ((MissionBehaviour) this).get_Mission().SpawnAgent(agentBuildData, false, 0);
      AgentComponentExtensions.SetWatchState(agent, (AgentAIStateFlagComponent.WatchState) 2);
      agent.WieldInitialWeapons();
      agent.set_Controller(isPlayer ? (Agent.ControllerType) 2 : (Agent.ControllerType) 1);
      return agent;
    }

    private void SetFormationRegion(
      Formation formation,
      float width,
      bool isPlayerSide,
      float distanceToInitialPosition)
    {
      bool isAttackerSide = isPlayerSide ? this.FreeBattleConfig.isPlayerAttacker : !this.FreeBattleConfig.isPlayerAttacker;
      Vec3 formationPosition = this.GetFormationPosition(isAttackerSide, distanceToInitialPosition);
      Vec2 formationDirection = this.GetFormationDirection(isAttackerSide);
      formation.SetPositioning(new WorldPosition?(EngineExtensions.ToWorldPosition(formationPosition, ((MissionBehaviour) this).get_Mission().get_Scene())), new Vec2?(formationDirection), new int?());
      formation.set_FormOrder(FormOrder.FormOrderCustom(width));
    }

    private Tuple<float, float> GetInitialFormationArea(
      int formationIndex,
      bool isPlayerSide,
      FormationClass fc)
    {
      EnhancedFreeBattleConfig freeBattleConfig = this.FreeBattleConfig;
      int troopCount = isPlayerSide ? freeBattleConfig.playerTroops[formationIndex].troopCount : freeBattleConfig.enemyTroops[formationIndex].troopCount;
      return Utility.GetFormationArea(fc, troopCount, freeBattleConfig.SoldiersPerRow);
    }

    private Vec2 GetFormationDirection(bool isAttackerSide)
    {
      return isAttackerSide ? this.FreeBattleConfig.FormationDirection : Vec2.op_UnaryNegation(this.FreeBattleConfig.FormationDirection);
    }

    private Vec3 GetFormationPosition(bool isAttackerSide, float distanceToInitialPosition)
    {
      Vec2 formationDirection = this.FreeBattleConfig.FormationDirection;
      Vec2 pos = Vec2.op_Addition(this.FreeBattleConfig.FormationPosition, Vec2.op_Multiply(distanceToInitialPosition, isAttackerSide ? Vec2.op_UnaryNegation(formationDirection) : formationDirection));
      if (!isAttackerSide)
        pos = Vec2.op_Addition(pos, Vec2.op_Multiply(formationDirection, this.FreeBattleConfig.Distance));
      return ((Vec2) ref pos).ToVec3(Utility.GetSceneHeightForAgent(((MissionBehaviour) this).get_Mission().get_Scene(), pos));
    }
  }
}
