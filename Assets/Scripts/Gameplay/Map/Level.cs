using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {
	public Transform paddleOrigin;
	public List<Brick> bricks = new();
	public DeadZone deadZone;
}
   