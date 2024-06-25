using Godot;
using System;

public partial class CardReleasedState : CardState
{
    public void Enter()
    {
        card_ui.Color.Color = new Color((float)0.580392, 0, (float)0.827451, 1);//DARK VIOLET
        card_ui.State.Text = "RELEASED";
    }
}
