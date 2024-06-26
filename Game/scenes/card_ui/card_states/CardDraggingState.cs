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
            card_ui.Reparent(uiLayer);
        }

        card_ui.Color.Color = new Color(0, 0, (float)0.501961, 1); //NAVY BLUE
        card_ui.State.Text = "DRAGGING";

        minimumDragTimeElapsed = false;
        SceneTreeTimer thresholdTimer = GetTree().CreateTimer(DRAG_MINIMUM_THRESHOLD, false);
        thresholdTimer.Timeout += () => minimumDragTimeElapsed = true;
    }

    public override void OnInput(InputEvent @event)
    {
        bool mouseMotion = @event is InputEventMouseMotion;
        bool cancel = @event.IsActionPressed("right_mouse");
        bool confirm = @event.IsActionReleased("left_mouse") || @event.IsActionPressed("left_mouse");

        if(mouseMotion)
        {
            card_ui.GlobalPosition = card_ui.GetGlobalMousePosition() - card_ui.PivotOffset;
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
