using Godot;

public partial class CardUI : Control
{
    [Signal]
    public delegate void ReparentRequestedEventHandler(CardUI whichCardUI);

    public ColorRect Color;
    public Label State;

    public override void _Ready()
    {
        Color = GetNode<ColorRect>("Color");
        State = GetNode<Label>("State");
    }
}
