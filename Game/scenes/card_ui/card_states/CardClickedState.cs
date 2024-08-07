using Godot;

public partial class CardClickedState : CardState
{
    public override void Enter()
    {
        Card_UI.GetNode<Area2D>("DropPointDetector").Monitoring = true;
        Card_UI.OriginalIndex = Card_UI.GetIndex();
    }

    public override void OnInput(InputEvent @event)
    {
        if(@event is InputEventMouseMotion)
        {
            EmitSignal(nameof(TransitionRequested), this, (int)State.Dragging);
        }
    }
}
