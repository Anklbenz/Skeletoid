using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Brick : MonoBehaviour, IDamageable {
	[SerializeField] protected int hitPoints;
	[Tooltip("x value min, y value max; minInclusive, maxExclusive")]
	[SerializeField] protected Vector2Int scoreInterval;
	
	public int cost => Random.Range(scoreInterval.x, scoreInterval.y);

	public event Action<Brick> HitPointsOutEvent;
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
