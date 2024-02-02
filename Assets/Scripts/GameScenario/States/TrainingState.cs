public class TrainingState : State {
	private readonly StateSwitcher _stateSwitcher;
	private readonly PauseHandler _pauseHandler;
	private readonly HudSystem _hudSystem;
	private readonly IInput _input;

	public TrainingState(StateSwitcher stateSwitcher, PauseHandler pauseHandler, HudSystem hudSystem, IInput input) : base(stateSwitcher) {
		_stateSwitcher = stateSwitcher;
		_pauseHandler = pauseHandler;
		_hudSystem = hudSystem;
		_input = input;
	}

	public override void Enter() {
		PlayTraining();
		_pauseHandler.SetPause(false);
		_input.ShotEvent += CancelTraining;
	}

	public override void Exit() =>
			_input.AnyPressedEvent -= CancelTraining;

	private void PlayTraining() =>
			_hudSystem.ShowTraining();


	private void CancelTraining() {
		_hudSystem.HideTraining();
		_stateSwitcher.SetState<GameState>();
	}
}