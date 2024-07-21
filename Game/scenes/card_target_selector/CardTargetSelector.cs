using Godot;
using System.Collections.Generic;

public partial class CardTargetSelector : Node2D
{
    private const int ARC_POINTS = 8;
    private Area2D area2D;
    private Line2D cardArc;
    private CardUI currentCard;
    private bool targeting = false;
    public override void _Ready()
    {
        area2D = GetNode<Area2D>("Area2D");
        cardArc = GetNode<Line2D>("CanvasLayer/CardArc");
        Events.Instance.Connect("CardAimStarted", new Callable(this, nameof(OnCardAimStarted)));
        Events.Instance.Connect("CardAimEnded", new Callable(this, nameof(OnCardAimEnded)));
    }

    public override void _Process(double delta)
    {
        if (!targeting)
            return;

        area2D.Position = GetLocalMousePosition();
        cardArc.Points = GetPoints();
    }
    private Vector2[] GetPoints()
    {
        List<Vector2> points = new List<Vector2>();
        Vector2 start = currentCard.GlobalPosition;
        start.X += (currentCard.Size.X / 2);
        Vector2 target = GetLocalMousePosition();
        Vector2 distance = (target - start);

        for (int i = 0; i < ARC_POINTS; i++)
        {
            float t = (1.0f / ARC_POINTS) * i;
            float x = start.X + (distance.X / ARC_POINTS) * i;
            float y = start.Y + EaseOutCubic(t) * distance.Y;
            points.Add(new Vector2(x, y));
        }

        points.Add(target);

        return points.ToArray();
    }
    private float EaseOutCubic(float number)
    {
        return 1.0f - Mathf.Pow(1.0f - number, 3.0f);
    }
    private void OnCardAimStarted(CardUI card)
    {
        if (!card.Card.IsSingleTargeted())
            return;

        targeting = true;
        area2D.Monitoring = true;
        area2D.Monitorable = true;
        currentCard = card;
    }

    private void OnCardAimEnded(CardUI card)
    {
        targeting = false;
        cardArc.ClearPoints();
        area2D.Position = Vector2.Zero;
        area2D.Monitoring = false;
        area2D.Monitorable = false;
        currentCard = null;
    }

    private void OnArea2DAreaEntered(Area2D area)
    {
        if (currentCard == null || !targeting)
            return;

        if (!currentCard.Targets.Contains(area))
        {
            currentCard.Targets.Add(area);
        }
    }

    private void OnArea2DAreaExited(Area2D area)
    {
        if (currentCard == null || !targeting)
            return;

        currentCard.Targets.Remove(area);
    }
}
