using Godot;

public partial class Hand : HBoxContainer
{
    public override void _Ready()
    {
        foreach (CardUI child in GetChildren())
        {
            child.Parent = this;
            child.Connect("ReparentRequested", new Callable(this, nameof(OnCardUIReparentRequested)));
        }
    }

    public void OnCardUIReparentRequested(CardUI child)
    {
        child.Reparent(this);
    }
}
