using Godot;
using System;

public partial class Shaker : Node
{
    public static void Shake(Node2D thing, float strength, float duration = 0.2f)
    {
        if (thing == null)
            return;

        Vector2 origPos = thing.Position;
        int shakeCount = 10;
        Tween tween = thing.CreateTween();

        for (int i = 0; i < shakeCount; i++)
        {
            Vector2 shakeOffset = new Vector2((float)GD.RandRange(-1.0, 1.0), (float)GD.RandRange(-1.0, 1.0));
            Vector2 target = origPos + strength * shakeOffset;
            if (i % 2 == 0)
            {
                target = origPos;
            }
            tween.TweenProperty(thing, "position", target, duration / shakeCount);
            strength *= 0.75f;
        }

        tween.Finished += () => thing.Position = origPos;
    }
}
