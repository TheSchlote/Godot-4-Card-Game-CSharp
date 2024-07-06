using Godot;
using System.Collections.Generic;

public partial class CardUI : Control
{
    [Signal]
    public delegate void ReparentRequestedEventHandler(CardUI whichCardUI);

    public readonly StyleBox BaseStyleBox = GD.Load<StyleBox>("res://scenes/card_ui/CardBaseStyleBox.tres");
    public readonly StyleBox DragStyleBox = GD.Load<StyleBox>("res://scenes/card_ui/CardDraggingStyleBox.tres");
    public readonly StyleBox HoverStyleBox = GD.Load<StyleBox>("res://scenes/card_ui/CardHoverStyleBox.tres");

    [Export]
    public CharacterStats CharStats
    {
        get => charStats;
        set => SetCharStats(value);
    }
    private CharacterStats charStats;

    [Export]
    public Card Card
    {
        get => card;
        set => SetCard(value);
    }
    private Card card;
    public Panel CardPanel { get; set; }
    public Label Cost { get; set; }
    public TextureRect Icon { get; set; }
    public Area2D DropPointDetector;
    public CardStateMachine StateMachine;
    public Godot.Collections.Array<Node> Targets = new Godot.Collections.Array<Node>();
    public Control Parent;
    public Tween tween;
    public bool Playable
    {
        get => playable;
        set => SetPlayable(value);
    }
    private bool playable = true;
    public bool Disabled = false;
    public int OriginalIndex;

    public override void _Ready()
    {
        CardPanel = GetNode<Panel>("Panel");
        Cost = GetNode<Label>("Cost");
        Icon = GetNode<TextureRect>("Icon");
        DropPointDetector = GetNode<Area2D>("DropPointDetector");
        StateMachine = GetNode<CardStateMachine>("CardStateMachine");

        Events events = GetNode<Events>("/root/Events");
        events.Connect(nameof(Events.CardAimStarted), new Callable(this, nameof(OnCardDragOrAimingStarted)));
        events.Connect(nameof(Events.CardDragStarted), new Callable(this, nameof(OnCardDragOrAimingStarted)));
        events.Connect(nameof(Events.CardDragEnded), new Callable(this, nameof(OnCardDragOrAimEnded)));
        events.Connect(nameof(Events.CardAimEnded), new Callable(this, nameof(OnCardDragOrAimEnded)));


        StateMachine.Init(this);
    }

    public override void _Input(InputEvent @event)
    {
        StateMachine.OnInput(@event);
    }

    public void AnimateToPosition(Vector2 newPosiiton, float duration)
    {
        tween = CreateTween().SetTrans(Tween.TransitionType.Circ).SetEase(Tween.EaseType.Out);
        tween.TweenProperty(this, "position", newPosiiton, duration);
    }

    public void Play()
    {
        Events events = GetNode<Events>("/root/Events");
        Card.Play(Targets, charStats, events);
        QueueFree();
    }
    public void _OnGuiInput(InputEvent @event)
    {
        StateMachine.OnGUIInput(@event);
    }

    public void _OnMouseEntered()
    {
        StateMachine.OnMouseEntered();
    }

    public void _OnMouseExited()
    {
        StateMachine.OnMouseExited();
    }

    private void SetCard(Card value)
    {
        if (!IsInsideTree())
        {
            CallDeferred(nameof(SetCard), value);
        }

        card = value;
        if (Cost != null)
        {
            Cost.Text = value.Cost.ToString();
        }
        if (Icon != null)
        {
            Icon.Texture = (Texture2D)value.Icon;
        }
    }
    private void SetCharStats(CharacterStats value)
    {
        charStats = value;
        charStats.Connect(nameof(CharacterStats.StatsChanged), new Callable(this, nameof(OnCharStatsChanged)));
    }
    private void SetPlayable(bool value)
    {
        playable = value;
        if (!playable)
        {
            Cost.AddThemeColorOverride("font_color", new Color(1, 0, 0));//RED
            Icon.Modulate = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            Cost.RemoveThemeColorOverride("font_color");
            Icon.Modulate = new Color(1, 1, 1, 1);
        }
    }
    public void OnDropPointDetectorAreaEntered(Area2D area2D)
    {
        if (!Targets.Contains(area2D))
        {
            Targets.Add(area2D);
        }
    }

    public void OnDropPointDetectorAreaExited(Area2D area2D)
    {
        Targets.Remove(area2D);
    }
    private void OnCardDragOrAimingStarted(CardUI usedCard)
    {
        if (usedCard == this)
        {
            return;
        }

        Disabled = true;
    }

    private void OnCardDragOrAimEnded(CardUI card)
    {
        Disabled = false;
        Playable = charStats.CanPlayCard(Card);
    }
    private void OnCharStatsChanged()
    {
        Playable = charStats.CanPlayCard(Card);
    }
}
