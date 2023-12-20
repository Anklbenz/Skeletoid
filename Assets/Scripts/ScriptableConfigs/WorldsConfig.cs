using UnityEngine;

[CreateAssetMenu(menuName = "Configs/WorldsConfig", fileName = "WorldsConfig")]
public class WorldsConfig : ScriptableObject
{
    [SerializeField] private World[] worlds;

    public int wordsCount => worlds.Length;

    public World GetWorldByIndex(int index) =>
        worlds[index];
    
    public LevelTemplate GetLevelPrefab(int wordIndex/*, int levelIndex*/) {
        return worlds[wordIndex].level/*[levelIndex]*/;
    }
}
