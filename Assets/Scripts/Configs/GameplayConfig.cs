using UnityEngine;

[CreateAssetMenu(fileName = "GameplayConfig", menuName = "Configs/GameplayConfig")]
public class GameplayConfig : ScriptableObject {
	[SerializeField] private Map[] map;
	[SerializeField] private Ball ball;
	[SerializeField] private Paddle paddle;
	
	public Ball ballPrefab => ball;
	public Paddle paddlePrefab => paddle;
	public Map GetMapPrefab(int index) =>
			map[index];
}
