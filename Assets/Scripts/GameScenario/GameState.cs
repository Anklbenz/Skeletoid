using Cysharp.Threading.Tasks;
using UnityEngine;

public sealed class GameState : State
{
	private const int DELAY_WIN_SYSTEM_ENABLE = 300;

	private readonly ParticlesService _particlesService;
	private readonly FlyingCoinService _flyingCoinService;

	private readonly HudSystem _hudSystem;
	private readonly ProgressSystem _progressData;
	private readonly StateSwitcher _stateSwitcher;
	private readonly GameplaySystem _gameplaySystem;
	private readonly PauseHandler _pauseHandler;

	public GameState(
		StateSwitcher stateSwitcher,
		GameplaySystem gameplaySystem,
		PauseHandler pauseHandler,
		HudSystem hudSystem,
		ParticlesService particlesService,
		FlyingCoinService flyingCoinService,
		ProgressSystem progress) : base(stateSwitcher) {

		_progressData = progress;
		_flyingCoinService = flyingCoinService;
		_stateSwitcher = stateSwitcher;
		_gameplaySystem = gameplaySystem;
		_hudSystem = hudSystem;
		_particlesService = particlesService;
		_pauseHandler = pauseHandler;

		_pauseHandler.Register(_gameplaySystem);
		_hudSystem.PauseValueChangedEvent += OnPause;

		_gameplaySystem.DeadZoneReachedEvent += OnLoss;
		_gameplaySystem.BallCollisionEvent += OnBallCollision;
		_gameplaySystem.BrickDestroyedEvent += OnBrickDestroy;
		_gameplaySystem.AllBricksDestroyedEvent += OnWin;

		_flyingCoinService.CollectedEvent += RefreshHudValues;
	}

	public override void Enter() {
		//for pause state
		if (_gameplaySystem.state == GameplayState.PlayEnded)
			_gameplaySystem.Restart();
	
		_hudSystem.SetActive(true);
		RefreshHudValues();
	}

	private void RefreshHudValues() =>
		_hudSystem.Refresh();

	private void OnBrickDestroy(Brick brick) {
		var destroyedBrickPosition = brick.transform.position;
		_progressData.IncreaseCurrentCoins(brick.costs);

		_particlesService.PlayDestroy(destroyedBrickPosition);
		_flyingCoinService.SpawnCoins(destroyedBrickPosition, brick.costs);
	}

	private void OnBallCollision(Vector3 obj) =>
		_particlesService.PlayCollision(obj);

	private async void OnWin() {
		await UniTask.Delay(DELAY_WIN_SYSTEM_ENABLE);
		_stateSwitcher.SetState<WinState>();
	}

	private void OnLoss() =>
		_stateSwitcher.SetState<LoseState>();

	private void OnPause() =>
		_stateSwitcher.SetState<PauseState>();
}