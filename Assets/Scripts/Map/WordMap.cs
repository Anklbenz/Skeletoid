using Cysharp.Threading.Tasks;
using UnityEngine;

public class WordMap {
	private const int PARTICLES_PLAY_DELAY = 500;
	private readonly MapHud _mapHud;
	private readonly ProgressSystem _progressSystem;
	private readonly SceneLoaderService _sceneLoaderService;
	private readonly MapParticlesPlayer _mapParticlesPlayer;
	private readonly KeysRecoverySystem _keysRecoverySystem;
	private readonly FlyingService _flyingService;
	private readonly CameraShaker _cameraShaker;
	private Camera _cameraMain;

	public WordMap(
			MapHud mapHud,
			ProgressSystem progressSystem,
			SceneLoaderService sceneLoaderService,
			MapParticlesPlayer mapParticlesPlayer,
			KeysRecoverySystem keysRecoverySystem,
			FlyingService flyingService,
			CameraShaker cameraShaker) {

		_mapHud = mapHud;
		_progressSystem = progressSystem;
		_sceneLoaderService = sceneLoaderService;
		_mapParticlesPlayer = mapParticlesPlayer;
		_keysRecoverySystem = keysRecoverySystem;
		_flyingService = flyingService;
		_cameraShaker = cameraShaker;
		_flyingService.CollectedEvent += AddCoin;
	}

	public void Initialize(MapItem[] items) {
		_keysRecoverySystem.Initialize();
		UpdateMap(items);
		_mapHud.Refresh();
		InitializeFlyingSystem();
		ApplyPreviousRewards();
	}
	private void InitializeFlyingSystem() {
		var coinsPosition = _mapHud.coinsTargetTransform.position;
		_cameraMain = Camera.main;
		_flyingService.destination = CameraScreenToWorldPosition(coinsPosition);
		
	}
	private Vector3 CameraScreenToWorldPosition(Vector3 screenPosition) =>
		_cameraMain.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, _cameraMain.nearClipPlane));

	private void ApplyPreviousRewards() {
		var lastEarnedCoins = _progressSystem.currentCoinsCount;
		var screenCenterPosition = CameraScreenToWorldPosition(new Vector2((float)Screen.width / 2, (float)Screen.height / 2));
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

	private async UniTask UnlockWorld(Vector3 mapItemPosition) {
		await UniTask.Delay(PARTICLES_PLAY_DELAY);
		_mapParticlesPlayer.PlayAtPoint(mapItemPosition);
		_cameraShaker.Shake();
		//_cameraSystem.Shake();
	}

	private void OnLevelSelect(int worldIndex) {
		if (_progressSystem.keysCount == 0) {
			Debug.Log("No Keys Left");
			return;
		}

		_progressSystem.SetWorld(worldIndex);
		_sceneLoaderService.GoToGameplayScene();
	}
	
	private void AddCoin() {
		_mapHud.AddCoin();
	}
}