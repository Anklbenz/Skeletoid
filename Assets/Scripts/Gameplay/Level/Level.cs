using UnityEngine;
using System.Collections.Generic;
using Unity.AI.Navigation;

public class Level : MonoBehaviour {
	public int brickCount => bricks.Count;
	public Player player { get; set; }
	public Ball ball { get; set; }
	public StoneBackWall backWall;
	public Environment environment;
	public DeadZone deadZone;

	public NavMeshSurface navMeshSurface;
	public List<Enemy> enemies = new();
	public List<Brick> bricks = new();
	public List<Brick> junk = new();
	public Wall[] walls;
	public Floor floor;
	public Transform paddleOrigin;
	public NavMap navMap;
}