using Godot;
using System;

//Change name of class
public partial class EffectTemplate : Effect
{
    [Export]
    public int MemberVar { get; set; } = 0;

    public override void Execute(Godot.Collections.Array<Node> targets)
    {
        GD.Print($"My effect targets them: {targets}");
        GD.Print($"It does {MemberVar} something");
    }
}
