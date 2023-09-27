using UnityEngine;
using System;
public class GameState : State {
	private readonly IInput _input;
	private readonly ParticlesService _particlesService;
	private readonly GameplaySystem _gameplaySystem;

	private readonly ProgressData _progressData;
	private readonly StateSwitcher _stateSwitcher;

	private CoinService _coinService;

	public GameState(StateSwitcher stateSwitcher, GameplaySystem gameplaySystem, ParticlesService particlesService,CoinService coinService, ProgressData progress, IInput input) : base(stateSwitcher) {
		_stateSwitcher = stateSwitcher;
		_progressData = progress;
		_input = input;
		_gameplaySystem = gameplaySystem;
		_particlesService = particlesService;

		_coinService = coinService;
		
		_gameplaySystem.BallCollisionEvent += OnBallCollision;
		_gameplaySystem.BrickDestroyedEvent += OnBrickDestroy;
		_gameplaySystem.AllBricksDestroyedEvent += OnAllBrickDestroyed;
		_gameplaySystem.DeadZoneReachedEvent += OnLoss;
	}
	public override void Enter() {
		_input.Enabled = true;
		_gameplaySystem.GetReady();
	}

	public override void Exit() {
		_input.Enabled = false;
	}
	private void OnLoss() {
		Debug.Log("GotoLoss");
	}
	private void OnAllBrickDestroyed() {
		Debug.Log("GotoWin");
	}
	private void OnBrickDestroy(Vector3 obj) {
		_particlesService.PlayDestroy(obj);
		_coinService.SpawnCoins(obj, 3);
	}
	private void OnBallCollision(Vector3 obj) {
		_particlesService.PlayCollision(obj);
	}
}