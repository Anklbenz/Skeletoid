using Cysharp.Threading.Tasks;
using UnityEngine;

public class WordMapSystem
{
    private const int PARTICLES_PLAY_DELAY = 500;
    private readonly MapHud _mapHud;
    private readonly ProgressSystem _progressSystem;
    private readonly SceneLoaderService _sceneLoaderService;
    private readonly MapParticlesPlayer _mapParticlesPlayer;


    public WordMapSystem(
        MapHud mapHud,
        ProgressSystem progressSystem,
        SceneLoaderService sceneLoaderService,
        MapParticlesPlayer mapParticlesPlayer) {

        _mapHud = mapHud;
        _progressSystem = progressSystem;

        _sceneLoaderService = sceneLoaderService;
        _mapParticlesPlayer = mapParticlesPlayer;
    }

    public void Initialize(MapItem[] items) {
        UpdateMap(items);
        _mapHud.Refresh();
    }

    private async void UpdateMap(MapItem[] items) {
        for (var i = 0; i < _progressSystem.worldsCount; i++) {
            var info = _progressSystem.GetWordInfoByIndex(i);
            var mapItem = items[i];

            mapItem.isLevelCompleted = info.isCompleted;
            mapItem.levelStarsCount = info.starsCount;
            mapItem.levelsCount = info.levelsCount;
            mapItem.StartEvent += OnLevelSelect;

            if (info.freshUnlockedTrigger)
                await UnlockedWorldParticlesPlay(mapItem.dustParticlesTransform.position);

            mapItem.isLevelUnlocked = info.isUnlocked;
        }
    }

    private async UniTask UnlockedWorldParticlesPlay(Vector3 mapItemPosition) {
        await UniTask.Delay(PARTICLES_PLAY_DELAY);
        _mapParticlesPlayer.PlayAtPoint(mapItemPosition);
    }

    private void OnLevelSelect(int worldIndex) {
        _progressSystem.SetWorld(worldIndex);
        _sceneLoaderService.GoToGameplayScene();
    }
}