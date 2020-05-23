// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.EnhancedBattleEndLogic
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Missions.Handlers;
using TaleWorlds.MountAndBlade.Source.Missions;

namespace VirtualBattlegrounds
{
  internal class EnhancedBattleEndLogic : MissionLogic, IBattleEndLogic
  {
    private MissionTime _enemiesNotYetRetreatingTime;
    private BasicTimer _checkRetreatingTimer;
    private BattleConfigBase _config;
    private bool _shouldCelebrate;
    private bool _isEnemySideRetreating;
    private bool _isEnemySideDepleted;
    private bool _isPlayerSideDepleted;
    private bool _canCheckForEndCondition;
    private bool _missionEndedMessageShown;
    private bool _victoryReactionsActivated;
    private bool _victoryReactionsActivatedForRetreating;
    private bool _scoreBoardOpenedOnceOnMissionEnd;

    public bool PlayerVictory
    {
      get
      {
        return this._isEnemySideRetreating || this._isEnemySideDepleted;
      }
    }

    public bool EnemyVictory
    {
      get
      {
        return this._isPlayerSideDepleted;
      }
    }

    private bool _notificationsDisabled { get; set; }

    public EnhancedBattleEndLogic(BattleConfigBase config)
    {
      this.\u002Ector();
      this._config = config;
    }

    public virtual bool MissionEnded(ref MissionResult missionResult)
    {
      bool flag = false;
      if (this._isEnemySideRetreating || this._isEnemySideDepleted)
      {
        missionResult = MissionResult.CreateSuccessful((IMission) ((MissionBehaviour) this).get_Mission());
        flag = true;
      }
      else if (this._isPlayerSideDepleted)
      {
        missionResult = MissionResult.CreateDefeated((IMission) ((MissionBehaviour) this).get_Mission());
        flag = true;
      }
      return flag;
    }

    public virtual void OnClearScene()
    {
      ((MissionBehaviour) this).OnClearScene();
      ((MissionBehaviour) this).OnBehaviourInitialize();
    }

    public virtual void OnMissionTick(float dt)
    {
      if (this._shouldCelebrate && ((MissionBehaviour) this).get_Mission().get_IsMissionEnding())
      {
        if (this._notificationsDisabled)
          this._scoreBoardOpenedOnceOnMissionEnd = true;
        if (this._missionEndedMessageShown && !this._scoreBoardOpenedOnceOnMissionEnd)
        {
          if ((double) this._checkRetreatingTimer.get_ElapsedTime() > 7.0)
          {
            this.CheckIsEnemySideRetreatingOrOneSideDepleted();
            this._checkRetreatingTimer.Reset();
            if (((MissionBehaviour) this).get_Mission().get_MissionResult() != null && ((MissionBehaviour) this).get_Mission().get_MissionResult().get_PlayerDefeated())
            {
              GameTexts.SetVariable("leave_key", GameKeyTextExtensions.GetHotKeyGameText(Game.get_Current().get_GameTextManager(), "CombatHotKeyCategory", 4));
              InformationManager.AddQuickInformation(GameTexts.FindText("str_battle_lost_press_tab_to_view_results", (string) null), 0, (BasicCharacterObject) null, "");
            }
            else if (((MissionBehaviour) this).get_Mission().get_MissionResult() != null && ((MissionBehaviour) this).get_Mission().get_MissionResult().get_PlayerVictory())
            {
              if (this._isEnemySideDepleted)
              {
                GameTexts.SetVariable("leave_key", GameKeyTextExtensions.GetHotKeyGameText(Game.get_Current().get_GameTextManager(), "CombatHotKeyCategory", 4));
                InformationManager.AddQuickInformation(GameTexts.FindText("str_battle_won_press_tab_to_view_results", (string) null), 0, (BasicCharacterObject) null, "");
              }
            }
            else
            {
              GameTexts.SetVariable("leave_key", GameKeyTextExtensions.GetHotKeyGameText(Game.get_Current().get_GameTextManager(), "CombatHotKeyCategory", 4));
              InformationManager.AddQuickInformation(GameTexts.FindText("str_battle_finished_press_tab_to_view_results", (string) null), 0, (BasicCharacterObject) null, "");
            }
          }
        }
        else if ((double) this._checkRetreatingTimer.get_ElapsedTime() > 3.0 && !this._scoreBoardOpenedOnceOnMissionEnd)
        {
          if (((MissionBehaviour) this).get_Mission().get_MissionResult() != null && ((MissionBehaviour) this).get_Mission().get_MissionResult().get_PlayerDefeated())
            InformationManager.AddQuickInformation(GameTexts.FindText("str_battle_lost", (string) null), 0, (BasicCharacterObject) null, "");
          else if (((MissionBehaviour) this).get_Mission().get_MissionResult() != null && ((MissionBehaviour) this).get_Mission().get_MissionResult().get_PlayerVictory())
          {
            if (this._isEnemySideDepleted)
              InformationManager.AddQuickInformation(GameTexts.FindText("str_battle_won", (string) null), 0, (BasicCharacterObject) null, "");
            else if (this._isEnemySideRetreating)
              InformationManager.AddQuickInformation(GameTexts.FindText("str_enemies_are_fleeing_you_won", (string) null), 0, (BasicCharacterObject) null, "");
          }
          else
            InformationManager.AddQuickInformation(GameTexts.FindText("str_battle_finished", (string) null), 0, (BasicCharacterObject) null, "");
          this._missionEndedMessageShown = true;
          this._checkRetreatingTimer.Reset();
        }
        if (this._victoryReactionsActivated)
          return;
        AgentVictoryLogic missionBehaviour = (AgentVictoryLogic) ((MissionBehaviour) this).get_Mission().GetMissionBehaviour<AgentVictoryLogic>();
        if (missionBehaviour == null)
          return;
        this.CheckIsEnemySideRetreatingOrOneSideDepleted();
        if (this._isEnemySideDepleted)
        {
          missionBehaviour.SetTimersOfVictoryReactions(((MissionBehaviour) this).get_Mission().get_PlayerTeam().get_Side());
          this._victoryReactionsActivated = true;
        }
        else if (this._isPlayerSideDepleted)
        {
          missionBehaviour.SetTimersOfVictoryReactions(((MissionBehaviour) this).get_Mission().get_PlayerEnemyTeam().get_Side());
          this._victoryReactionsActivated = true;
        }
        else
        {
          if (!this._isEnemySideRetreating || this._victoryReactionsActivatedForRetreating)
            return;
          missionBehaviour.SetTimersOfVictoryReactionsForRetreating(((MissionBehaviour) this).get_Mission().get_PlayerTeam().get_Side());
          this._victoryReactionsActivatedForRetreating = true;
        }
      }
      else
      {
        if ((double) this._checkRetreatingTimer.get_ElapsedTime() <= 1.0)
          return;
        this.CheckIsEnemySideRetreatingOrOneSideDepleted();
        this._checkRetreatingTimer.Reset();
      }
    }

