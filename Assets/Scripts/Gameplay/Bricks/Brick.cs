using System;
using UnityEngine;
using Random = UnityEngine.Random;

public sealed class Brick : Obstacle, ICost {
	public event Action<Brick> NoLivesLeft;
	public event Action<Vector3> HitEvent, DamagedEvent;
	public int randomizedCost => Random.Range(coinsMinMax.x, coinsMinMax.y + 1);
	public Vector3 position => transform.position;
	
	[SerializeField] private int hitPoints;
	[SerializeField] private Vector2Int coinsMinMax;
	[SerializeField] private SkinSwitcher skinSwitcher;
	protected override void Reflect(IBall ball, Collision collision) {
		Hit(ball.damage);
		ball.Reflect(-collision.contacts[0].normal, this);
	}

	private void Hit(int damage) {
		hitPoints -= damage;
		NotifyHit();

		if (hitPoints > 0) {
			NotifyDamaged();
			skinSwitcher.MoveNext();
		}
		else {
			NotifyIfNoLives();
		}
	}
	private void NotifyDamaged() =>
			DamagedEvent?.Invoke(transform.position);
	private void NotifyHit() =>
			HitEvent?.Invoke(transform.position);
	private void NotifyIfNoLives() =>
			NoLivesLeft?.Invoke(this);
}