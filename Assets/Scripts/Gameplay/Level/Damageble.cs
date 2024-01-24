using UnityEngine;

public abstract class Damageble : Obstacle {
	[SerializeField] private int hitPoints;
	public Vector3 position => transform.position;
	public Quaternion rotation => transform.rotation;
	public virtual void Hit(int damage) {
		hitPoints -= damage;
		OnHit();

		if (hitPoints > 0)
			OnDamaged();
		else
			OnDestroyed();
	}

	public virtual void DeathHit() =>
			Hit(hitPoints);
	protected virtual void OnDamaged() {}
	protected virtual void OnHit() {}
	protected virtual void OnDestroyed() {}
}