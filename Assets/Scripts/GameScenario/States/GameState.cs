using Cysharp.Threading.Tasks;

public sealed class GameState : State {
	private bool isTrainingRequired => !_isTrainingWasShown && _progressSystem.isFirstLevel;
	
	private readonly ParticlesPlayer _particlesPlayer;
	private readonly FlyingService _flyingService;
	private readonly StarsTimer _starsTimer;
	private readonly HudSystem _hudSystem;
	private readonly LevelVfx _levelVfx;
	private readonly ProgressSystem _progressSystem;
	private readonly StateSwitcher _stateSwitcher;

	private readonly GameplayConfig _config;
	private readonly Gameplay _gameplay;
	private readonly PauseHandler _pauseHandler;

	private bool _isTrainingWasShown;

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

		_progressSystem = progress;
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

		_pauseHandler = pauseHandler;
	
		
		_flyingService.CollectedEvent += IncreaseHudItemsCount;
	}

	public override async void Enter() {
		if ( isTrainingRequired)
			await PlayTraining();

		_pauseHandler.SetPause(false);
		
		//If enter is state happened from GameMenu
		if (_gameplay.state == GameplayState.Lose)
			_gameplay.Restart();

		_starsTimer.Start();
		_hudSystem.SetActive(true);
		RefreshHudValues();
	}

	public override void Exit() {
		_pauseHandler.SetPause(true);
		_starsTimer.Stop();
	}

	private async UniTask PlayTraining() {
		await _hudSystem.PlayTraining();
		_isTrainingWasShown = true;
	}
	private void IncreaseHudItemsCount() =>
			_hudSystem.IncreaseCoinsCount();

	private void RefreshHudValues() =>
			_hudSystem.Refresh();

	private void GetAndShowCoins(Brick brick) {
		var destroyedBrickPosition = brick.transform.position;
		var brickCost = brick.randomizedCost;
		_progressSystem.IncreaseCurrentCoins(brickCost);
		_flyingService.SpawnInSphere(destroyedBrickPosition, brickCost);
	}

	private async void OnWin() {
		await UniTask.Delay(_config.delayBeforeWinSystemActivate);
		_stateSwitcher.SetState<WinState>();
	}

	private void OnLoss() =>
			_stateSwitcher.SetState<LoseState>();

	private void OnPause() =>
			_stateSwitcher.SetState<GameMenuState>();
}