using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {
	public Transform paddleOrigin;
	public List<Brick> bricks = new();
	public DeadZone deadZone;

	public void Destroy(Brick brick) {
		bricks.Remove(brick);
		UnityEngine.Object.Destroy(brick.gameObject);
	}
}
   