using UnityEngine;

public interface IBall {
	public int damage { get; set; }
	public Vector3 direction { get; set; }
	public void Reflect(Vector3 hitNormal, Obstacle sender = null);
}