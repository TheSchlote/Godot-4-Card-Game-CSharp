using Godot;
using System;

public partial class EnemyActionPicker : Node
{
    [Export]
    public Enemy Enemy
    {
        get => enemy;
        set => SetEnemy(value);
    }
    private Enemy enemy;

    [Export]
    public Node2D Target
    {
        get => target;
        set => SetTarget(value);
    }
    private Node2D target;

    private float totalWeight = 0.0f;

    public override void _Ready()
    {
        Target = GetTree().GetFirstNodeInGroup("Player") as Node2D;
        SetupChances();
    }

    public EnemyAction GetAction()
    {
        EnemyAction action = GetFirstConditionalAction();
        if (action != null)
        {
            return action;
        }

        return GetChanceBasedAction();
    }

    public EnemyAction GetFirstConditionalAction()
    {
        foreach (Node node in GetChildren())
        {
            if (node is EnemyAction action && action.Type == EnemyAction.ActionType.Conditional)
            {
                if (action.IsPerformable())
                {
                    return action;
                }
            }
        }

        return null;
    }

    private EnemyAction GetChanceBasedAction()
    {
        float roll = (float)GD.RandRange(0.0, totalWeight);

        foreach (Node node in GetChildren())
        {
            if (node is EnemyAction action && action.Type == EnemyAction.ActionType.ChanceBased)
            {
                if (action.AccumulatedWeight > roll)
                {
                    return action;
                }
            }
        }

        return null;
    }

    private void SetupChances()
    {
        totalWeight = 0.0f; // Reset total weight before recalculating

        foreach (Node node in GetChildren())
        {
            if (node is EnemyAction action && action.Type == EnemyAction.ActionType.ChanceBased)
            {
                totalWeight += action.ChanceWeight;
                action.AccumulatedWeight = totalWeight;
            }
        }
    }

    private void SetEnemy(Enemy value)
    {
        enemy = value;

        foreach (Node node in GetChildren())
        {
            if (node is EnemyAction action)
            {
                action.Enemy = enemy;
            }
        }
    }

    private void SetTarget(Node2D value)
    {
        target = value;

        foreach (Node node in GetChildren())
        {
            if (node is EnemyAction action)
            {
                action.Target = target;
            }
        }
    }
}
