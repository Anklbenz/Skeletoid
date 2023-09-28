public class LoseState : State {

	private readonly StateSwitcher _stateSwitcher;
	private readonly LoseSystem _loseSystem;
	private readonly SceneLoaderService _sceneLoaderService;

	public LoseState(StateSwitcher stateSwitcher, LoseSystem loseSystem, SceneLoaderService sceneLoaderService) : base(stateSwitcher) {
		_loseSystem = loseSystem;
		_stateSwitcher = stateSwitcher;
		_sceneLoaderService = sceneLoaderService;
		_loseSystem.RestartEvent += OnRestartSelected;
		_loseSystem.QuitEvent += OnQuitSelected;
	}
	public override void Enter() {
		_loseSystem.OnLose();
	}

	private void OnQuitSelected() =>
			_sceneLoaderService.GoToMainMenuScene();

	private void OnRestartSelected() =>
			_stateSwitcher.SetState<GameState>();

}