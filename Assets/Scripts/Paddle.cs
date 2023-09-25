using UnityEngine;
using Zenject;

public class Paddle : Motor, IPaddle {
	private const float MAX_REFLECT_ANGLE = 70;

	[SerializeField] private Transform ballParent;
	[SerializeField] private BoxCollider boxCollider;
	public Transform ballTransform => ballParent;
	private IInput _input;

	[Inject]
	public void Constructor(IInput input) {
		_input = input;
		_input.Left += MoveLeft;
		_input.Right += MoveRight;
	}

	private void MoveLeft() =>
			Move(Vector3.left);

	private void MoveRight() =>
			Move(Vector3.right);

	private void OnCollisionEnter(Collision collision) {
		var reflectInstance = collision.transform.GetComponent<IReflect>();
		if (reflectInstance is null) return;

		var hitPoint = collision.contacts[0].point;
		var normal = collision.contacts[0].normal;

		var isFrontSideHit = Vector3.Dot(transform.forward, normal) < 0;

		if (!isFrontSideHit) {
			reflectInstance.Reflect(-normal);
			return;
		}

		var dir = GetDirectionDependsOnLocalPaddleHitPoint(hitPoint);
		reflectInstance.SetDirection(dir);
	}

	private Vector3 GetDirectionDependsOnLocalPaddleHitPoint(Vector3 collisionPoint) {
		var colliderMinX = boxCollider.center.x - boxCollider.size.x / 2;
		var colliderMaxX = boxCollider.center.x + boxCollider.size.x / 2;

		var hitPointLocal = transform.InverseTransformPoint(collisionPoint);
		var lerpX = Mathf.InverseLerp(colliderMinX, colliderMaxX, hitPointLocal.x);
		var angle = Mathf.Lerp(-MAX_REFLECT_ANGLE, MAX_REFLECT_ANGLE, lerpX);

		return (Quaternion.AngleAxis(angle, Vector3.up) * transform.forward);
	}
}

//Debug.Log($"Angle {angle}, minX {colliderMinX} maxX {colliderMaxX} hitX {hitPointLocal.x}");