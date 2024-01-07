public class PauseState : State {
	private readonly StateSwitcher _stateSwitcher;
	private readonly Pause _pause;
	private readonly PauseHandler _pauseHandler;
	private readonly KeysRecoverySystem _keysRecoverySystem;
	private readonly ProgressSystem _progressSystem;
	private readonly SceneLoader _sceneLoader;


	public PauseState(StateSwitcher stateSwitcher, Pause pause, PauseHandler pauseHandler, KeysRecoverySystem keysRecoverySystem, ProgressSystem progressSystem, SceneLoader sceneLoader) : base(stateSwitcher) {
		_stateSwitcher = stateSwitcher;
		_pause = pause;
		_pauseHandler = pauseHandler;
		_keysRecoverySystem = keysRecoverySystem;
		_progressSystem = progressSystem;
		_sceneLoader = sceneLoader;


		_pause.ContinueEvent += OnResume;
		_pause.RestartEvent += OnRestart;
		_pause.QuitEvent += OnQuit;
	}

	public override void Enter() {
		_pauseHandler.SetPause(true);

		_pause.Open();
	}

	public override void Exit() {
		_pauseHandler.SetPause(false);
	}

	private void OnResume() {
		_pause.Close();
		_pauseHandler.SetPause(false);
		_stateSwitcher.SetState<GameState>();
	}

	private void OnRestart() {
		//reset score
		_stateSwitcher.SetState<InitializeLevelState>();
		_pause.Close();
	}

	private void OnQuit() {
		_progressSystem.ResetCurrentCoins();
	//	_sceneLoader.GoToWorldMapScene();
	_stateSwitcher.SetState<FinalizeState>();
	}
}