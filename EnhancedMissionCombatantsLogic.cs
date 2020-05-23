// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.EnhancedMissionCombatantsLogic
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace VirtualBattlegrounds
{
  internal class EnhancedMissionCombatantsLogic : MissionCombatantsLogic
  {
    private Mission.MissionTeamAITypeEnum _teamAIType;
    private IEnumerable<IBattleCombatant> _battleCombatants;
    private BattleConfigBase _config;

    public EnhancedMissionCombatantsLogic(
      IEnumerable<IBattleCombatant> battleCombatants,
      IBattleCombatant playerBattleCombatant,
      IBattleCombatant defenderLeaderBattleCombatant,
      IBattleCombatant attackerLeaderBattleCombatant,
      Mission.MissionTeamAITypeEnum teamAIType,
      bool isPlayerSergeant,
      BattleConfigBase config)
    {
      this.\u002Ector(battleCombatants, playerBattleCombatant, defenderLeaderBattleCombatant, attackerLeaderBattleCombatant, teamAIType, isPlayerSergeant);
      this._teamAIType = teamAIType;
      if (battleCombatants == null)
        battleCombatants = (IEnumerable<IBattleCombatant>) new IBattleCombatant[2]
        {
          defenderLeaderBattleCombatant,
          attackerLeaderBattleCombatant
        };
      this._battleCombatants = battleCombatants;
      this._config = config;
    }

    public virtual void EarlyStart()
    {
      ((MissionBehaviour) this).get_Mission().set_MissionTeamAIType(this._teamAIType);
      switch (this._teamAIType - 1)
      {
        case 0:
          using (IEnumerator<Team> enumerator = ((ReadOnlyCollection<Team>) ((MissionBehaviour) this).get_Mission().get_Teams()).GetEnumerator())
          {
            while (((IEnumerator) enumerator).MoveNext())
            {
              Team current = enumerator.Current;
              current.AddTeamAI((TeamAIComponent) new TeamAIGeneral(((MissionBehaviour) this).get_Mission(), current, 10f, 1f), false);
            }
            break;
          }
        case 1:
          using (IEnumerator<Team> enumerator = ((ReadOnlyCollection<Team>) ((MissionBehaviour) this).get_Mission().get_Teams()).GetEnumerator())
          {
            while (((IEnumerator) enumerator).MoveNext())
            {
              Team current = enumerator.Current;
              if (current.get_Side() == 1)
                current.AddTeamAI((TeamAIComponent) new TeamAISiegeAttacker(((MissionBehaviour) this).get_Mission(), current, 5f, 1f), false);
              if (current.get_Side() == 0)
                current.AddTeamAI((TeamAIComponent) new TeamAISiegeDefender(((MissionBehaviour) this).get_Mission(), current, 5f, 1f), false);
            }
            break;
          }
        case 2:
          using (IEnumerator<Team> enumerator = ((ReadOnlyCollection<Team>) ((MissionBehaviour) this).get_Mission().get_Teams()).GetEnumerator())
          {
            while (((IEnumerator) enumerator).MoveNext())
            {
              Team current = enumerator.Current;
              if (current.get_Side() == 1)
                current.AddTeamAI((TeamAIComponent) new TeamAISallyOutDefender(((MissionBehaviour) this).get_Mission(), current, 5f, 1f), false);
              else
                current.AddTeamAI((TeamAIComponent) new TeamAISallyOutAttacker(((MissionBehaviour) this).get_Mission(), current, 5f, 1f), false);
            }
            break;
          }
      }
      if (!((IEnumerable<Team>) ((MissionBehaviour) this).get_Mission().get_Teams()).Any<Team>())
        return;
      switch ((int) ((MissionBehaviour) this).get_Mission().get_MissionTeamAIType())
      {
        case 0:
          using (IEnumerator<Team> enumerator = ((IEnumerable<Team>) ((MissionBehaviour) this).get_Mission().get_Teams()).Where<Team>((Func<Team, bool>) (t => t.get_HasTeamAi())).GetEnumerator())
          {
            while (((IEnumerator) enumerator).MoveNext())
            {
              Team current = enumerator.Current;
              current.AddTacticOption((TacticComponent) new TacticCharge(current));
            }
            break;
          }
        case 1:
        case 2:
          using (IEnumerator<Team> enumerator = ((IEnumerable<Team>) ((MissionBehaviour) this).get_Mission().get_Teams()).Where<Team>((Func<Team, bool>) (t => t.get_HasTeamAi())).GetEnumerator())
          {
            while (((IEnumerator) enumerator).MoveNext())
            {
              Team current = enumerator.Current;
              if (current.get_Side() == 0)
              {
                bool flag = false;
                foreach (TacticOptionInfo defenderTacticOption in this._config.defenderTacticOptions)
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
                foreach (TacticOptionInfo attackerTacticOption in this._config.attackerTacticOptions)
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
            break;
          }
        case 3:
          using (IEnumerator<Team> enumerator = ((IEnumerable<Team>) ((MissionBehaviour) this).get_Mission().get_Teams()).Where<Team>((Func<Team, bool>) (t => t.get_HasTeamAi())).GetEnumerator())
          {
            while (((IEnumerator) enumerator).MoveNext())
            {
              Team current = enumerator.Current;
              if (current.get_Side() == 0)
                current.AddTacticOption((TacticComponent) new TacticSallyOutHitAndRun(current));
              if (current.get_Side() == 1)
                current.AddTacticOption((TacticComponent) new TacticSallyOutDefense(current));
              current.AddTacticOption((TacticComponent) new TacticCharge(current));
            }
            break;
          }
      }
      using (IEnumerator<Team> enumerator = ((ReadOnlyCollection<Team>) ((MissionBehaviour) this).get_Mission().get_Teams()).GetEnumerator())
      {
        while (((IEnumerator) enumerator).MoveNext())
        {
          Team current = enumerator.Current;
          current.ExpireAIQuerySystem();
          current.ResetTactic();
        }
      }
    }
  }
}
