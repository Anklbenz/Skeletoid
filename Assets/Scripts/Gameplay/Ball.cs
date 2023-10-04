using System;
using UnityEngine;

public sealed class Ball : Motor, IReflectObject, IThrowObject
{
	[Header("Reflect settings")] [SerializeField]
	private float permissibleAngle, correctionStep;

	public event Action<Vector3> OnCollisionEvent;
	public bool isActive { get; set; } = true;
	public int damage { get; set; }
	public Vector3 direction { get; set; }

	private void FixedUpdate() {
		if (!isActive) return;
		Move(direction);
	}

	private void OnCollisionEnter(Collision collision) {
		OnCollisionEvent?.Invoke(collision.contacts[0].point);

		if (collision.transform.TryGetComponent<IPaddle>(out var paddle)) {
			if (paddle.CouldSpecialReflectionBePerformed(collision.contacts[0].point, -collision.contacts[0].normal)) {
				direction = paddle.GetDirectionDependsOnLocalPaddleHitPoint(collision.contacts[0].point);
				return;
			}
			Reflect(collision.contacts[0].normal);
			return;
		}

		Reflect(collision.contacts[0].normal);

		var damageable = collision.gameObject.GetComponentInParent<IDamageable>();
		if (damageable != null)
			TryCauseDamage(damageable);
	}

	public void Reflect(Vector3 hitNormal) {
		var isOppositeDirection = Vector3.Dot(direction, hitNormal) < 0;
		if (!isOppositeDirection) return;

		var collisionAngle = Vector3.SignedAngle(-hitNormal, direction, Vector3.up);
//		Debug.Log($"CollisionAngle {collisionAngle} Correct {collisionAngle} ");
		if (Mathf.Abs(collisionAngle) < permissibleAngle)
			direction = (Quaternion.AngleAxis(collisionAngle <= 0 ? -correctionStep : correctionStep, Vector3.up) * direction);

		var reflectedDirection2D = Vector2.Reflect(new Vector2(direction.x, direction.z), new Vector2(hitNormal.x, hitNormal.z));
		direction = new Vector3(reflectedDirection2D.x, 0, reflectedDirection2D.y);
	}

	private void TryCauseDamage(IDamageable damageable) =>
		damageable?.Hit(damage);

	public void Throw(Vector3 throwDirection) {
		rigidBody.AddForce(throwDirection, ForceMode.Impulse);
	}
}


//Bug: Reflect from wall implemented in CollisionStay, because Sometimes when paddle push ball to wall, ball stay on Collision Enter state and cant reflect, moving along the wall
/*
private void OnCollisionStay(Collision collisionInfo) {
	if (!collisionInfo.transform.TryGetComponent<IWall>(out _)) return;
	Reflect(collisionInfo.contacts[0].normal);
}
*/