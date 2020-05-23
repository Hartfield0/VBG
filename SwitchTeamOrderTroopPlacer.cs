// Decompiled with JetBrains decompiler
// Type: VirtualBattlegrounds.SwitchTeamOrderTroopPlacer
// Assembly: VirtualBattlegrounds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8B8B98BC-1FFC-4BBE-9960-D6E0EC951214
// Assembly location: G:\steam\steamapps\common\Mount & Blade II Bannerlord\Modules\VirtualBattlegrounds\bin\Win64_Shipping_Client\VirtualBattlegrounds.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Engine.Screens;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Missions.Handlers;
using TaleWorlds.MountAndBlade.View.Missions;

namespace VirtualBattlegrounds
{
  public class SwitchTeamOrderTroopPlacer : MissionView
  {
    private SwitchTeamLogic _controller;
    private bool _suspendTroopPlacer;
    private bool _isMouseDown;
    private List<GameEntity> _orderPositionEntities;
    private List<GameEntity> _orderRotationEntities;
    private bool _formationDrawingMode;
    private Formation _mouseOverFormation;
    private Formation _clickedFormation;
    private Vec2 _lastMousePosition;
    private Vec2 _deltaMousePosition;
    private int _mouseOverDirection;
    private WorldPosition? _formationDrawingStartingPosition;
    private Vec2? _formationDrawingStartingPointOfMouse;
    private float? _formationDrawingStartingTime;
    private OrderController PlayerOrderController;
    private Team PlayerTeam;
    private bool _initialized;
    private Timer formationDrawTimer;
    public bool IsDrawingForced;
    public bool IsDrawingFacing;
    public bool IsDrawingForming;
    public bool IsDrawingAttaching;
    private bool _wasDrawingForced;
    private bool _wasDrawingFacing;
    private bool _wasDrawingForming;
    private GameEntity attachArrow;
    private float attachArrowLength;
    private GameEntity widthEntityLeft;
    private GameEntity widthEntityRight;
    private bool isDrawnThisFrame;
    private bool wasDrawnPreviousFrame;
    private static Material _meshMaterial;

    private void RegisterReload()
    {
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
      if (this._controller == null)
        return;
      this._controller.PostSwitchTeam += new SwitchTeamLogic.SwitchTeamDelegate(this.OnPostSwitchTeam);
    }

    private void OnPostSwitchTeam()
    {
      this.InitializeInADisgustingManner();
    }

    public virtual void OnMissionScreenInitialize()
    {
      base.OnMissionScreenInitialize();
      this.RegisterReload();
    }

    public bool SuspendTroopPlacer
    {
      get
      {
        return this._suspendTroopPlacer;
      }
      set
      {
        this._suspendTroopPlacer = value;
        if (value)
          this.HideOrderPositionEntities();
        else
          this._formationDrawingStartingPosition = new WorldPosition?();
        this.Reset();
      }
    }

    public Formation AttachTarget { get; private set; }

    public MovementOrder.Side AttachSide { get; private set; }

    public WorldPosition AttachPosition { get; private set; }

    public virtual void AfterStart()
    {
      ((MissionBehaviour) this).AfterStart();
      this._formationDrawingStartingPosition = new WorldPosition?();
      this._formationDrawingStartingPointOfMouse = new Vec2?();
      this._formationDrawingStartingTime = new float?();
      this._orderRotationEntities = new List<GameEntity>();
      this._orderPositionEntities = new List<GameEntity>();
      this.formationDrawTimer = new Timer(MBCommon.GetTime((MBCommon.TimeType) 0), 0.03333334f, true);
      this.attachArrow = GameEntity.CreateEmpty(((MissionBehaviour) this).get_Mission().get_Scene(), true);
      this.attachArrow.AddComponent((GameEntityComponent) MetaMesh.GetCopy("order_arrow_a", true, false));
      this.attachArrow.SetVisibilityExcludeParents(false);
      BoundingBox boundingBox = this.attachArrow.GetMetaMesh(0).GetBoundingBox();
      this.attachArrowLength = (float) (((Vec3) boundingBox.max).y - ((Vec3) boundingBox.min).y);
      this.widthEntityLeft = GameEntity.CreateEmpty(((MissionBehaviour) this).get_Mission().get_Scene(), true);
      this.widthEntityLeft.AddComponent((GameEntityComponent) MetaMesh.GetCopy("order_arrow_a", true, false));
      this.widthEntityLeft.SetVisibilityExcludeParents(false);
      this.widthEntityRight = GameEntity.CreateEmpty(((MissionBehaviour) this).get_Mission().get_Scene(), true);
      this.widthEntityRight.AddComponent((GameEntityComponent) MetaMesh.GetCopy("order_arrow_a", true, false));
      this.widthEntityRight.SetVisibilityExcludeParents(false);
    }

    private void InitializeInADisgustingManner()
    {
      this.PlayerTeam = ((MissionBehaviour) this).get_Mission().get_PlayerTeam();
      this.PlayerOrderController = this.PlayerTeam.get_PlayerOrderController();
    }

