// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.EnhancedCustomBattleViews
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System.Collections.Generic;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.LegacyGUI.Missions;
using TaleWorlds.MountAndBlade.View.Missions;

namespace VirtualBattlegrounds
{
  [ViewCreatorModule]
  public class EnhancedCustomBattleViews
  {
    [ViewMethod("EnhancedCustomBattleConfig")]
    public static MissionView[] OpenCustomBattleConfig(Mission mission)
    {
      CharacterSelectionView selectionView = new CharacterSelectionView(true);
      return new MissionView[3]
      {
        (MissionView) selectionView,
        (MissionView) new EnhancedCustomBattleConfigView(selectionView),
        (MissionView) new MissionMenuView((BattleConfigBase) EnhancedCustomBattleConfig.Get())
      };
    }

    [ViewMethod("EnhancedCustomBattle")]
    public static MissionView[] OpenCustomBattleMission(Mission mission)
    {
      EnhancedCustomBattleConfig customBattleConfig = EnhancedCustomBattleConfig.Get();
      List<MissionView> missionViewList1 = new List<MissionView>();
      missionViewList1.Add((MissionView) new MissionMenuView((BattleConfigBase) customBattleConfig));
      missionViewList1.Add((MissionView) new MissionCustomBattlePreloadView());
      missionViewList1.Add(ViewCreator.CreateOptionsUIHandler());
      missionViewList1.Add(ViewCreator.CreatePlayerRoleSelectionUIHandler(mission));
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
      List<MissionView> missionViewList2 = missionViewList1;
      if (!customBattleConfig.noAgentLabel)
        missionViewList2.Add(ViewCreator.CreateMissionAgentLabelUIHandler(mission));
      if (!customBattleConfig.noKillNotification)
        missionViewList2.Add(ViewCreator.CreateSingleplayerMissionKillNotificationUIHandler());
      return missionViewList2.ToArray();
    }
  }
}
