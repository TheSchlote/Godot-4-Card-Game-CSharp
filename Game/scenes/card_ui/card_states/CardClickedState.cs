using Godot;

public partial class CardClickedState : CardState
{
    public override void Enter()
    {
        Card_UI.Color.Color = new Color(1, (float)0.647059, 0, 1);//ORANGE
        Card_UI.State.Text = "CLICKED";
        Card_UI.GetNode<Area2D>("DropPointDetector").Monitoring = true;
    }

    public override void OnInput(InputEvent @event)
    {
        if(@event is InputEventMouseMotion)
        {
            EmitSignal(nameof(TransitionRequested), this, (int)State.Dragging);
        }
    }
}
