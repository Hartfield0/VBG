// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.ReadPositionLogic
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace VirtualBattlegrounds
{
  internal class ReadPositionLogic : MissionLogic
  {
    public virtual void OnMissionTick(float dt)
    {
      ((MissionBehaviour) this).OnMissionTick(dt);
      if (!((MissionBehaviour) this).get_Mission().get_InputManager().IsKeyPressed((InputKey) 23))
        return;
      Agent mainAgent = ((MissionBehaviour) this).get_Mission().get_MainAgent();
      Vec3 vec3 = mainAgent != null ? mainAgent.get_Position() : ((MissionBehaviour) this).get_Mission().get_Scene().get_LastFinalRenderCameraPosition();
      WorldPosition worldPosition = new WorldPosition(((MissionBehaviour) this).get_Mission().get_Scene(), vec3);
      string str = ((WorldPosition) ref worldPosition).GetNavMesh().ToString() ?? "";
      Utility.DisplayMessage(string.Format("Position: {0} | Navmesh: {1} | Time: {2}", (object) vec3, (object) str, (object) ((MissionBehaviour) this).get_Mission().get_Time()));
    }

    public ReadPositionLogic()
    {
      base.\u002Ector();
    }
  }
}
