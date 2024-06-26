using Godot;
using System.Collections.Generic;

public partial class CardUI : Control
{
    [Signal]
    public delegate void ReparentRequestedEventHandler(CardUI whichCardUI);

    [Export]
    public Card card;
    public ColorRect Color;
    public Label State;
    public Area2D DropPointDetector;
    public CardStateMachine StateMachine;
    public List<Area2D> Targets = new List<Area2D>();
    public Control Parent;
    public Tween tween;

    public override void _Ready()
    {
        Color = GetNode<ColorRect>("Color");
        State = GetNode<Label>("State");
        DropPointDetector = GetNode<Area2D>("DropPointDetector");
        StateMachine = GetNode<CardStateMachine>("CardStateMachine");

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

}
