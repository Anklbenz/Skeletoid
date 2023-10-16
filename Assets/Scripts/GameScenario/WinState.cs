public class WinState : State
{
	private readonly StateSwitcher _stateSwitcher;
	private readonly WinSystem _winSystem;
	private readonly PauseHandler _pauseHandler;
	private readonly ProgressData _progressData;
	private readonly SceneLoaderService _sceneLoaderService;

	public WinState(StateSwitcher stateSwitcher, WinSystem winSystem, PauseHandler pauseHandler, ProgressData progressData, SceneLoaderService sceneLoaderService) :
		base(stateSwitcher) {
		_winSystem = winSystem;
		_pauseHandler = pauseHandler;
		_progressData = progressData;
		_stateSwitcher = stateSwitcher;
		_sceneLoaderService = sceneLoaderService;
		_winSystem.ContinueEvent += OnContinueSelected;
	}

	public override void Enter() {
		_pauseHandler.SetPause(true);
		_winSystem.OnWin();
	}

	public override void Exit() {
		_pauseHandler.SetPause(false);
	}

	private void OnContinueSelected() {
		if (_progressData.levelsHolder.TryMoveNext())
			_stateSwitcher.SetState<InitializeLevelState>();

		_progressData.SetCurrentCompleted();
		_sceneLoaderService.GoToMainMenuScene();
	}
}