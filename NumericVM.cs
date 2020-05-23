// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.NumericVM
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System;
using TaleWorlds.Library;

namespace VirtualBattlegrounds
{
  public class NumericVM : ViewModel
  {
    private readonly float _initialValue;
    private float _min;
    private float _max;
    private float _optionValue;
    private bool _isDiscrete;
    private Action<float> _updateAction;

    public NumericVM(
      string name,
      float initialValue,
      float min,
      float max,
      bool isDiscrete,
      Action<float> updateAction)
    {
      this.\u002Ector();
      this.Name = name;
      this._initialValue = initialValue;
      this._min = min;
      this._max = max;
      this._optionValue = initialValue;
      this._isDiscrete = isDiscrete;
      this._updateAction = updateAction;
    }

    public string Name { get; }

    [DataSourceProperty]
    public float Min
    {
      get
      {
        return this._min;
      }
      set
      {
        if ((double) Math.Abs(value - this._min) < 0.00999999977648258)
          return;
        this._min = value;
        this.OnPropertyChanged(nameof (Min));
      }
    }

    [DataSourceProperty]
    public float Max
    {
      get
      {
        return this._max;
      }
      set
      {
        if ((double) Math.Abs(value - this._max) < 0.00999999977648258)
          return;
        this._max = value;
        this.OnPropertyChanged(nameof (Max));
      }
    }

    [DataSourceProperty]
    public float OptionValue
    {
      get
      {
        return this._optionValue;
      }
      set
      {
        if (Math.Abs((double) value - (double) this._optionValue) < 0.00999999977648258)
          return;
        this._optionValue = (float) MathF.Round(value * 100f) / 100f;
        this.OnPropertyChanged(nameof (OptionValue));
        this.OnPropertyChanged("OptionValueAsString");
        this._updateAction(this.OptionValue);
      }
    }

    [DataSourceProperty]
    public bool IsDiscrete
    {
      get
      {
        return this._isDiscrete;
      }
      set
      {
        if (value == this._isDiscrete)
          return;
        this._isDiscrete = value;
        this.OnPropertyChanged(nameof (IsDiscrete));
      }
    }

    [DataSourceProperty]
    public string OptionValueAsString
    {
      get
      {
        return this.IsDiscrete ? ((int) this._optionValue).ToString() : this._optionValue.ToString("F");
      }
    }
  }
}
