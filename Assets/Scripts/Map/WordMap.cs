using Cinemachine;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class WordMap {
	private const int PARTICLES_PLAY_DELAY = 500;
	private const int KEY_SPEND_PREVIEW_TIME = 500;
	private readonly MapHud _mapHud;
	private readonly ProgressSystem _progressSystem;
	private readonly SceneLoader _sceneLoader;
	private readonly MapParticlesPlayer _mapParticlesPlayer;
	private readonly KeysRecoverySystem _keysRecoverySystem;
	private readonly FlyingService _flyingService;
	private readonly CameraShaker _cameraShaker;
	private readonly KeyShop _keyShop;
	private readonly KeySpendView _keySpendView;
	private readonly CinemachineVirtualCamera _virtualCamera;
	private Camera _cameraMain;

	public WordMap(
			MapHud mapHud,
			ProgressSystem progressSystem,
			SceneLoader sceneLoader,
			KeyShop keyShop,
			MapParticlesPlayer mapParticlesPlayer,
			KeysRecoverySystem keysRecoverySystem,
			FlyingService flyingService,
			CameraShaker cameraShaker,
			KeySpendView keySpendView,
			CinemachineVirtualCamera virtualCamera) {

		_mapHud = mapHud;
		_progressSystem = progressSystem;
		_sceneLoader = sceneLoader;
		_keyShop = keyShop;
		_mapParticlesPlayer = mapParticlesPlayer;
		_keysRecoverySystem = keysRecoverySystem;
		_flyingService = flyingService;
		_cameraShaker = cameraShaker;
		_keySpendView = keySpendView;
		_virtualCamera = virtualCamera;
		_flyingService.CollectedEvent += AddCoin;
	}

	public void Initialize(MapItem[] items) {
		_cameraMain = Camera.main;
		CenterCameraAtLastUnlocked(items[_progressSystem.lastUnlockedWorldIndex].transform.position);

		_keysRecoverySystem.Initialize();
		UpdateMap(items);
		_mapHud.Refresh();
		InitializeFlyingSystem();
		ApplyPreviousRewards();
	}
	private void InitializeFlyingSystem() {
		var coinsPosition = _mapHud.coinsTargetTransform.position;
		
		//_flyingService.destination = CameraScreenToWorldPosition(coinsPosition);
		_flyingService.destination = coinsPosition;

	}
	private Vector3 CameraScreenToWorldPosition(Vector3 screenPosition) =>
			_cameraMain.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, _cameraMain.nearClipPlane));

	private void ApplyPreviousRewards() {
		var lastEarnedCoins = _progressSystem.currentCoinsCount;
		//var screenCenterPosition = CameraScreenToWorldPosition(new Vector2((float)Screen.width / 2, (float)Screen.height / 2));
		var screenCenterPosition = new Vector2((float)Screen.width / 2, (float)Screen.height / 2);
		_flyingService.SpawnInCircle(screenCenterPosition, lastEarnedCoins);
		_progressSystem.ApplyCurrentCoins();
	}

	private async void UpdateMap(MapItem[] items) {
		for (var i = 0; i < _progressSystem.worldsCount; i++) {
			var info = _progressSystem.GetWordInfoByIndex(i);
			var mapItem = items[i];

			mapItem.isLevelCompleted = info.isCompleted;
			mapItem.levelStarsCount = info.starsCount;
			mapItem.StartEvent += OnLevelSelect;

			if (info.freshUnlocked) {
				info.freshUnlocked = false;
				await UnlockWorld(mapItem.dustParticlesTransform.position);
			}

			mapItem.isLevelUnlocked = info.isUnlocked;
		}

		var lastUnlockedLevelIndex = _progressSystem.lastUnlockedWorldIndex;
		items[lastUnlockedLevelIndex].isHighlight = true;


	}
	private void CenterCameraAtLastUnlocked(Vector2 lastUnlockedPosition) =>
		_virtualCamera.transform.position = new Vector3(lastUnlockedPosition.x, lastUnlockedPosition.y, _virtualCamera.transform.position.z);
	

	private async UniTask UnlockWorld(Vector3 mapItemPosition) {
		await UniTask.Delay(PARTICLES_PLAY_DELAY);
		_mapParticlesPlayer.PlayAtPoint(mapItemPosition);
		_cameraShaker.Shake();
	}

	private async void OnLevelSelect(int worldIndex) {
		if (_progressSystem.keysCount == 0) {
			_keyShop.Open();
			return;
		}
		var keyCount = _progressSystem.keysCount;

		await _keySpendView.Show(keyCount, keyCount - 1);
		_keysRecoverySystem.KeyDecrease();

		_progressSystem.SetWorld(worldIndex);
		_sceneLoader.GoToGameplayScene();
	}

	private void AddCoin() {
		_mapHud.AddCoin();
	}
}