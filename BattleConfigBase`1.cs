// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.BattleConfigBase`1
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

namespace VirtualBattlegrounds
{
  public abstract class BattleConfigBase<T> : BattleConfigBase where T : BattleConfigBase<T>
  {
    protected BattleConfigBase(BattleType t)
      : base(t)
    {
    }

    protected virtual void CopyFrom(T other)
    {
      this.ConfigVersion = other.ConfigVersion;
      if (other.playerClass != null)
        this.playerClass = other.playerClass;
      this.SpawnPlayer = other.SpawnPlayer;
      this.isPlayerAttacker = other.isPlayerAttacker;
      if (other.enemyClass != null)
        this.enemyClass = other.enemyClass;
      this.SpawnEnemyCommander = other.SpawnEnemyCommander;
      if (other.playerTroops != null)
        this.playerTroops = other.playerTroops;
      if (other.enemyTroops != null)
        this.enemyTroops = other.enemyTroops;
      this.attackerTacticOptions = other.attackerTacticOptions;
      this.defenderTacticOptions = other.defenderTacticOptions;
      this.aiEnableType = other.aiEnableType;
      this.disableDeath = other.disableDeath;
      this.useRealisticBlocking = other.useRealisticBlocking;
      this.noAgentLabel = other.noAgentLabel;
      this.noKillNotification = other.noKillNotification;
      this.changeCombatAI = other.changeCombatAI;
      this.combatAI = other.combatAI;
    }
  }
}
