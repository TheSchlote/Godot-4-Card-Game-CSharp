using Godot;
using System;

public partial class CardAimingState : CardState
{
    private const float MouseYSnapbackThreshold = 138.0f;
    public override void Enter()
    {
        Card_UI.Color.Color = new Color((float)0.501961, 0, (float)0.501961, 1);//WEB PURPLE
        Card_UI.State.Text = "AIMING";
        Card_UI.Targets.Clear();
        Vector2 offset = new Vector2(Card_UI.Parent.Size.X / 2, -Card_UI.Size.Y / 2);
        offset.X -= Card_UI.Size.X / 2;
        Card_UI.AnimateToPosition(Card_UI.Parent.GlobalPosition + offset, 0.2f);
        Card_UI.DropPointDetector.Monitoring = false;

        var events = GetNode<Events>("/root/Events");
        events.EmitSignal("CardAimStarted", Card_UI);
    }
    public override void Exit()
    {
        var events = GetNode<Events>("/root/Events");
        events.EmitSignal("CardAimEnded", Card_UI);
    }
    public override void OnInput(InputEvent @event)
    {
        bool mouseMotion = @event is InputEventMouseMotion;
        bool mouseAtBottom = Card_UI.GetGlobalMousePosition().Y > MouseYSnapbackThreshold;

        if ((mouseMotion && mouseAtBottom) || @event.IsActionPressed("right_mouse"))
        {
            EmitSignal(nameof(TransitionRequested), this, (int)State.Base);
        }
        else if (@event.IsActionReleased("left_mouse") || @event.IsActionPressed("left_mouse"))
        {
            GetViewport().SetInputAsHandled();
            EmitSignal(nameof(TransitionRequested), this, (int)State.Released);
        }
    }
}
