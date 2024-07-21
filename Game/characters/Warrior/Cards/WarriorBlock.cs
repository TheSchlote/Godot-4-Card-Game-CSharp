using Godot;
using Godot.Collections;
using System;

public partial class WarriorBlock : Card
{
    public override void ApplyEffects(Array<Node> targets)
    {
        var blockEffect = new BlockEffect
        {
            Amount = 5,
            Sound = Sound
        };
        blockEffect.Execute(targets);
    }
}
