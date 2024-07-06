using Godot;
using System;
using System.Data;

public partial class Enemy : Area2D
{
    private const int ArrowOffset = 5;

    private Stats stats;
    [Export]
    public Stats Stats
    {
        get { return stats; }
        set { SetEnemyStats(value); }
    }
    private Sprite2D sprite2D;
    private Sprite2D arrow;
    private StatsUI statsUI;


    public override void _Ready()
    {
        sprite2D = GetNode<Sprite2D>("Sprite2D");
        arrow = GetNode<Sprite2D>("Arrow");
        statsUI = GetNode<StatsUI>("StatsUI");
    }

    private void SetEnemyStats(Stats value)
    {
        stats = value.CreateInstance();

        if (!stats.IsConnected(nameof(Stats.StatsChanged), new Callable(this, nameof(UpdateStats))))
        {
            stats.Connect(nameof(Stats.StatsChanged), new Callable(this, nameof(UpdateStats)));
        }

        UpdateEnemy();
    }
    private void UpdateStats()
    {
        statsUI.UpdateStats(stats);
    }
    private async void UpdateEnemy()
    {
        if (!(stats is Stats))
            return;
        if (!IsInsideTree())
            await ToSignal(this, "ready");

        sprite2D.Texture = (Texture2D)stats.Art;
        arrow.Position = Vector2.Right * (sprite2D.GetRect().Size.X / 2 + ArrowOffset);
        UpdateStats();
    }
    public void TakeDamage(int damage)
    {
        if (stats.Health <= 0)
            return;

        if (stats.Health <= 0)
        {
            QueueFree();
        }
    }

    public void OnAreaEntered(Area2D area)
    {
        arrow.Show();
    }
    public void OnAreaExited(Area2D area)
    {
        arrow.Hide();
    }
}
