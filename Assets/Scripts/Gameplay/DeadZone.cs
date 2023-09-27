using System;
using UnityEngine;

public class DeadZone : MonoBehaviour {
	public event Action DeadZoneReachedEvent;
	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponentInParent<Ball>())
			DeadZoneReachedEvent?.Invoke();
	}
}
