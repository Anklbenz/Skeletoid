using UnityEngine;

public class ParticlesPlayer {
	private const string PARTICLES_PARENT_NAME = "ParticlesParent";

	private readonly ParticlesConfig _config;
	private PoolObjects<ParticleSystem> _dustDarkPool, _dustBrightPool, _dustCirclePool, _firePool, _waterPool, _grenadePool, _sparkPool, _damagePool;
	private GameObject _particlesParent;

	public ParticlesPlayer(ParticlesConfig gameObjectsConfig) {
		_config = gameObjectsConfig;
	//	_backWallParticles = Object.Instantiate(_config.backWallParticlesPrefab);
	}
	public void Initialize() {
		_particlesParent = new GameObject(PARTICLES_PARENT_NAME);
		_sparkPool = new PoolObjects<ParticleSystem>(_config.sparkParticlePrefab, _config.sparkPoolSize, true, _particlesParent.transform);
		_dustDarkPool = new PoolObjects<ParticleSystem>(_config.dustDarkParticlesPrefab, _config.dustDarkPoolSize, true, _particlesParent.transform);
		_dustBrightPool = new PoolObjects<ParticleSystem>(_config.dustBrightParticlesPrefab, _config.dustBrightPoolSize, true, _particlesParent.transform);
		_dustCirclePool = new PoolObjects<ParticleSystem>(_config.dustCircleParticlesPrefab, _config.dustCirclePoolSize, true, _particlesParent.transform);
		_firePool = new PoolObjects<ParticleSystem>(_config.fireParticlesPrefab, _config.firePoolSize, true, _particlesParent.transform);
		_waterPool = new PoolObjects<ParticleSystem>(_config.waterParticlesPrefab, _config.waterPoolSize, true, _particlesParent.transform);
		_grenadePool = new PoolObjects<ParticleSystem>(_config.grenadeParticlesPrefab, _config.grenadePoolSize, true, _particlesParent.transform);
		_damagePool = new PoolObjects<ParticleSystem>(_config.damagePrefab, _config.damagePoolSize, true, _particlesParent.transform);
	}

	public void PlaySpark(Vector3 position) =>
			PlayOnPosition(_sparkPool.GetFreeElement(), position);

	public void PlayDustDarkExplosion(Vector3 position) =>
			PlayOnPosition(_dustDarkPool.GetFreeElement(), position);

	public void PlayDustBrightExplosion(Vector3 position) =>
			PlayOnPosition(_dustBrightPool.GetFreeElement(), position);

	public void PlayDustCircleExplosion(Vector3 position) =>
			PlayOnPosition(_dustCirclePool.GetFreeElement(), position);

	public void PlayFireExplosion(Vector3 position) =>
			PlayOnPosition(_firePool.GetFreeElement(), position);

	public void PlayWaterExplosion(Vector3 position) =>
			PlayOnPosition(_waterPool.GetFreeElement(), position);

	public void PlayGrenadeExplosion(Vector3 position) =>
			PlayOnPosition(_grenadePool.GetFreeElement(), position);

	public void PlayDamage(Vector3 position) =>
			PlayOnPosition(_damagePool.GetFreeElement(), position);
	
	private void PlayOnPosition(ParticleSystem particle, Vector3 position) {
		particle.transform.position = position;
		particle.Play();
	}
}