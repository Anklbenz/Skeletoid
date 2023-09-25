public class GameScenario {
	private readonly StateSwitcher _stateSwitcher;
	
	public GameScenario (StateSwitcher stateSwitcher, InitialState initialState, GameplayState gameplayState ) {
		_stateSwitcher = stateSwitcher;
		_stateSwitcher.AddState(initialState);
		_stateSwitcher.AddState(gameplayState);
		_stateSwitcher.SetState<InitialState>();
	} 
}
