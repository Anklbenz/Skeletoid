using UnityEngine;

public class WordMapSystem
{
    private readonly ProgressData _progressData;
    private readonly SceneLoaderService _sceneLoaderService;

    public WordMapSystem(ProgressData progressData, SceneLoaderService sceneLoaderService) {
        _progressData = progressData;
        _sceneLoaderService = sceneLoaderService;
    }

    public void Initialize(MapItem[] items) {
        UpdateMap(items);
    }

    private void UpdateMap(MapItem[] items) {
        for (var i = 0; i < _progressData.worldsInfo.Length; i++) {
            var info = _progressData.worldsInfo[i];
            var mapItem = items[i];

            if (info.isCompleted) {
                mapItem.isLevelCompleted = true;
                if (items[i + 1] != null)
                    items[i + 1].isLevelUnlocked = true;
            }

            mapItem.isLevelUnlocked = info.isUnlocked;
            mapItem.levelStarsCount = info.starsCount;
            mapItem.levelsCount = info.levelsCount;
            mapItem.StartEvent += OnLevelSelect;
        }
    }

    public void OnLevelSelect(int worldIndex) {
        _progressData.SetWorld(worldIndex);
        _sceneLoaderService.GoToGameplayScene();
    }
}