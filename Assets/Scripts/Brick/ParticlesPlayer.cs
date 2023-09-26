using UnityEngine;

public class ParticlesPlayer {
	private ParticleSystem _boxDestroyParticle, _onBallCollisionParticle;
	
	public ParticlesPlayer(GameObjectsConfig gameObjectsConfig, IFactory factory) {
      _boxDestroyParticle = factory.Get<ParticleSystem>(gameObjectsConfig.destroyParticlesPrefab);
      _onBallCollisionParticle = factory.Get<ParticleSystem>(gameObjectsConfig.hitParticlesPrefab);
      _boxDestroyParticle.Stop();
      _onBallCollisionParticle.Stop();
	}

	public void BallCollisionPlay(Vector3 position) {
		PlayOnPosition(_onBallCollisionParticle, position);
	}

	public void PlayDestroy(Vector3 position) {
		PlayOnPosition(_boxDestroyParticle, position);
	}

	public void PlayOnPosition(ParticleSystem particle, Vector3 position) {
		particle.transform.position = position;
		particle.Play();
	}
}
