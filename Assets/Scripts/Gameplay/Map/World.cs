[System.Serializable]
public class World {
	public LevelTemplate[] levels;
	public string worldName;
	
	public int levelsCount => levels.Length;
}
