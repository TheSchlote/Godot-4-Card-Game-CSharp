using Godot;

public partial class CardClickedState : CardState
{
    public override void Enter()
    {
        card_ui.Color.Color = new Color(1, (float)0.647059, 0, 1);//ORANGE
        card_ui.State.Text = "CLICKED";
        card_ui.GetNode<Area2D>("DropPointDetector").Monitoring = true;
    }

    public override void OnInput(InputEvent @event)
    {
        if(@event is InputEventMouseMotion)
        {
            EmitSignal(nameof(TransitionRequested), this, (int)State.Dragging);
        }
    }
}
