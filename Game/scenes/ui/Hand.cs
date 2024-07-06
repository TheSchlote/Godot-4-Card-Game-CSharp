using Godot;

public partial class Hand : HBoxContainer
{
    public int CardsPlayedThisTurn = 0;
    public override void _Ready()
    {
        Events events = GetNode<Events>("/root/Events");
        events.Connect(nameof(Events.CardPlayed), new Callable(this, nameof(OnCardPlayed)));

        foreach (CardUI child in GetChildren())
        {
            child.Parent = this;
            child.Connect("ReparentRequested", new Callable(this, nameof(OnCardUIReparentRequested)));
        }
    }

    public void OnCardPlayed(Card card)
    {
        CardsPlayedThisTurn += 1;
    }

    public void OnCardUIReparentRequested(CardUI child)
    {
        child.Reparent(this);
        int newIndex = Mathf.Clamp(child.OriginalIndex, 0, GetChildCount());
        //CallDeferred(nameof(MoveChild), child, newIndex);
    }
}
