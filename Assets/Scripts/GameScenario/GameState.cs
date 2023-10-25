using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public sealed class GameState : State
{
	private readonly ParticlesService _particlesService;
	private readonly FlyingCoinService _flyingCoinService;
	private readonly CameraSystem _cameraSystem;
	private readonly Timer _timer;

	private readonly HudSystem _hudSystem;
	private readonly ProgressSystem _progressData;
	private readonly StateSwitcher _stateSwitcher;
	private readonly GameplayConfig _config;
	private readonly GameplaySystem _gameplaySystem;

	public GameState(
		StateSwitcher stateSwitcher,
		GameplayConfig config,
		GameplaySystem gameplaySystem,
		PauseHandler pauseHandler,
		HudSystem hudSystem,
		ParticlesService particlesService,
		FlyingCoinService flyingCoinService,
		CameraSystem cameraSystem,
		Timer timer,
		ProgressSystem progress) : base(stateSwitcher) {
		
		_progressData = progress;
		_flyingCoinService = flyingCoinService;
		_cameraSystem = cameraSystem;
		_timer = timer;
		_stateSwitcher = stateSwitcher;
		_config = config;
		_gameplaySystem = gameplaySystem;
		_hudSystem = hudSystem;
		_particlesService = particlesService;

		_hudSystem.PauseValueChangedEvent += OnPause;

		_gameplaySystem.DeadZoneReachedEvent += OnLoss;
		_gameplaySystem.BallCollisionEvent += OnBallCollision;
		_gameplaySystem.BrickDestroyedEvent += OnBrickDestroy;
		_gameplaySystem.AllBricksDestroyedEvent += OnWin;

		_flyingCoinService.CollectedEvent += IncreaseHudCoinsCount;
		pauseHandler.Register(_gameplaySystem);
		_timer.TickEvent += DDD;
	}

	private void DDD(float obj) {
		
		Debug.Log($"{_timer.current.Minutes:00}:{_timer.current.Seconds:00}:{_timer.current.Milliseconds:000}");
    
		
	}

	public override void Enter() {
		//for pause state
		if (_gameplaySystem.state == GameplayState.PlayEnded)
			_gameplaySystem.Restart();
	    
		_timer.Start();
		_hudSystem.SetActive(true);
		RefreshHudValues();
		DDD(1);
	}

	public override void Exit() {
		_timer.Stop();
	}

	private void IncreaseHudCoinsCount()=>
		_hudSystem.IncreaseCoinsCount();
	
	private void RefreshHudValues() =>
		_hudSystem.Refresh();

	private void OnBrickDestroy(Brick brick) {
		var destroyedBrickPosition = brick.transform.position;
		var brickCost = brick.randomizedCost;
		_progressData.IncreaseCurrentCoins(brickCost);
		_particlesService.PlayDestroy(destroyedBrickPosition);
		_flyingCoinService.SpawnCoins(destroyedBrickPosition, brickCost);
		_cameraSystem.Shake();
	}

	private void OnBallCollision(Vector3 obj) =>
		_particlesService.PlayCollision(obj);

	private async void OnWin() {
		await UniTask.Delay(_config.delayBeforeWinSystemActivate);
		_stateSwitcher.SetState<WinState>();
	}

	private void OnLoss() =>
		_stateSwitcher.SetState<LoseState>();

	private void OnPause() =>
		_stateSwitcher.SetState<PauseState>();
}