public class WinState : State
{
	private readonly StateSwitcher _stateSwitcher;
	private readonly WinSystem _winSystem;
	private readonly ProgressData _progressData;
	private readonly SceneLoaderService _sceneLoaderService;

	public WinState(StateSwitcher stateSwitcher, WinSystem winSystem, ProgressData progressData, SceneLoaderService sceneLoaderService) : base(stateSwitcher) {
		_winSystem = winSystem;
		_progressData = progressData;
		_stateSwitcher = stateSwitcher;
		_sceneLoaderService = sceneLoaderService;
		_winSystem.ContinueEvent += OnContinueSelected;
	}

	public override void Enter() =>
		_winSystem.OnWin();

	private void OnContinueSelected() {
		if(_progressData.levelsHolder.TryMoveNext())
			_stateSwitcher.SetState<InitializeLevelState>();
		else 
			_sceneLoaderService.GoToMainMenuScene();
	}
}