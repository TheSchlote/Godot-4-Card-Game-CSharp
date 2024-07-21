using Godot;
using System;

public partial class WarriorSlash : Card
{
	public override void ApplyEffects(Godot.Collections.Array<Node> targets)
	{
        var damageEffect = new DamageEffect
        {
            Amount = 4,
            Sound = Sound
        };
        damageEffect.Execute(targets);
    }
}

