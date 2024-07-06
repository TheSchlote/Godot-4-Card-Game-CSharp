using Godot;
using System;

public partial class DamageEffect : Effect
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
                enemy.Stats.TakeDamage(Amount);
            }
            else if (target is Player player)
            {
                player.Stats.TakeDamage(Amount);
            }
        }
    }
}
