// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.VirtualBattlegroundsGameManager
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System;
using System.Collections;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace VirtualBattlegrounds
{
  public class VirtualBattlegroundsGameManager : MBGameManager
  {
    private GameType _gameType;
    private Action _startMission;

    public VirtualBattlegroundsGameManager(GameType gameType, Action startMission)
    {
      this.\u002Ector();
      this._gameType = gameType;
      this._startMission = startMission;
    }

    protected virtual void DoLoadingForGameManager(
      GameManagerLoadingSteps gameManagerLoadingStep,
      out GameManagerLoadingSteps nextStep)
    {
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref nextStep = -1;
      switch ((int) gameManagerLoadingStep)
      {
        case 0:
          MBGameManager.LoadModuleData(false);
          MBGlobals.InitializeReferences();
          Game.CreateGame(this._gameType, (GameManagerBase) this).DoLoading();
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(int&) ref nextStep = 1;
          break;
        case 1:
          bool flag = true;
          using (IEnumerator<MBSubModuleBase> enumerator = Module.get_CurrentModule().get_SubModules().GetEnumerator())
          {
            while (((IEnumerator) enumerator).MoveNext())
            {
              MBSubModuleBase current = enumerator.Current;
              flag = flag && current.DoLoading(Game.get_Current());
            }
          }
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(int&) ref nextStep = flag ? 2 : 1;
          break;
        case 2:
          MBGameManager.StartNewGame();
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(int&) ref nextStep = 3;
          break;
        case 3:
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(int&) ref nextStep = Game.get_Current().DoLoading() ? 4 : 3;
          break;
        case 4:
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(int&) ref nextStep = 5;
          break;
        case 5:
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(int&) ref nextStep = -1;
          break;
      }
    }

    public virtual void OnLoadFinished()
    {
      ((GameManagerBase) this).OnLoadFinished();
      this._startMission();
    }
  }
}
