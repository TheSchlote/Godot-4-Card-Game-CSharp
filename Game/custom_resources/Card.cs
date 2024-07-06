using Godot;
using System;
using static Godot.HttpRequest;

[GlobalClass]
public partial class Card : Resource
{
    public enum CardType { Attack, Skill, Power }
    public enum TargetType { Self, SingleEnemy, AllEnemies, Everyone }

    [ExportGroup("Card Attributes")]
    [Export]
    public string Id { get; set; }
    [Export]
    public CardType Type { get; set; }
    [Export]
    public TargetType Target { get; set; }
    [Export]
    public int Cost { get; set; }

    [ExportGroup("Card Visuals")]
    [Export]
    public Texture Icon { get; set; }
    [Export(PropertyHint.MultilineText)]
    public string ToolTipText { get; set; }

    public bool IsSingleTargeted()
    {
        return Target == TargetType.SingleEnemy;
    }
    private Godot.Collections.Array<Node> GetTargets(Godot.Collections.Array<Node> targets)
    {
        if (targets.Count == 0)
        {
            return new Godot.Collections.Array<Node>();
        }

        var tree = targets[0].GetTree();
        Godot.Collections.Array<Node> result = new Godot.Collections.Array<Node>();

        switch (Target)
        {
            case TargetType.Self:
                result = tree.GetNodesInGroup("Player");
                break;
            case TargetType.AllEnemies:
                result = tree.GetNodesInGroup("Enemies");
                break;
            case TargetType.Everyone:
                var playerTargets = tree.GetNodesInGroup("Player");
                var enemyTargets = tree.GetNodesInGroup("Enemies");
                result = new Godot.Collections.Array<Node>(playerTargets);
                foreach (var enemy in enemyTargets)
                {
                    result.Add(enemy);
                }
                break;
        }

        return result;
    }
    public void Play(Godot.Collections.Array<Node> targets, CharacterStats charStats, Events events)
    {
        events.EmitSignal("CardPlayed", this);
        charStats.Mana -= Cost;

        if (IsSingleTargeted())
        {
            ApplyEffects(targets);
        }
        else
        {
            ApplyEffects(GetTargets(targets));
        }
    }
    public virtual void ApplyEffects(Godot.Collections.Array<Node> targets)
    {

    }
}
