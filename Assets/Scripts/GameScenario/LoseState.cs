using Cysharp.Threading.Tasks;

public class LoseState : State {
	private readonly StateSwitcher _stateSwitcher;
	private readonly GameplayConfig _gameplayConfig;
	private readonly KeysRecoverySystem _keysRecoverySystem;

	private readonly SceneLoaderService _sceneLoaderService;
	private readonly PauseHandler _pauseHandler;
	private readonly CameraZoom _cameraZoom;
	private readonly Lose _lose;

	public LoseState(
			StateSwitcher stateSwitcher,
			GameplayConfig gameplayConfig,
			Lose lose,
			KeysRecoverySystem keysRecoverySystem,
			PauseHandler pauseHandler,
			SceneLoaderService sceneLoaderService,
			CameraZoom cameraZoom) : base(stateSwitcher) {
		_keysRecoverySystem = keysRecoverySystem;
		_pauseHandler = pauseHandler;
		_stateSwitcher = stateSwitcher;
		_gameplayConfig = gameplayConfig;
		_sceneLoaderService = sceneLoaderService;
		_cameraZoom = cameraZoom;
		
		_lose = lose;
		_lose.RestartEvent += OnRestartSelected;
		_lose.QuitEvent += OnQuitSelected;
	}

	public override async void Enter() {
		_pauseHandler.SetPause(true);
		await LookAtSkeleton();

		_lose.OnLose();
	}

	public override void Exit() {
		_pauseHandler.SetPause(false);
		_cameraZoom.zoomedIsActive = false;
	}

	private void OnQuitSelected() {
		_keysRecoverySystem.KeyDecrease();
		_sceneLoaderService.GoToMainMenuScene();
	}

	private async UniTask LookAtSkeleton() {
		_cameraZoom.zoomedIsActive = true;
		await UniTask.Delay(_gameplayConfig.delayBeforeLookAtSkeleton);
	}

	private void OnRestartSelected() =>
			_stateSwitcher.SetState<GameState>();
}