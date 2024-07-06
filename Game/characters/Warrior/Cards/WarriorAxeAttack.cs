using Godot;
using Godot.Collections;
using System;

public partial class WarriorAxeAttack : Card
{
    public override void ApplyEffects(Array<Node> targets)
    {
        var damageEffect = new DamageEffect
        {
            Amount = 1
        };
        damageEffect.Execute(targets);
    }
}
