using Godot;
using System;

[GlobalClass]
public partial class Card : Resource
{
    public enum CardType { Attack, Skill, Power}
    public enum TargetType { Self, SingleEnemy, AllEnemies, Everyone}

    [ExportGroup("Card Attributes")]
    [Export]
    public string Id { get; set; }
    [Export]
    public CardType Type { get; set; }
    [Export]
    public TargetType Target { get; set; }

    public bool IsSingleTargeted()
    {
        return Target == TargetType.SingleEnemy;
    }
}
