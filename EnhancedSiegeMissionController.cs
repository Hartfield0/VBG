// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.EnhancedSiegeMissionController
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.AI;
using TaleWorlds.MountAndBlade.Missions;
using TaleWorlds.MountAndBlade.Missions.Handlers;

namespace VirtualBattlegrounds
{
  internal class EnhancedSiegeMissionController : SiegeMissionController
  {
    private SiegeDeploymentHandler _siegeDeploymentHandler;
    private MissionBoundaryPlacer _missionBoundaryPlacer;
    private MissionAgentSpawnLogic _missionAgentSpawnLogic;
    private readonly Dictionary<Type, int> _availableSiegeWeaponsOfAttackers;
    private readonly Dictionary<Type, int> _availableSiegeWeaponsOfDefenders;
    private readonly bool _isPlayerAttacker;
    private bool _teamSetupOver;

    public EnhancedSiegeMissionController(
      Dictionary<Type, int> siegeWeaponsCountOfAttackers,
      Dictionary<Type, int> siegeWeaponsCountOfDefenders,
      bool isPlayerAttacker,
      bool isSallyOut)
    {
      base.\u002Ector(siegeWeaponsCountOfAttackers, siegeWeaponsCountOfDefenders, isPlayerAttacker, isSallyOut);
      this._availableSiegeWeaponsOfAttackers = siegeWeaponsCountOfAttackers;
      this._availableSiegeWeaponsOfDefenders = siegeWeaponsCountOfDefenders;
      this._isPlayerAttacker = isPlayerAttacker;
    }

    public virtual void OnBehaviourInitialize()
    {
      base.OnBehaviourInitialize();
      this._siegeDeploymentHandler = (SiegeDeploymentHandler) ((MissionBehaviour) this).get_Mission().GetMissionBehaviour<SiegeDeploymentHandler>();
      this._missionBoundaryPlacer = (MissionBoundaryPlacer) ((MissionBehaviour) this).get_Mission().GetMissionBehaviour<MissionBoundaryPlacer>();
      this._missionAgentSpawnLogic = (MissionAgentSpawnLogic) ((MissionBehaviour) this).get_Mission().GetMissionBehaviour<MissionAgentSpawnLogic>();
    }

    public virtual void AfterStart()
    {
      ((MissionBehaviour) this).get_Mission().AllowAiTicking = (__Null) 0;
      ((DeploymentHandler) ((MissionBehaviour) this).get_Mission().GetMissionBehaviour<DeploymentHandler>()).InitializeDeploymentPoints();
      bool isSallyOut = this.get_IsSallyOut();
      for (int index = 0; index < 2; ++index)
      {
        this._missionAgentSpawnLogic.SetSpawnHorses((BattleSideEnum) index, isSallyOut);
        this._missionAgentSpawnLogic.SetSpawnTroops((BattleSideEnum) index, false, false);
      }
    }

    private void SetupTeams()
    {
      if (this._isPlayerAttacker)
      {
        this.SetupTeam(((MissionBehaviour) this).get_Mission().get_AttackerTeam());
      }
      else
      {
        this.SetupTeam(((MissionBehaviour) this).get_Mission().get_AttackerTeam());
        this.OnTeamDeploymentFinish((BattleSideEnum) 1);
        this.SetupTeam(((MissionBehaviour) this).get_Mission().get_DefenderTeam());
      }
      this.SetupAllyTeam(((MissionBehaviour) this).get_Mission().get_PlayerAllyTeam());
      this._teamSetupOver = true;
    }

    public virtual void OnMissionTick(float dt)
    {
      if (this._teamSetupOver)
        return;
      this.SetupTeams();
      using (IEnumerator<Team> enumerator1 = ((ReadOnlyCollection<Team>) ((MissionBehaviour) this).get_Mission().get_Teams()).GetEnumerator())
      {
        while (((IEnumerator) enumerator1).MoveNext())
        {
          using (IEnumerator<Formation> enumerator2 = enumerator1.Current.get_Formations().GetEnumerator())
          {
            while (((IEnumerator) enumerator2).MoveNext())
              enumerator2.Current.get_QuerySystem().EvaluateAllPreliminaryQueryData();
          }
        }
      }
    }

