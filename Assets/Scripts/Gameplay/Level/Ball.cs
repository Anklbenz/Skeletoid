using System;
using UnityEngine;

public sealed class Ball : Motor, IBall, IThrowable {
	//public event Action<Vector3> OnCollisionEvent;
	public event Action FallOnFloorEvent;
	public float permissibleAngle { get; set; }
	public float correctionStep { get; set; }
	public bool isActive { get; set; } = true;
	public int damage { get; set; }
	public Vector3 direction { get; set; }

	private void FixedUpdate() {
		if (!isActive) return;
		Move(direction);
	}
	public void Throw(Vector3 throwDirection) =>
			rigidBody.AddForce(throwDirection, ForceMode.Impulse);

	private void OnCollisionEnter(Collision collision) {
		if (!isActive)
			CheckFallOnFloor(collision);
	}

	private void CheckFallOnFloor(Collision collision) {
		if (collision.transform.TryGetComponent<IFloor>(out _))
			FallOnFloorEvent?.Invoke();
	}

	public void Reflect(Vector3 hitNormal, Obstacle sender = null) {
		var isOppositeDirection = Vector3.Dot(direction, hitNormal) < 0;
		if (!isOppositeDirection) return;

		var collisionAngle = Vector3.SignedAngle(-hitNormal, direction, Vector3.up);

		if (Mathf.Abs(collisionAngle) < permissibleAngle)
			direction = (Quaternion.AngleAxis(collisionAngle <= 0 ? -correctionStep : correctionStep, Vector3.up) * direction);

		var reflectedDirection2D = Vector2.Reflect(new Vector2(direction.x, direction.z), new Vector2(hitNormal.x, hitNormal.z));
		direction = new Vector3(reflectedDirection2D.x, 0, reflectedDirection2D.y);
	}
}