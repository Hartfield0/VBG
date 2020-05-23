// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.SpawnPlayerLogic
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace VirtualBattlegrounds
{
  public class SpawnPlayerLogic : MissionLogic
  {
    private CustomBattleCombatant _playerParty;
    private EnhancedTroopSupplier _troopSupplier;
    private BasicCharacterObject _playerCharacter;
    private bool _withHorse;

    public SpawnPlayerLogic(
      CustomBattleCombatant playerParty,
      EnhancedTroopSupplier troopSupplier,
      BasicCharacterObject player,
      bool withHorse)
    {
      this.\u002Ector();
      this._playerParty = playerParty;
      this._troopSupplier = troopSupplier;
      this._playerCharacter = player;
      this._withHorse = withHorse;
    }

    public virtual void AfterStart()
    {
      ((MissionBehaviour) this).AfterStart();
      Mission.get_Current().SpawnTroop(Utility.CreateOrigin(this._playerParty, this._playerCharacter, -1, this._troopSupplier), true, true, this._withHorse, false, true, 1, 0, true, true, false, (string) null, new MatrixFrame?()).set_Controller((Agent.ControllerType) 2);
    }
  }
}