    public virtual void OnMissionTick(float dt)
    {
      ((MissionBehaviour) this).OnMissionTick(dt);
      if (this._initialized)
        return;
      MissionPeer missionPeer = GameNetwork.get_IsMyPeerReady() ? (MissionPeer) PeerExtensions.GetComponent<MissionPeer>(GameNetwork.get_MyPeer()) : (MissionPeer) null;
      if (((MissionBehaviour) this).get_Mission().get_PlayerTeam() == null && (missionPeer == null || missionPeer.get_Team() != ((MissionBehaviour) this).get_Mission().get_AttackerTeam() && missionPeer.get_Team() != ((MissionBehaviour) this).get_Mission().get_DefenderTeam()))
        return;
      this.InitializeInADisgustingManner();
      this._initialized = true;
    }

    public void UpdateAttachVisuals(bool isVisible)
    {
      if (this.AttachTarget == null)
        isVisible = false;
      this.attachArrow.SetVisibilityExcludeParents(isVisible);
      if (isVisible)
      {
        Vec2 vec2 = this.AttachTarget.get_Direction();
        switch ((int) this.AttachSide)
        {
          case 0:
            vec2 = Vec2.op_Multiply(vec2, -1f);
            break;
          case 2:
            vec2 = ((Vec2) ref vec2).RightVec();
            break;
          case 3:
            vec2 = ((Vec2) ref vec2).LeftVec();
            break;
        }
        float rotationInRadians = ((Vec2) ref vec2).get_RotationInRadians();
        Mat3 identity1 = Mat3.get_Identity();
        ((Mat3) ref identity1).RotateAboutUp(rotationInRadians);
        MatrixFrame identity2 = MatrixFrame.get_Identity();
        identity2.rotation = (__Null) identity1;
        ref MatrixFrame local = ref identity2;
        WorldPosition attachPosition = this.AttachPosition;
        Vec3 groundVec3 = ((WorldPosition) ref attachPosition).GetGroundVec3();
        local.origin = (__Null) groundVec3;
        ((MatrixFrame) ref identity2).Advance(-this.attachArrowLength);
        this.attachArrow.SetFrame(ref identity2);
      }
      if (!isVisible)
        return;
      this.get_MissionScreen().GetOrderFlagPosition();
      this.AddAttachPoints();
    }

    private void UpdateFormationDrawingForFacingOrder(bool giveOrder)
    {
      this.isDrawnThisFrame = true;
      OrderController playerOrderController = this.PlayerOrderController;
      MBReadOnlyList<Formation> selectedFormations = this.PlayerOrderController.get_SelectedFormations();
      Vec3 orderFlagPosition = this.get_MissionScreen().GetOrderFlagPosition();
      Vec2 asVec2 = ((Vec3) ref orderFlagPosition).get_AsVec2();
      Vec2 orderLookAtDirection = OrderController.GetOrderLookAtDirection((IEnumerable<Formation>) selectedFormations, asVec2);
      List<(Agent, WorldFrame)> valueTupleList;
      ref List<(Agent, WorldFrame)> local = ref valueTupleList;
      playerOrderController.SimulateNewFacingOrder(orderLookAtDirection, ref local);
      int entityIndex = 0;
      this.HideOrderPositionEntities();
      using (List<(Agent, WorldFrame)>.Enumerator enumerator = valueTupleList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          WorldFrame frame = enumerator.Current.Item2;
          this.AddOrderPositionEntity(entityIndex, ref frame, giveOrder, -1f);
          ++entityIndex;
        }
      }
    }

