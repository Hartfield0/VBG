// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.CharacterSelectionVM
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TaleWorlds.Core;
using TaleWorlds.GauntletUI;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ObjectSystem;

namespace VirtualBattlegrounds
{
  public class CharacterSelectionVM : ViewModel
  {
    private CharacterSelectionParams _params;
    private bool _inChange;
    private MBBindingList<NameVM> _cultures;
    private MBBindingList<NameVM> _groups;
    private MBBindingList<CharacterVM> _characters;
    private MBBindingList<PerkVM> _firstPerks;
    private MBBindingList<PerkVM> _secondPerks;

    public int SelectedCultureIndex { get; set; }

    public int SelectedGroupIndex { get; set; }

    public int SelectedCharacterIndex { get; set; }

    public int SelectedFirstPerkIndex { get; set; }

    public int SelectedSecondPerkIndex { get; set; }

    [DataSourceProperty]
    public MBBindingList<NameVM> Cultures
    {
      get
      {
        return this._cultures;
      }
      set
      {
        if (value == this._cultures)
          return;
        this._cultures = value;
        this.OnPropertyChanged(nameof (Cultures));
      }
    }

    [DataSourceProperty]
    public MBBindingList<NameVM> Groups
    {
      get
      {
        return this._groups;
      }
      set
      {
        if (value == this._groups)
          return;
        this._groups = value;
        this.OnPropertyChanged(nameof (Groups));
      }
    }

    [DataSourceProperty]
    public MBBindingList<CharacterVM> Characters
    {
      get
      {
        return this._characters;
      }
      set
      {
        if (value == this._characters)
          return;
        this._characters = value;
        this.OnPropertyChanged(nameof (Characters));
      }
    }

    [DataSourceProperty]
    public MBBindingList<PerkVM> FirstPerks
    {
      get
      {
        return this._firstPerks;
      }
      set
      {
        if (value == this._firstPerks)
          return;
        this._firstPerks = value;
        this.OnPropertyChanged(nameof (FirstPerks));
      }
    }

    [DataSourceProperty]
    public MBBindingList<PerkVM> SecondPerks
    {
      get
      {
        return this._secondPerks;
      }
      set
      {
        if (value == this._secondPerks)
          return;
        this._secondPerks = value;
        this.OnPropertyChanged(nameof (SecondPerks));
      }
    }

    public CharacterSelectionVM(CharacterSelectionParams p)
    {
      this.\u002Ector();
      this._params = p;
      MultiplayerClassDivisions.MPHeroClass selectedCharacter = p.selectedHeroClass;
      this.FillCultures();
      this.SelectedCultureIndex = Extensions.FindIndex<NameVM>((IReadOnlyList<M0>) this.Cultures, (Func<M0, bool>) (item => item.StringId == ((MBObjectBase) selectedCharacter.get_Culture()).get_StringId()));
      this.FillGroups();
      this.SelectedGroupIndex = Extensions.FindIndex<NameVM>((IReadOnlyList<M0>) this.Groups, (Func<M0, bool>) (item => item.StringId == (string) selectedCharacter.get_ClassGroup().StringId));
      this.FillCharacters();
      this.SelectedCharacterIndex = Extensions.FindIndex<CharacterVM>((IReadOnlyList<M0>) this.Characters, (Func<M0, bool>) (item => ((MBObjectBase) item.character).get_StringId() == ((MBObjectBase) selectedCharacter).get_StringId()));
      this.FillFirstPerks();
      this.FillSecondPerks();
      this.SelectedFirstPerkIndex = Extensions.FindIndex<PerkVM>((IReadOnlyList<M0>) this.FirstPerks, (Func<M0, bool>) (item => item.perkIndex == this._params.selectedFirstPerk));
      this.SelectedSecondPerkIndex = Extensions.FindIndex<PerkVM>((IReadOnlyList<M0>) this.SecondPerks, (Func<M0, bool>) (item => item.perkIndex == this._params.selectedSecondPerk));
    }

    public void SelectedCultureChanged(ListPanel listPanel)
    {
      this._inChange = true;
      this.SelectedCultureIndex = ((Container) listPanel).get_IntValue();
      this.FillGroups();
      this.FillCharacters();
      this.FillFirstPerks();
      this.FillSecondPerks();
      this._inChange = false;
    }

    public void SelectedGroupChanged(ListPanel listPanel)
    {
      int intValue = ((Container) listPanel).get_IntValue();
      if (intValue < 0 || this._inChange)
        return;
      this._inChange = true;
      this.SelectedGroupIndex = intValue;
      this.FillCharacters();
      this.FillFirstPerks();
      this.FillSecondPerks();
      this._inChange = false;
    }

    public void SelectedCharacterChanged(ListPanel listPanel)
    {
      int intValue = ((Container) listPanel).get_IntValue();
      if (intValue < 0 || this._inChange)
        return;
      this._inChange = true;
      this.SelectedCharacterIndex = intValue;
      this.FillFirstPerks();
      this.FillSecondPerks();
      this._inChange = false;
    }

