using Godot;
using System;

[GlobalClass]
public partial class CardTemplate : Card
{
    [Export]
    public AudioStream OptionalSound { get; set; }

    public override void ApplyEffects(Godot.Collections.Array<Node> targets)
    {
        GD.Print("My awesome card has been played!");
        GD.Print($"Targets: {targets}");
    }
}
