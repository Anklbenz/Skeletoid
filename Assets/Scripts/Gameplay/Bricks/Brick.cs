using System;
using UnityEngine;
using Random = UnityEngine.Random;

public sealed class Brick : Damageble, ICost {
	public event Action<Brick> NoLivesLeft;
	public event Action<Vector3> HitEvent, DamagedEvent;
	public int randomizedCost => Random.Range(coinsMinMax.x, coinsMinMax.y + 1);
	public Vector3 position => transform.position;
	public bool required => requiredForComplete;

	[SerializeField] private Vector2Int coinsMinMax;
	[SerializeField] private SkinSwitcher skinSwitcher;
	[SerializeField] private bool requiredForComplete;
	protected override void Reflect(IBall ball, Collision collision) {
		Hit(ball.damage);
		ball.Reflect(-collision.contacts[0].normal);
	}
	protected override void OnHit() =>
			HitEvent?.Invoke(position);

	protected override void OnDestroyed() =>
			NoLivesLeft?.Invoke(this);

	protected override void OnDamaged() {
		DamagedEvent?.Invoke(position);
		skinSwitcher.MoveNext();
	}
}