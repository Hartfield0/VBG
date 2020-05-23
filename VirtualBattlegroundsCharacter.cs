// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.VirtualBattlegroundsCharacter
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using TaleWorlds.Core;

namespace VirtualBattlegrounds
{
  internal class VirtualBattlegroundsCharacter : BasicCharacterObject
  {
    private bool _isHero;

    public virtual bool IsHero
    {
      get
      {
        return this._isHero;
      }
    }

    public void SetIsHero(bool isHero)
    {
      this._isHero = isHero;
    }

    public VirtualBattlegroundsCharacter()
    {
      base.\u002Ector();
    }
  }
}
