using UnityEngine;
using System.Collections.Generic;

public class Level : MonoBehaviour {
	public int brickCount => bricks.Count;
	public Player player { get; set; }
	public Ball ball { get; set; }
	public StoneBackWall backWall;
	public Environment environment;
	public DeadZone deadZone;

	public List<Brick> bricks = new();
	public Transform paddleOrigin;
	public Wall[] walls;

	public void Destroy(Brick brick) {
		bricks.Remove(brick);
		UnityEngine.Object.Destroy(brick.gameObject);
	}
}