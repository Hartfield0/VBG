// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.SwitchTeamMissionOrderUIHandler
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Engine.Screens;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Missions.Handlers;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.Missions;
using TaleWorlds.MountAndBlade.View.Screen;
using TaleWorlds.MountAndBlade.ViewModelCollection;
using TaleWorlds.MountAndBlade.ViewModelCollection.Order;

namespace VirtualBattlegrounds
{
  internal class SwitchTeamMissionOrderUIHandler : MissionView, ISiegeDeploymentView
  {
    private SwitchTeamLogic _controller;
    private SiegeMissionView _siegeMissionView;
    private const float DEPLOYMENT_ICON_SIZE = 75f;
    private List<DeploymentSiegeMachineVM> _deploymentPointDataSources;
    private Vec2 _deploymentPointWidgetSize;
    private SwitchTeamOrderTroopPlacer _orderTroopPlacer;
    private GauntletLayer _gauntletLayer;
    private MissionOrderVM _dataSource;
    private GauntletMovie _viewMovie;
    private SiegeDeploymentHandler _siegeDeploymentHandler;
    private bool IsDeployment;
    private bool isInitialized;
    private bool _isTransferEnabled;

    private void RegisterReload()
    {
      if (this._controller != null)
        return;
      using (IEnumerator<MissionLogic> enumerator = ((MissionBehaviour) this).get_Mission().get_MissionLogics().GetEnumerator())
      {
        while (((IEnumerator) enumerator).MoveNext())
        {
          if (enumerator.Current is SwitchTeamLogic current)
          {
            this._controller = current;
            break;
          }
        }
      }
      if (this._controller != null)
      {
        this._controller.PreSwitchTeam += new SwitchTeamLogic.SwitchTeamDelegate(this.OnPreSwitchTeam);
        this._controller.PostSwitchTeam += new SwitchTeamLogic.SwitchTeamDelegate(this.OnPostSwitchTeam);
      }
    }

    private void OnPreSwitchTeam()
    {
      this._dataSource.CloseToggleOrder();
      base.OnMissionScreenFinalize();
    }

    private void OnPostSwitchTeam()
    {
      base.OnMissionScreenInitialize();
      base.OnMissionScreenActivate();
    }

    public SwitchTeamMissionOrderUIHandler()
    {
      base.\u002Ector();
      this.ViewOrderPriorty = (__Null) 19;
    }

    public void OnActivateToggleOrder()
    {
      if (this._dataSource == null || this._dataSource.get_ActiveTargetState() == 0)
        this._orderTroopPlacer.SuspendTroopPlacer = false;
      this.get_MissionScreen().SetOrderFlagVisibility(true);
      if (this._gauntletLayer != null)
        ScreenManager.SetSuspendLayer((ScreenLayer) this._gauntletLayer, false);
      Game.get_Current().get_EventManager().TriggerEvent<MissionPlayerToggledOrderViewEvent>((M0) new MissionPlayerToggledOrderViewEvent(true));
    }

    public void OnDeactivateToggleOrder()
    {
      this._orderTroopPlacer.SuspendTroopPlacer = true;
      this.get_MissionScreen().SetOrderFlagVisibility(false);
      if (this._gauntletLayer != null)
        ScreenManager.SetSuspendLayer((ScreenLayer) this._gauntletLayer, true);
      Game.get_Current().get_EventManager().TriggerEvent<MissionPlayerToggledOrderViewEvent>((M0) new MissionPlayerToggledOrderViewEvent(false));
    }

