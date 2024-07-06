using Godot;
using System;
using System.Data;

public partial class ManaUi : Panel
{
    [Export]
    public CharacterStats CharStats
    {
        get => charStats;
        set => SetCharStats(value);
    }
    private CharacterStats charStats;

    private Label manaLabel;

    public override void _Ready()
    {
        manaLabel = GetNode<Label>("ManaLabel");

        if (charStats != null)
        {
            SetCharStats(charStats);
        }
    }

    private async void SetCharStats(CharacterStats value)
    {
        charStats = value;

        if (!charStats.IsConnected(nameof(CharacterStats.StatsChanged), new Callable(this, nameof(OnStatsChanged))))
        {
            charStats.Connect(nameof(CharacterStats.StatsChanged), new Callable(this, nameof(OnStatsChanged)));
        }

        if (!IsInsideTree())
        {
            await ToSignal(this, "ready");
        }

        OnStatsChanged();
    }

    private void OnStatsChanged()
    {
        manaLabel.Text = $"{charStats.Mana}/{charStats.MaxMana}";
    }
}
