public class WinState : State {
	private readonly StateSwitcher _stateSwitcher;
	private readonly Win _win;
	private readonly ProgressSystem _progressSystem;
	private readonly StarsTimer _starsTimer;

	public WinState(StateSwitcher stateSwitcher, Win win, PauseHandler pauseHandler, ProgressSystem progressSystem, StarsTimer starsTimer) :
			base(stateSwitcher) {
		_win = win;
		
		_progressSystem = progressSystem;
		_stateSwitcher = stateSwitcher;
		_starsTimer = starsTimer;
		_win.ContinueEvent += OnContinueSelected;
	}

	public override void Enter() {
	_win.OnWin(_starsTimer.starsCount, _starsTimer.levelTimeSpan);

		SaveProgress();
	}

			

	private void OnContinueSelected() =>
			_stateSwitcher.SetState<FinalizeState>();

	private void SaveProgress() {
		_progressSystem.SetCurrentLevelStars(_starsTimer.starsCount);
		_progressSystem.SetCurrentLevelTime(_starsTimer.levelTime);
		_progressSystem.SetCurrentLevelCompleted();
		//	_progressSystem.ApplyCurrentCoins();
	}
}