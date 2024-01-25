public class TrainingState : State {
	private readonly StateSwitcher _stateSwitcher;
	private readonly HudSystem _hudSystem;
	private readonly IInput _input;

	public TrainingState(StateSwitcher stateSwitcher, HudSystem hudSystem, IInput input) : base(stateSwitcher) {
		_stateSwitcher = stateSwitcher;
		_hudSystem = hudSystem;
		_input = input;
	}

	public override void Enter() {
		PlayTraining();
		_input.AnyPressedEvent += CancelTraining;
	}

	public override void Exit() =>
			_input.AnyPressedEvent -= CancelTraining;

	private void PlayTraining() =>
			_hudSystem.PlayTraining(true);

	private void CancelTraining() {
		_hudSystem.PlayTraining(false);
		_stateSwitcher.SetState<GameState>();
	}
}