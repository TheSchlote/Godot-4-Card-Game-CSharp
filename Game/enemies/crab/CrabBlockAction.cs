using Godot;
using System;

public partial class CrabBlockAction : EnemyAction
{
    [Export]
    public int Block { get; set; } = 6;

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
