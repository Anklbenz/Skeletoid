using System;
using UnityEngine;

public class Brick : MonoBehaviour, IDamageable {
	[SerializeField] protected int hitPoints;
	public event Action<Brick>  HitPointsOutEvent;
	public event Action<Vector3> HitEvent;

	public void Hit(int power) {
		hitPoints -= power;
		HitNotify();
		NotifyIfHitPointOut();
	}
	
	private void HitNotify() =>
			HitEvent?.Invoke(transform.position);

	private void NotifyIfHitPointOut() {
		if (hitPoints > 0) return;

		HitPointsOutEvent?.Invoke(this);
	}
}
