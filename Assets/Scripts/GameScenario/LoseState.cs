using Cysharp.Threading.Tasks;

public class LoseState : State
{
	private readonly StateSwitcher _stateSwitcher;
	private readonly GameplayConfig _gameplayConfig;
	private readonly Lose _lose;
	private readonly KeysRecoverySystem _keysRecoverySystem;

	private readonly PauseHandler _pauseHandler;
	private readonly SceneLoaderService _sceneLoaderService;
	private readonly CameraSystem _cameraSystem;

	public LoseState(StateSwitcher stateSwitcher, GameplayConfig gameplayConfig, Lose lose, KeysRecoverySystem keysRecoverySystem, PauseHandler pauseHandler, SceneLoaderService sceneLoaderService,  CameraSystem cameraSystem) : base(stateSwitcher) {
		_lose = lose;
		_keysRecoverySystem = keysRecoverySystem;
		
		_pauseHandler = pauseHandler;
		_stateSwitcher = stateSwitcher;
		_gameplayConfig = gameplayConfig;
		_sceneLoaderService = sceneLoaderService;
		_cameraSystem = cameraSystem;
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
		_cameraSystem.zoomedIsActive = false;
	}

	private void OnQuitSelected() {
		_keysRecoverySystem.KeyDecrease();
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