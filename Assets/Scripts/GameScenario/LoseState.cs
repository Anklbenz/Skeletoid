public class LoseState : State
{
	private readonly StateSwitcher _stateSwitcher;
	private readonly LoseSystem _loseSystem;
	private readonly PauseHandler _pauseHandler;
	private readonly SceneLoaderService _sceneLoaderService;

	public LoseState(StateSwitcher stateSwitcher, LoseSystem loseSystem, PauseHandler pauseHandler, SceneLoaderService sceneLoaderService) : base(stateSwitcher) {
		_loseSystem = loseSystem;
		_pauseHandler = pauseHandler;
		_stateSwitcher = stateSwitcher;
		_sceneLoaderService = sceneLoaderService;
		_loseSystem.RestartEvent += OnRestartSelected;
		_loseSystem.QuitEvent += OnQuitSelected;
	}

	public override void Enter() {
		_pauseHandler.SetPause(true);
		_loseSystem.OnLose();
	}

	public override void Exit() =>
		_pauseHandler.SetPause(false);

	private void OnQuitSelected() {
		
		_sceneLoaderService.GoToMainMenuScene();
	}
	
	private void OnRestartSelected() =>
		_stateSwitcher.SetState<GameState>();
}