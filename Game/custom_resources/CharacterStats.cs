using Godot;
using System;

[GlobalClass]
public partial class CharacterStats : Stats
{
    [Export]
    public CardPile StartingDeck { get; set; }

    [Export]
    public int CardsPerTurn { get; set; }

    [Export]
    public int MaxMana { get; set; }

    private int mana;
    public int Mana
    {
        get { return mana; }
        set { SetMana(value); }
    }

    public CardPile Deck { get; set; }
    public CardPile Discard { get; set; }
    public CardPile DrawPile { get; set; }

    private void SetMana(int value)
    {
        mana = value;
        EmitSignal(nameof(StatsChanged));
    }

    public void ResetMana()
    {
        Mana = MaxMana;
    }
    public override void TakeDamage(int damage)
    {
        int initialHealth = Health;
        base.TakeDamage(damage);
        if (initialHealth > Health)
        {
            Events.Instance.EmitSignal(nameof(Events.PlayerHit));
        }
    }
    public bool CanPlayCard(Card card)
    {
        return Mana >= card.Cost;
    }

    public new CharacterStats CreateInstance()
    {
        CharacterStats instance = (CharacterStats)Duplicate();
        instance.Health = MaxHealth;
        instance.Block = 0;
        instance.ResetMana();
        instance.Deck = (CardPile)StartingDeck.Duplicate();
        instance.DrawPile = new CardPile();
        instance.Discard = new CardPile();
        return instance;
    }
}
