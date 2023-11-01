using System;
using UnityEngine;

[System.Serializable]
public class World {
	public LevelTemplate[] levels;
	public string worldName;
	
	[Header("Stars")]
	[SerializeField] private float firstSeconds;
	[SerializeField] private  float secondSeconds;
	[SerializeField] private  float thirdSeconds;
	
	public int levelsCount => levels.Length;
	public float firstStarSeconds => firstSeconds;
	public float secondStarSeconds => secondSeconds;
	public float thirdStarSeconds => thirdSeconds;

}
