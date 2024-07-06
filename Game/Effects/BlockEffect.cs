using Godot;
using System;

public partial class BlockEffect : Effect
{
    public int Amount { get; set; } = 0;
    public override void Execute(Godot.Collections.Array<Node> targets)
    {
        foreach (var target in targets)
        {
            if (target == null)
            {
                continue;
            }

            if (target is Enemy enemy)
            {
                enemy.Stats.Block += Amount;
            }
            else if (target is Player player)
            {
                player.Stats.Block += Amount;
            }
        }
    }
}
