using Godot;
using System;

public partial class Effect : RefCounted
{
    public AudioStream Sound { get; set; }
    public virtual void Execute(Godot.Collections.Array<Node> targets)
    {

    }
}
