using UnityEngine;

public class GameState : State {
	private readonly IInput _input;
	private readonly ParticlesService _particlesService;
	private readonly FlyingCoinService _flyingCoinService;

	private readonly HudSystem _hudSystem;
	private readonly ProgressData _progressData;
	private readonly StateSwitcher _stateSwitcher;
	private readonly GameplaySystem _gameplaySystem;

	public GameState(StateSwitcher stateSwitcher, GameplaySystem gameplaySystem, HudSystem hudSystem, ParticlesService particlesService, FlyingCoinService flyingCoinService, ProgressData progress, IInput input) : base(stateSwitcher) {
		_input = input;
		_progressData = progress;
		_flyingCoinService = flyingCoinService;
		_stateSwitcher = stateSwitcher;
		_gameplaySystem = gameplaySystem;
		_hudSystem = hudSystem;
		_particlesService = particlesService;

		_gameplaySystem.DeadZoneReachedEvent += OnLoss;
		_gameplaySystem.BallCollisionEvent += OnBallCollision;
		_gameplaySystem.BrickDestroyedEvent += OnBrickDestroy;
		_gameplaySystem.AllBricksDestroyedEvent += OnAllBrickDestroyed;
	}

	public override void Enter() {
		_input.Enabled = true;
		_hudSystem.SetActive(true);
		_gameplaySystem.GetReady();
		RefreshHudValues();
	}

	public override void Exit() {
		_input.Enabled = false;
		_hudSystem.SetActive(true);
	}

	private void RefreshHudValues() =>
			_hudSystem.Refresh();

	private void OnAllBrickDestroyed() =>
			_stateSwitcher.SetState<WinState>();

	private void OnLoss() =>
			_stateSwitcher.SetState<LoseState>();

	private void OnBrickDestroy(Brick brick) {
		var destroyedBrickPosition = brick.transform.position;
		_progressData.currentCoins.Increase(brick.costs);

		_particlesService.PlayDestroy(destroyedBrickPosition);
		_flyingCoinService.SpawnCoins(destroyedBrickPosition, brick.costs);
		RefreshHudValues();
	}

	private void OnBallCollision(Vector3 obj) =>
			_particlesService.PlayCollision(obj);
}