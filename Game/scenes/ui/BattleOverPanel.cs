using Godot;
using System;

public partial class BattleOverPanel : Panel
{
    public enum ScreenType
    {
        Win,
        Lose
    }

    private Label label;
    private Button continueButton;
    private Button restartButton;

    public override void _Ready()
    {
        label = GetNode<Label>("%Label");
        continueButton = GetNode<Button>("%ContinueButton");
        restartButton = GetNode<Button>("%RestartButton");

        continueButton.Pressed += OnContinueButtonPressed;
        restartButton.Pressed += OnRestartButtonPressed;
        Events.Instance.Connect(nameof(Events.BattleOverScreenRequested), new Callable(this, nameof(ShowScreen)));
    }

    private void OnContinueButtonPressed()
    {
        GetTree().Quit();
    }

    private void OnRestartButtonPressed()
    {
        GetTree().ReloadCurrentScene();
    }

    public void ShowScreen(string text, ScreenType type)
    {
        label.Text = text;
        continueButton.Visible = type == ScreenType.Win;
        restartButton.Visible = type == ScreenType.Lose;
        Show();
        GetTree().Paused = true;
    }
}
