using Godot;
using System;

public partial class CrabAttackAction : EnemyAction
{
    [Export]
    public int Damage { get; set; } = 7;

    public override void PerformAction()
    {
        if (Enemy == null || Target == null)
        {
            return;
        }

        Tween tween = CreateTween().SetTrans(Tween.TransitionType.Quint);
        Vector2 start = Enemy.GlobalPosition;
        Vector2 end = Target.GlobalPosition + Vector2.Right * 32;
        DamageEffect damageEffect = new DamageEffect
        {
            Amount = Damage
        };
        Godot.Collections.Array<Node> targetArray = new Godot.Collections.Array<Node> { Target };

        tween.TweenProperty(Enemy, "global_position", end, 0.4f);

        // Using a method to execute the damage effect without passing parameters
        tween.TweenCallback(Callable.From(() => damageEffect.Execute(targetArray)));

        tween.TweenInterval(0.25f);
        tween.TweenProperty(Enemy, "global_position", start, 0.4f);

        tween.Finished += () =>
        {
            Events events = GetNode<Events>("/root/Events");
            events.EmitSignal(nameof(Events.EnemyActionCompleted), Enemy);
        };
    }
}
