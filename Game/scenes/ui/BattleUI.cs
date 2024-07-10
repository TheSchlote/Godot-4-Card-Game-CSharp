using Godot;
using System;

public partial class BattleUI : CanvasLayer
{
    [Export]
    public CharacterStats CharStats
    {
        get => _charStats;
        set => SetCharStats(value);
    }

    private CharacterStats _charStats;
    public Hand Hand;
    public ManaUi ManaUI;
    public Button EndTurnButton;

    public override void _Ready()
    {
        Hand = GetNode<Hand>("Hand");
        ManaUI = GetNode<ManaUi>("ManaUI");
        EndTurnButton = GetNode<Button>("%EndTurnButton");

        Events events = GetNode<Events>("/root/Events");
        events.Connect("PlayerHandDrawn", new Callable(this, nameof(OnPlayerHandDrawn)));
        EndTurnButton.Connect("pressed", new Callable(this, nameof(OnEndTurnButtonPressed)));
    }

    private void SetCharStats(CharacterStats value)
    {
        _charStats = value;
        ManaUI.CharStats = _charStats;
        Hand.CharStats = _charStats;
    }

    private void OnPlayerHandDrawn()
    {
        EndTurnButton.Disabled = false;
    }

    private void OnEndTurnButtonPressed()
    {
        EndTurnButton.Disabled = true;
        Events events = GetNode<Events>("/root/Events");
        events.EmitSignal("PlayerTurnEnded");
    }
}
