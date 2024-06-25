using Godot;

public partial class CardUI : Control
{
    [Signal]
    public delegate void ReparentRequestedEventHandler(CardUI whichCardUI);

    public ColorRect Color;
    public Label State;
    public CardStateMachine StateMachine;

    public override void _Ready()
    {
        Color = GetNode<ColorRect>("Color");
        State = GetNode<Label>("State");
        StateMachine = GetNode<CardStateMachine>("CardStateMachine");

        StateMachine.Init(this);
    }

    public override void _Input(InputEvent @event)
    {
        StateMachine.OnInput(@event);
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
}
