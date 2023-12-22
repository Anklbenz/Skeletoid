using UnityEngine;
using System.Collections.Generic;

public class Level : MonoBehaviour {
	public int brickCount => bricks.Count;

	public Ball ball;
	public Player player;
	public Wall frontWall, backWall, leftWall, rightWall;
	public List<Brick> bricks = new();
	public DeadZone deadZone;

	public Environment environment;
	public Transform paddleOrigin;
	public void Destroy(Brick brick) {
		bricks.Remove(brick);
		UnityEngine.Object.Destroy(brick.gameObject);
	}
}