public class GameplayState : State {
	private IInput _input;
	
	public GameplayState(StateSwitcher stateSwitcher, IInput input) : base(stateSwitcher) {
		_input = input;
	}
}
