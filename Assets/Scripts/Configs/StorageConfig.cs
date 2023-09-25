using UnityEngine;

[CreateAssetMenu(fileName="LevelConfig", menuName="Configs/LevelConfig")]
public class StorageConfig : ScriptableObject {
	[SerializeField] private Map[] map;
	[SerializeField] private Paddle paddle;

	public bool Has(int index) =>
		 index >= 0 && index < map.Length && map[index] != null;
	
	public Map GetPrefab(int index) =>
		map[index];
	
	public Paddle GetPaddlePrefab() =>
			paddle;
}
