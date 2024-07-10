using Godot;
using System;

public partial class Tooltip : PanelContainer
{
    [Export]
    public float FadeSeconds = 0.2f;

    public TextureRect TooltipIcon;
    public RichTextLabel TooltipTextLabel;

    private Tween _tween;
    private bool _isVisibleNow = false;
    public override void _Ready()
    {
        TooltipIcon = GetNode<TextureRect>("%TooltipIcon");
        TooltipTextLabel = GetNode<RichTextLabel>("%TooltipText");

        Events events = GetNode<Events>("/root/Events");
        events.Connect("CardTooltipRequested", new Callable(this, nameof(ShowTooltip)));
        events.Connect("TooltipHide", new Callable(this, nameof(HideTooltip)));

        Modulate = new Color(1, 1, 1, 0); // Transparent
        Hide();
    }
    private void ShowTooltip(Texture icon, string text)
    {
        _isVisibleNow = true;
        _tween?.Kill();

        TooltipIcon.Texture = (Texture2D)icon;
        TooltipTextLabel.Text = text;
        _tween = CreateTween().SetEase(Tween.EaseType.Out).SetTrans(Tween.TransitionType.Cubic);
        _tween.TweenCallback(Callable.From(Show));
        _tween.TweenProperty(this, "modulate", new Color(1, 1, 1, 1), FadeSeconds);
    }
    private void HideTooltip()
    {
        _isVisibleNow = false;
        _tween?.Kill();

        var timer = GetTree().CreateTimer(FadeSeconds);
        timer.Timeout += HideAnimation;
    }
    private void HideAnimation()
    {
        if (!_isVisibleNow)
        {
            _tween = CreateTween().SetEase(Tween.EaseType.Out).SetTrans(Tween.TransitionType.Cubic);
            _tween.TweenProperty(this, "modulate", new Color(1, 1, 1, 0), FadeSeconds);
            _tween.TweenCallback(Callable.From(Hide));
        }
    }
}
