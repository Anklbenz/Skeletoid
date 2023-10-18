public class InitializeLevelState : State {
	private readonly GameplaySystem _gameplaySystem;
	private readonly ProgressSystem _progressData;
	private readonly LevelFactory _levelFactory;

	public InitializeLevelState(StateSwitcher stateSwitcher, GameplaySystem gameplaySystem, ProgressSystem progressData, LevelFactory levelFactory) : base(stateSwitcher) {
		_gameplaySystem = gameplaySystem;
		_progressData = progressData;
		_levelFactory = levelFactory;
	}

	public override void Enter() {
		SetNewLevel();
		GotoGameplayState();
	}

	private void SetNewLevel() {
		var worldIndex = _progressData.currentWorldIndex;
		var levelIndex = _progressData.currentLevelIndex;
		var level = _levelFactory.CreateLevel(worldIndex, levelIndex);
		_gameplaySystem.SetNewLevel(level);
	}

	private void GotoGameplayState() {
		switcher.SetState<GameState>();
	}
}