    public virtual void OnMissionScreenInitialize()
    {
      base.OnMissionScreenInitialize();
      this.RegisterReload();
      ((ScreenLayer) this.get_MissionScreen().get_SceneLayer()).get_Input().RegisterHotKeyCategory(HotKeyManager.GetCategory("MissionOrderHotkeyCategory"));
      this.get_MissionScreen().set_OrderFlag(new OrderFlag(((MissionBehaviour) this).get_Mission(), this.get_MissionScreen()));
      this._orderTroopPlacer = (SwitchTeamOrderTroopPlacer) ((MissionBehaviour) this).get_Mission().GetMissionBehaviour<SwitchTeamOrderTroopPlacer>();
      this.get_MissionScreen().SetOrderFlagVisibility(false);
      this._siegeDeploymentHandler = (SiegeDeploymentHandler) ((MissionBehaviour) this).get_Mission().GetMissionBehaviour<SiegeDeploymentHandler>();
      this.IsDeployment = this._siegeDeploymentHandler != null;
      if (this.IsDeployment)
      {
        this._siegeMissionView = (SiegeMissionView) ((MissionBehaviour) this).get_Mission().GetMissionBehaviour<SiegeMissionView>();
        if (this._siegeMissionView != null)
        {
          SiegeMissionView siegeMissionView = this._siegeMissionView;
          // ISSUE: method pointer
          siegeMissionView.OnDeploymentFinish = (__Null) Delegate.Combine((Delegate) siegeMissionView.OnDeploymentFinish, (Delegate) new OnPlayerDeploymentFinishDelegate((object) this, __methodptr(OnDeploymentFinish)));
        }
        this._deploymentPointDataSources = new List<DeploymentSiegeMachineVM>();
      }
      // ISSUE: method pointer
      // ISSUE: method pointer
      // ISSUE: method pointer
      // ISSUE: method pointer
      // ISSUE: method pointer
      this._dataSource = new MissionOrderVM(((MissionBehaviour) this).get_Mission(), this.get_MissionScreen().get_CombatCamera(), this.IsDeployment ? this._siegeDeploymentHandler.get_DeploymentPoints().ToList<DeploymentPoint>() : new List<DeploymentPoint>(), new Action<bool>(this.ToggleScreenRotation), this.IsDeployment, new GetOrderFlagPositionDelegate((object) this.get_MissionScreen(), __methodptr(GetOrderFlagPosition)), new OnRefreshVisualsDelegate((object) this, __methodptr(RefreshVisuals)), new ToggleOrderPositionVisibilityDelegate((object) this, __methodptr(SetSuspendTroopPlacer)), new OnToggleActivateOrderStateDelegate((object) this, __methodptr(OnActivateToggleOrder)), new OnToggleActivateOrderStateDelegate((object) this, __methodptr(OnDeactivateToggleOrder)));
      if (this.IsDeployment)
      {
        using (IEnumerator<DeploymentPoint> enumerator = this._siegeDeploymentHandler.get_DeploymentPoints().GetEnumerator())
        {
          while (((IEnumerator) enumerator).MoveNext())
          {
            DeploymentPoint current = enumerator.Current;
            DeploymentSiegeMachineVM deploymentSiegeMachineVm = new DeploymentSiegeMachineVM(current, (SiegeWeapon) null, this.get_MissionScreen().get_CombatCamera(), new Action<DeploymentSiegeMachineVM>(this._dataSource.OnRefreshSelectedDeploymentPoint), new Action<DeploymentPoint>(this._dataSource.OnEntityHover), false);
            Vec3 origin = (Vec3) ((ScriptComponentBehaviour) current).get_GameEntity().GetFrame().origin;
            for (int index = 0; index < ((ScriptComponentBehaviour) current).get_GameEntity().get_ChildCount(); ++index)
            {
              if (((IEnumerable<string>) ((ScriptComponentBehaviour) current).get_GameEntity().GetChild(index).get_Tags()).Contains<string>("deployment_point_icon_target"))
              {
                Vec3.op_Addition(origin, (Vec3) ((ScriptComponentBehaviour) current).get_GameEntity().GetChild(index).GetFrame().origin);
                break;
              }
            }
            this._deploymentPointDataSources.Add(deploymentSiegeMachineVm);
            deploymentSiegeMachineVm.set_RemainingCount(0);
            this._deploymentPointWidgetSize = new Vec2(75f / TaleWorlds.Engine.Screen.get_RealScreenResolutionWidth(), 75f / TaleWorlds.Engine.Screen.get_RealScreenResolutionHeight());
          }
        }
      }
      this._gauntletLayer = new GauntletLayer((int) this.ViewOrderPriorty, "GauntletLayer");
      ((ScreenLayer) this._gauntletLayer).get_Input().RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
      this._viewMovie = this._gauntletLayer.LoadMovie("Order", (ViewModel) this._dataSource);
      ((ScreenBase) this.get_MissionScreen()).AddLayer((ScreenLayer) this._gauntletLayer);
      if (this.IsDeployment)
        ((ScreenLayer) this._gauntletLayer).get_InputRestrictions().SetInputRestrictions(true, (InputUsageMask) 7);
      else if (!this._dataSource.get_IsToggleOrderShown())
        ScreenManager.SetSuspendLayer((ScreenLayer) this._gauntletLayer, true);
      this._dataSource.InputRestrictions = (__Null) ((ScreenLayer) this._gauntletLayer).get_InputRestrictions();
    }

