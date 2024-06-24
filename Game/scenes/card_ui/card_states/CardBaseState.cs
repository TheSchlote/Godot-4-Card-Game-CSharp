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
        card_ui.Color.Color = Color.Color8(26, 72, 45);
        card_ui.State.Text = "BASE";
        card_ui.PivotOffset = Vector2.Zero;
    }

    public void OnGUIInput(InputEvent @event)
    {
        if (@event.IsActionPressed("left_mouse"))
        {
            card_ui.PivotOffset = card_ui.GetGlobalMousePosition() - card_ui.GlobalPosition;
            EmitSignal(nameof(TransitionRequested), this, State.Clicked);
        }
    }
}
