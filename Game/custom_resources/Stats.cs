using Godot;
using System;

[GlobalClass]
public partial class Stats : Resource
{
    [Signal]
    public delegate void StatsChangedEventHandler();

    [Export]
    public int MaxHealth { get; set; } = 1;

    [Export]
    public Texture Art { get; set; }

    private int health;
    public int Health
    {
        get { return health; }
        set { SetHealth(value); }
    }

    private int block;
    public int Block
    {
        get { return block; }
        set { SetBlock(value); }
    }

    private void SetHealth(int value)
    {
        health = Math.Clamp(value, 0, MaxHealth);
        EmitSignal(nameof(StatsChanged));
    }

    private void SetBlock(int value)
    {
        block = Math.Clamp(value, 0, 999);
        EmitSignal(nameof(StatsChanged));
    }

    public virtual void TakeDamage(int damage, Events events)
    {
        if (damage <= 0)
        {
            return;
        }

        int initialDamage = damage;
        damage = Math.Clamp(damage - block, 0, damage);
        Block = Math.Clamp(block - initialDamage, 0, block);
        Health -= damage;
    }

    public void Heal(int amount)
    {
        Health += amount;
    }

    public Stats CreateInstance()
    {
        Stats instance = (Stats)Duplicate();
        instance.Health = MaxHealth;
        instance.Block = 0;
        return instance;
    }
}
