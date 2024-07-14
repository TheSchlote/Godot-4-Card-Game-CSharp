using Godot;
using System;

public partial class Player : Node2D
{
    private CharacterStats stats;
    [Export]
    public CharacterStats Stats
    {
        get { return stats; }
        set { SetCharacterStats(value); }
    }

    private Sprite2D sprite2D;
    private StatsUI statsUI;

    public override void _Ready()
    {
        sprite2D = GetNode<Sprite2D>("Sprite2D");
        statsUI = GetNode<StatsUI>("StatsUI");
    }

    private void SetCharacterStats(CharacterStats value)
    {
        stats = value;
        if (!stats.IsConnected(nameof(Stats.StatsChanged), new Callable(this, nameof(UpdateStats))))
        {
            stats.Connect(nameof(Stats.StatsChanged), new Callable(this, nameof(UpdateStats)));
        }

        UpdatePlayer();
    }

    private async void UpdatePlayer()
    {
        if (!(stats is CharacterStats))
            return;
        if (!IsInsideTree())
            await ToSignal(this, "ready");

        sprite2D.Texture = (Texture2D)stats.Art;
        UpdateStats();
    }

    private void UpdateStats()
    {
        statsUI.UpdateStats(stats);
    }

    public void TakeDamage(int damage)
    {
        if (stats.Health <= 0)
            return;

        stats.TakeDamage(damage);

        if(stats.Health <- 0)
        {
            Events events = GetNode<Events>("/root/Events");
            events.EmitSignal("PlayerDied");
            QueueFree();
        }
    }
}
