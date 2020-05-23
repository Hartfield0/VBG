// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.MissionFreeBattlePreloadView
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View.Missions;

namespace VirtualBattlegrounds
{
  public class MissionFreeBattlePreloadView : MissionView
  {
    private EnhancedFreeBattleConfig _config;

    public MissionFreeBattlePreloadView(EnhancedFreeBattleConfig config)
    {
      this.\u002Ector();
      this._config = config;
    }

    public virtual void OnPreMissionTick(float dt)
    {
      List<BasicCharacterObject> basicCharacterObjectList = new List<BasicCharacterObject>();
      basicCharacterObjectList.Add(this._config.PlayerHeroClass.get_HeroCharacter());
      basicCharacterObjectList.Add(this._config.EnemyHeroClass.get_HeroCharacter());
      basicCharacterObjectList.Add(this._config.GetPlayerTroopHeroClass(0).get_TroopCharacter());
      basicCharacterObjectList.Add(this._config.GetPlayerTroopHeroClass(1).get_TroopCharacter());
      basicCharacterObjectList.Add(this._config.GetPlayerTroopHeroClass(2).get_TroopCharacter());
      basicCharacterObjectList.Add(this._config.GetPlayerTroopHeroClass(3).get_TroopCharacter());
      basicCharacterObjectList.Add(this._config.GetPlayerTroopHeroClass(4).get_TroopCharacter());
      basicCharacterObjectList.Add(this._config.GetPlayerTroopHeroClass(5).get_TroopCharacter());
      basicCharacterObjectList.Add(this._config.GetPlayerTroopHeroClass(6).get_TroopCharacter());
      basicCharacterObjectList.Add(this._config.GetPlayerTroopHeroClass(7).get_TroopCharacter());
      basicCharacterObjectList.Add(this._config.GetEnemyTroopHeroClass(0).get_TroopCharacter());
      basicCharacterObjectList.Add(this._config.GetEnemyTroopHeroClass(1).get_TroopCharacter());
      basicCharacterObjectList.Add(this._config.GetEnemyTroopHeroClass(2).get_TroopCharacter());
      basicCharacterObjectList.Add(this._config.GetEnemyTroopHeroClass(3).get_TroopCharacter());
      MissionPreloadHelper.PreloadCharacters(basicCharacterObjectList);
      ((MissionBehaviour) this).get_Mission().RemoveMissionBehaviour((MissionBehaviour) this);
    }
  }
}
