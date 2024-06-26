using Godot;

public partial class CardState : Node
{
    public enum State { Base, Clicked, Dragging, Aiming, Released}

    [Signal]
    public delegate void TransitionRequestedEventHandler(CardState from, int to);

    [Export]
    public State state;

    public CardUI card_ui;

    public virtual void _Ready()
    {
        
    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void OnInput(InputEvent @event)
    {
        
    }

    public virtual void OnGUIInput(InputEvent @event)
    {

    }

    public virtual void OnMouseEntered()
    {

    }

    public virtual void OnMouseExited()
    {

    }
}
