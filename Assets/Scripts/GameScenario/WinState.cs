using UnityEngine;

public class WinState : State
{
	private readonly StateSwitcher _stateSwitcher;
	private readonly Win _win;
	private readonly PauseHandler _pauseHandler;
	private readonly ProgressSystem _progressSystem;
	private readonly SceneLoaderService _sceneLoaderService;
	private readonly StarsSystem _starsSystem;

	public WinState(StateSwitcher stateSwitcher, Win win, PauseHandler pauseHandler, ProgressSystem progressSystem, SceneLoaderService sceneLoaderService, StarsSystem starsSystem) :
		base(stateSwitcher) {
		_win = win;
		_pauseHandler = pauseHandler;
		_progressSystem = progressSystem;
		_stateSwitcher = stateSwitcher;
		_sceneLoaderService = sceneLoaderService;
		_starsSystem = starsSystem;
		_win.ContinueEvent += OnContinueSelected;
	}

	public override void Enter() {
		_pauseHandler.SetPause(true);
		_win.OnWin(_starsSystem.starsCount, _starsSystem.levelTimeSpan);
	
		SaveProgress();
	}

	public override void Exit() {
		_pauseHandler.SetPause(false);
	}

	private void OnContinueSelected() {
		if (_progressSystem.TrySetNextLevel())
			_stateSwitcher.SetState<InitializeLevelState>();
	
		_sceneLoaderService.GoToMainMenuScene();
	}

	private void SaveProgress() {
		_progressSystem.SetCurrentLevelStars(_starsSystem.starsCount);
		_progressSystem.SetCurrentLevelTime(_starsSystem.levelTime);
		_progressSystem.SetCurrentLevelCompleted();
	//	_progressSystem.ApplyCurrentCoins();
	}
}