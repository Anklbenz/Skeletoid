using System;
using ParticleEnum;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour {
	[SerializeField] private Particles effectType;
	public Particles effect => effectType;
	protected virtual void OnCollisionEnter(Collision collision) {
		collision.gameObject.TryGetComponent<IBall>(out var ball);
		if (ball != null)
			Reflect(ball, collision);
	}


	protected abstract void Reflect(IBall ball, Collision collision);
}
