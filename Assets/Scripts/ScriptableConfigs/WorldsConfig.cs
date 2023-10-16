using UnityEngine;

[CreateAssetMenu(menuName = "Configs/WorldsConfig", fileName = "WorldsConfig")]
public class WorldsConfig : ScriptableObject
{
    [SerializeField] private World[] worlds;

    public int wordsCount => worlds.Length;

    public int GetLevelCountByWordIndex(int index) =>
        worlds[index].levelsCount;
    
    public bool LevelPrefabExists(int wordIndex, int levelIndex) {
        if (wordIndex < worlds.Length)
            if (levelIndex < worlds[wordIndex].levels.Length)
                return true;
        return false;
    }

    public LevelTemplate GetLevelPrefab(int wordIndex, int levelIndex) {
        return worlds[wordIndex].levels[levelIndex];
    }
}
