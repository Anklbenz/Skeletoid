using UnityEngine;

public class WinState : State
{
	private readonly StateSwitcher _stateSwitcher;
	private readonly Win _win;
	private readonly PauseHandler _pauseHandler;
	private readonly ProgressSystem _progressSystem;
	private readonly SceneLoader _sceneLoader;
	private readonly StarsSystem _starsSystem;

	public WinState(StateSwitcher stateSwitcher, Win win, PauseHandler pauseHandler, ProgressSystem progressSystem, SceneLoader sceneLoader, StarsSystem starsSystem) :
		base(stateSwitcher) {
		_win = win;
		_pauseHandler = pauseHandler;
		_progressSystem = progressSystem;
		_stateSwitcher = stateSwitcher;
		_sceneLoader = sceneLoader;
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
	//	if (_progressSystem.TrySetNextLevel())
	//		_stateSwitcher.SetState<InitializeLevelState>();
	
		_sceneLoader.GoToWorldMapScene();
	}

	private void SaveProgress() {
		_progressSystem.SetCurrentLevelStars(_starsSystem.starsCount);
		_progressSystem.SetCurrentLevelTime(_starsSystem.levelTime);
		_progressSystem.SetCurrentLevelCompleted();
	//	_progressSystem.ApplyCurrentCoins();
	}
}