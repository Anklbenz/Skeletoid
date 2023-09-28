public class WinState : State
{
	private readonly StateSwitcher _stateSwitcher;
	private readonly WinSystem _winSystem;
	private readonly SceneLoaderService _sceneLoaderService;

	public WinState(StateSwitcher stateSwitcher, WinSystem winSystem, SceneLoaderService sceneLoaderService) : base(stateSwitcher) {
		_winSystem = winSystem;
		_stateSwitcher = stateSwitcher;
		_sceneLoaderService = sceneLoaderService;
		_winSystem.ContinueEvent += OnContinueSelected;
	}

	public override void Enter() =>
		_winSystem.OnWin();

	private void OnContinueSelected() =>
		_sceneLoaderService.GoToMainMenuScene();
}