// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.VirtualBattlegroundsSkillList
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System.Collections.Generic;
using TaleWorlds.Core;

namespace VirtualBattlegrounds
{
  internal class VirtualBattlegroundsSkillList : SkillList
  {
    internal VirtualBattlegroundsSkillList()
    {
      base.\u002Ector();
    }

    public virtual IEnumerable<SkillObject> GetSkillList()
    {
      return DefaultSkills.GetAllSkills();
    }
  }
}
