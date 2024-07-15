using Godot;
using System;

public partial class RedFlash : CanvasLayer
{
    private ColorRect colorRect;
    private Timer timer;

    public override void _Ready()
    {
        colorRect = GetNode<ColorRect>("ColorRect");
        timer = GetNode<Timer>("Timer");

        Events events = GetNode<Events>("/root/Events");
        events.Connect(nameof(Events.PlayerHit), new Callable(this, nameof(OnPlayerHit)));
        timer.Timeout += OnTimerTimeout;
    }

    private void OnPlayerHit()
    {
        Color color = colorRect.Color;
        color.A = 0.2f;  // Set alpha to 0.2
        colorRect.Color = color;
        timer.Start();
    }

    private void OnTimerTimeout()
    {
        Color color = colorRect.Color;
        color.A = 0.0f;  // Set alpha to 0
        colorRect.Color = color;
    }
}
