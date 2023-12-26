using UnityEngine;
using System.Collections.Generic;

public class Level : MonoBehaviour {
	public int brickCount => bricks.Count;

	public List<Brick> bricks = new();
	public Transform paddleOrigin;
	public Wall[] walls;
	public StoneBackWall stoneBackWall;
	public DeadZone deadZone;
	public Ball ball;
	public Player player;

	public Environment environment;
	public void Destroy(Brick brick) {
		bricks.Remove(brick);
		UnityEngine.Object.Destroy(brick.gameObject);
	}
}