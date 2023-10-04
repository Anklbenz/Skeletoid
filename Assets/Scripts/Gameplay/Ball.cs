using System;
using UnityEngine;

public sealed class Ball : Motor, IReflectObject
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

		if (collision.transform.TryGetComponent<IPaddle>(out _))
			return;

		Reflect(collision.contacts[0].normal);
		TryCauseDamage(collision.gameObject);
	}

	//Bug: Reflect from wall implemented in CollisionStay, because Sometimes when paddle push ball to wall, ball stay on Collision Enter state and cant reflect, moving along the wall
	private void OnCollisionStay(Collision collisionInfo) {
		if (!collisionInfo.transform.TryGetComponent<IWall>(out _)) return;
		Reflect(collisionInfo.contacts[0].normal);
	}

	public void Reflect(Vector3 hitNormal) {
		var isOppositeDirection = Vector3.Dot(direction, hitNormal) < 0;
		if (!isOppositeDirection) return;

		var collisionAngle = Vector3.SignedAngle(-hitNormal, direction, Vector3.up);

//			Debug.Log($"CollisionAngle {collisionAngle} Correct {collisionAngle} ");
		if (Mathf.Abs(collisionAngle) < permissibleAngle)
			direction = (Quaternion.AngleAxis(collisionAngle <= 0 ? -correctionStep : correctionStep, Vector3.up) * direction);

		var reflectedDirection2D = Vector2.Reflect(new Vector2(direction.x, direction.z), new Vector2(hitNormal.x, hitNormal.z));
		direction = new Vector3(reflectedDirection2D.x, 0, reflectedDirection2D.y);
	}

	private void TryCauseDamage(GameObject collision) {
		var damageable = collision.GetComponentInParent<IDamageable>();
		damageable?.Hit(damage);
	}
}