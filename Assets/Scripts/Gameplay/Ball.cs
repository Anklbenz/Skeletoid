using System;
using UnityEngine;

public class Ball : Motor, IReflect {
	[SerializeField] private int damage;
	[Header("Reflect settings")]
	[SerializeField] private float permissibleAngle, correctionStep;

	public Action<Vector3> OnCollisionEvent;
	public Vector3 direction { get; set; }

	private void FixedUpdate() =>
		Move(direction);
	
	private void OnCollisionEnter(Collision collision) {
		OnCollisionEvent?.Invoke(collision.contacts[0].point);

		if (collision.transform.TryGetComponent<IPaddle>(out _))
			return;

		Reflect(collision.contacts[0].normal);
		TryCauseDamage(collision.gameObject);
	}

	public void Reflect(Vector3 hitNormal) {
		var isOppositeDirection = Vector3.Dot(direction, hitNormal) < 0;
		if (!isOppositeDirection) return;

		var collisionAngle = Vector3.SignedAngle(-hitNormal, direction, Vector3.up);

		if (Mathf.Abs(collisionAngle) < permissibleAngle) {
			Debug.Log($"CollisionAngle {collisionAngle} Correct {collisionAngle} ");
			direction = (Quaternion.AngleAxis(collisionAngle <= 0 ? -correctionStep : correctionStep, Vector3.up) * direction);
		}

		var direction2 = Vector2.Reflect(new Vector2(direction.x, direction.z), new Vector2(hitNormal.x, hitNormal.z));
		direction = new Vector3(direction2.x, 0, direction2.y);
	}

	private void TryCauseDamage(GameObject collision) {
		var damageable = collision.GetComponentInParent<IDamageable>();
		damageable?.Hit(damage);
	}
}