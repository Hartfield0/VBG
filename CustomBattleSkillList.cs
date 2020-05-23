// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.CustomBattleSkillList
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System.Collections.Generic;
using TaleWorlds.Core;

namespace VirtualBattlegrounds
{
  internal class CustomBattleSkillList : SkillList
  {
    public virtual IEnumerable<SkillObject> GetSkillList()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerable<SkillObject>) new CustomBattleSkillList.\u003CGetSkillList\u003Ed__0(-2)
      {
        \u003C\u003E4__this = this
      };
    }

    public CustomBattleSkillList()
    {
      base.\u002Ector();
    }
  }
}
