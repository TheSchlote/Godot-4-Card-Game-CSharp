using Godot;
using System;

public partial class CardBaseState : CardState
{
    public async void Enter()
    {
        if (!card_ui.IsNodeReady())
        {
            await ToSignal(card_ui, "ready");
        }

        EmitSignal(nameof(card_ui.ReparentRequested), card_ui);
        card_ui.Color.Color = new Color(0, (float)0.501961, 0, 1);//WEB_GREEN
        card_ui.State.Text = "BASE";
        card_ui.PivotOffset = Vector2.Zero;
    }

    public void OnGUIInput(InputEvent @event)
    {
        if (@event.IsActionPressed("left_mouse"))
        {
            card_ui.PivotOffset = card_ui.GetGlobalMousePosition() - card_ui.GlobalPosition;
            EmitSignal(nameof(TransitionRequested), this, (int)State.Clicked);
        }
    }
}
