using UnityEngine;

public abstract class Damageble : Obstacle {
	[SerializeField] private int hitPoints;
	protected virtual void Hit(int damage) {
		hitPoints -= damage;
		OnHit();

		if (hitPoints > 0)
			OnDamaged();
		else
			OnDestroyed();
	}
	protected virtual void OnDamaged() {}
	protected virtual void OnHit() {}
	protected virtual void OnDestroyed() {}
}