    public virtual void OnMissionScreenFinalize()
    {
      base.OnMissionScreenFinalize();
      this._deploymentPointDataSources = (List<DeploymentSiegeMachineVM>) null;
      this._orderTroopPlacer = (SwitchTeamOrderTroopPlacer) null;
      this._gauntletLayer = (GauntletLayer) null;
      ((ViewModel) this._dataSource).OnFinalize();
      this._dataSource = (MissionOrderVM) null;
      this._viewMovie = (GauntletMovie) null;
      this._siegeDeploymentHandler = (SiegeDeploymentHandler) null;
    }

    private void OnDeploymentFinish()
    {
      this.IsDeployment = false;
      this._dataSource.FinalizeDeployment();
      this._deploymentPointDataSources.Clear();
      this._orderTroopPlacer.SuspendTroopPlacer = true;
      this.get_MissionScreen().SetOrderFlagVisibility(false);
      if (this._siegeMissionView == null)
        return;
      SiegeMissionView siegeMissionView = this._siegeMissionView;
      // ISSUE: method pointer
      siegeMissionView.OnDeploymentFinish = (__Null) Delegate.Remove((Delegate) siegeMissionView.OnDeploymentFinish, (Delegate) new OnPlayerDeploymentFinishDelegate((object) this, __methodptr(OnDeploymentFinish)));
    }

    public virtual bool OnEscape()
    {
      return this._dataSource.CloseToggleOrder();
    }

    public virtual void OnMissionScreenTick(float dt)
    {
      base.OnMissionScreenTick(dt);
      this.TickInput(dt);
      this._dataSource.Tick(dt);
      if (this._dataSource.get_IsToggleOrderShown())
      {
        if (this._orderTroopPlacer.SuspendTroopPlacer && this._dataSource.get_ActiveTargetState() == 0)
          this._orderTroopPlacer.SuspendTroopPlacer = false;
        this._orderTroopPlacer.IsDrawingForced = this._dataSource.get_IsMovementSubOrdersShown();
        this._orderTroopPlacer.IsDrawingFacing = this._dataSource.get_IsFacingSubOrdersShown();
        this._orderTroopPlacer.IsDrawingForming = false;
        this._orderTroopPlacer.IsDrawingAttaching = this.cursorState == 3;
        this._orderTroopPlacer.UpdateAttachVisuals(this.cursorState == 3);
        if (this.cursorState == 1)
        {
          OrderFlag orderFlag = this.get_MissionScreen().get_OrderFlag();
          MBReadOnlyList<Formation> selectedFormations = ((MissionBehaviour) this).get_Mission().get_MainAgent().get_Team().get_PlayerOrderController().get_SelectedFormations();
          Vec3 position = this.get_MissionScreen().get_OrderFlag().get_Position();
          Vec2 asVec2 = ((Vec3) ref position).get_AsVec2();
          Vec2 orderLookAtDirection = OrderController.GetOrderLookAtDirection((IEnumerable<Formation>) selectedFormations, asVec2);
          orderFlag.SetArrowVisibility(true, orderLookAtDirection);
        }
        else
          this.get_MissionScreen().get_OrderFlag().SetArrowVisibility(false, (Vec2) Vec2.Invalid);
        if (this.cursorState == 2)
          this.get_MissionScreen().get_OrderFlag().SetWidthVisibility(true, OrderController.GetOrderFormCustomWidth((IEnumerable<Formation>) ((MissionBehaviour) this).get_Mission().get_MainAgent().get_Team().get_PlayerOrderController().get_SelectedFormations(), this.get_MissionScreen().get_OrderFlag().get_Position()));
        else
          this.get_MissionScreen().get_OrderFlag().SetWidthVisibility(false, -1f);
      }
      else
      {
        if (!this._orderTroopPlacer.SuspendTroopPlacer)
          this._orderTroopPlacer.SuspendTroopPlacer = true;
        ((ScreenLayer) this._gauntletLayer).get_InputRestrictions().ResetInputRestrictions();
      }
      if (this.IsDeployment)
      {
        if (((ScreenLayer) this.get_MissionScreen().get_SceneLayer()).get_Input().IsKeyDown((InputKey) 225))
          ((ScreenLayer) this._gauntletLayer).get_InputRestrictions().SetMouseVisibility(false);
        else
          ((ScreenLayer) this._gauntletLayer).get_InputRestrictions().SetInputRestrictions(true, (InputUsageMask) 7);
      }
      this.get_MissionScreen().get_OrderFlag().IsTroop = (__Null) (this._dataSource.get_ActiveTargetState() == 0 ? 1 : 0);
      this.get_MissionScreen().get_OrderFlag().Tick(dt);
    }

