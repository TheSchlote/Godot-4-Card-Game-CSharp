using Godot;
using System;

public partial class CardState : Node
{
    public enum State { Base, Clicked, Dragging, Aiming, Released}

    [Signal]
    public delegate void TransitionRequestedEventHandler(CardState from, State to);

    [Export]
    public State state;

    public CardUI card_ui;

    public override void _Ready()
    {
        
    }

    public void Enter()
    {

    }

    public void Exit()
    {

    }

    public void OnInput(InputEvent @event)
    {
        
    }

    public void OnGUIInput(in InputEvent @event)
    {

    }

    public void OnMouseEntered()
    {

    }

    public void OnMouseExited()
    {

    }
}
