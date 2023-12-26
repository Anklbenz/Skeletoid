using Cysharp.Threading.Tasks;

public sealed class GameState : State
{
	private readonly ParticlesPlayer _particlesPlayer;
	private readonly FlyingService _flyingService;
	private readonly StarsSystem _starsSystem;
	private readonly HudSystem _hudSystem;
	private readonly LevelVfx _levelVfx;
	private readonly ProgressSystem _progressData;
	private readonly StateSwitcher _stateSwitcher;
	private readonly GameplayConfig _config;
	private readonly Gameplay _gameplay;

	public GameState(
		StateSwitcher stateSwitcher,
		GameplayConfig config,
		Gameplay gameplay,
		PauseHandler pauseHandler,
		HudSystem hudSystem,
		LevelVfx levelVfx,
		FlyingService flyingService,
		StarsSystem starsSystem,
		ProgressSystem progress) : base(stateSwitcher) {
		
		_progressData = progress;
		_flyingService = flyingService;
		_starsSystem = starsSystem;
		_stateSwitcher = stateSwitcher;
		_config = config;
		_hudSystem = hudSystem;
		_levelVfx = levelVfx;
		_hudSystem.PauseValueChangedEvent += OnPause;

		_gameplay = gameplay;
		_gameplay.DeadZoneReachedEvent += OnLoss;
		_gameplay.BrickDestroyedEvent += OnBrickDestroy;
		_gameplay.AllBricksDestroyedEvent += OnWin;

		_flyingService.CollectedEvent += IncreaseHudItemsCount;
		pauseHandler.Register(_gameplay);
	}
	
	public override void Enter() {
		//for pause state
		if (_gameplay.state == GameplayState.PlayEnded)
			_gameplay.Restart();
	    
		_levelVfx.Start(_gameplay.currentLevel);
		
		_starsSystem.Start();
		_hudSystem.SetActive(true);
		RefreshHudValues();
	}

	public override void Exit() {
		_levelVfx.Stop();
		_starsSystem.Stop();
	}

	private void IncreaseHudItemsCount()=>
		_hudSystem.IncreaseCoinsCount();
	
	private void RefreshHudValues() =>
		_hudSystem.Refresh();

	private void OnBrickDestroy(Brick brick) {
		var destroyedBrickPosition = brick.transform.position;
		var brickCost = brick.randomizedCost;
		_progressData.IncreaseCurrentCoins(brickCost);
		_flyingService.SpawnInSphere(destroyedBrickPosition, brickCost);
	}

	private async void OnWin() {
		await UniTask.Delay(_config.delayBeforeWinSystemActivate);
		_stateSwitcher.SetState<WinState>();
	}

	private void OnLoss() =>
		_stateSwitcher.SetState<LoseState>();

	private void OnPause() =>
		_stateSwitcher.SetState<PauseState>();
}