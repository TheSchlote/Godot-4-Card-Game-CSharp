using Godot;

public partial class CardDraggingState : CardState
{
    private const float DRAG_MINIMUM_THRESHOLD = 0.5f;
    private bool minimumDragTimeElapsed = false;
    public override void Enter()
    {
        Node uiLayer = GetTree().GetFirstNodeInGroup("UI_Layer");
        if (uiLayer != null)
        {
            Card_UI.Reparent(uiLayer);
        }

        Card_UI.Color.Color = new Color(0, 0, (float)0.501961, 1); //NAVY BLUE
        Card_UI.State.Text = "DRAGGING";

        minimumDragTimeElapsed = false;
        SceneTreeTimer thresholdTimer = GetTree().CreateTimer(DRAG_MINIMUM_THRESHOLD, false);
        thresholdTimer.Timeout += () => minimumDragTimeElapsed = true;
    }

    public override void OnInput(InputEvent @event)
    {
        bool singleTargeted = Card_UI.card.IsSingleTargeted();
        bool mouseMotion = @event is InputEventMouseMotion;
        bool cancel = @event.IsActionPressed("right_mouse");
        bool confirm = @event.IsActionReleased("left_mouse") || @event.IsActionPressed("left_mouse");

        if (singleTargeted && mouseMotion && Card_UI.Targets.Count > 0)
        {
            EmitSignal(nameof(TransitionRequested), this, (int)State.Aiming);
            return;
        }

        if(mouseMotion)
        {
            Card_UI.GlobalPosition = Card_UI.GetGlobalMousePosition() - Card_UI.PivotOffset;
        }

        if (cancel)
        {
            EmitSignal(nameof(TransitionRequested), this, (int)State.Base);
        }
        else if (minimumDragTimeElapsed && confirm)
        {
            GetViewport().SetInputAsHandled();
            EmitSignal(nameof(TransitionRequested), this, (int)State.Released);
        }

    }
}
