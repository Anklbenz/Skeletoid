public class PauseState : State
{
	private readonly StateSwitcher _stateSwitcher;
	private readonly PauseUiSystem _pauseUiSystem;
	private readonly PauseHandler _pauseHandler;
	private readonly KeysRecoverySystem _keysRecoverySystem;
	private readonly SceneLoaderService _sceneLoaderService;
	

	public PauseState(StateSwitcher stateSwitcher, PauseUiSystem pauseUiSystem, PauseHandler pauseHandler,KeysRecoverySystem keysRecoverySystem, SceneLoaderService sceneLoaderService) : base(stateSwitcher) {
		_stateSwitcher = stateSwitcher;
		_pauseUiSystem = pauseUiSystem;
		_pauseHandler = pauseHandler;
		_keysRecoverySystem = keysRecoverySystem;
		_sceneLoaderService = sceneLoaderService;


		_pauseUiSystem.ContinueEvent += OnResume;
		_pauseUiSystem.RestartEvent += OnRestart;
		_pauseUiSystem.QuitEvent += OnQuit;
	}

	public override void Enter() {
		_pauseHandler.SetPause(true);

		_pauseUiSystem.Open();
	}

	public override void Exit() {
		_pauseHandler.SetPause(false);
	}

	private void OnResume() {
		_pauseUiSystem.Close();
		_pauseHandler.SetPause(false);
		_stateSwitcher.SetState<GameState>();
	}

	private void OnRestart() {
		//reset score
		_stateSwitcher.SetState<InitializeLevelState>();
		_pauseUiSystem.Close();
	}

	private void OnQuit() {
		_keysRecoverySystem.KeyDecrease();
		_sceneLoaderService.GoToMainMenuScene();
	}
}