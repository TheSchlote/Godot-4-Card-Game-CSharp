using Godot;
using System.Collections.Generic;

public partial class CardStateMachine : Node
{
    [Export]
    public CardState IntialState { get; set; }

    private CardState _currentState;
    Dictionary<CardState.State, CardState> _states = new Dictionary<CardState.State, CardState> ();

    public void Init(CardUI card)
    {
        foreach (CardState child in GetChildren())
        {
            if (child != null)
            {
                _states[child.state] = child;
                child.Connect(nameof(CardState.TransitionRequested), new Callable(this, nameof(OnTransitionRequested)));
                child.card_ui = card;
            }
        }
        if (IntialState != null)
        {
            _currentState = IntialState;
            IntialState.Enter();
        }
    }

    public void OnInput(InputEvent @event)
    {
        if (_currentState != null)
        {
            _currentState.OnInput(@event);
        }
    }

    public void OnGUIInput(InputEvent @event)
    {
        if (_currentState != null)
        {
            _currentState.OnGUIInput(@event);
        }
    }

    public void OnMouseEntered()
    {
        if (_currentState != null)
        {
            _currentState.OnMouseEntered();
        }
    }

    public void OnMouseExited()
    {
        if( _currentState != null)
        { 
            _currentState.OnMouseExited();
        }
    }

    private void OnTransitionRequested(CardState from, CardState.State to)
    {
        if (from != _currentState)
            return;

        if (!_states.TryGetValue(to, out CardState newState) || newState == null)
            return;

        _currentState?.Exit();
        newState.Enter();
        _currentState = newState;
    }
}
