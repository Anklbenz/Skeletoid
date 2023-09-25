public class PlayState : State {
	private IInput _input;
	
	public PlayState(StateSwitcher stateSwitcher, IInput input) : base(stateSwitcher) {
		_input = input;
	}
}
