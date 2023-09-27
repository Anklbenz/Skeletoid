using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Motor : MonoBehaviour {
	[SerializeField] protected float speed;
	public bool isActive { get; set; } = true;

	private Rigidbody _rigidBody;

	private void Awake() =>
			_rigidBody = GetComponent<Rigidbody>();

	protected void Move(Vector3 direction) {
		if (!isActive) return;

		_rigidBody.velocity = direction * speed;
	}

	private void OnDrawGizmos() {
		if (_rigidBody == null) return;
		Gizmos.DrawRay(transform.position, _rigidBody.velocity);
	}
}