    private void CheckIsEnemySideRetreatingOrOneSideDepleted()
    {
      if (!this._canCheckForEndCondition)
      {
        this._canCheckForEndCondition = ((MissionBehaviour) this).get_Mission().GetMissionBehaviour<SiegeDeploymentHandler>() == null;
      }
      else
      {
        BattleSideEnum side = ((MissionBehaviour) this).get_Mission().get_PlayerTeam().get_Side();
        this._isPlayerSideDepleted = ((MissionBehaviour) this).get_Mission().GetMemberCountOfSide(side) == 0;
        this._isEnemySideDepleted = ((MissionBehaviour) this).get_Mission().GetMemberCountOfSide(Extensions.GetOppositeSide(side)) == 0;
        if (this._isEnemySideDepleted || this._isPlayerSideDepleted || ((MissionBehaviour) this).get_Mission().GetMissionBehaviour<HideoutPhasedMissionController>() != null)
          return;
        bool flag = true;
        using (IEnumerator<Team> enumerator1 = ((ReadOnlyCollection<Team>) ((MissionBehaviour) this).get_Mission().get_Teams()).GetEnumerator())
        {
          while (((IEnumerator) enumerator1).MoveNext())
          {
            Team current = enumerator1.Current;
            if (current.IsEnemyOf(((MissionBehaviour) this).get_Mission().get_PlayerTeam()))
            {
              using (List<Agent>.Enumerator enumerator2 = current.get_ActiveAgents().GetEnumerator())
              {
                while (enumerator2.MoveNext())
                {
                  if (!enumerator2.Current.get_IsRunningAway())
                  {
                    flag = false;
                    break;
                  }
                }
              }
            }
          }
        }
        if (!flag)
          this._enemiesNotYetRetreatingTime = MissionTime.get_Now();
        if ((double) ((MissionTime) ref this._enemiesNotYetRetreatingTime).get_ElapsedSeconds() <= 3.0)
          return;
        this._isEnemySideRetreating = true;
      }
    }

    public BattleEndLogic.ExitResult TryExit()
    {
      if (GameNetwork.get_IsClientOrReplay() || ((MissionBehaviour) this).get_Mission().get_MainAgent() != null && ((MissionBehaviour) this).get_Mission().get_MainAgent().IsActive() && ((MissionBehaviour) this).get_Mission().IsPlayerCloseToAnEnemy(5f) || !((MissionBehaviour) this).get_Mission().MissionEnded() && (this.PlayerVictory || this.EnemyVictory))
        return (BattleEndLogic.ExitResult) 0;
      if (!((MissionBehaviour) this).get_Mission().MissionEnded() && !this._isEnemySideRetreating)
        return (BattleEndLogic.ExitResult) 1;
      ((MissionBehaviour) this).get_Mission().EndMission();
      return (BattleEndLogic.ExitResult) 2;
    }

    public virtual void OnBehaviourInitialize()
    {
      ((MissionBehaviour) this).OnBehaviourInitialize();
      this._enemiesNotYetRetreatingTime = (MissionTime) null;
      this._checkRetreatingTimer = new BasicTimer((MBCommon.TimeType) 1);
      this._shouldCelebrate = this._config.ShouldCelebrateVictory;
      this._isEnemySideRetreating = false;
      this._isEnemySideDepleted = false;
      this._isPlayerSideDepleted = false;
      this._canCheckForEndCondition = false;
      this._missionEndedMessageShown = false;
      this._victoryReactionsActivated = false;
      this._victoryReactionsActivatedForRetreating = false;
      this._scoreBoardOpenedOnceOnMissionEnd = false;
    }

    protected virtual void OnEndMission()
    {
      if (!this._isEnemySideRetreating)
        return;
      using (List<Agent>.Enumerator enumerator = ((MissionBehaviour) this).get_Mission().get_PlayerEnemyTeam().get_ActiveAgents().GetEnumerator())
      {
        while (enumerator.MoveNext())
          enumerator.Current.get_Origin()?.SetRouted();
      }
    }

    public bool IsEnemySideRetreating
    {
      get
      {
        return this._isEnemySideRetreating;
      }
    }

    public void SetNotificationDisabled(bool value)
    {
      this._notificationsDisabled = value;
    }

    public enum ExitResult
    {
      False,
      NeedsPlayerConfirmation,
      True,
    }
  }
}
