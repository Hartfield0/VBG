// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.EnhancedSiegeBattleViews
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System;
using System.Collections.Generic;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.LegacyGUI.Missions;
using TaleWorlds.MountAndBlade.View.Missions;

namespace VirtualBattlegrounds
{
  [ViewCreatorModule]
  public class EnhancedSiegeBattleViews
  {
    [ViewMethod("EnhancedSiegeBattleConfig")]
    public static MissionView[] OpenConfigMission(Mission mission)
    {
      CharacterSelectionView selectionView = new CharacterSelectionView(true);
      return new MissionView[3]
      {
        (MissionView) selectionView,
        (MissionView) new EnhancedSiegeBattleConfigView(selectionView),
        (MissionView) new MissionMenuView((BattleConfigBase) EnhancedSiegeBattleConfig.Get())
      };
    }

    [ViewMethod("EnhancedSiegeBattle")]
    public static MissionView[] OpenSiegeMission(Mission mission)
    {
      EnhancedSiegeBattleConfig siegeBattleConfig = EnhancedSiegeBattleConfig.Get();
      List<MissionView> missionViewList1 = new List<MissionView>();
      missionViewList1.Add((MissionView) new MissionMenuView((BattleConfigBase) siegeBattleConfig));
      missionViewList1.Add((MissionView) new MissionCustomBattlePreloadView());
      missionViewList1.Add(ViewCreator.CreateOptionsUIHandler());
      missionViewList1.Add((MissionView) new MissionEntitySelectionUIHandler((Action<GameEntity>) null, (Action<GameEntity>) null));
      missionViewList1.Add((MissionView) new MissionBoundaryMarker((IEntityFactory) new FlagFactory("flag_rope_cloth"), 2f));
      missionViewList1.Add((MissionView) new SiegeDeploymentVisualizationMissionView());
      missionViewList1.Add((MissionView) new SiegeMissionView());
      missionViewList1.Add(ViewCreator.CreateMissionAgentStatusUIHandler(mission));
      missionViewList1.Add(ViewCreator.CreateMissionMainAgentEquipmentController(mission));
      missionViewList1.Add(ViewCreator.CreateMissionLeaveView());
      missionViewList1.Add(ViewCreator.CreateMissionSingleplayerEscapeMenu());
      missionViewList1.Add((MissionView) new SwitchTeamMissionOrderUIHandler());
      missionViewList1.Add((MissionView) new SwitchTeamOrderTroopPlacer());
      missionViewList1.Add((MissionView) new MissionItemContourControllerView());
      missionViewList1.Add((MissionView) new MissionAgentContourControllerView());
      missionViewList1.Add(ViewCreator.CreateMissionBoundaryCrossingView());
      missionViewList1.Add((MissionView) new MissionBoundaryWallView());
      missionViewList1.Add((MissionView) new SpectatorCameraView());
      missionViewList1.Add((MissionView) new InitializeCameraPosView(siegeBattleConfig.isPlayerAttacker ? siegeBattleConfig.FormationPosition : Vec2.op_Addition(siegeBattleConfig.FormationPosition, Vec2.op_Multiply(siegeBattleConfig.FormationDirection, siegeBattleConfig.Distance)), siegeBattleConfig.isPlayerAttacker ? siegeBattleConfig.FormationDirection : Vec2.op_UnaryNegation(siegeBattleConfig.FormationDirection)));
      List<MissionView> missionViewList2 = missionViewList1;
      if (!siegeBattleConfig.noAgentLabel)
        missionViewList2.Add(ViewCreator.CreateMissionAgentLabelUIHandler(mission));
      if (!siegeBattleConfig.noKillNotification)
        missionViewList2.Add(ViewCreator.CreateSingleplayerMissionKillNotificationUIHandler());
      return missionViewList2.ToArray();
    }
  }
}
