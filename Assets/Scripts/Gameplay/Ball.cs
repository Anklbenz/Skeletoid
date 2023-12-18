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
		//Handle paddle reflect
	//	if (CheckPaddleCollision(collision)) return;
	//	OnCollisionEvent?.Invoke(collision.contacts[0].point);

	//	Reflect(collision.contacts[0].normal);
	//	CheckDamageableCollision(collision);

		if (!isActive)
			CheckFallOnFloor(collision);
	}

	private void CheckFallOnFloor(Collision collision) {
		if (collision.transform.TryGetComponent<IFloor>(out _))
			FallOnFloorEvent?.Invoke();
	}
	/*private void CheckDamageableCollision(Collision collision) {
		var damageable = collision.gameObject.GetComponentInParent<IDamageable>();
		if (damageable != null)
			damageable.Hit(damage);
	}
	*/

	/*private bool CheckPaddleCollision(Collision collision) {
		if (collision.transform.TryGetComponent<ISpecialReflect>(out var paddle)) {
			if (paddle.CouldSpecialReflectionBePerformed(collision.contacts[0].point, -collision.contacts[0].normal)) {
				direction = paddle.GetDirectionDependsOnLocalPaddleHitPoint(collision.contacts[0].point);
				return true;
			}
			Reflect(collision.contacts[0].normal);
			return true;
		}
		return false;
	}*/

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