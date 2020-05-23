// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.AddEntityLogic
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace VirtualBattlegrounds
{
  public class AddEntityLogic : MissionLogic
  {
    private EnhancedSiegeBattleConfig _config;

    public AddEntityLogic(EnhancedSiegeBattleConfig config)
    {
      this._config = config;
    }

    public override void EarlyStart()
    {
      EarlyStart();
      this.AddEntity("attacker", this._config.FormationPosition, this._config.FormationDirection, ((IEnumerable<ClassInfo>) this._config.playerTroops).Select<ClassInfo, int>((Func<ClassInfo, int>) (info => info.troopCount)).ToArray<int>(), this._config.SoldiersPerRow);
      this.AddEntity("defender", this._config.FormationPosition, this._config.Distance * this._config.FormationDirection - this._config.FormationDirection, ((IEnumerable<ClassInfo>) this._config.enemyTroops).Select<ClassInfo, int>((Func<ClassInfo, int>) (info => info.troopCount)).ToArray<int>(), this._config.SoldiersPerRow);
    }

    private void AddEntity(
      string sideString,
      Vec2 position,
      Vec2 direction,
      int[] troopCount,
      int soldiersPerRow)
    {
      GameEntity.Instantiate(Mission.Scene, "sp_" + sideString + "_horsearcher", Utility.ToMatrixFrame(Mission.Scene, position + 2f * direction, direction));
      GameEntity.Instantiate(Mission.Scene, "sp_" + sideString + "_infantry", Utility.ToMatrixFrame(Mission.Scene, position, direction));
      Tuple<float, float> formationArea1 = Utility.GetFormationArea((FormationClass) 0, troopCount[0], soldiersPerRow);
      GameEntity.Instantiate(Mission.Scene, "sp_" + sideString + "_archer", Utility.ToMatrixFrame(Mission.Scene, position - direction * formationArea1.Item2, direction));
      Tuple<float, float> formationArea2 = Utility.GetFormationArea((FormationClass) 1, troopCount[1], soldiersPerRow);
      GameEntity.Instantiate(Mission.Scene, "sp_" + sideString + "_cavalry", Utility.ToMatrixFrame(Mission.Scene, position - direction * (formationArea1.Item2 + formationArea2.Item2), direction));
    }
  }
}
