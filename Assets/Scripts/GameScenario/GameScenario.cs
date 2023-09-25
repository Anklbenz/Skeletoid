public class GameScenario {
	private readonly StateSwitcher _stateSwitcher;
	
	public GameScenario (StateSwitcher stateSwitcher, InitialState initialState, PlayState playState ) {
		_stateSwitcher = stateSwitcher;
		_stateSwitcher.AddState(initialState);
		_stateSwitcher.AddState(playState);
		_stateSwitcher.SetState<InitialState>();
	} 
}
