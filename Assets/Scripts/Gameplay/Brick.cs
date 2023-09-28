using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Brick : MonoBehaviour, IDamageable, ICost {
	[SerializeField] protected int hitPoints;
	[Tooltip("x value min, y value max; minInclusive, maxExclusive")]
	[SerializeField] protected Vector2Int scoreInterval;
	public event Action<Brick> NoLivesLeft;
	public event Action<Vector3> HitEvent;
	public int costs => Random.Range(scoreInterval.x, scoreInterval.y);

	public void Hit(int power) {
		hitPoints -= power;
		HitNotify();
		NotifyIfHitPointOut();
	}

	private void HitNotify() =>
			HitEvent?.Invoke(transform.position);

	private void NotifyIfHitPointOut() {
		if (hitPoints > 0) return;

		NoLivesLeft?.Invoke(this);
	}
}