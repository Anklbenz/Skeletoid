public class GameScenario {
	private readonly StateSwitcher _stateSwitcher;

	public GameScenario(StateSwitcher stateSwitcher, InitialState initialState, InitializeLevelState initializeLevelState, GameState gameState, LoseState loseState, WinState winState) {
		_stateSwitcher = stateSwitcher;
		_stateSwitcher.AddState(initialState);
		_stateSwitcher.AddState(initializeLevelState);
		_stateSwitcher.AddState(gameState);
		_stateSwitcher.AddState(loseState);
		_stateSwitcher.AddState(winState);
	}

	public void Start() {
		_stateSwitcher.SetState<InitialState>();
	}
}