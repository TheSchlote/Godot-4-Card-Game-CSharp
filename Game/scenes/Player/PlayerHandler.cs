using Godot;
using System;

public partial class PlayerHandler : Node
{
    private const float HandDrawInterval = 0.25f;
    private const float HandDiscardInterval = 0.25f;

    [Export]
    public Hand Hand { get; set; }

    private CharacterStats _character;

    public override void _Ready()
    {
        Events.Instance.Connect("CardPlayed", new Callable(this, nameof(OnCardPlayed)));
    }

    public void StartBattle(CharacterStats charStats)
    {
        _character = charStats;
        _character.DrawPile = (CardPile)_character.Deck.Duplicate(true);
        _character.DrawPile.Shuffle();
        _character.Discard = new CardPile();
        StartTurn();
    }

    public void StartTurn()
    {
        _character.Block = 0;
        _character.ResetMana();
        DrawCards(_character.CardsPerTurn);
    }

    public void EndTurn()
    {
        Hand.DisableHand();
        DiscardCards();
    }

    public void DrawCard()
    {
        ReshuffleDeckFromDiscard();
        Hand.AddCard(_character.DrawPile.DrawCard());
        ReshuffleDeckFromDiscard();
    }

    public void DrawCards(int amount)
    {
        var tween = CreateTween();
        for (int i = 0; i < amount; i++)
        {
            tween.TweenCallback(Callable.From(DrawCard));
            tween.TweenInterval(HandDrawInterval);
        }

        tween.Finished += () =>
        {
            Events.Instance.EmitSignal("PlayerHandDrawn");
        };
    }

    public void DiscardCards()
    {
        var tween = CreateTween();
        foreach (CardUI cardUi in Hand.GetChildren())
        {
            tween.TweenCallback(Callable.From(() => _character.Discard.AddCard(cardUi.Card)));
            tween.TweenCallback(Callable.From(() => Hand.DiscardCard(cardUi)));
            tween.TweenInterval(HandDiscardInterval);
        }

        tween.Finished += () =>
        {
            Events.Instance.EmitSignal("PlayerHandDiscarded");
        };
    }

    public void ReshuffleDeckFromDiscard()
    {
        if (!_character.DrawPile.IsEmpty())
            return;

        while (!_character.Discard.IsEmpty())
        {
            _character.DrawPile.AddCard(_character.Discard.DrawCard());
        }

        _character.DrawPile.Shuffle();
    }

    private void OnCardPlayed(Card card)
    {
        _character.Discard.AddCard(card);
    }
}
