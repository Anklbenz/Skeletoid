public class InitializeLevelState : State {
	private readonly Gameplay _gameplay;
	private readonly ProgressSystem _progressSystem;
	private readonly LevelFactory _levelFactory;
	private readonly CameraZoom _cameraZoom;
	private readonly StarsSystem _starsSystem;

	public InitializeLevelState(
			StateSwitcher stateSwitcher,
			Gameplay gameplay,
			ProgressSystem progressSystem,
			LevelFactory levelFactory,

			CameraZoom cameraZoom,
			StarsSystem starsSystem) : base(stateSwitcher) {
		_gameplay = gameplay;
		_progressSystem = progressSystem;
		_levelFactory = levelFactory;
		_cameraZoom = cameraZoom;
		_starsSystem = starsSystem;
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
		_starsSystem.Initialize(currentTimeLimits);
	}

	private void GotoGameplayState() {
		switcher.SetState<GameState>();
	}
}