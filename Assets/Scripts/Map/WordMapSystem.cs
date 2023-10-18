using Cysharp.Threading.Tasks;
using UnityEngine;

public class WordMapSystem
{
    private const int PARTICLES_PLAY_DELAY = 500;
    private readonly ProgressSystem _progressSystem;
    private readonly SceneLoaderService _sceneLoaderService;
    private readonly MainStats _mainStats;
    private readonly MapParticlesPlayer _mapParticlesPlayer;

    public WordMapSystem(ProgressSystem progressSystem, SceneLoaderService sceneLoaderService, MainStats mainStats, MapParticlesPlayer mapParticlesPlayer) {
        _progressSystem = progressSystem;
        _sceneLoaderService = sceneLoaderService;
        _mainStats = mainStats;
        _mapParticlesPlayer = mapParticlesPlayer;
    }

    public void Initialize(MapItem[] items) {
        UpdateMap(items);
        _mainStats.Refresh();
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