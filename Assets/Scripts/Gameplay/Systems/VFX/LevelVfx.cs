using System;
using ParticleEnum;
using UnityEngine;

public class LevelVfx
{
	private const string COMBO_MESSAGE = "Комбо\nx";

	private readonly ParticlesPlayer _particlesPlayer;
	private readonly TextDrawer _textDrawer;
	private readonly CameraShaker _cameraShaker;
	private readonly IComboEvent _comboEvent;
	private readonly IStoneWallEvent _stoneWallEvent;

	private Level _level;

	public LevelVfx(ParticlesPlayer particlesPlayer, TextDrawer textDrawer, CameraShaker cameraShaker, IComboEvent comboEvent, IStoneWallEvent stoneWallEvent) {
		_particlesPlayer = particlesPlayer;
		_textDrawer = textDrawer;
		_cameraShaker = cameraShaker;
		_comboEvent = comboEvent;
		_stoneWallEvent = stoneWallEvent;
	}

	public void Initialize() {
		_particlesPlayer.Initialize();
		_textDrawer.Initialize();
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

	private void OnBrickDamage(Vector3 hitPoint) {
		_particlesPlayer.PlayDamage(hitPoint);
	//	_particlesPlayer.PlayDamage(hitPoint);
	//	_particlesPlayer.PlaySpark(hitPoint);
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

	private void OnComboEvent(Vector3 position, int comboCount) =>
		_textDrawer.ShowHint(position, $"{COMBO_MESSAGE}{comboCount}");

	private void OnWallActivate() =>
			_cameraShaker.Shake();
	private void OnWallHit(Vector3 position) {
		_particlesPlayer.PlaySpark(position);
	}
	private void Subscribe() {
		_comboEvent.ComboEvent += OnComboEvent;
		_stoneWallEvent.WallActivateEvent += OnWallActivate;

		foreach (var brick in _level.bricks) {
			brick.DamagedEvent += OnBrickDamage;
			brick.NoLivesLeft += OnBrickDestroy;
		}

		foreach (var wall in _level.walls)
			wall.WallHitEvent += OnWallHit;	
		
		_level.player.HitOnPaddleEvent += OnPaddleHit;
	}


	private void UnSubscribe() {
		_comboEvent.ComboEvent -= OnComboEvent;
		_stoneWallEvent.WallActivateEvent -= OnWallActivate;

		foreach (var brick in _level.bricks) {
			brick.DamagedEvent -= OnBrickDamage;
			brick.NoLivesLeft -= OnBrickDestroy;
		}

		foreach (var wall in _level.walls)
			wall.WallHitEvent -= OnWallHit;	

		_level.player.HitOnPaddleEvent -= OnPaddleHit;
	}
}