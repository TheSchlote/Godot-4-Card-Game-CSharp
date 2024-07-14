using Godot;
using System;
using System.Data;

public partial class Enemy : Area2D
{
    private const int ArrowOffset = 5;

    private EnemyStats stats;
    [Export]
    public EnemyStats Stats
    {
        get { return stats; }
        set { SetEnemyStats(value); }
    }
    private Sprite2D sprite2D;
    private Sprite2D arrow;
    private StatsUI statsUI;
    private EnemyActionPicker enemyActionPicker;
    private EnemyAction currentAction;

    public EnemyAction CurrentAction
    {
        get { return currentAction; }
        set { SetCurrentAction(value); }
    }

    public override void _Ready()
    {
        sprite2D = GetNode<Sprite2D>("Sprite2D");
        arrow = GetNode<Sprite2D>("Arrow");
        statsUI = GetNode<StatsUI>("StatsUI");
    }
    private void SetCurrentAction(EnemyAction value)
    {
        currentAction = value;
        if (currentAction != null)
        {
            //intentUI.UpdateIntent(currentAction.Intent);
        }
    }
    private void SetEnemyStats(EnemyStats value)
    {
        stats = (EnemyStats)value.CreateInstance();

        if (!stats.IsConnected(nameof(Stats.StatsChanged), new Callable(this, nameof(UpdateStats))))
        {
            stats.Connect(nameof(Stats.StatsChanged), new Callable(this, nameof(UpdateStats)));
            stats.Connect(nameof(Stats.StatsChanged), new Callable(this, nameof(UpdateAction)));
        }

        UpdateEnemy();
    }
    private void SetupAI()
    {
        if (enemyActionPicker != null)
        {
            enemyActionPicker.QueueFree();
        }

        EnemyActionPicker newActionPicker = (EnemyActionPicker)stats.AI.Instantiate();
        AddChild(newActionPicker);
        enemyActionPicker = newActionPicker;
        enemyActionPicker.Enemy = this;
    }
    public void UpdateAction()
    {
        if (enemyActionPicker == null)
            return;

        if (currentAction == null)
        {
            currentAction = enemyActionPicker.GetAction();
            return;
        }

        EnemyAction newConditionalAction = enemyActionPicker.GetFirstConditionalAction();
        if (newConditionalAction != null && currentAction != newConditionalAction)
        {
            currentAction = newConditionalAction;
        }
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
        SetupAI();
        UpdateStats();
    }
    public void DoTurn()
    {
        stats.Block = 0;

        if (currentAction == null)
            return;

        currentAction.PerformAction();
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
