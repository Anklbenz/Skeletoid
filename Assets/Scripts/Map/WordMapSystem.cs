using Cysharp.Threading.Tasks;
using UnityEngine;

public class WordMapSystem
{
    private const int PARTICLES_PLAY_DELAY = 500;
    private readonly MapHud _mapHud;
    private readonly ProgressSystem _progressSystem;
    private readonly SceneLoaderService _sceneLoaderService;
    private readonly MapParticlesPlayer _mapParticlesPlayer;
    private readonly KeysRecoverySystem _keysRecoverySystem;
    private readonly CameraSystem _cameraSystem;

    public WordMapSystem(
        MapHud mapHud,
        ProgressSystem progressSystem,
        SceneLoaderService sceneLoaderService,
        MapParticlesPlayer mapParticlesPlayer,
        KeysRecoverySystem keysRecoverySystem,
        CameraSystem cameraSystem) {

        _mapHud = mapHud;
        _progressSystem = progressSystem;
        _sceneLoaderService = sceneLoaderService;
        _mapParticlesPlayer = mapParticlesPlayer;
        _keysRecoverySystem = keysRecoverySystem;
        _cameraSystem = cameraSystem;
    }

    public void Initialize(MapItem[] items) {
        _keysRecoverySystem.Initialize();
        UpdateMap(items);
        _mapHud.Refresh();
    }

    private async void UpdateMap(MapItem[] items) {
        for (var i = 0; i < _progressSystem.worldsCount; i++) {
            var info = _progressSystem.GetWordInfoByIndex(i);
            var mapItem = items[i];

            mapItem.isLevelCompleted = info.isCompleted;
            mapItem.levelStarsCount = info.starsCount;
          //  mapItem.levelsCount = info.levelsCount;
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
        _cameraSystem.Shake();
    }

    private void OnLevelSelect(int worldIndex) {
        if (_progressSystem.keysCount == 0) {
            Debug.Log("No Keys Left");
            return;
        }
        
        _progressSystem.SetWorld(worldIndex);
        _sceneLoaderService.GoToGameplayScene();
    }
}