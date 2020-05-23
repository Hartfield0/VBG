// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.CommanderLogic
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using TaleWorlds.MountAndBlade;

namespace VirtualBattlegrounds
{
  internal class CommanderLogic : MissionLogic
  {
    protected virtual void OnAgentControllerChanged(Agent agent)
    {
      if (agent != ((MissionBehaviour) this).get_Mission().get_MainAgent())
        return;
      if (agent.get_Controller() == 2)
        Utility.SetPlayerAsCommander();
      else
        Utility.CancelPlayerCommander();
    }

    public CommanderLogic()
    {
      base.\u002Ector();
    }
  }
}
