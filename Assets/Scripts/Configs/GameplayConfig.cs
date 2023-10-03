using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameplayConfig", menuName = "Configs/GameplayConfig")]
public class GameplayConfig : ScriptableObject {
	[SerializeField] private World[] worlds;
	[Header("Skull")]
	[SerializeField] private Ball ball;
	[SerializeField] private int damage;

	[SerializeField] private float skullStartSpeed = 5;
	[Header("Paddle")]
	
	[SerializeField] private Paddle paddle;
	[SerializeField] private float paddleStartSpeed = 5;

	public int skullDamage => damage;
	public float paddleSpeed => paddleStartSpeed;
	public float ballSpeed => skullStartSpeed;
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
