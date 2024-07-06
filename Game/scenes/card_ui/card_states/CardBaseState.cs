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
        Card_UI.CardPanel.Set("theme_override_styles/panel", Card_UI.BaseStyleBox);
        Card_UI.EmitSignal("ReparentRequested", Card_UI);
        Card_UI.PivotOffset = Vector2.Zero;
    }

    public override void OnGUIInput(InputEvent @event)
    {
        if(!Card_UI.Playable || Card_UI.Disabled)
        {
            return;
        }
        if (@event.IsActionPressed("left_mouse"))
        {
            Card_UI.PivotOffset = Card_UI.GetGlobalMousePosition() - Card_UI.GlobalPosition;
            EmitSignal(nameof(TransitionRequested), this, (int)State.Clicked);
        }
    }

    public override void OnMouseEntered()
    {
        if (!Card_UI.Playable || Card_UI.Disabled)
        {
            return;
        }
        Card_UI.CardPanel.Set("theme_override_styles/panel", Card_UI.HoverStyleBox);
    }

    public override void OnMouseExited()
    {
        if (!Card_UI.Playable || Card_UI.Disabled)
        {
            return;
        }
        Card_UI.CardPanel.Set("theme_override_styles/panel", Card_UI.BaseStyleBox);
    }
}
