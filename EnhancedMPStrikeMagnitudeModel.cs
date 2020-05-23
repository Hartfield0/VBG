// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.EnhancedMPStrikeMagnitudeModel
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using TaleWorlds.Core;

namespace VirtualBattlegrounds
{
  internal class EnhancedMPStrikeMagnitudeModel : StrikeMagnitudeCalculationModel
  {
    public virtual float CalculateStrikeMagnitudeForSwing(
      float swingSpeed,
      float impactPoint,
      float weaponWeight,
      float weaponLength,
      float weaponInertia,
      float weaponCoM,
      float extraLinearSpeed)
    {
      return CombatStatCalculator.CalculateStrikeMagnitudeForSwing(swingSpeed, impactPoint, weaponWeight, weaponLength, weaponInertia, weaponCoM, extraLinearSpeed);
    }

    public virtual float CalculateStrikeMagnitudeForThrust(
      float thrustWeaponSpeed,
      float weaponWeight,
      float extraLinearSpeed,
      bool isThrown = false)
    {
      return CombatStatCalculator.CalculateStrikeMagnitudeForThrust(thrustWeaponSpeed, weaponWeight, extraLinearSpeed, isThrown);
    }

    public virtual float ComputeRawDamage(
      DamageTypes damageType,
      float magnitude,
      float armorEffectiveness,
      float absorbedDamageRatio)
    {
      return CombatStatCalculator.ComputeRawDamageOld(damageType, magnitude, armorEffectiveness, absorbedDamageRatio);
    }

    public virtual float CalculateHorseArcheryFactor(BasicCharacterObject characterObject)
    {
      return 100f;
    }

    public EnhancedMPStrikeMagnitudeModel()
    {
      base.\u002Ector();
    }
  }
}
