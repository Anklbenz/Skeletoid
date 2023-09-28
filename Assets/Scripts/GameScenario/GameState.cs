using UnityEngine;
public class GameState : State {
	private readonly IInput _input;
	private readonly ParticlesService _particlesService;
	private readonly GameplaySystem _gameplaySystem;

	private readonly ProgressData _progressData;
	private readonly StateSwitcher _stateSwitcher;

	private CoinService _coinService;

	public GameState(StateSwitcher stateSwitcher, GameplaySystem gameplaySystem, ParticlesService particlesService, CoinService coinService, ProgressData progress, IInput input) : base(stateSwitcher) {
		_input = input;
		_progressData = progress;
		_coinService = coinService;
		_stateSwitcher = stateSwitcher;
		_gameplaySystem = gameplaySystem;
		_particlesService = particlesService;

		_gameplaySystem.DeadZoneReachedEvent += OnLoss;
		_gameplaySystem.BallCollisionEvent += OnBallCollision;
		_gameplaySystem.BrickDestroyedEvent += OnBrickDestroy;
		_gameplaySystem.AllBricksDestroyedEvent += OnAllBrickDestroyed;
	}
	public override void Enter() {
		_input.Enabled = true;
		_gameplaySystem.GetReady();
	}

	public override void Exit() {
		_input.Enabled = false;
	}
	
	private void OnAllBrickDestroyed() {
		Debug.Log("GotoWin");
	}
	
	private void OnLoss() {
		_stateSwitcher.SetState<LoseState>();
	}

	private void OnBrickDestroy(Brick brick) {
		var destroyedBrickPosition = brick.transform.position;
		_progressData.coins += brick.costs;

		_particlesService.PlayDestroy(destroyedBrickPosition);
		_coinService.SpawnCoins(destroyedBrickPosition, brick.costs);
	}
	private void OnBallCollision(Vector3 obj) {
		_particlesService.PlayCollision(obj);
	}
}