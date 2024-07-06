using Godot;

public partial class CardReleasedState : CardState
{
    private bool played;
    public override void Enter()
    {
        played = false;

        if(Card_UI.Targets.Count > 0)
        {
            played = true;
            Card_UI.Play();
        }
    }

    public override void OnInput(InputEvent @event)
    {
        if (played) 
        {
            return;
        }
        EmitSignal(nameof(TransitionRequested), this, (int)State.Base);
    }
}
