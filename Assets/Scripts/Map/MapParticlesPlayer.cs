using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapParticlesPlayer 
{
	private PoolObjects<ParticleSystem> _poolParticles;

	public MapParticlesPlayer(MapConfig config, Transform particlesParent) {
		_poolParticles = new PoolObjects<ParticleSystem>(config.unlockParticles, 3, true, particlesParent);
	}

	public void PlayAtPoint(Vector3 position) {
		var particle = _poolParticles.GetFreeElement();
		particle.transform.position = position;
		particle.Play();
	}
}