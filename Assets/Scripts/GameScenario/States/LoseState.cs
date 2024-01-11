using Cysharp.Threading.Tasks;

public class LoseState : State {
	private readonly StateSwitcher _stateSwitcher;
	private readonly GameplayConfig _gameplayConfig;

	private readonly CameraZoom _cameraZoom;
	private readonly Lose _lose;

	public LoseState(
			StateSwitcher stateSwitcher,
			GameplayConfig gameplayConfig,
			Lose lose,
			PauseHandler pauseHandler,
			CameraZoom cameraZoom) : base(stateSwitcher) {

	
		_stateSwitcher = stateSwitcher;
		_gameplayConfig = gameplayConfig;
		_cameraZoom = cameraZoom;

		_lose = lose;
		_lose.RestartEvent += OnRestartSelected;
		_lose.QuitEvent += OnQuitSelected;
	}

	public override async void Enter() {
	
		await LookAtSkeleton();

		_lose.OnLose();
	}

	public override void Exit() {
	_cameraZoom.zoomedIsActive = false;
	}

	private void OnQuitSelected() =>
			_stateSwitcher.SetState<FinalizeState>();

	private async UniTask LookAtSkeleton() {
		_cameraZoom.zoomedIsActive = true;
		await UniTask.Delay(_gameplayConfig.delayBeforeLookAtSkeleton);
	}

	private void OnRestartSelected() =>
			_stateSwitcher.SetState<GameState>();
}