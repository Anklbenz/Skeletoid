using System;
using UnityEngine;

public class Combo {
	public event Action<Vector3, int> ComboEvent;

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
	private void OnBrickDestroy(Brick brick) {
		var comboCount = _comboCounter.Increase();
		ComboNotify(brick.transform.position, comboCount);
	}
	private void ComboNotify(Vector3 position, int comboCount) =>
			ComboEvent?.Invoke(position, comboCount);

	private void Subscribe() {
		foreach (var brick in _level.bricks) {
			brick.HitEvent += OnBrickHit;
			brick.NoLivesLeft += OnBrickDestroy;
		}
		_level.player.HitOnPaddleEvent += OnPaddleHit;

		foreach (var wall in _level.walls)
			wall.WallHitEvent += OnWallHit;

		_level.stoneBackWall.wall.WallHitEvent += OnWallHit;
	}
	private void UnSubscribe() {
		foreach (var brick in _level.bricks) {
			brick.HitEvent -= OnBrickHit;
			brick.NoLivesLeft -= OnBrickDestroy;
		}
		_level.player.HitOnPaddleEvent -= OnPaddleHit;

		foreach (var wall in _level.walls)
			wall.WallHitEvent -= OnWallHit;
		_level.stoneBackWall.wall.WallHitEvent -= OnWallHit;
	}
}