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
	public List<Brick> junk = new();
	public Wall[] walls;
	public Transform paddleOrigin;
}