    private void UpdateFormationDrawingForDestination(bool giveOrder)
    {
      this.isDrawnThisFrame = true;
      List<(Agent, WorldFrame)> valueTupleList;
      this.PlayerOrderController.SimulateDestinationFrames(ref valueTupleList, 3f);
      int entityIndex = 0;
      this.HideOrderPositionEntities();
      using (List<(Agent, WorldFrame)>.Enumerator enumerator = valueTupleList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          WorldFrame frame = enumerator.Current.Item2;
          this.AddOrderPositionEntity(entityIndex, ref frame, giveOrder, 0.7f);
          ++entityIndex;
        }
      }
    }

    private void UpdateFormationDrawingForFormingOrder(bool giveOrder)
    {
      this.isDrawnThisFrame = true;
      MatrixFrame orderFlagFrame = this.get_MissionScreen().GetOrderFlagFrame();
      Vec3 origin = (Vec3) orderFlagFrame.origin;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      Vec2 asVec2 = ((Vec3) ref (^(Mat3&) ref orderFlagFrame.rotation).f).get_AsVec2();
      float orderFormCustomWidth = OrderController.GetOrderFormCustomWidth((IEnumerable<Formation>) this.PlayerOrderController.get_SelectedFormations(), origin);
      List<(Agent, WorldFrame)> valueTupleList;
      this.PlayerOrderController.SimulateNewCustomWidthOrder(orderFormCustomWidth, ref valueTupleList);
      Formation formation = (Formation) Extensions.MaxBy<Formation, int>((IEnumerable<M0>) this.PlayerOrderController.get_SelectedFormations(), (Func<M0, M1>) (f => ((IEnumerable<Agent>) ((Team) f.Team).get_ActiveAgents()).Count<Agent>()));
      int entityIndex = 0;
      this.HideOrderPositionEntities();
      using (List<(Agent, WorldFrame)>.Enumerator enumerator = valueTupleList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          WorldFrame frame = enumerator.Current.Item2;
          this.AddOrderPositionEntity(entityIndex, ref frame, giveOrder, -1f);
          ++entityIndex;
        }
      }
      float unitDiameter = formation.get_UnitDiameter();
      float interval = formation.get_Interval();
      int num1 = Math.Max(0, (int) (((double) orderFormCustomWidth - (double) unitDiameter) / ((double) interval + (double) unitDiameter) + 9.99999974737875E-06)) + 1;
      float num2 = (float) (num1 - 1) * (interval + unitDiameter);
      for (int index = 0; index < num1; ++index)
      {
        Vec2 vec2;
        ((Vec2) ref vec2).\u002Ector((float) ((double) index * ((double) interval + (double) unitDiameter) - (double) num2 / 2.0), 0.0f);
        Vec2 parentUnitF = ((Vec2) ref asVec2).TransformToParentUnitF(vec2);
        WorldPosition worldPosition;
        ((WorldPosition) ref worldPosition).\u002Ector(Mission.get_Current().get_Scene(), UIntPtr.Zero, origin, false);
        ((WorldPosition) ref worldPosition).SetVec2(Vec2.op_Addition(((WorldPosition) ref worldPosition).get_AsVec2(), parentUnitF));
        WorldFrame frame;
        ((WorldFrame) ref frame).\u002Ector((Mat3) orderFlagFrame.rotation, worldPosition);
        this.AddOrderPositionEntity(entityIndex++, ref frame, false, -1f);
      }
    }

    private void UpdateFormationDrawing(bool giveOrder)
    {
      this.isDrawnThisFrame = true;
      this.HideOrderPositionEntities();
      if (!this._formationDrawingStartingPosition.HasValue)
        return;
      WorldPosition invalid = (WorldPosition) WorldPosition.Invalid;
      bool flag = false;
      if (((ScreenBase) this.get_MissionScreen()).get_MouseVisible() && this._formationDrawingStartingPointOfMouse.HasValue)
      {
        Vec2 vec2 = Vec2.op_Subtraction(this._formationDrawingStartingPointOfMouse.Value, this.get_Input().GetMousePositionPixel());
        if ((double) Math.Abs((float) vec2.x) < 10.0 && (double) Math.Abs((float) vec2.y) < 10.0)
        {
          flag = true;
          invalid = this._formationDrawingStartingPosition.Value;
        }
      }
      if (((ScreenBase) this.get_MissionScreen()).get_MouseVisible() && this._formationDrawingStartingTime.HasValue && (double) MBCommon.GetTime((MBCommon.TimeType) 1) - (double) this._formationDrawingStartingTime.Value < 0.300000011920929)
      {
        flag = true;
        invalid = this._formationDrawingStartingPosition.Value;
      }
      if (!flag)
      {
        Vec3 vec3_1;
        Vec3 vec3_2;
        this.get_MissionScreen().ScreenPointToWorldRay(this.GetScreenPoint(), ref vec3_1, ref vec3_2);
        float num1;
        if (!((MissionBehaviour) this).get_Mission().get_Scene().RayCastForClosestEntityOrTerrain(vec3_1, vec3_2, ref num1, 0.3f, (BodyFlags) 79617))
          return;
        Vec3 vec3_3 = Vec3.op_Subtraction(vec3_2, vec3_1);
        double num2 = (double) ((Vec3) ref vec3_3).Normalize();
        ((WorldPosition) ref invalid).\u002Ector(Mission.get_Current().get_Scene(), UIntPtr.Zero, Vec3.op_Addition(vec3_1, Vec3.op_Multiply(vec3_3, num1)), false);
      }
      WorldPosition formationRealStartingPosition;
      if (this._mouseOverDirection == 1)
      {
        formationRealStartingPosition = invalid;
        invalid = this._formationDrawingStartingPosition.Value;
      }
      else
        formationRealStartingPosition = this._formationDrawingStartingPosition.Value;
      if (!OrderFlag.IsPositionOnValidGround(formationRealStartingPosition))
        return;
      bool isFormationLayoutVertical = !((MissionBehaviour) this).get_DebugInput().IsControlDown();
      if ((!Input.IsDown((InputKey) 224) || this._formationDrawingStartingPointOfMouse.HasValue) && this.IsDrawingAttaching)
        this.UpdateFormationDrawingForAttachOrder(giveOrder, isFormationLayoutVertical);
      else
        this.UpdateFormationDrawingForMovementOrder(giveOrder, formationRealStartingPosition, invalid, isFormationLayoutVertical);
      Vec2 deltaMousePosition = this._deltaMousePosition;
      Vec2 vec2_1 = Vec2.op_Subtraction(this.get_Input().GetMousePositionRanged(), this._lastMousePosition);
      double num = (double) Math.Max((float) (1.0 - (double) ((Vec2) ref vec2_1).get_Length() * 10.0), 0.0f);
      this._deltaMousePosition = Vec2.op_Multiply(deltaMousePosition, (float) num);
      this._lastMousePosition = this.get_Input().GetMousePositionRanged();
    }

    private void UpdateFormationDrawingForMovementOrder(
      bool giveOrder,
      WorldPosition formationRealStartingPosition,
      WorldPosition formationRealEndingPosition,
      bool isFormationLayoutVertical)
    {
      this.isDrawnThisFrame = true;
      List<(Agent, WorldFrame)> valueTupleList;
      this.PlayerOrderController.SimulateNewOrderWithPositionAndDirection(formationRealStartingPosition, formationRealEndingPosition, ref valueTupleList, isFormationLayoutVertical);
      if (giveOrder)
      {
        if (!isFormationLayoutVertical)
          this.PlayerOrderController.SetOrderWithTwoPositions((OrderType) 3, formationRealStartingPosition, formationRealEndingPosition);
        else
          this.PlayerOrderController.SetOrderWithTwoPositions((OrderType) 2, formationRealStartingPosition, formationRealEndingPosition);
      }
      int entityIndex = 0;
      using (List<(Agent, WorldFrame)>.Enumerator enumerator = valueTupleList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          WorldFrame frame = enumerator.Current.Item2;
          this.AddOrderPositionEntity(entityIndex, ref frame, giveOrder, -1f);
          ++entityIndex;
        }
      }
    }

    private void UpdateFormationDrawingForAttachOrder(
      bool giveOrder,
      bool isFormationLayoutVertical)
    {
      this.isDrawnThisFrame = true;
      int entityIndex = 0;
      using (List<Formation>.Enumerator enumerator1 = this.PlayerOrderController.get_SelectedFormations().GetEnumerator())
      {
        while (enumerator1.MoveNext())
        {
          Formation current = enumerator1.Current;
          WorldPosition attachPosition = MovementOrder.GetAttachPosition(current, this.AttachTarget, this.AttachSide);
          Vec2 direction = this.AttachTarget.get_Direction();
          Vec2 vec2 = Vec2.op_Multiply(((Vec2) ref direction).LeftVec(), current.get_Width() / 2f);
          WorldPosition worldPosition1 = attachPosition;
          ((WorldPosition) ref worldPosition1).SetVec2(Vec2.op_Addition(((WorldPosition) ref worldPosition1).get_AsVec2(), vec2));
          WorldPosition worldPosition2 = attachPosition;
          ((WorldPosition) ref worldPosition2).SetVec2(Vec2.op_Subtraction(((WorldPosition) ref worldPosition2).get_AsVec2(), vec2));
          List<(Agent, WorldFrame)> valueTupleList;
          OrderController.SimulateNewOrderWithPositionAndDirection(Enumerable.Repeat<Formation>(current, 1), this.PlayerOrderController.get_simulationFormations(), worldPosition1, worldPosition2, ref valueTupleList, isFormationLayoutVertical);
          using (List<(Agent, WorldFrame)>.Enumerator enumerator2 = valueTupleList.GetEnumerator())
          {
            while (enumerator2.MoveNext())
            {
              WorldFrame frame = enumerator2.Current.Item2;
              this.AddOrderPositionEntity(entityIndex, ref frame, giveOrder, -1f);
              ++entityIndex;
            }
          }
        }
      }
      if (!giveOrder)
        return;
      this.PlayerOrderController.SetOrderWithFormationAndNumber((OrderType) 10, this.AttachTarget, (int) this.AttachSide);
    }

    private void HandleMouseDown()
    {
      if (Extensions.IsEmpty<Formation>((IEnumerable<M0>) this.PlayerOrderController.get_SelectedFormations()) || this._clickedFormation != null)
        return;
      switch (this.GetCursorState())
      {
        case SwitchTeamOrderTroopPlacer.CursorState.Normal:
          this._formationDrawingMode = true;
          Vec3 vec3_1;
          Vec3 vec3_2;
          this.get_MissionScreen().ScreenPointToWorldRay(this.GetScreenPoint(), ref vec3_1, ref vec3_2);
          float num1;
          if (((MissionBehaviour) this).get_Mission().get_Scene().RayCastForClosestEntityOrTerrain(vec3_1, vec3_2, ref num1, 0.3f, (BodyFlags) 79617))
          {
            Vec3 vec3_3 = Vec3.op_Subtraction(vec3_2, vec3_1);
            double num2 = (double) ((Vec3) ref vec3_3).Normalize();
            this._formationDrawingStartingPosition = new WorldPosition?(new WorldPosition(Mission.get_Current().get_Scene(), UIntPtr.Zero, Vec3.op_Addition(vec3_1, Vec3.op_Multiply(vec3_3, num1)), false));
            this._formationDrawingStartingPointOfMouse = new Vec2?(this.get_Input().GetMousePositionPixel());
            this._formationDrawingStartingTime = new float?(MBCommon.GetTime((MBCommon.TimeType) 1));
            break;
          }
          this._formationDrawingStartingPosition = new WorldPosition?();
          this._formationDrawingStartingPointOfMouse = new Vec2?();
          this._formationDrawingStartingTime = new float?();
          break;
        case SwitchTeamOrderTroopPlacer.CursorState.Enemy:
        case SwitchTeamOrderTroopPlacer.CursorState.Friend:
          this._clickedFormation = this._mouseOverFormation;
          break;
        case SwitchTeamOrderTroopPlacer.CursorState.Rotation:
          if (Extensions.IsEmpty<Agent>((IEnumerable<M0>) ((Team) this._mouseOverFormation.Team).get_ActiveAgents()))
            break;
          this.HideNonSelectedOrderRotationEntities(this._mouseOverFormation);
          this.PlayerOrderController.ClearSelectedFormations();
          this.PlayerOrderController.SelectFormation(this._mouseOverFormation);
          this._formationDrawingMode = true;
          WorldPosition orderPosition = this._mouseOverFormation.get_OrderPosition();
          Vec2 direction = this._mouseOverFormation.get_Direction();
          ((Vec2) ref direction).RotateCCW(-1.570796f);
          this._formationDrawingStartingPosition = new WorldPosition?(orderPosition);
          WorldPosition worldPosition1 = this._formationDrawingStartingPosition.Value;
          ref WorldPosition local = ref worldPosition1;
          WorldPosition worldPosition2 = this._formationDrawingStartingPosition.Value;
          Vec2 vec2 = Vec2.op_Addition(((WorldPosition) ref worldPosition2).get_AsVec2(), Vec2.op_Multiply(Vec2.op_Multiply(direction, this._mouseOverDirection == 1 ? 0.5f : -0.5f), this._mouseOverFormation.get_Width()));
          ((WorldPosition) ref local).SetVec2(vec2);
          WorldPosition worldPosition3 = orderPosition;
          ((WorldPosition) ref worldPosition3).SetVec2(Vec2.op_Addition(((WorldPosition) ref worldPosition3).get_AsVec2(), Vec2.op_Multiply(Vec2.op_Multiply(direction, this._mouseOverDirection == 1 ? -0.5f : 0.5f), this._mouseOverFormation.get_Width())));
          this._deltaMousePosition = Vec2.op_Subtraction(this.get_MissionScreen().get_SceneView().WorldPointToScreenPoint(((WorldPosition) ref worldPosition3).GetGroundVec3()), this.GetScreenPoint());
          this._lastMousePosition = this.get_Input().GetMousePositionRanged();
          break;
      }
    }

    private void HandleMouseUp()
    {
      if (this._clickedFormation != null)
      {
        if (!Extensions.IsEmpty<Agent>((IEnumerable<M0>) ((Team) this._clickedFormation.Team).get_ActiveAgents()) && this._clickedFormation.Team == this.PlayerTeam)
        {
          Formation clickedFormation = this._clickedFormation;
          this._clickedFormation = (Formation) null;
          int cursorState = (int) this.GetCursorState();
          this._clickedFormation = clickedFormation;
          this.HideNonSelectedOrderRotationEntities(this._clickedFormation);
          this.PlayerOrderController.ClearSelectedFormations();
          this.PlayerOrderController.SelectFormation(this._clickedFormation);
        }
        this._clickedFormation = (Formation) null;
      }
      else if (this.GetCursorState() == SwitchTeamOrderTroopPlacer.CursorState.Ground)
      {
        if (this.IsDrawingFacing || this._wasDrawingFacing)
          this.UpdateFormationDrawingForFacingOrder(true);
        else if (this.IsDrawingForming || this._wasDrawingForming)
          this.UpdateFormationDrawingForFormingOrder(true);
        else
          this.UpdateFormationDrawing(true);
        if (this.IsDeployment)
          SoundEvent.PlaySound2D("event:/ui/mission/deploy");
      }
      this._formationDrawingMode = false;
      this._deltaMousePosition = (Vec2) Vec2.Zero;
    }

    private Vec2 GetScreenPoint()
    {
      return !((ScreenBase) this.get_MissionScreen()).get_MouseVisible() ? Vec2.op_Addition(new Vec2(0.5f, 0.5f), this._deltaMousePosition) : Vec2.op_Addition(this.get_Input().GetMousePositionRanged(), this._deltaMousePosition);
    }

    private SwitchTeamOrderTroopPlacer.CursorState GetCursorState()
    {
      SwitchTeamOrderTroopPlacer.CursorState cursorState = SwitchTeamOrderTroopPlacer.CursorState.Invisible;
      this.AttachTarget = (Formation) null;
      if (!Extensions.IsEmpty<Formation>((IEnumerable<M0>) this.PlayerOrderController.get_SelectedFormations()) && this._clickedFormation == null)
      {
        Vec3 vec3_1;
        Vec3 vec3_2;
        this.get_MissionScreen().ScreenPointToWorldRay(this.GetScreenPoint(), ref vec3_1, ref vec3_2);
        float num;
        GameEntity gameEntity;
        if (!((MissionBehaviour) this).get_Mission().get_Scene().RayCastForClosestEntityOrTerrain(vec3_1, vec3_2, ref num, ref gameEntity, 0.3f, (BodyFlags) 79617))
          num = 1000f;
        if (cursorState == SwitchTeamOrderTroopPlacer.CursorState.Invisible && (double) num < 1000.0)
        {
          if (!this._formationDrawingMode && NativeObject.op_Equality((NativeObject) gameEntity, (NativeObject) null))
          {
            for (int index = 0; index < this._orderRotationEntities.Count; ++index)
            {
              GameEntity orderRotationEntity = this._orderRotationEntities[index];
              if (orderRotationEntity.IsVisibleIncludeParents() && NativeObject.op_Equality((NativeObject) gameEntity, (NativeObject) orderRotationEntity))
              {
                this._mouseOverFormation = ((IEnumerable<Formation>) this.PlayerOrderController.get_SelectedFormations()).ElementAt<Formation>(index / 2);
                this._mouseOverDirection = 1 - (index & 1);
                cursorState = SwitchTeamOrderTroopPlacer.CursorState.Rotation;
                break;
              }
            }
          }
          if (cursorState == SwitchTeamOrderTroopPlacer.CursorState.Invisible && this.get_MissionScreen().get_OrderFlag().get_FocusedOrderableObject() != null)
            cursorState = SwitchTeamOrderTroopPlacer.CursorState.OrderableEntity;
          if (cursorState == SwitchTeamOrderTroopPlacer.CursorState.Invisible)
          {
            cursorState = this.IsCursorStateGroundOrNormal();
            this.UpdateAttachData();
          }
        }
      }
      if (cursorState != SwitchTeamOrderTroopPlacer.CursorState.Ground && cursorState != SwitchTeamOrderTroopPlacer.CursorState.Rotation)
        this._mouseOverDirection = 0;
      return cursorState;
    }

    private SwitchTeamOrderTroopPlacer.CursorState IsCursorStateGroundOrNormal()
    {
      return !this._formationDrawingMode ? SwitchTeamOrderTroopPlacer.CursorState.Normal : SwitchTeamOrderTroopPlacer.CursorState.Ground;
    }

    private void UpdateAttachData()
    {
      if (!this.IsDrawingForced)
        return;
      Vec3 orderFlagPosition = this.get_MissionScreen().GetOrderFlagPosition();
      using (IEnumerator<Formation> enumerator = this.PlayerTeam.get_Formations().Where<Formation>((Func<Formation, bool>) (f => !this.PlayerOrderController.IsFormationListening(f))).GetEnumerator())
      {
        while (((IEnumerator) enumerator).MoveNext())
        {
          Formation current = enumerator.Current;
          Vec2 asVec2;
          if (this.AttachTarget != null)
          {
            WorldPosition rearAttachmentPoint = current.get_RearAttachmentPoint();
            asVec2 = ((WorldPosition) ref rearAttachmentPoint).get_AsVec2();
            double num1 = (double) ((Vec2) ref asVec2).DistanceSquared(((Vec3) ref orderFlagPosition).get_AsVec2());
            WorldPosition attachPosition = this.AttachPosition;
            asVec2 = ((WorldPosition) ref attachPosition).get_AsVec2();
            double num2 = (double) ((Vec2) ref asVec2).DistanceSquared(((Vec3) ref orderFlagPosition).get_AsVec2());
            if (num1 >= num2)
              goto label_7;
          }
          this.AttachTarget = current;
          this.AttachSide = (MovementOrder.Side) 1;
          this.AttachPosition = current.get_RearAttachmentPoint();
label_7:
          WorldPosition leftAttachmentPoint = current.get_LeftAttachmentPoint();
          asVec2 = ((WorldPosition) ref leftAttachmentPoint).get_AsVec2();
          double num3 = (double) ((Vec2) ref asVec2).DistanceSquared(((Vec3) ref orderFlagPosition).get_AsVec2());
          WorldPosition attachPosition1 = this.AttachPosition;
          asVec2 = ((WorldPosition) ref attachPosition1).get_AsVec2();
          double num4 = (double) ((Vec2) ref asVec2).DistanceSquared(((Vec3) ref orderFlagPosition).get_AsVec2());
          if (num3 < num4)
          {
            this.AttachTarget = current;
            this.AttachSide = (MovementOrder.Side) 2;
            this.AttachPosition = current.get_LeftAttachmentPoint();
          }
          WorldPosition rightAttachmentPoint = current.get_RightAttachmentPoint();
          asVec2 = ((WorldPosition) ref rightAttachmentPoint).get_AsVec2();
          double num5 = (double) ((Vec2) ref asVec2).DistanceSquared(((Vec3) ref orderFlagPosition).get_AsVec2());
          WorldPosition attachPosition2 = this.AttachPosition;
          asVec2 = ((WorldPosition) ref attachPosition2).get_AsVec2();
          double num6 = (double) ((Vec2) ref asVec2).DistanceSquared(((Vec3) ref orderFlagPosition).get_AsVec2());
          if (num5 < num6)
          {
            this.AttachTarget = current;
            this.AttachSide = (MovementOrder.Side) 3;
            this.AttachPosition = current.get_RightAttachmentPoint();
          }
          WorldPosition frontAttachmentPoint = current.get_FrontAttachmentPoint();
          asVec2 = ((WorldPosition) ref frontAttachmentPoint).get_AsVec2();
          double num7 = (double) ((Vec2) ref asVec2).DistanceSquared(((Vec3) ref orderFlagPosition).get_AsVec2());
          WorldPosition attachPosition3 = this.AttachPosition;
          asVec2 = ((WorldPosition) ref attachPosition3).get_AsVec2();
          double num8 = (double) ((Vec2) ref asVec2).DistanceSquared(((Vec3) ref orderFlagPosition).get_AsVec2());
          if (num7 < num8)
          {
            this.AttachTarget = current;
            this.AttachSide = (MovementOrder.Side) 0;
            this.AttachPosition = current.get_FrontAttachmentPoint();
          }
        }
      }
    }

    private void AddOrderPositionEntity(
      int entityIndex,
      ref WorldFrame frame,
      bool fadeOut,
      float alpha = -1f)
    {
      while (this._orderPositionEntities.Count <= entityIndex)
      {
        GameEntity empty = GameEntity.CreateEmpty(((MissionBehaviour) this).get_Mission().get_Scene(), true);
        GameEntity gameEntity = empty;
        gameEntity.set_EntityFlags((EntityFlags) (gameEntity.get_EntityFlags() | 4194304));
        MetaMesh copy = MetaMesh.GetCopy("order_flag_small", true, false);
        if (NativeObject.op_Equality((NativeObject) SwitchTeamOrderTroopPlacer._meshMaterial, (NativeObject) null))
        {
          SwitchTeamOrderTroopPlacer._meshMaterial = copy.GetMeshAtIndex(0).GetMaterial().CreateCopy();
          SwitchTeamOrderTroopPlacer._meshMaterial.SetAlphaBlendMode((Material.MBAlphaBlendMode) 6);
        }
        copy.SetMaterial(SwitchTeamOrderTroopPlacer._meshMaterial);
        empty.AddComponent((GameEntityComponent) copy);
        empty.SetVisibilityExcludeParents(false);
        this._orderPositionEntities.Add(empty);
      }
      GameEntity orderPositionEntity = this._orderPositionEntities[entityIndex];
      Vec3 vec3_1;
      Vec3 vec3_2;
      this.get_MissionScreen().ScreenPointToWorldRay(Vec2.op_Multiply((Vec2) Vec2.One, 0.5f), ref vec3_1, ref vec3_2);
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      float rotationZ = ((Vec3) ref (^(Mat3&) ref MatrixFrame.CreateLookAt(vec3_1, ((WorldPosition) ref frame.Origin).GetGroundVec3(), (Vec3) Vec3.Up).rotation).f).get_RotationZ();
      frame.Rotation = (__Null) Mat3.get_Identity();
      ((Mat3) ref frame.Rotation).RotateAboutUp(rotationZ);
      MatrixFrame groundMatrixFrame = ((WorldFrame) ref frame).ToGroundMatrixFrame();
      orderPositionEntity.SetFrame(ref groundMatrixFrame);
      if ((double) alpha != -1.0)
      {
        orderPositionEntity.SetVisibilityExcludeParents(true);
        orderPositionEntity.SetAlpha(alpha);
      }
      else if (fadeOut)
        GameEntityExtensions.FadeOut(orderPositionEntity, 0.3f, false);
      else
        GameEntityExtensions.FadeIn(orderPositionEntity, true);
    }

    private void HideNonSelectedOrderRotationEntities(Formation formation)
    {
      for (int index = 0; index < this._orderRotationEntities.Count; ++index)
      {
        GameEntity orderRotationEntity = this._orderRotationEntities[index];
        if (NativeObject.op_Equality((NativeObject) orderRotationEntity, (NativeObject) null) && orderRotationEntity.IsVisibleIncludeParents() && ((IEnumerable<Formation>) this.PlayerOrderController.get_SelectedFormations()).ElementAt<Formation>(index / 2) != formation)
        {
          orderRotationEntity.SetVisibilityExcludeParents(false);
          GameEntity gameEntity = orderRotationEntity;
          gameEntity.set_BodyFlag((BodyFlags) (gameEntity.get_BodyFlag() | 1));
        }
      }
    }

    private void HideOrderPositionEntities()
    {
      using (List<GameEntity>.Enumerator enumerator = this._orderPositionEntities.GetEnumerator())
      {
        while (enumerator.MoveNext())
          GameEntityExtensions.HideIfNotFadingOut(enumerator.Current);
      }
      for (int index = 0; index < this._orderRotationEntities.Count; ++index)
      {
        GameEntity orderRotationEntity = this._orderRotationEntities[index];
        orderRotationEntity.SetVisibilityExcludeParents(false);
        GameEntity gameEntity = orderRotationEntity;
        gameEntity.set_BodyFlag((BodyFlags) (gameEntity.get_BodyFlag() | 1));
      }
    }

    [Conditional("DEBUG")]
    private void DebugTick(float dt)
    {
      int num = this._initialized ? 1 : 0;
    }

    private void Reset()
    {
      this._isMouseDown = false;
      this._formationDrawingMode = false;
      this._formationDrawingStartingPosition = new WorldPosition?();
      this._formationDrawingStartingPointOfMouse = new Vec2?();
      this._formationDrawingStartingTime = new float?();
      this._mouseOverFormation = (Formation) null;
      this._clickedFormation = (Formation) null;
    }

    public virtual void OnMissionScreenTick(float dt)
    {
      if (!this._initialized)
        return;
      base.OnMissionScreenTick(dt);
      if (!((IEnumerable<Formation>) this.PlayerOrderController.get_SelectedFormations()).Any<Formation>())
        return;
      this.isDrawnThisFrame = false;
      if (this.SuspendTroopPlacer)
        return;
      if (this.get_Input().IsKeyPressed((InputKey) 224))
      {
        this._isMouseDown = true;
        this.HandleMouseDown();
      }
      if (this.get_Input().IsKeyReleased((InputKey) 224) && this._isMouseDown)
      {
        this._isMouseDown = false;
        this.HandleMouseUp();
      }
      else if (this.get_Input().IsKeyDown((InputKey) 224) && this._isMouseDown)
      {
        if (this.formationDrawTimer.Check(MBCommon.GetTime((MBCommon.TimeType) 0)) && !this.IsDrawingFacing && (!this.IsDrawingForming && this.IsCursorStateGroundOrNormal() == SwitchTeamOrderTroopPlacer.CursorState.Ground) && this.GetCursorState() == SwitchTeamOrderTroopPlacer.CursorState.Ground)
          this.UpdateFormationDrawing(false);
      }
      else if (this.IsDrawingForced)
      {
        this.Reset();
        this.HandleMouseDown();
        this.UpdateFormationDrawing(false);
      }
      else if (this.IsDrawingFacing || this._wasDrawingFacing)
      {
        if (this.IsDrawingFacing)
        {
          this.Reset();
          this.UpdateFormationDrawingForFacingOrder(false);
        }
      }
      else if (this.IsDrawingForming || this._wasDrawingForming)
      {
        if (this.IsDrawingForming)
        {
          this.Reset();
          this.UpdateFormationDrawingForFormingOrder(false);
        }
      }
      else if (this._wasDrawingForced)
        this.Reset();
      else
        this.UpdateFormationDrawingForDestination(false);
      using (List<GameEntity>.Enumerator enumerator = this._orderPositionEntities.GetEnumerator())
      {
        while (enumerator.MoveNext())
          enumerator.Current.SetPreviousFrameInvalid();
      }
      using (List<GameEntity>.Enumerator enumerator = this._orderRotationEntities.GetEnumerator())
      {
        while (enumerator.MoveNext())
          enumerator.Current.SetPreviousFrameInvalid();
      }
      this._wasDrawingForced = this.IsDrawingForced;
      this._wasDrawingFacing = this.IsDrawingFacing;
      this._wasDrawingForming = this.IsDrawingForming;
      this.wasDrawnPreviousFrame = this.isDrawnThisFrame;
    }

    private bool IsDeployment
    {
      get
      {
        return ((MissionBehaviour) this).get_Mission().GetMissionBehaviour<SiegeDeploymentHandler>() != null;
      }
    }

    private void AddAttachPoints()
    {
      using (IEnumerator<Formation> enumerator = this.PlayerTeam.get_FormationsIncludingSpecial().Except<Formation>((IEnumerable<Formation>) this.PlayerOrderController.get_SelectedFormations()).GetEnumerator())
      {
        while (((IEnumerator) enumerator).MoveNext())
        {
          Formation current = enumerator.Current;
          current.get_RearAttachmentPoint();
          current.get_FrontAttachmentPoint();
          current.get_LeftAttachmentPoint();
          current.get_RightAttachmentPoint();
        }
      }
      if (this.AttachTarget == null)
        return;
      WorldPosition attachPosition = this.AttachPosition;
    }

    public SwitchTeamOrderTroopPlacer()
    {
      base.\u002Ector();
    }

    protected enum CursorState
    {
      Invisible,
      Normal,
      Ground,
      Enemy,
      Friend,
      Rotation,
      Count,
      OrderableEntity,
    }
  }
}