    [Conditional("DEBUG")]
    private void DebugTick()
    {
      if (!Input.get_DebugInput().IsHotKeyPressed("SwapToEnemy"))
        return;
      ((MissionBehaviour) this).get_Mission().get_MainAgent().set_Controller((Agent.ControllerType) 1);
      ((MissionBehaviour) this).get_Mission().get_PlayerEnemyTeam().get_Leader().set_Controller((Agent.ControllerType) 2);
      this.SwapTeams();
    }

    private void SwapTeams()
    {
      ((MissionBehaviour) this).get_Mission().set_PlayerTeam(((MissionBehaviour) this).get_Mission().get_PlayerEnemyTeam());
    }

    private void SetupAllyTeam(Team team)
    {
      if (team == null)
        return;
      using (IEnumerator<Formation> enumerator = team.get_FormationsIncludingSpecial().GetEnumerator())
      {
        while (((IEnumerator) enumerator).MoveNext())
          enumerator.Current.set_IsAIControlled(true);
      }
      team.ExpireAIQuerySystem();
    }

    private void RemoveUnavailableDeploymentPoints(BattleSideEnum side)
    {
      SiegeWeaponCollection weapons = new SiegeWeaponCollection(side == 1 ? this._availableSiegeWeaponsOfAttackers : this._availableSiegeWeaponsOfDefenders);
      foreach (DeploymentPoint deploymentPoint in ((IEnumerable<DeploymentPoint>) MBExtensions.FindAllWithType<DeploymentPoint>(((MissionBehaviour) this).get_Mission().get_ActiveMissionObjects())).Where<DeploymentPoint>((Func<DeploymentPoint, bool>) (dp => dp.Side == side)).ToArray<DeploymentPoint>())
      {
        if (!deploymentPoint.get_DeployableWeaponTypes().Any<Type>((Func<Type, bool>) (wt => weapons.GetMaxDeployableWeaponCount(wt) > 0)))
        {
          using (IEnumerator<SiegeWeapon> enumerator = deploymentPoint.get_DeployableWeapons().Select<SynchedMissionObject, SiegeWeapon>((Func<SynchedMissionObject, SiegeWeapon>) (sw => sw as SiegeWeapon)).GetEnumerator())
          {
            while (((IEnumerator) enumerator).MoveNext())
              ((SynchedMissionObject) enumerator.Current).SetDisabledSynched();
          }
          ((SynchedMissionObject) deploymentPoint).SetDisabledSynched();
        }
      }
    }

    private void SetupTeam(Team team)
    {
      BattleSideEnum side = team.get_Side();
      ((DeploymentHandler) this._siegeDeploymentHandler).SetDeploymentBoundary(side);
      if (team == ((MissionBehaviour) this).get_Mission().get_PlayerTeam())
      {
        this.RemoveUnavailableDeploymentPoints(side);
        using (IEnumerator<DeploymentPoint> enumerator = ((IEnumerable<DeploymentPoint>) MBExtensions.FindAllWithType<DeploymentPoint>(((MissionBehaviour) this).get_Mission().get_ActiveMissionObjects())).Where<DeploymentPoint>((Func<DeploymentPoint, bool>) (dp => !((MissionObject) dp).get_IsDisabled() && dp.Side == side)).GetEnumerator())
        {
          while (((IEnumerator) enumerator).MoveNext())
            enumerator.Current.Show();
        }
      }
      else
        this.DeploySiegeWeaponsByAi(side);
      this._missionAgentSpawnLogic.SetSpawnTroops(side, true, true);
      using (IEnumerator<Agent> enumerator = team.get_FormationsIncludingSpecial().SelectMany<Formation, Agent>((Func<Formation, IEnumerable<Agent>>) (f => (IEnumerable<Agent>) f.Team)).Where<Agent>((Func<Agent, bool>) (u => u.get_IsAIControlled())).GetEnumerator())
      {
        while (((IEnumerator) enumerator).MoveNext())
          AgentComponentExtensions.SetIsAIPaused(enumerator.Current, true);
      }
    }

