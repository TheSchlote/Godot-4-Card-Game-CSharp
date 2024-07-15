using Godot;
using System;

public partial class Events : Node
{
    [Signal]
    public delegate void CardDragStartedEventHandler(CardUI cardUI);
    [Signal]
    public delegate void CardDragEndedEventHandler(CardUI cardUI);
    [Signal]
    public delegate void CardAimStartedEventHandler(CardUI cardUI);
    [Signal]
    public delegate void CardAimEndedEventHandler(CardUI cardUI);
    [Signal]
    public delegate void CardPlayedEventHandler(Card card);
    [Signal]
    public delegate void CardTooltipRequestedEventHandler(Card card);
    [Signal]
    public delegate void TooltipHideEventHandler();

    //Player related events
    [Signal]
    public delegate void PlayerHandDrawnEventHandler();
    [Signal]
    public delegate void PlayerHandDiscardedEventHandler();
    [Signal]
    public delegate void PlayerTurnEndedEventHandler();
    [Signal]
    public delegate void PlayerHitEventHandler();
    [Signal]
    public delegate void PlayerDiedEventHandler();

    //Enemy related events
    [Signal]
    public delegate void EnemyActionCompletedEventHandler();
    [Signal]
    public delegate void EnemyTurnEndedEventHandler();
}
