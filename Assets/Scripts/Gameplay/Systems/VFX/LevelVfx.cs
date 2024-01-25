using System;
using ParticleEnum;
using UnityEngine;

public class LevelVfx {
	private const string COMBO_MESSAGE = "Комбо\nx";

	private readonly ParticlesPlayer _particlesPlayer;
	private readonly TextDrawer _textDrawer;
	private readonly SoundsPlayer _soundsPlayer;
	private readonly CameraShaker _cameraShaker;

	private readonly IBonusEvents _bonusEvents;
	private ILevelEvents _levelEvents;

	public LevelVfx(ParticlesPlayer particlesPlayer, TextDrawer textDrawer, SoundsPlayer soundsPlayer, CameraShaker cameraShaker, IBonusEvents bonusEvents) {
		_particlesPlayer = particlesPlayer;
		_textDrawer = textDrawer;
		_soundsPlayer = soundsPlayer;
		_cameraShaker = cameraShaker;
		_bonusEvents = bonusEvents;
	}

	public void Initialize() {
		_particlesPlayer.Initialize();
		_textDrawer.Initialize();
		_soundsPlayer.Initialize();
	}

	private void OnPaddleHit(Vector3 hitPoint) {
		_particlesPlayer.PlaySpark(hitPoint);
		_soundsPlayer.PlayPaddleHit();
	}

	private void OnComboEvent(Vector3 position, int comboCount) =>
			_textDrawer.ShowHint(position, $"{COMBO_MESSAGE}{comboCount}");

	private void OnWallActivate() =>
			_cameraShaker.Shake();

	private void OnWallHit(Vector3 position) {
		_particlesPlayer.PlaySpark(position);
		_soundsPlayer.PlayWallHit();
	}

	private void OnBrickDamage(Vector3 hitPoint) {
		_particlesPlayer.PlayDamage(hitPoint);
		_soundsPlayer.PlaySteelHit();
	}

	private void OnBrickHit(Vector3 position) {
		_particlesPlayer.PlaySpark(position);
	}

	private void OnDamagebleDestroy(Damageble sender) {
		PlayExplosion(sender);
		_cameraShaker.Shake();
	}

	private void PlayExplosion(Damageble sender) {
		switch (sender.effect) {
			case Particles.DustDark:
				_particlesPlayer.PlayDustDarkExplosion(sender.position);
				_soundsPlayer.PlayWoodHit();
				break;
			case Particles.DustBright:
				_particlesPlayer.PlayDustBrightExplosion(sender.position);
				break;
			case Particles.DustCircle:
				_particlesPlayer.PlayDustCircleExplosion(sender.position);
				break;
			case Particles.Grenade:
				_particlesPlayer.PlayGrenadeExplosion(sender.position);
				break;
			case Particles.Water:
				_particlesPlayer.PlayWaterExplosion(sender.position);
				break;
			case Particles.Fire:
				_particlesPlayer.PlayFireExplosion(sender.position);
				break;
			case Particles.Skull:
				_particlesPlayer.PlaySkull(sender.position, sender.rotation);
				_soundsPlayer.PlaySkeletonHit();
				break;
			case Particles.None:
				return;

			default:
				throw new Exception($"Particle type {sender.effect.ToString()} not exists ");
		}
	}

	public void Subscribe(ILevelEvents levelEvents) {
		_levelEvents = levelEvents;
		_levelEvents.BrickHitEvent += OnBrickHit;
		_levelEvents.BrickDamagedEvent += OnBrickDamage;
		_levelEvents.DamagebleDestroyedEvent += OnDamagebleDestroy;
		_levelEvents.WallHitEvent += OnWallHit;
		_levelEvents.PaddleHitEvent += OnPaddleHit;

		_bonusEvents.WallActivateEvent += OnWallActivate;
		_bonusEvents.ComboEvent += OnComboEvent;

	}

	public void UnSubscribe() {
		_levelEvents.BrickHitEvent -= OnBrickHit;
		_levelEvents.BrickDamagedEvent -= OnBrickDamage;
		_levelEvents.DamagebleDestroyedEvent -= OnDamagebleDestroy;
		_levelEvents.WallHitEvent -= OnWallHit;
		_levelEvents.PaddleHitEvent -= OnPaddleHit;

		_bonusEvents.ComboEvent -= OnComboEvent;
		_bonusEvents.WallActivateEvent -= OnWallActivate;
		_levelEvents = null;
	}
}


//_comboEvent.ComboEvent += OnComboEvent;

/*foreach (var brick in _level.bricks) {
	brick.DamagedEvent += OnBrickDamage;
	brick.NoLivesLeft += OnBrickDestroy;
}*/

//	foreach (var wall in _level.walls)
//		wall.WallHitEvent += OnWallHit;	

//_level.player.HitOnPaddleEvent += OnPaddleHit;