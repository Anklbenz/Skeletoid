using System;
using UnityEngine;

public class Combo {
	public event Action<int> ComboEvent;

	private readonly ComboCounter _comboCounter;
	private Level _level;

	public Combo() {
		_comboCounter = new ComboCounter();
	}

	public void Start(Level level) {
		_level = level;
		Subscribe();
	}

	public void Stop() {
		UnSubscribe();
		_level = null;
	}

	private void OnWallHit() =>
		_comboCounter.Reset();
	
	private void OnBrickHit(Vector3 obj) {}
	private void OnPaddleHit(Vector3 obj) =>
		_comboCounter.Reset();
	private void OnBrickDestroy(Brick obj) {
		var comboCount = _comboCounter.Increase();
		ComboNotify(comboCount);
	}
	private void ComboNotify(int comboCount) =>
		ComboEvent?.Invoke(comboCount);

	private void Subscribe() {
		foreach (var brick in _level.bricks) {
			brick.HitEvent += OnBrickHit;
			brick.NoLivesLeft += OnBrickDestroy;
		}
		_level.player.HitOnPaddleEvent += OnPaddleHit;

		_level.leftWall.WallHitEvent += OnWallHit;
		_level.rightWall.WallHitEvent += OnWallHit;
		_level.frontWall.WallHitEvent += OnWallHit;
		_level.backWall.WallHitEvent += OnWallHit;
	}
	private void UnSubscribe() {
		foreach (var brick in _level.bricks) {
			brick.HitEvent -= OnBrickHit;
			brick.NoLivesLeft -= OnBrickDestroy;
		}
		_level.player.HitOnPaddleEvent -= OnPaddleHit;

		_level.leftWall.WallHitEvent -= OnWallHit;
		_level.rightWall.WallHitEvent -= OnWallHit;
		_level.frontWall.WallHitEvent -= OnWallHit;
		_level.backWall.WallHitEvent -= OnWallHit;

	}
}