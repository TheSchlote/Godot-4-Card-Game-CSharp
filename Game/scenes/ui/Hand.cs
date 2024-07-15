using Godot;

public partial class Hand : HBoxContainer
{
    [Export]
    public CharacterStats CharStats;

    public PackedScene CardUiScene = GD.Load<PackedScene>("res://scenes/card_ui/card_ui.tscn");

    public void AddCard(Card card)
    {
        var newCardUi = (CardUI)CardUiScene.Instantiate();
        AddChild(newCardUi);
        newCardUi.Connect("ReparentRequested", new Callable(this, nameof(OnCardUIReparentRequested)));
        newCardUi.Card = card;
        newCardUi.Parent = this;
        newCardUi.CharStats = CharStats;
    }
    public void DiscardCard(CardUI card)
    {
        card.QueueFree();
    }

    public void DisableHand()
    {
        foreach (CardUI card in GetChildren())
        {
            card.Disabled = true;
        }
    }

    public void OnCardUIReparentRequested(CardUI child)
    {
        child.Disabled = true;
        child.Reparent(this);
        int newIndex = Mathf.Clamp(child.OriginalIndex, 0, GetChildCount());
        CallDeferred(nameof(DeferredMoveChild), child, newIndex);
        child.SetDeferred("Disabled", false);
    }

    private void DeferredMoveChild(CardUI child, int newIndex)
    {
        MoveChild(child, newIndex);
    }
}
