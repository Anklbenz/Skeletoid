using UnityEngine;

public class ParticlesService {
	private const string PARTICLES_PARENT_NAME = "ParticlersParent";
	
	private readonly ParticlesConfig _particlesConfig;
	private PoolObjects<ParticleSystem> _destroyParticlesPool, _hitParticlesPool;
	private GameObject _particlesParent;
	
	public ParticlesService(ParticlesConfig gameObjectsConfig) {
		_particlesConfig = gameObjectsConfig;
	}
	public void Initialize() {
		_particlesParent = new GameObject(PARTICLES_PARENT_NAME);
		_destroyParticlesPool = new PoolObjects<ParticleSystem>(_particlesConfig.destroyParticlesPrefab, 4, true, _particlesParent.transform);
		_hitParticlesPool = new PoolObjects<ParticleSystem>(_particlesConfig.hitParticlesPrefab, 4, true, _particlesParent.transform);
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
