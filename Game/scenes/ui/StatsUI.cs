using Godot;
using System;

public partial class StatsUI : HBoxContainer
{
    private HBoxContainer block;
    private Label blockLabel;
    private HBoxContainer health;
    private Label healthLabel;

    public override void _Ready()
    {
        block = GetNode<HBoxContainer>("Block");
        blockLabel = GetNode<Label>("%BlockLabel");
        health = GetNode<HBoxContainer>("Health");
        healthLabel = GetNode<Label>("%HealthLabel");
    }

    public void UpdateStats(Stats stats)
    {
        blockLabel.Text = stats.Block.ToString();
        healthLabel.Text = stats.Health.ToString();

        block.Visible = stats.Block > 0;
        health.Visible = stats.Health > 0;
    }
}
