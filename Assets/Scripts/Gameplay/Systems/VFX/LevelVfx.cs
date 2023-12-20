using System;
using ParticleEnum;
using UnityEngine;

public class LevelVfx {
	private readonly ParticlesPlayer _particlesPlayer;
	private readonly CameraShaker _cameraShaker;

	private Level _level;

	public LevelVfx(ParticlesPlayer particlesPlayer, CameraShaker cameraShaker) {
		_particlesPlayer = particlesPlayer;
		_cameraShaker = cameraShaker;
	}

	public void Start(Level level) {
		_level = level;
		Subscribe();
	}

	public void Stop() {
		UnSubscribe();
		_level = null;
	}

	private void OnPaddleHit(Vector3 hitPoint) {
		_particlesPlayer.PlaySpark(hitPoint);
	}

	private void OnBrickDestroy(Brick brick) {
		PlayExplosion(brick);
		_cameraShaker.Shake();
	}
	private void PlayExplosion(Brick brick) {
		switch (brick.effect) {
			case Particles.DustDark:
				_particlesPlayer.PlayDustDarkExplosion(brick.position);
				break;
			case Particles.DustBright:
				_particlesPlayer.PlayDustBrightExplosion(brick.position);
				break;
			case Particles.DustCircle:
				_particlesPlayer.PlayDustCircleExplosion(brick.position);
				break;
			case Particles.Grenade:
				_particlesPlayer.PlayGrenadeExplosion(brick.position);
				break;
			case Particles.Water:
				_particlesPlayer.PlayWaterExplosion(brick.position);
				break;
			case Particles.Fire:
				_particlesPlayer.PlayFireExplosion(brick.position);
				break;
			case Particles.None:
				return;

			default:
				throw new Exception($"Particle type {brick.effect.ToString()} not exists ");
		}
	}

	private void Subscribe() {
		foreach (var brick in _level.bricks) {
			brick.NoLivesLeft += OnBrickDestroy;
		}
		_level.player.HitOnPaddleEvent += OnPaddleHit;

	}

	private void UnSubscribe() {
		foreach (var brick in _level.bricks) {
			brick.NoLivesLeft -= OnBrickDestroy;
		}
		_level.player.HitOnPaddleEvent -= OnPaddleHit;
	}
}