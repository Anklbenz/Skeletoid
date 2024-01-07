public class GameScenario {
	private readonly StateSwitcher _stateSwitcher;

	public GameScenario(
			StateSwitcher stateSwitcher,
			InitialState initialState,
			InitializeLevelState initializeLevelState,
			PauseState pauseState,
			GameState gameState,
			LoseState loseState,
			WinState winState,
			FinalizeState finalizeState) {
		_stateSwitcher = stateSwitcher;
		_stateSwitcher.AddState(initialState);
		_stateSwitcher.AddState(initializeLevelState);
		_stateSwitcher.AddState(gameState);
		_stateSwitcher.AddState(loseState);
		_stateSwitcher.AddState(pauseState);
		_stateSwitcher.AddState(winState);
		_stateSwitcher.AddState(finalizeState);
	}

	public void Start() {
		_stateSwitcher.SetState<InitialState>();
	}
}