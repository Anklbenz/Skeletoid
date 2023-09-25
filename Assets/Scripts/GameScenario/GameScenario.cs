public class GameScenario {
	private readonly StateSwitcher _stateSwitcher;
	
	public GameScenario (StateSwitcher stateSwitcher, InitializeState initializeState) {
		_stateSwitcher = stateSwitcher;
		_stateSwitcher.AddState(initializeState);
		_stateSwitcher.SetState<InitializeState>();
	} 
}
