using System;
using UnityEngine;

public class GameplaySystem {
	public event Action<Vector3> BallCollisionEvent;
	public event Action<Brick> BrickDestroyedEvent;
	public event Action AllBricksDestroyedEvent, DeadZoneReachedEvent;

	private readonly IInput _input;
	private Level _level;

	public GameplaySystem(IInput input) {
		_input = input;
	}

	public void InitializeNewLevel(Level level) {
		if (_level != null)
			RemovePreviousLevel();
	
		_level = level;
		GetReady();
		SubscribeLevelEvents();
		SubscribeInput();
	}

	private void RemovePreviousLevel() {
		UnSubscribeLevelEvents();
		UnityEngine.Object.Destroy(_level.gameObject);
	}
	
	public void Start() {
		_level.ball.isActive = true;
		_level.ball.direction = new Vector3(0, 0, 1f);
	}

	public void Stop() {
		_level.ball.isActive = false;
	}

	public void GetReady() {
		Stop();
		_level.SetBallToDefaultPosition();
	}

	private void OnDeadZoneReached() =>
			DeadZoneReachedEvent?.Invoke();

	private void OnBallHit(Vector3 hitPosition) =>
			BallCollisionEvent?.Invoke(hitPosition);

	private void OnBrickNoLivesLeft(Brick sender) {
		BrickDestroyedEvent?.Invoke(sender);
		DestroyAndUnsubscribeBrick(sender);

		if (_level.bricks.Count <= 0)
			AllBricksDestroyedEvent?.Invoke();
	}

	private void DestroyAndUnsubscribeBrick(Brick brick) {
		brick.HitEvent -= OnBallHit;
		brick.NoLivesLeft -= OnBrickNoLivesLeft;
		_level.Destroy(brick);
	}

	private void SubscribeInput() =>
			_input.ShotEvent += Start;

	private void SubscribeLevelEvents() {
		foreach (var brick in _level.bricks)
			brick.NoLivesLeft += OnBrickNoLivesLeft;

		_level.deadZone.DeadZoneReachedEvent += OnDeadZoneReached;
		_level.ball.OnCollisionEvent += OnBallHit;
	}
	
	private void UnSubscribeLevelEvents() {
		foreach (var brick in _level.bricks)
			brick.NoLivesLeft -= OnBrickNoLivesLeft;

		_level.deadZone.DeadZoneReachedEvent -= OnDeadZoneReached;
		_level.ball.OnCollisionEvent -= OnBallHit;
	}
}