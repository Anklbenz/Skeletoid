using UnityEngine;
using System.Collections.Generic;

public class Level : MonoBehaviour {
	public int brickCount => bricks.Count;

	public Ball ball;
	public Player player;
	public BallLauncher ballLauncher;
	public List<Brick> bricks = new();
	public DeadZone deadZone;

	public Environment environment;
	public Transform paddleOrigin;

	public void SetBallToDefaultPosition() {
		ball.transform.position = player.ballTransform.position;
	}
	
	public void Destroy(Brick brick) {
		bricks.Remove(brick);
		UnityEngine.Object.Destroy(brick.gameObject);
	}
}
   