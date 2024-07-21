using Godot;
using System;
using System.Data;

public partial class Enemy : Area2D
{
    private const int ArrowOffset = 5;
    private static readonly Material WhiteSpriteMaterial = (Material)GD.Load("res://art/white_sprite_material.tres");


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
    private IntentUI intentUI;
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
        intentUI = GetNode<IntentUI>("IntentUI");
    }
    private void SetCurrentAction(EnemyAction value)
    {
        currentAction = value;
        if (currentAction != null)
        {
            intentUI.UpdateIntent(currentAction.Intent);
        }
    }
    private void SetEnemyStats(EnemyStats value)
    {
        stats = (EnemyStats)value.CreateInstance();
        GD.Print($"Enemy stats set: {stats}");

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

        if (CurrentAction == null)
        {
            CurrentAction = enemyActionPicker.GetAction();
            return;
        }

        EnemyAction newConditionalAction = enemyActionPicker.GetFirstConditionalAction();
        if (newConditionalAction != null && CurrentAction != newConditionalAction)
        {
           CurrentAction = newConditionalAction;
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

        if (CurrentAction == null)
        {
            return;
        }

        CurrentAction.PerformAction();
    }
    public void TakeDamage(int damage)
    {
        if (stats.Health <= 0)
            return;

        sprite2D.Material = WhiteSpriteMaterial;

        var tween = CreateTween();
        tween.TweenCallback(Callable.From(() => Shaker.Shake(this, 16, 0.15f)));
        tween.TweenCallback(Callable.From(() => stats.TakeDamage(damage)));
        tween.TweenInterval(0.17f);

        tween.Finished += () =>
        {
            sprite2D.Material = null;

            if (stats.Health <= 0)
            {
                QueueFree();
            }
        };
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
