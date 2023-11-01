public class InitializeLevelState : State
{
	private readonly GameplaySystem _gameplaySystem;
	private readonly ProgressSystem _progressSystem;
	private readonly LevelFactory _levelFactory;
	private readonly CameraSystem _cameraSystem;
	private readonly RewardSystem _rewardSystem;

	public InitializeLevelState(StateSwitcher stateSwitcher, GameplaySystem gameplaySystem, ProgressSystem progressSystem, LevelFactory levelFactory,
		CameraSystem cameraSystem, RewardSystem rewardSystem) : base(stateSwitcher) {
		_gameplaySystem = gameplaySystem;
		_progressSystem = progressSystem;
		_levelFactory = levelFactory;
		_cameraSystem = cameraSystem;
		_rewardSystem = rewardSystem;
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
		_rewardSystem.Initialize(currentTimeLimits);
	}

	private void GotoGameplayState() {
		switcher.SetState<GameState>();
	}
}