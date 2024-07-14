using Godot;
using System;

[GlobalClass]
public partial class EnemyStats : Stats
{
    [Export]
    public PackedScene AI {  get; set; }
}
