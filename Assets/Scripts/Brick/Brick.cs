using System;
using UnityEngine;

public class Brick : MonoBehaviour, IDamageable {
	[SerializeField] protected int hitPoints;
	public event Action<Vector3> HitEvent, DestroyedEvent;

	public void Hit(int power) {
		hitPoints -= power;
		HitNotify();
		DestroyAndNotify();
	}
	
	private void HitNotify() =>
			HitEvent?.Invoke(transform.position);

	private void DestroyAndNotify() {
		if (hitPoints > 0) return;

		DestroyedEvent?.Invoke(transform.position);
	}
}
