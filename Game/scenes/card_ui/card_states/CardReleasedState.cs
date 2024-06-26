using Godot;

public partial class CardReleasedState : CardState
{
    private bool played;
    public override void Enter()
    {
        card_ui.Color.Color = new Color((float)0.580392, 0, (float)0.827451, 1);//DARK VIOLET
        card_ui.State.Text = "RELEASED";

        played = false;

        if(card_ui.Targets.Count > 0)
        {
            played = true;
            GD.Print("Play card for target(s) ", card_ui.Targets);
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
