using System;
using UnityEngine;

[System.Serializable]
public class World {
	public LevelTemplate[] levels;
	public string worldName;
	
	[Header("Stars")]
	public float firstSeconds;
	public float secondSeconds;
	public float thirdSeconds;
	
	public int levelsCount => levels.Length;
}
