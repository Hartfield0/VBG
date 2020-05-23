// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.EnhancedFreeBattleGame
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ObjectSystem;

namespace VirtualBattlegrounds
{
  public class EnhancedFreeBattleGame : GameType
  {
    private Func<BattleConfigBase> _getConfig;

    public static EnhancedFreeBattleGame Current
    {
      get
      {
        return Game.get_Current().get_GameType() as EnhancedFreeBattleGame;
      }
    }

    public EnhancedFreeBattleGame(Func<BattleConfigBase> getConfig)
    {
      this.\u002Ector();
      this._getConfig = getConfig;
    }

    protected virtual void OnInitialize()
    {
      Game currentGame = this.get_CurrentGame();
      currentGame.FirstInitialize(false);
      this.AddGameTexts();
      IGameStarter basicGameStarter = (IGameStarter) new BasicGameStarter();
      this.AddGameModels(basicGameStarter);
      this.get_GameManager().OnGameStart(this.get_CurrentGame(), basicGameStarter);
      currentGame.SecondInitialize(basicGameStarter.get_Models());
      currentGame.CreateGameManager();
      currentGame.ThirdInitialize();
      this.get_GameManager().BeginGameStart(this.get_CurrentGame());
      currentGame.CreateObjects();
      currentGame.InitializeDefaultGameObjects();
      currentGame.LoadBasicFiles(false);
      MBObjectManagerExtensions.LoadXML(this.get_ObjectManager(), "Items", (Type) null, false);
      MBObjectManagerExtensions.LoadXML(this.get_ObjectManager(), "MPCharacters", (Type) null, false);
      MBObjectManagerExtensions.LoadXML(this.get_ObjectManager(), "BasicCultures", (Type) null, false);
      currentGame.CreateLists();
      MBObjectManagerExtensions.LoadXML(this.get_ObjectManager(), "MPClassDivisions", (Type) null, false);
      this.get_ObjectManager().ClearEmptyObjects();
      MultiplayerClassDivisions.Initialize();
      this.get_GameManager().OnCampaignStart(this.get_CurrentGame(), (object) null);
      this.get_GameManager().OnAfterCampaignStart(this.get_CurrentGame());
      this.get_GameManager().OnGameInitializationFinished(this.get_CurrentGame());
      this.get_CurrentGame().AddGameHandler<ChatBox>();
    }

    private void AddGameTexts()
    {
      this.get_CurrentGame().get_GameTextManager().LoadGameTexts(BasePath.get_Name() + "Modules/Native/ModuleData/multiplayer_strings.xml");
      this.get_CurrentGame().get_GameTextManager().LoadGameTexts(BasePath.get_Name() + "Modules/Native/ModuleData/global_strings.xml");
      this.get_CurrentGame().get_GameTextManager().LoadGameTexts(BasePath.get_Name() + "Modules/Native/ModuleData/module_strings.xml");
      this.get_CurrentGame().get_GameTextManager().LoadGameTexts(BasePath.get_Name() + "Modules/Native/ModuleData/native_strings.xml");
      this.get_CurrentGame().get_GameTextManager().LoadGameTexts(BasePath.get_Name() + "Modules/VirtualBattlegrounds/ModuleData/module_strings.xml");
    }

    private void AddGameModels(IGameStarter basicGameStarter)
    {
      basicGameStarter.AddModel((GameModel) new VirtualBattlegroundsSkillList());
      basicGameStarter.AddModel((GameModel) new DefaultRidingModel());
      basicGameStarter.AddModel((GameModel) new EnhancedMPStrikeMagnitudeModel());
      basicGameStarter.AddModel((GameModel) new MultiplayerAgentDecideKilledOrUnconsciousModel());
      basicGameStarter.AddModel((GameModel) new EnhancedMPAgentStatCalculateModel(this._getConfig()));
      basicGameStarter.AddModel((GameModel) new MultiplayerAgentApplyDamageModel());
      basicGameStarter.AddModel((GameModel) new MultiplayerBattleMoraleModel());
    }

    protected virtual void BeforeRegisterTypes(MBObjectManager objectManager)
    {
    }

    protected virtual void OnRegisterTypes(MBObjectManager objectManager)
    {
      objectManager.RegisterType<BasicCharacterObject>("NPCCharacter", "MPCharacters", 1U, true);
      objectManager.RegisterType<BasicCultureObject>("Culture", "BasicCultures", 1U, true);
      objectManager.RegisterType<MultiplayerClassDivisions.MPHeroClass>("MPClassDivision", "MPClassDivisions", 1U, true);
    }

    protected virtual void DoLoadingForGameType(
      GameTypeLoadingStates gameTypeLoadingState,
      out GameTypeLoadingStates nextState)
    {
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref nextState = -1;
      switch ((int) gameTypeLoadingState)
      {
        case 0:
          this.get_CurrentGame().Initialize();
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(int&) ref nextState = 1;
          break;
        case 1:
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(int&) ref nextState = 2;
          break;
        case 2:
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(int&) ref nextState = 3;
          break;
      }
    }

    public virtual void OnDestroy()
    {
    }

    public virtual void OnStateChanged(GameState oldState)
    {
    }
  }
}
