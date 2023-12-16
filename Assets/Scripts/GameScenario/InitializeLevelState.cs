public class InitializeLevelState : State
{
	private readonly GameplaySystem _gameplaySystem;
	private readonly ProgressSystem _progressSystem;
	private readonly LevelFactory _levelFactory;
	private readonly CameraSystem _cameraSystem;
	private readonly StarsSystem _starsSystem;

	public InitializeLevelState(StateSwitcher stateSwitcher, GameplaySystem gameplaySystem, ProgressSystem progressSystem, LevelFactory levelFactory,
		CameraSystem cameraSystem, StarsSystem starsSystem) : base(stateSwitcher) {
		_gameplaySystem = gameplaySystem;
		_progressSystem = progressSystem;
		_levelFactory = levelFactory;
		_cameraSystem = cameraSystem;
		_starsSystem = starsSystem;
	}

	public override void Enter() {
		SetNewLevel();
		GotoGameplayState();
	}

	private void SetNewLevel() {
		var worldIndex = _progressSystem.currentWorldIndex;
		var levelIndex = _progressSystem.currentLevelIndex;
		var level = _levelFactory.CreateLevel(worldIndex, levelIndex);
		_gameplaySystem.SetNewLevel(level);
		_cameraSystem.zoomedLookAt = level.player.skeletonHeadTransform;
		_progressSystem.currentLevel = level;

		var currentTimeLimits = _progressSystem.GetCurrentTimeLimits();
		_starsSystem.Initialize(currentTimeLimits);
	}

	private void GotoGameplayState() {
		switcher.SetState<GameState>();
	}
}