// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.CharacterSelectionParams
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System;
using System.Collections.Generic;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ObjectSystem;

namespace VirtualBattlegrounds
{
  public class CharacterSelectionParams
  {
    public Dictionary<string, Dictionary<string, List<MultiplayerClassDivisions.MPHeroClass>>> allMpHeroClassMap;
    public bool isTroop;
    public MultiplayerClassDivisions.MPHeroClass selectedHeroClass;
    public int selectedFirstPerk;
    public int selectedSecondPerk;
    public Action<CharacterSelectionParams> selectAction;

    public static CharacterSelectionParams CharacterSelectionParamsFor(
      Dictionary<string, Dictionary<string, List<MultiplayerClassDivisions.MPHeroClass>>> allMpHeroClassMap,
      ClassInfo classInfo,
      bool isTroop,
      Action<CharacterSelectionParams> selectionAction)
    {
      return new CharacterSelectionParams()
      {
        allMpHeroClassMap = allMpHeroClassMap,
        isTroop = isTroop,
        selectedHeroClass = (MultiplayerClassDivisions.MPHeroClass) MBObjectManager.get_Instance().GetObject<MultiplayerClassDivisions.MPHeroClass>(classInfo.classStringId),
        selectedFirstPerk = classInfo.selectedFirstPerk,
        selectedSecondPerk = classInfo.selectedSecondPerk,
        selectAction = selectionAction
      };
    }
  }
}
