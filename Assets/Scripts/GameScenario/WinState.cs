using UnityEngine;

public class WinState : State
{
	private readonly StateSwitcher _stateSwitcher;
	private readonly WinSystem _winSystem;
	private readonly PauseHandler _pauseHandler;
	private readonly ProgressSystem _progressSystem;
	private readonly SceneLoaderService _sceneLoaderService;
	private readonly RewardSystem _rewardSystem;

	public WinState(StateSwitcher stateSwitcher, WinSystem winSystem, PauseHandler pauseHandler, ProgressSystem progressSystem, SceneLoaderService sceneLoaderService, RewardSystem rewardSystem) :
		base(stateSwitcher) {
		_winSystem = winSystem;
		_pauseHandler = pauseHandler;
		_progressSystem = progressSystem;
		_stateSwitcher = stateSwitcher;
		_sceneLoaderService = sceneLoaderService;
		_rewardSystem = rewardSystem;
		_winSystem.ContinueEvent += OnContinueSelected;
	}

	public override void Enter() {
		_pauseHandler.SetPause(true);
		_winSystem.OnWin();
	
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
		_progressSystem.SetStars(_rewardSystem.starsCount);
		_progressSystem.SetLevelTime(_rewardSystem.levelTime);
		_progressSystem.SetCurrentLevelCompleted();
		_progressSystem.ApplyCurrentCoins();
	}
}