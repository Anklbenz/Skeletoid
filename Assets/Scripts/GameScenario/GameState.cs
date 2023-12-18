using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public sealed class GameState : State
{
	private readonly ParticlesPlayer _particlesPlayer;
	private readonly FlyingService _flyingService;
	private readonly CameraSystem _cameraSystem;
	private readonly StarsSystem _starsSystem;
	private readonly HudSystem _hudSystem;
	private readonly LevelVfx _levelVfx;
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
		ParticlesPlayer particlesPlayer,
		LevelVfx levelVfx,
		FlyingService flyingService,
		CameraSystem cameraSystem,
		StarsSystem starsSystem,
		ProgressSystem progress) : base(stateSwitcher) {
		
		_progressData = progress;
		_flyingService = flyingService;
		_cameraSystem = cameraSystem;
		_starsSystem = starsSystem;
		_stateSwitcher = stateSwitcher;
		_config = config;
		_gameplaySystem = gameplaySystem;
		_hudSystem = hudSystem;
		_levelVfx = levelVfx;
		//	_particlesPlayer = particlesPlayer;

		_hudSystem.PauseValueChangedEvent += OnPause;

		_gameplaySystem.DeadZoneReachedEvent += OnLoss;
	//	_gameplaySystem.BallCollisionEvent += OnBallCollision;
		_gameplaySystem.BrickDestroyedEvent += OnBrickDestroy;
		//_gameplaySystem.
		_gameplaySystem.AllBricksDestroyedEvent += OnWin;

		_flyingService.CollectedEvent += IncreaseHudItemsCount;
		pauseHandler.Register(_gameplaySystem);
	}
	
	public override void Enter() {
		//for pause state
		if (_gameplaySystem.state == GameplayState.PlayEnded)
			_gameplaySystem.Restart();
	    
		_levelVfx.Start(_gameplaySystem.currentLevel);
		
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
	//	_particlesPlayer.PlayDestroy(destroyedBrickPosition);
		_flyingService.SpawnInSphere(destroyedBrickPosition, brickCost);
		_cameraSystem.Shake();
	}

	/*private void OnBallCollision(Vector3 obj) =>
		_particlesPlayer.PlaySpark(obj);*/

	private async void OnWin() {
		await UniTask.Delay(_config.delayBeforeWinSystemActivate);
		_stateSwitcher.SetState<WinState>();
	}

	private void OnLoss() =>
		_stateSwitcher.SetState<LoseState>();

	private void OnPause() =>
		_stateSwitcher.SetState<PauseState>();
}