    private void RefreshVisuals()
    {
      if (!this.IsDeployment)
        return;
      using (List<DeploymentSiegeMachineVM>.Enumerator enumerator = this._deploymentPointDataSources.GetEnumerator())
      {
        while (enumerator.MoveNext())
          enumerator.Current.RefreshWithDeployedWeapon();
      }
    }

    public virtual void OnMissionScreenActivate()
    {
      base.OnMissionScreenActivate();
      this._dataSource.AfterInitialize();
      this.isInitialized = true;
    }

    public virtual void OnAgentBuild(Agent agent, Banner banner)
    {
      if (!this.isInitialized || !agent.get_IsHuman())
        return;
      this._dataSource.AddTroops(agent);
    }

    public virtual void OnAgentRemoved(
      Agent affectedAgent,
      Agent affectorAgent,
      AgentState agentState,
      KillingBlow killingBlow)
    {
      ((MissionBehaviour) this).OnAgentRemoved(affectedAgent, affectorAgent, agentState, killingBlow);
      if (!affectedAgent.get_IsHuman())
        return;
      this._dataSource.RemoveTroops(affectedAgent);
    }

    private IOrderable GetFocusedOrderableObject()
    {
      return this.get_MissionScreen().get_OrderFlag().get_FocusedOrderableObject();
    }

    private void SetSuspendTroopPlacer(bool value)
    {
      this._orderTroopPlacer.SuspendTroopPlacer = value;
      this.get_MissionScreen().SetOrderFlagVisibility(!value);
    }

    void ISiegeDeploymentView.OnEntityHover(GameEntity hoveredEntity)
    {
      if (((ScreenLayer) this._gauntletLayer).HitTest())
        return;
      this._dataSource.OnEntityHover(hoveredEntity);
    }

    void ISiegeDeploymentView.OnEntitySelection(GameEntity selectedEntity)
    {
      this._dataSource.OnEntitySelect(selectedEntity);
    }

    private void ToggleScreenRotation(bool isLocked)
    {
      MissionScreen.SetFixedMissionCameraActive(isLocked);
    }

    [Conditional("DEBUG")]
    private void TickInputDebug()
    {
    }

    public MissionOrderVM.CursorState cursorState
    {
      get
      {
        return this._dataSource.get_IsFacingSubOrdersShown() ? (MissionOrderVM.CursorState) 1 : (MissionOrderVM.CursorState) 0;
      }
    }

