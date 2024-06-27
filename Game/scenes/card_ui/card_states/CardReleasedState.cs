using Godot;

public partial class CardReleasedState : CardState
{
    private bool played;
    public override void Enter()
    {
        Card_UI.Color.Color = new Color((float)0.580392, 0, (float)0.827451, 1);//DARK VIOLET
        Card_UI.State.Text = "RELEASED";

        played = false;

        if(Card_UI.Targets.Count > 0)
        {
            played = true;
            GD.Print("Play card for target(s) ", Card_UI.Targets);
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
