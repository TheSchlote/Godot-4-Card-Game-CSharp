using Godot;

public partial class CardReleasedState : CardState
{
    private bool _played;
    public override void Enter()
    {
        _played = false;

        if(Card_UI.Targets.Count > 0)
        {
            Events events = GetNode<Events>("/root/Events");
            events.EmitSignal("TooltipHide");
            _played = true;
            Card_UI.Play();
        }
    }

    public override void OnInput(InputEvent @event)
    {
        if (_played) 
        {
            return;
        }
        Events events = GetNode<Events>("/root/Events");
        events.EmitSignal("TooltipHide");
        EmitSignal(nameof(TransitionRequested), this, (int)State.Base);
    }
}
