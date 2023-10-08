using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Motor : MonoBehaviour, IPauseSensitive
{
	public float speed { get; set; }

	protected Rigidbody rigidBody;
	private bool _isActive = true;

	private void Awake() =>
		rigidBody = GetComponent<Rigidbody>();

	protected void Move(Vector3 direction) {
		rigidBody.velocity = _isActive ? direction * speed : Vector3.zero;
	}

	public void SetPause(bool isPaused) {
		_isActive = !isPaused;
		rigidBody.isKinematic = isPaused;
		if (isPaused)
			rigidBody.velocity = Vector3.zero;
	}

	private void OnDrawGizmos() {
		if (rigidBody == null) return;
		Gizmos.DrawRay(transform.position, rigidBody.velocity);
	}
}