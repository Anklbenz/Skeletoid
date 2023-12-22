using Cysharp.Threading.Tasks;

public class LoseState : State {
	private readonly StateSwitcher _stateSwitcher;
	private readonly GameplayConfig _gameplayConfig;
	private readonly KeysRecoverySystem _keysRecoverySystem;

	private readonly SceneLoader _sceneLoader;
	private readonly PauseHandler _pauseHandler;
	private readonly CameraZoom _cameraZoom;
	private readonly Lose _lose;

	public LoseState(
			StateSwitcher stateSwitcher,
			GameplayConfig gameplayConfig,
			Lose lose,
			KeysRecoverySystem keysRecoverySystem,
			PauseHandler pauseHandler,
			SceneLoader sceneLoader,
			CameraZoom cameraZoom) : base(stateSwitcher) {
		_keysRecoverySystem = keysRecoverySystem;
		_pauseHandler = pauseHandler;
		_stateSwitcher = stateSwitcher;
		_gameplayConfig = gameplayConfig;
		_sceneLoader = sceneLoader;
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
		
		_sceneLoader.GoToWorldMapScene();
	}

	private async UniTask LookAtSkeleton() {
		_cameraZoom.zoomedIsActive = true;
		await UniTask.Delay(_gameplayConfig.delayBeforeLookAtSkeleton);
	}

	private void OnRestartSelected() =>
			_stateSwitcher.SetState<GameState>();
}