public class GameScenario {
	private readonly StateSwitcher _stateSwitcher;

	public GameScenario(StateSwitcher stateSwitcher, InitialState initialState, GameplayState gameplayState){//GameObjectsConfig gameObjectsConfig, IInput input, IFactory factory) {
		_stateSwitcher = stateSwitcher;
		_stateSwitcher.AddState(initialState);
		_stateSwitcher.AddState(gameplayState);
	}

	public void Start() {
		_stateSwitcher.SetState<InitialState>();
	}
}