    public void SelectedFirstPerkChanged(ListPanel listPanel)
    {
      int intValue = ((Container) listPanel).get_IntValue();
      if (intValue < 0 || this._inChange)
        return;
      this._inChange = true;
      this.SelectedFirstPerkIndex = intValue;
      this._inChange = false;
    }

    public void SelectedSecondPerkChanged(ListPanel listPanel)
    {
      int intValue = ((Container) listPanel).get_IntValue();
      if (intValue < 0 || this._inChange)
        return;
      this._inChange = true;
      this.SelectedSecondPerkIndex = intValue;
      this._inChange = false;
    }

    public void Done()
    {
      this._params.selectedHeroClass = ((Collection<CharacterVM>) this.Characters)[this.SelectedCharacterIndex].character;
      this._params.selectedFirstPerk = this.SelectedFirstPerkIndex;
      this._params.selectedSecondPerk = this.SelectedSecondPerkIndex;
      this._params.selectAction(this._params);
    }

    private void FillCultures()
    {
      this.Cultures = new MBBindingList<NameVM>();
      using (Dictionary<string, Dictionary<string, List<MultiplayerClassDivisions.MPHeroClass>>>.KeyCollection.Enumerator enumerator = this._params.allMpHeroClassMap.Keys.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          string current = enumerator.Current;
          BasicCultureObject basicCultureObject = (BasicCultureObject) MBObjectManager.get_Instance().GetObject<BasicCultureObject>(current);
          ((Collection<NameVM>) this.Cultures).Add(new NameVM()
          {
            StringId = current,
            Name = ((object) basicCultureObject.get_Name()).ToString()
          });
        }
      }
      this.SelectedCultureIndex = 0;
    }

    private void FillGroups()
    {
      string stringId = ((Collection<NameVM>) this.Cultures)[this.SelectedCultureIndex].StringId;
      if (this.Groups != null)
        ((Collection<NameVM>) this.Groups).Clear();
      else
        this.Groups = new MBBindingList<NameVM>();
      using (Dictionary<string, List<MultiplayerClassDivisions.MPHeroClass>>.KeyCollection.Enumerator enumerator = this._params.allMpHeroClassMap[stringId].Keys.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          string current = enumerator.Current;
          ((Collection<NameVM>) this.Groups).Add(new NameVM()
          {
            StringId = current,
            Name = ((object) GameTexts.FindText("str_character_group", current)).ToString()
          });
        }
      }
      this.SelectedGroupIndex = 0;
    }

    private void FillCharacters()
    {
      List<MultiplayerClassDivisions.MPHeroClass> mpHeroClassList = this._params.allMpHeroClassMap[((Collection<NameVM>) this.Cultures)[this.SelectedCultureIndex].StringId][((Collection<NameVM>) this.Groups)[this.SelectedGroupIndex].StringId];
      if (this.Characters != null)
        ((Collection<CharacterVM>) this.Characters).Clear();
      else
        this.Characters = new MBBindingList<CharacterVM>();
      using (List<MultiplayerClassDivisions.MPHeroClass>.Enumerator enumerator = mpHeroClassList.GetEnumerator())
      {
        while (enumerator.MoveNext())
          ((Collection<CharacterVM>) this.Characters).Add(new CharacterVM(enumerator.Current, this._params.isTroop));
      }
      this.SelectedCharacterIndex = 0;
    }

    private void FillFirstPerks()
    {
      MultiplayerClassDivisions.MPHeroClass character = ((Collection<CharacterVM>) this.Characters)[this.SelectedCharacterIndex].character;
      if (this.FirstPerks != null)
        ((Collection<PerkVM>) this.FirstPerks).Clear();
      else
        this.FirstPerks = new MBBindingList<PerkVM>();
      int num = 0;
      using (List<MPPerkObject>.Enumerator enumerator = character.GetAllAvailablePerksForListIndex(0).GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          MPPerkObject current = enumerator.Current;
          ((Collection<PerkVM>) this.FirstPerks).Add(new PerkVM()
          {
            Name = ((object) current.get_Name()).ToString(),
            perkIndex = num++
          });
        }
      }
      this.SelectedFirstPerkIndex = 0;
    }

    private void FillSecondPerks()
    {
      MultiplayerClassDivisions.MPHeroClass character = ((Collection<CharacterVM>) this.Characters)[this.SelectedCharacterIndex].character;
      if (this.SecondPerks != null)
        ((Collection<PerkVM>) this.SecondPerks).Clear();
      else
        this.SecondPerks = new MBBindingList<PerkVM>();
      int num = 0;
      using (List<MPPerkObject>.Enumerator enumerator = character.GetAllAvailablePerksForListIndex(1).GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          MPPerkObject current = enumerator.Current;
          ((Collection<PerkVM>) this.SecondPerks).Add(new PerkVM()
          {
            Name = ((object) current.get_Name()).ToString(),
            perkIndex = num++
          });
        }
      }
      this.SelectedSecondPerkIndex = 0;
    }
  }
}
