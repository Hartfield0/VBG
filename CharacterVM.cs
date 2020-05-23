// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.CharacterVM
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace VirtualBattlegrounds
{
  public class CharacterVM : ViewModel
  {
    public MultiplayerClassDivisions.MPHeroClass character;
    public bool isTroop;

    public CharacterVM(MultiplayerClassDivisions.MPHeroClass character, bool isTroop)
    {
      this.\u002Ector();
      this.character = character;
      this.isTroop = isTroop;
    }

    public string Name
    {
      get
      {
        return this.isTroop ? ((object) this.character.get_TroopName()).ToString() : ((object) this.character.get_HeroName()).ToString();
      }
    }
  }
}
