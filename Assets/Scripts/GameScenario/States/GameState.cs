using Cysharp.Threading.Tasks;

public sealed class GameState : State {
	private readonly ParticlesPlayer _particlesPlayer;
	private readonly FlyingService _flyingService;
	private readonly StarsTimer _starsTimer;
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
			StarsTimer starsTimer,

			ProgressSystem progress) : base(stateSwitcher) {

		_progressData = progress;
		_flyingService = flyingService;
		_starsTimer = starsTimer;

		_stateSwitcher = stateSwitcher;
		_config = config;
		_hudSystem = hudSystem;
		_levelVfx = levelVfx;
		_hudSystem.PauseValueChangedEvent += OnPause;

		_gameplay = gameplay;
		_gameplay.LoseEvent += OnLoss;
		_gameplay.BeforeBrickDestroyedEvent += GetAndShowCoins;
		_gameplay.AllBricksDestroyedEvent += OnWin;

		_flyingService.CollectedEvent += IncreaseHudItemsCount;
		pauseHandler.Register(_gameplay);
	}

	public override void Enter() {
		//for pause state
		if (_gameplay.state == GameplayState.Lose)
			_gameplay.Restart();

		_starsTimer.Start();
		_hudSystem.SetActive(true);
		RefreshHudValues();
	}

	public override void Exit() {
		//	_levelVfx.Stop();
		_starsTimer.Stop();
	}

	private void IncreaseHudItemsCount() =>
			_hudSystem.IncreaseCoinsCount();

	private void RefreshHudValues() =>
			_hudSystem.Refresh();

	private void GetAndShowCoins(Brick brick) {
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