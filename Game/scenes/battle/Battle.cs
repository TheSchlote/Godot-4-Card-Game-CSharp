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
        Events events = GetNode<Events>("/root/Events");
        events.Connect("EnemyTurnEnded", new Callable(this, nameof(OnEnemyTurnEnded)));
        events.Connect("PlayerTurnEnded", new Callable(_playerHandler, nameof(PlayerHandler.EndTurn)));
        events.Connect("PlayerHandDiscarded", new Callable(_enemyHandler, nameof(EnemyHandler.StartTurn)));
        events.Connect("PlayerDied", new Callable(this, nameof(OnPlayerDied)));

        StartBattle(newStats);
    }

    private void StartBattle(CharacterStats stats)
    {
        GetTree().Paused = false;
        //MusicPlayer.Play(Music, true);
        _enemyHandler.ResetEnemyActions();
        _playerHandler.StartBattle(stats);
    }

    private void OnEnemiesChildOrderChanged()
    {
        if (_enemyHandler.GetChildCount() == 0)
        {
            GD.Print("Victory!");
            //Events events = GetNode<Events>("/root/Events");
            //events.EmitSignal("BattleOverScreenRequested", "Victorious!", (int)BattleOverPanel.Type.WIN);
        }
    }

    private void OnEnemyTurnEnded()
    {
        _playerHandler.StartTurn();
        _enemyHandler.ResetEnemyActions();
    }

    private void OnPlayerDied()
    {
        GD.Print("Game Over!");
        Events events = GetNode<Events>("/root/Events");
        //events.EmitSignal("BattleOverScreenRequested", "Game Over!", (int)BattleOverPanel.Type.LOSE);
    }
}
