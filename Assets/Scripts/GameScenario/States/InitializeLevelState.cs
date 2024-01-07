public class InitializeLevelState : State {
	private readonly Gameplay _gameplay;
	private readonly ProgressSystem _progressSystem;
	private readonly LevelFactory _levelFactory;
	private readonly CameraZoom _cameraZoom;
	private readonly StarsTimer _starsTimer;
	private readonly LevelEventsHandler _levelEventsHandler;
	private readonly LevelVfx _levelVfx;
	private readonly BonusSystem _bonusSystem;

	public InitializeLevelState(
			StateSwitcher stateSwitcher,
			Gameplay gameplay,
			ProgressSystem progressSystem,
			LevelFactory levelFactory,
			CameraZoom cameraZoom,
			StarsTimer starsTimer,
			LevelEventsHandler levelEventsHandler,
			LevelVfx levelVfx,
			BonusSystem bonusSystem) : base(stateSwitcher) {
		_gameplay = gameplay;
		_progressSystem = progressSystem;
		_levelFactory = levelFactory;
		_cameraZoom = cameraZoom;
		_starsTimer = starsTimer;
		_levelEventsHandler = levelEventsHandler;
		_levelVfx = levelVfx;
		_bonusSystem = bonusSystem;
	}

	public override void Enter() {
		SetNewLevel();
		GotoGameplayState();
	}

	private void SetNewLevel() {
		var worldIndex = _progressSystem.currentWorldIndex;

		var level = _levelFactory.CreateLevel(worldIndex);
		_gameplay.SetNewLevel(level);
		_cameraZoom.zoomedLookAt = level.player.skeletonHeadTransform;
		_progressSystem.currentLevel = level;
		_progressSystem.SetCurrentWorldLives();

		var currentTimeLimits = _progressSystem.GetCurrentTimeLimits();
		_starsTimer.Initialize(currentTimeLimits);
		
		_levelEventsHandler.Set(level);
		_levelVfx.Subscribe(_levelEventsHandler);
		_bonusSystem.Initialize(level);
	}

	private void GotoGameplayState() {
		switcher.SetState<GameState>();
	}
}