    private void TickInput(float dt)
    {
      if (this._dataSource.get_IsToggleOrderShown())
      {
        if (this._dataSource.get_IsTransferActive() && ((ScreenLayer) this._gauntletLayer).get_Input().IsHotKeyReleased("Exit"))
          this._dataSource.set_IsTransferActive(false);
        if (this._dataSource.get_IsTransferActive() != this._isTransferEnabled)
        {
          this._isTransferEnabled = this._dataSource.get_IsTransferActive();
          if (!this._isTransferEnabled)
          {
            ((ScreenLayer) this._gauntletLayer).set_IsFocusLayer(false);
            ScreenManager.TryLoseFocus((ScreenLayer) this._gauntletLayer);
          }
          else
          {
            ((ScreenLayer) this._gauntletLayer).set_IsFocusLayer(true);
            ScreenManager.TrySetFocus((ScreenLayer) this._gauntletLayer);
          }
        }
        if (this._dataSource.get_ActiveTargetState() == 0 && this.get_Input().IsKeyReleased((InputKey) 224))
        {
          switch ((int) this.cursorState)
          {
            case 0:
              IOrderable focusedOrderableObject = this.GetFocusedOrderableObject();
              if (focusedOrderableObject != null)
              {
                this._dataSource.get_OrderController().SetOrderWithOrderableObject(focusedOrderableObject);
                break;
              }
              break;
            case 1:
              this._dataSource.get_OrderController().SetOrderWithPosition((OrderType) 17, new WorldPosition(((MissionBehaviour) this).get_Mission().get_Scene(), UIntPtr.Zero, this.get_MissionScreen().GetOrderFlagPosition(), false));
              break;
            case 2:
              this._dataSource.get_OrderController().SetOrderWithPosition((OrderType) 26, new WorldPosition(((MissionBehaviour) this).get_Mission().get_Scene(), UIntPtr.Zero, this.get_MissionScreen().GetOrderFlagPosition(), false));
              break;
          }
        }
        if (((MissionBehaviour) this).get_DebugInput().IsAltDown())
        {
          bool flag = this._dataSource.get_IsTransferActive() || !((ScreenLayer) this._gauntletLayer).get_InputRestrictions().get_MouseVisibility();
          ((ScreenLayer) this._gauntletLayer).get_InputRestrictions().SetInputRestrictions(flag, flag ? (InputUsageMask) 3 : (InputUsageMask) 0);
        }
        if (this.get_Input().IsKeyReleased((InputKey) 225))
          this._dataSource.OnEscape();
      }
      int num1 = -1;
      if (!((MissionBehaviour) this).get_DebugInput().IsControlDown())
      {
        if (this.get_Input().IsGameKeyPressed(51))
          num1 = 0;
        else if (this.get_Input().IsGameKeyPressed(52))
          num1 = 1;
        else if (this.get_Input().IsGameKeyPressed(53))
          num1 = 2;
        else if (this.get_Input().IsGameKeyPressed(54))
          num1 = 3;
        else if (this.get_Input().IsGameKeyPressed(55))
          num1 = 4;
        else if (this.get_Input().IsGameKeyPressed(56))
          num1 = 5;
        else if (this.get_Input().IsGameKeyPressed(57))
          num1 = 6;
        else if (this.get_Input().IsGameKeyPressed(58))
          num1 = 7;
        else if (this.get_Input().IsGameKeyPressed(59))
          num1 = 8;
      }
      if (num1 > -1)
        this._dataSource.OnGiveOrder(num1);
      int num2 = -1;
      if (this.get_Input().IsGameKeyPressed(60))
        num2 = 100;
      else if (this.get_Input().IsGameKeyPressed(61))
        num2 = 0;
      else if (this.get_Input().IsGameKeyPressed(62))
        num2 = 1;
      else if (this.get_Input().IsGameKeyPressed(63))
        num2 = 2;
      else if (this.get_Input().IsGameKeyPressed(64))
        num2 = 3;
      else if (this.get_Input().IsGameKeyPressed(65))
        num2 = 4;
      else if (this.get_Input().IsGameKeyPressed(66))
        num2 = 5;
      else if (this.get_Input().IsGameKeyPressed(67))
        num2 = 6;
      else if (this.get_Input().IsGameKeyPressed(68))
        num2 = 7;
      if (num2 != -1)
        this._dataSource.OnSelect(num2);
      if (!this.get_Input().IsGameKeyPressed(50))
        return;
      this._dataSource.ViewOrders();
    }
  }
}
