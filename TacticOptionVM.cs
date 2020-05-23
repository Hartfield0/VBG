// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.TacticOptionVM
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace VirtualBattlegrounds
{
  public class TacticOptionVM : ViewModel
  {
    public MissionMenuVM parent;
    public BattleSideEnum side;
    public TacticOptionEnum tacticOption;
    private bool _isSelected;

    [DataSourceProperty]
    public string Name { get; set; }

    [DataSourceProperty]
    public bool IsSelected
    {
      get
      {
        return this._isSelected;
      }
      set
      {
        if (value == this._isSelected)
          return;
        this._isSelected = value;
        this.OnPropertyChanged(nameof (IsSelected));
      }
    }

    private void OnSelect()
    {
      bool flag = !this.IsSelected;
      Func<BattleSideEnum, TacticOptionEnum, bool, bool> updateSelectedTactic = this.parent.updateSelectedTactic;
      if (updateSelectedTactic == null || !updateSelectedTactic(this.side, this.tacticOption, flag))
        return;
      this.IsSelected = flag;
    }

    public TacticOptionVM()
    {
      base.\u002Ector();
    }
  }
}
