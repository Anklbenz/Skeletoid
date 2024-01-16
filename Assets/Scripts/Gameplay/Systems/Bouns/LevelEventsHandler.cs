using System;
using UnityEngine;

public class LevelEventsHandler : ILevelEvents {
	public event Action<Vector3, int> ComboEvent;
	public event Action<Vector3> WallHitEvent, PaddleHitEvent, BrickHitEvent, BrickDamagedEvent;
	public event Action<Brick> BrickDestroyedEvent;
	public event Action<Enemy> EnemyDestroyedEvent;
	public event Action<Damageble> DamagebleDestroyedEvent;
	public event Action DeadZoneReachedEvent, AnyHitEvent;

	//remove from here
	private readonly ComboCounter _comboCounter;
	//---
	private Level _level;

	public LevelEventsHandler() {
		_comboCounter = new ComboCounter();
	}
	public void Set(Level level) {
		_level = level;
		Subscribe();
	}

	public void Clear() {
		UnSubscribe();
		_level = null;
	}

	private void OnWallHit(Vector3 position) {
		_comboCounter.Reset();
		WallHitEvent?.Invoke(position);
		AnyHitEvent?.Invoke();
	}
	private void ObstacleHitNotify(Vector3 position) {
		BrickHitEvent?.Invoke(position);
		AnyHitEvent?.Invoke();
	}
	private void BrickDamagedNotify(Vector3 position) =>
			BrickDamagedEvent?.Invoke(position);

	private void OnBrickDestroy(Brick brick) {
		BrickDestroyedEvent?.Invoke(brick);
		DamagebleDestroyedNotify(brick);	
		var comboCount = _comboCounter.Increase();
		ComboNotify(brick.transform.position, comboCount);
	}

	private void OnEnemyDestroyed(Enemy enemy) {
		EnemyDestroyedEvent?.Invoke(enemy);
		DamagebleDestroyedNotify(enemy);
	}

	private void DamagebleDestroyedNotify(Damageble damageble) =>
			DamagebleDestroyedEvent?.Invoke(damageble);

	private void OnPaddleHit(Vector3 hitPosition) {
		_comboCounter.Reset();
		PaddleHitEvent?.Invoke(hitPosition);
		AnyHitEvent?.Invoke();
	}
	private void DeadZoneReachedNotify() =>
			DeadZoneReachedEvent?.Invoke();
	private void ComboNotify(Vector3 position, int comboCount) =>
			ComboEvent?.Invoke(position, comboCount);
	private void Subscribe() {
		foreach (var brick in _level.bricks) {
			brick.HitEvent += ObstacleHitNotify;
			brick.DamagedEvent += BrickDamagedNotify;
			brick.NoLivesLeft += OnBrickDestroy;
		}

		foreach (var junk in _level.junk) {
			junk.HitEvent += ObstacleHitNotify;
			junk.DamagedEvent += BrickDamagedNotify;
			junk.NoLivesLeft += OnBrickDestroy;
		}

		foreach (var enemy in _level.enemies) {
			enemy.HitEvent += ObstacleHitNotify;
			enemy.NoLivesLeft += OnEnemyDestroyed;
		}

		foreach (var wall in _level.walls)
			wall.WallHitEvent += OnWallHit;

		_level.player.HitOnPaddleEvent += OnPaddleHit;
		_level.deadZone.DeadZoneReachedEvent += DeadZoneReachedNotify;
	}
	private void UnSubscribe() {
		foreach (var brick in _level.bricks) {
			brick.HitEvent -= ObstacleHitNotify;
			brick.DamagedEvent -= BrickDamagedNotify;
			brick.NoLivesLeft -= OnBrickDestroy;
		}
		
		foreach (var junk in _level.junk) {
			junk.HitEvent -= ObstacleHitNotify;
			junk.DamagedEvent -= BrickDamagedNotify;
			junk.NoLivesLeft -= OnBrickDestroy;
		}

		foreach (var wall in _level.walls)
			wall.WallHitEvent -= OnWallHit;

		_level.player.HitOnPaddleEvent -= OnPaddleHit;
		_level.deadZone.DeadZoneReachedEvent -= DeadZoneReachedNotify;
	}
}
