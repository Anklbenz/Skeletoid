using UnityEngine;

public class ParticlesFactory
{
	private ParticlesConfig _particlesConfig;
	private PoolObjects<ParticleSystem> _destroyParticlesPool, _hitParticlesPool;

	public ParticlesFactory(ParticlesConfig gameObjectsConfig) {
		_particlesConfig = gameObjectsConfig;
		_destroyParticlesPool = new PoolObjects<ParticleSystem>(_particlesConfig.destroyParticlesPrefab, 4, true, null);
		_hitParticlesPool = new PoolObjects<ParticleSystem>(_particlesConfig.hitParticlesPrefab, 4, true, null);
	}
	
	public void PlayCollision(Vector3 position) =>
		PlayOnPosition(_hitParticlesPool.GetFreeElement(), position);

	public void PlayDestroy(Vector3 position) =>
		PlayOnPosition(_destroyParticlesPool.GetFreeElement(), position);

	private void PlayOnPosition(ParticleSystem particle, Vector3 position) {
		particle.transform.position = position;
		particle.Play();
	}
}
