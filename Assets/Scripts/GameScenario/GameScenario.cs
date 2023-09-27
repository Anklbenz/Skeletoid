public class GameScenario {
	private readonly StateSwitcher _stateSwitcher;

	public GameScenario(StateSwitcher stateSwitcher, InitialState initialState, GameState gameState){//GameObjectsConfig gameObjectsConfig, IInput input, IFactory factory) {
		_stateSwitcher = stateSwitcher;
		_stateSwitcher.AddState(initialState);
		_stateSwitcher.AddState(gameState);
	}

	public void Start() {
		_stateSwitcher.SetState<InitialState>();
	}
}
