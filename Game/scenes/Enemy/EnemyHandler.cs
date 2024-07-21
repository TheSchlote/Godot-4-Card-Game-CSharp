using Godot;
using System;

public partial class EnemyHandler : Node2D
{
    public override void _Ready()
    {
        Events.Instance.Connect("EnemyActionCompleted", new Callable(this, nameof(OnEnemyActionCompleted)));
    }

    public void ResetEnemyActions()
    {
        foreach (Enemy enemy in GetChildren())
        {
            enemy.CurrentAction = null;
            enemy.UpdateAction();
        }
    }

    public void StartTurn()
    {
        if (GetChildCount() == 0)
        {
            return;
        }

        Enemy firstEnemy = GetChild<Enemy>(0);
        firstEnemy.DoTurn();
    }

    private void OnEnemyActionCompleted(Enemy enemy)
    {
        if (enemy.GetIndex() == GetChildCount() - 1)
        {
            Events.Instance.EmitSignal("EnemyTurnEnded");
            return;
        }

        Enemy nextEnemy = GetChild<Enemy>(enemy.GetIndex() + 1);
        nextEnemy.DoTurn();
    }
}
