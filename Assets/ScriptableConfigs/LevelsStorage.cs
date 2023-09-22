using UnityEngine;

[CreateAssetMenu(fileName="LevelConfig", menuName="Configs/LevelConfig")]
public class LevelsStorage : ScriptableObject {
	[SerializeField] private Level[] levels;

	public bool Has(int index) =>
		 index >= 0 && index < levels.Length && levels[index] != null;
	
	public Level Get(int index) =>
		levels[index];
}
