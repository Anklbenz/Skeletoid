using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameplayConfig", menuName = "Configs/GameplayConfig")]
public class GameplayConfig : ScriptableObject {
	[SerializeField] private World[] worlds;
	[SerializeField] private Ball ball;
	[SerializeField] private Paddle paddle;

	public Ball ballPrefab => ball;
	public Paddle paddlePrefab => paddle;

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
