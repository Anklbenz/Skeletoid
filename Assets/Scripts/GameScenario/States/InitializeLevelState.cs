using UnityEngine;
public class InitializeLevelState : State {
	private bool isTrainingRequired => _progressSystem.isFirstLevel;

	private readonly Gameplay _gameplay;
	private readonly LevelVfx _levelVfx;
	private readonly CameraZoom _cameraZoom;
	private readonly StarsTimer _starsTimer;
	private readonly BonusSystem _bonusSystem;
	private readonly BallLaunch _ballLaunch;
	private readonly BallSpeedIncrease _ballSpeedIncrease;
	private readonly BotNavigationSystem _botNavigationSystem;
	private readonly LevelFactory _levelFactory;
	private readonly PauseHandler _pauseHandler;
	private readonly ProgressSystem _progressSystem;
	private readonly LevelEventsHandler _levelEventsHandler;

	public InitializeLevelState(
			StateSwitcher stateSwitcher,
			Gameplay gameplay,
			ProgressSystem progressSystem,
			LevelFactory levelFactory,
			CameraZoom cameraZoom,
			StarsTimer starsTimer,
			LevelEventsHandler levelEventsHandler,
			LevelVfx levelVfx,
			BonusSystem bonusSystem,
			BallLaunch ballLaunch,
			BallSpeedIncrease ballSpeedIncrease,
			BotNavigationSystem botNavigationSystem,
			PauseHandler pauseHandler) : base(stateSwitcher) {
		_gameplay = gameplay;
		_progressSystem = progressSystem;
		_levelFactory = levelFactory;
		_cameraZoom = cameraZoom;
		_starsTimer = starsTimer;
		_levelEventsHandler = levelEventsHandler;
		_levelVfx = levelVfx;
		_bonusSystem = bonusSystem;
		_ballLaunch = ballLaunch;
		_ballSpeedIncrease = ballSpeedIncrease;
		_botNavigationSystem = botNavigationSystem;
		_pauseHandler = pauseHandler;
	}

	public override void Enter() {
		InitializeLevel();
		GotoGameplayState();
	}

	private void InitializeLevel() {
		var worldIndex = _progressSystem.currentWorldIndex;

		var level = _levelFactory.CreateLevel(worldIndex);
		level.navMeshSurface.BuildNavMesh();
		//mesh needed only for navMeshSurfaceBuild
		level.navMap.isMeshRenderEnabled = false;

		_cameraZoom.zoomedLookAt = level.player.skeletonHeadTransform;
		_progressSystem.currentLevel = level;
		_progressSystem.SetCurrentWorldLives();

		var currentTimeLimits = _progressSystem.GetCurrentTimeLimits();
		_starsTimer.Initialize(currentTimeLimits);

		_levelEventsHandler.Set(level);
		_levelVfx.Subscribe(_levelEventsHandler);
		_bonusSystem.Initialize(level);

		_ballLaunch.Initialize(level.ball, level.player);
		_ballSpeedIncrease.Initialize(level.ball);

		_botNavigationSystem.Initialize(level.enemies, level.navMap.floorBounds);
		_gameplay.SetNewLevel(level);
		_pauseHandler.Register(_gameplay);
		_pauseHandler.SetPause(true);
	}

	private void GotoGameplayState() {
		if (isTrainingRequired)
			switcher.SetState<TrainingState>();
		else
			switcher.SetState<GameState>();
	}
}