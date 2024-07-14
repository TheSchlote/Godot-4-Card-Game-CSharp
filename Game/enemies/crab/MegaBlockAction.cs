using Godot;
using System;

public partial class MegaBlockAction : EnemyAction
{
    [Export]
    public int Block { get; set; } = 15;

    [Export]
    public int HpThreshold { get; set; } = 6;

    private bool alreadyUsed = false;

    public override bool IsPerformable()
    {
        if (Enemy == null || alreadyUsed)
        {
            return false;
        }

        bool isLow = Enemy.Stats.Health <= HpThreshold;
        alreadyUsed = isLow;

        return isLow;
    }

    public override void PerformAction()
    {
        if (Enemy == null || Target == null)
        {
            return;
        }

        BlockEffect blockEffect = new BlockEffect
        {
            Amount = Block
        };

        blockEffect.Execute(new Godot.Collections.Array<Node> { Enemy });

        SceneTreeTimer timer = GetTree().CreateTimer(0.6f, false);
        timer.Timeout += () =>
        {
            Events events = GetNode<Events>("/root/Events");
            events.EmitSignal(nameof(Events.EnemyActionCompleted), Enemy);
        };
    }
}
