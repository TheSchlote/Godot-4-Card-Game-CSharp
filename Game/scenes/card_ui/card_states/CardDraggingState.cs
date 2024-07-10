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
            
        Card_UI.CardPanel.Set("theme_override_styles/panel", Card_UI.DragStyleBox);
        Events events = GetNode<Events>("/root/Events");
        events.EmitSignal(nameof(Events.CardDragStarted), Card_UI);

        minimumDragTimeElapsed = false;
        SceneTreeTimer thresholdTimer = GetTree().CreateTimer(DRAG_MINIMUM_THRESHOLD, false);
        thresholdTimer.Timeout += () => minimumDragTimeElapsed = true;
    }
    public override void Exit()
    {
        Events events = GetNode<Events>("/root/Events");
        events.EmitSignal(nameof(Events.CardDragEnded), Card_UI);
    }
    public override void OnInput(InputEvent @event)
    {
        bool singleTargeted = Card_UI.Card.IsSingleTargeted();
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
            Events events = GetNode<Events>("/root/Events");
            events.EmitSignal("TooltipHide");
            EmitSignal(nameof(TransitionRequested), this, (int)State.Base);
        }
        else if (minimumDragTimeElapsed && confirm)
        {
            GetViewport().SetInputAsHandled();
            EmitSignal(nameof(TransitionRequested), this, (int)State.Released);
        }

    }
}
