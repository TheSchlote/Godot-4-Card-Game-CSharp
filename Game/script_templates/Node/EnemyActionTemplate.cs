using Godot;
using System;

[GlobalClass]
public partial class EnemyActionTemplate : EnemyAction
{
    public override void PerformAction()
    {
        if (Enemy == null || Target == null)
        {
            return;
        }

        Tween tween = CreateTween().SetTrans(Tween.TransitionType.Quint);
        Vector2 start = Enemy.GlobalPosition;
        Vector2 end = Target.GlobalPosition + Vector2.Right * 32;

        SoundPlayer.Instance.Play(Sound); // Ensure SFXPlayer is set up as a singleton

        Events.Instance.EmitSignal(nameof(Events.EnemyActionCompleted), Enemy);
    }
}
