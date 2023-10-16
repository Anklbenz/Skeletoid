public class InitializeLevelState : State {
	private readonly GameplaySystem _gameplaySystem;
	private readonly ProgressData _progressData;
	private readonly LevelFactory _levelFactory;

	public InitializeLevelState(StateSwitcher stateSwitcher, GameplaySystem gameplaySystem, ProgressData progressData, LevelFactory levelFactory) : base(stateSwitcher) {
		_gameplaySystem = gameplaySystem;
		_progressData = progressData;
		_levelFactory = levelFactory;
	}

	public override void Enter() {
		SetNewLevel();
		GotoGameplayState();
	}

	private void SetNewLevel() {
		var worldIndex = _progressData.current;
		var levelIndex = _progressData.levelsHolder.currentIndex;
		var level = _levelFactory.CreateLevel(worldIndex, levelIndex);
		_gameplaySystem.SetNewLevel(level);
	}

	private void GotoGameplayState() {
		switcher.SetState<GameState>();
	}
}