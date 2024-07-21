using Godot;
using System;
using System.Data;

public partial class Battle : Node2D
{
    [Export]
    public CharacterStats CharStats;

    [Export]
    public AudioStream Music;

    private BattleUI _battleUi;
    private PlayerHandler _playerHandler;
    private EnemyHandler _enemyHandler;
    private Player _player;

    public override void _Ready()
    {
        _battleUi = GetNode<BattleUI>("BattleUI");
        _playerHandler = GetNode<PlayerHandler>("PlayerHandler");
        _enemyHandler = GetNode<EnemyHandler>("EnemyHandler");
        _player = GetNode<Player>("Player");

        // Normally, we would do this on a 'Run'
        // level so we keep our health, gold and deck
        // between battles.
        var newStats = CharStats.CreateInstance();
        _battleUi.CharStats = newStats;
        _player.Stats = newStats;

        _enemyHandler.Connect("child_order_changed", new Callable(this, nameof(OnEnemiesChildOrderChanged)));
        Events.Instance.Connect("EnemyTurnEnded", new Callable(this, nameof(OnEnemyTurnEnded)));
        Events.Instance.Connect("PlayerTurnEnded", new Callable(_playerHandler, nameof(PlayerHandler.EndTurn)));
        Events.Instance.Connect("PlayerHandDiscarded", new Callable(_enemyHandler, nameof(EnemyHandler.StartTurn)));
        Events.Instance.Connect("PlayerDied", new Callable(this, nameof(OnPlayerDied)));

        StartBattle(newStats);
    }

    private void StartBattle(CharacterStats stats)
    {
        GetTree().Paused = false;
        SoundPlayer.Instance.Play(Music, true);
        _enemyHandler.ResetEnemyActions();
        _playerHandler.StartBattle(stats);
    }

    private void OnEnemiesChildOrderChanged()
    {
        if (_enemyHandler.GetChildCount() == 0)
        {
            Events.Instance.EmitSignal("BattleOverScreenRequested", "Victorious!", (int)BattleOverPanel.ScreenType.Win);
        }
    }

    private void OnEnemyTurnEnded()
    {
        _playerHandler.StartTurn();
        _enemyHandler.ResetEnemyActions();
    }

    private void OnPlayerDied()
    {
        Events.Instance.EmitSignal("BattleOverScreenRequested", "Game Over!", (int)BattleOverPanel.ScreenType.Lose);
    }
}
