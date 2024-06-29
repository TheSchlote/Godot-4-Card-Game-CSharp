using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

[GlobalClass]
public partial class CardPile : Resource
{
    [Signal]
    public delegate void CardPileSizeChangedEventHandler(int cardsAmount);

    [Export]
    public Card[] Cards { get; set; } = new Card[0];

    public bool IsEmpty()
    {
        return Cards.Length == 0;
    }

    public Card DrawCard()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("Cannot draw a card from an empty pile.");
        }

        Card card = Cards[0];
        var tempList = new List<Card>(Cards);
        tempList.RemoveAt(0);
        Cards = tempList.ToArray();
        EmitSignal(nameof(CardPileSizeChanged), Cards.Length);
        return card;
    }

    public void AddCard(Card card)
    {
        var tempList = new List<Card>(Cards) { card };
        Cards = tempList.ToArray();
        EmitSignal(nameof(CardPileSizeChanged), Cards.Length);
    }

    public void Shuffle()
    {
        Random rng = new Random();
        for (int i = Cards.Length - 1; i > 0; i--)
        {
            int k = rng.Next(i + 1);
            Card temp = Cards[i];
            Cards[i] = Cards[k];
            Cards[k] = temp;
        }
    }

    public void Clear()
    {
        Cards = new Card[0];
        EmitSignal(nameof(CardPileSizeChanged), Cards.Length);
    }

    public override string ToString()
    {
        List<string> cardStrings = new List<string>();
        for (int i = 0; i < Cards.Length; i++)
        {
            cardStrings.Add($"{i + 1}: {Cards[i].Id}");
        }
        return string.Join("\n", cardStrings);
    }
}