    private void DeploySiegeWeaponsByAi(BattleSideEnum side)
    {
      new SiegeWeaponDeploymentAI(((IEnumerable<DeploymentPoint>) MBExtensions.FindAllWithType<DeploymentPoint>(((MissionBehaviour) this).get_Mission().get_ActiveMissionObjects())).Where<DeploymentPoint>((Func<DeploymentPoint, bool>) (dp => dp.Side == side)).ToList<DeploymentPoint>(), side == 1 ? this._availableSiegeWeaponsOfAttackers : this._availableSiegeWeaponsOfDefenders).DeployAll(((MissionBehaviour) this).get_Mission(), side);
      this.RemoveDeploymentPoints(side);
    }

    private void RemoveDeploymentPoints(BattleSideEnum side)
    {
      foreach (DeploymentPoint deploymentPoint in ((IEnumerable<DeploymentPoint>) MBExtensions.FindAllWithType<DeploymentPoint>(((MissionBehaviour) this).get_Mission().get_ActiveMissionObjects())).Where<DeploymentPoint>((Func<DeploymentPoint, bool>) (dp => dp.Side == side)).ToArray<DeploymentPoint>())
      {
        foreach (SynchedMissionObject synchedMissionObject in deploymentPoint.get_DeployableWeapons().ToArray<SynchedMissionObject>())
        {
          SiegeWeapon siegeWeapon;
          int num;
          if (deploymentPoint.get_DeployedWeapon() == null || !((ScriptComponentBehaviour) synchedMissionObject).get_GameEntity().IsVisibleIncludeParents())
          {
            siegeWeapon = synchedMissionObject as SiegeWeapon;
            num = siegeWeapon != null ? 1 : 0;
          }
          else
            num = 0;
          if (num != 0)
            ((SynchedMissionObject) siegeWeapon).SetDisabledSynched();
        }
        ((SynchedMissionObject) deploymentPoint).SetDisabledSynched();
      }
    }

    private void OnTeamDeploymentFinish(BattleSideEnum side)
    {
      this.RemoveDeploymentPoints(side);
      using (List<SiegeLadder>.Enumerator enumerator = ((IEnumerable<SiegeLadder>) MBExtensions.FindAllWithType<SiegeLadder>(((MissionBehaviour) this).get_Mission().get_ActiveMissionObjects())).Where<SiegeLadder>((Func<SiegeLadder, bool>) (sl => !((ScriptComponentBehaviour) sl).get_GameEntity().IsVisibleIncludeParents())).ToList<SiegeLadder>().GetEnumerator())
      {
        while (enumerator.MoveNext())
          ((SynchedMissionObject) enumerator.Current).SetDisabledSynched();
      }
      Team team = side == 1 ? ((MissionBehaviour) this).get_Mission().get_AttackerTeam() : ((MissionBehaviour) this).get_Mission().get_DefenderTeam();
      if (side != ((MissionBehaviour) this).get_Mission().get_PlayerTeam().get_Side())
        this.DeployFormationsOfTeam(team);
      ((DeploymentHandler) this._siegeDeploymentHandler).RemoveAllBoundaries();
      this._missionBoundaryPlacer.AddBoundaries();
      using (List<Formation>.Enumerator enumerator = (side == 1 ? ((MissionBehaviour) this).get_Mission().get_AttackerTeam() : ((MissionBehaviour) this).get_Mission().get_DefenderTeam()).get_FormationsIncludingSpecialAndEmpty().GetEnumerator())
      {
        while (enumerator.MoveNext())
          enumerator.Current.set_IsAIControlled(true);
      }
    }

    private void DeployFormationsOfTeam(Team team)
    {
      using (IEnumerator<Formation> enumerator = team.get_Formations().GetEnumerator())
      {
        while (((IEnumerator) enumerator).MoveNext())
          enumerator.Current.set_IsAIControlled(true);
      }
      ((MissionBehaviour) this).get_Mission().AllowAiTicking = (__Null) 1;
      ((MissionBehaviour) this).get_Mission().set_ForceTickOccasionally(true);
      team.ResetTactic();
      bool teleportingAgents = ((MissionBehaviour) this).get_Mission().get_IsTeleportingAgents();
      ((MissionBehaviour) this).get_Mission().set_IsTeleportingAgents(true);
      team.Tick(0.0f);
      ((MissionBehaviour) this).get_Mission().set_IsTeleportingAgents(teleportingAgents);
      ((MissionBehaviour) this).get_Mission().AllowAiTicking = (__Null) 0;
      ((MissionBehaviour) this).get_Mission().set_ForceTickOccasionally(false);
    }
  }
}
