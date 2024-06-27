using Godot;

public partial class CardBaseState : CardState
{
    public async override void Enter()
    {
        if (!Card_UI.IsNodeReady())
        {
            await ToSignal(Card_UI, "ready");
        }
        if (Card_UI.tween != null && Card_UI.tween.IsRunning())
        {
            Card_UI.tween.Kill();
        }

        Card_UI.EmitSignal("ReparentRequested", Card_UI);
        Card_UI.Color.Color = new Color(0, (float)0.501961, 0, 1);//WEB_GREEN
        Card_UI.State.Text = "BASE";
        Card_UI.PivotOffset = Vector2.Zero;
    }

    public override void OnGUIInput(InputEvent @event)
    {
        if (@event.IsActionPressed("left_mouse"))
        {
            Card_UI.PivotOffset = Card_UI.GetGlobalMousePosition() - Card_UI.GlobalPosition;
            EmitSignal(nameof(TransitionRequested), this, (int)State.Clicked);
        }
    }
}
