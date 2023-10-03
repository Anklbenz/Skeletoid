using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Motor : MonoBehaviour, IPauseSensitive
{

	public float speed { get; set; }

	private Rigidbody _rigidBody;
	private bool _isActive = true;

	private void Awake() =>
		_rigidBody = GetComponent<Rigidbody>();

	protected void Move(Vector3 direction) {
		_rigidBody.velocity = _isActive ? direction * speed : Vector3.zero;
		
	}

	public void SetPause(bool isPaused) =>
		_isActive = !isPaused;

	private void OnDrawGizmos() {
		if (_rigidBody == null) return;
		Gizmos.DrawRay(transform.position, _rigidBody.velocity);
	}
}