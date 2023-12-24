using System;
using UnityEngine;
using Random = UnityEngine.Random;

public sealed class Brick : Obstacle/*, IDamageable*/, ICost {
	[SerializeField] private int hitPoints;
	
	[SerializeField] private Vector2Int coinsMinMax;
	
	public event Action<Brick> NoLivesLeft;
	public event Action<Vector3> HitEvent;
	public int randomizedCost => Random.Range(coinsMinMax.x, coinsMinMax.y + 1);
	public  Vector3 position => transform.position;

	
	protected override void Reflect(IBall ball, Collision collision) {
		Hit(ball.damage);
		ball.Reflect(-collision.contacts[0].normal, this);
	}
	
	private void Hit(int damage) {
		hitPoints -= damage;
		
		HitNotify();
		
		if (hitPoints > 0) return;
		NotifyIfHitPointOut();
	}

	private void HitNotify() =>
			HitEvent?.Invoke(transform.position);

	private void NotifyIfHitPointOut() =>
			NoLivesLeft?.Invoke(this);
}