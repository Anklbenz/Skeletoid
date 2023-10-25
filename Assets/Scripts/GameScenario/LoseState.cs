using Cysharp.Threading.Tasks;

public class LoseState : State
{
	private readonly StateSwitcher _stateSwitcher;
	private readonly GameplayConfig _gameplayConfig;
	private readonly LoseSystem _loseSystem;
	private readonly PauseHandler _pauseHandler;
	private readonly SceneLoaderService _sceneLoaderService;
	private readonly CameraSystem _cameraSystem;

	public LoseState(StateSwitcher stateSwitcher, GameplayConfig gameplayConfig, LoseSystem loseSystem, PauseHandler pauseHandler, SceneLoaderService sceneLoaderService,  CameraSystem cameraSystem) : base(stateSwitcher) {
		_loseSystem = loseSystem;
		_pauseHandler = pauseHandler;
		_stateSwitcher = stateSwitcher;
		_gameplayConfig = gameplayConfig;
		_sceneLoaderService = sceneLoaderService;
		_cameraSystem = cameraSystem;
		_loseSystem.RestartEvent += OnRestartSelected;
		_loseSystem.QuitEvent += OnQuitSelected;
	}

	public override async void Enter() {
		_pauseHandler.SetPause(true);
		await LookAtSkeleton();
		_loseSystem.OnLose();
	}

	public override void Exit() {
		_pauseHandler.SetPause(false);
		_cameraSystem.zoomedIsActive = false;
	}

	private void OnQuitSelected() {

		_sceneLoaderService.GoToMainMenuScene();
	}
	
	private async UniTask LookAtSkeleton() {
//		_progressSystem.currentLevel.player.aimTarget = _cameraSystem.zoomedCamTransform;
		_cameraSystem.zoomedIsActive = true;
		await UniTask.Delay(_gameplayConfig.delayBeforeLookAtSkeleton);
	}

	private void OnRestartSelected() =>
		_stateSwitcher.SetState<GameState>();
}