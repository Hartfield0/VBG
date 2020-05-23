// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.TacticOptionHelper
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace VirtualBattlegrounds
{
  public class TacticOptionHelper
  {
    public static Type GetTacticComponentType(TacticOptionEnum tacticOptionEnum)
    {
      switch (tacticOptionEnum)
      {
        case TacticOptionEnum.Charge:
          return typeof (TacticCharge);
        case TacticOptionEnum.FullScaleAttack:
          return typeof (TacticFullScaleAttack);
        case TacticOptionEnum.DefensiveEngagement:
          return typeof (TacticDefensiveEngagement);
        case TacticOptionEnum.DefensiveLine:
          return typeof (TacticDefensiveLine);
        case TacticOptionEnum.DefensiveRing:
          return typeof (TacticDefensiveRing);
        case TacticOptionEnum.HoldTheHill:
          return typeof (TacticHoldTheHill);
        case TacticOptionEnum.HoldChokePoint:
          return typeof (TacticHoldChokePoint);
        case TacticOptionEnum.ArchersOnTheHill:
          return typeof (TacticArchersOnTheHill);
        case TacticOptionEnum.RangedHarassmentOffensive:
          return typeof (TacticRangedHarrassmentOffensive);
        case TacticOptionEnum.FrontalCavalryCharge:
          return typeof (TacticFrontalCavalryCharge);
        case TacticOptionEnum.CoordinatedRetreat:
          return typeof (TacticCoordinatedRetreat);
        case TacticOptionEnum.BreachWalls:
          return typeof (TacticBreachWalls);
        case TacticOptionEnum.DefendCastle:
          return typeof (TacticDefendCastle);
        default:
          return (Type) null;
      }
    }

    public static void AddTacticComponent(
      Team team,
      TacticOptionEnum tacticOptionEnum,
      bool displayMessage = false)
    {
      if (displayMessage)
      {
        MBTextManager.SetTextVariable("CurrentTacticOption", GameTexts.FindText("str_tactic_option", tacticOptionEnum.ToString()), false);
        MBTextManager.SetTextVariable("CurrentSide", GameTexts.FindText("str_side", team.get_Side().ToString()), false);
        Utility.DisplayLocalizedText("str_add_tactic", (string) null);
      }
      team.AddTacticOption((TacticComponent) Activator.CreateInstance(TacticOptionHelper.GetTacticComponentType(tacticOptionEnum), (object) team));
      team.ExpireAIQuerySystem();
      team.ResetTactic();
    }

    public static void RemoveTacticComponent(
      Team team,
      TacticOptionEnum tacticOptionEnum,
      bool displayMessage = false)
    {
      if (displayMessage)
      {
        MBTextManager.SetTextVariable("CurrentTacticOption", GameTexts.FindText("str_tactic_option", tacticOptionEnum.ToString()), false);
        MBTextManager.SetTextVariable("CurrentSide", GameTexts.FindText("str_side", team.get_Side().ToString()), false);
        Utility.DisplayLocalizedText("str_remove_tactic", (string) null);
      }
      team.RemoveTacticOption(TacticOptionHelper.GetTacticComponentType(tacticOptionEnum));
      team.ExpireAIQuerySystem();
      team.ResetTactic();
    }
  }
}
