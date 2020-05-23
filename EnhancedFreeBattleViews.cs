// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.EnhancedFreeBattleViews
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System.Collections.Generic;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.LegacyGUI.Missions;
using TaleWorlds.MountAndBlade.View.Missions;

namespace VirtualBattlegrounds
{
  [ViewCreatorModule]
  public class EnhancedFreeBattleViews
  {
    [ViewMethod("EnhancedFreeBattleConfig")]
    public static MissionView[] OpenInitialMission(Mission mission)
    {
      CharacterSelectionView selectionView = new CharacterSelectionView(true);
      return new MissionView[3]
      {
        (MissionView) selectionView,
        (MissionView) new EnhancedFreeBattleConfigView(selectionView),
        (MissionView) new MissionMenuView((BattleConfigBase) EnhancedFreeBattleConfig.Get())
      };
    }

    [ViewMethod("EnhancedFreeBattle")]
    public static MissionView[] OpenTestMission(Mission mission)
    {
      EnhancedFreeBattleConfig config = EnhancedFreeBattleConfig.Get();
      List<MissionView> missionViewList1 = new List<MissionView>();
      missionViewList1.Add((MissionView) new MissionMenuView((BattleConfigBase) config));
      missionViewList1.Add((MissionView) new MissionFreeBattlePreloadView(config));
      missionViewList1.Add(ViewCreator.CreateMissionAgentStatusUIHandler(mission));
      missionViewList1.Add(ViewCreator.CreateMissionMainAgentEquipmentController(mission));
      missionViewList1.Add(ViewCreator.CreateMissionLeaveView());
      missionViewList1.Add(ViewCreator.CreateMissionSingleplayerEscapeMenu());
      missionViewList1.Add((MissionView) new SwitchTeamMissionOrderUIHandler());
      missionViewList1.Add((MissionView) new SwitchTeamOrderTroopPlacer());
      missionViewList1.Add((MissionView) new MissionItemContourControllerView());
      missionViewList1.Add((MissionView) new MissionAgentContourControllerView());
      missionViewList1.Add(ViewCreator.CreateOptionsUIHandler());
      missionViewList1.Add((MissionView) new SpectatorCameraView());
      missionViewList1.Add((MissionView) new InitializeCameraPosView(config.isPlayerAttacker ? config.FormationPosition : Vec2.op_Addition(config.FormationPosition, Vec2.op_Multiply(config.FormationDirection, config.Distance)), config.isPlayerAttacker ? config.FormationDirection : Vec2.op_UnaryNegation(config.FormationDirection)));
      List<MissionView> missionViewList2 = missionViewList1;
      if (config.hasBoundary)
      {
        missionViewList2.Add(ViewCreator.CreateMissionBoundaryCrossingView());
        missionViewList2.Add((MissionView) new MissionBoundaryWallView());
      }
      if (!config.noAgentLabel)
        missionViewList2.Add(ViewCreator.CreateMissionAgentLabelUIHandler(mission));
      if (!config.noKillNotification)
        missionViewList2.Add(ViewCreator.CreateSingleplayerMissionKillNotificationUIHandler());
      return missionViewList2.ToArray();
    }
  }
}
