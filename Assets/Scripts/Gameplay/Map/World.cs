using UnityEngine;

[System.Serializable]
public class World {
	public LevelTemplate /*[]*/ level;
	public string worldName;
	[SerializeField] private int livesCount;
	[Header("Stars")]
	[SerializeField] private float firstSeconds;
	[SerializeField] private float secondSeconds;
	[SerializeField] private float thirdSeconds;

	public int lives => livesCount;

	public float firstStarSeconds => firstSeconds;
	public float secondStarSeconds => secondSeconds;
	public float thirdStarSeconds => thirdSeconds;
}
