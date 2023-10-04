using System;
using UnityEngine;

public sealed class GameplaySystem : IPauseSensitive
{
	public event Action<Vector3> BallCollisionEvent;
	public event Action<Brick> BrickDestroyedEvent;
	public event Action AllBricksDestroyedEvent, DeadZoneReachedEvent;

	public bool isPlaying { get; private set; }
	private readonly IInput _input;
	private Level _level;

	public GameplaySystem(IInput input) {
		_input = input;
	}

	public void SetNewLevel(Level level) {
		if (_level != null)
			RemovePreviousLevel();

		_level = level;
		Restart();
		SubscribeLevelEvents();
		SubscribeInput();
	}

	private void RemovePreviousLevel() {
		UnSubscribeLevelEvents();
		UnityEngine.Object.Destroy(_level.gameObject);
	}

	public void Throw() {
		if (isPlaying) return;
		isPlaying = true;

	//	_level.ball.isActive = true;
	var d = _level.player.gameObject.GetComponent<Paddle>();
	var dir = d.GetDirectionDependsOnLocalPaddleHitPoint(_level.ball.transform.position);
		_level.ball.Throw(dir);
	}

	public void Restart() {
		isPlaying = false;
		_level.ball.isActive = false;
		_level.SetBallToDefaultPosition();
	}

	public void SetPause(bool isPaused) {
		_level.player.SetPause(isPaused);
		_level.ball.SetPause(isPaused);
	}

	private void OnDeadZoneReached() {
		DeadZoneReachedEvent?.Invoke();
		isPlaying = false;
	}

	private void OnBallHit(Vector3 hitPosition) =>
		BallCollisionEvent?.Invoke(hitPosition);

	private void OnBrickNoLivesLeft(Brick sender) {
		BrickDestroyedEvent?.Invoke(sender);
		DestroyAndUnsubscribeBrick(sender);

		if (_level.brickCount <= 0)
			AllBricksDestroyedEvent?.Invoke();
	}

	private void DestroyAndUnsubscribeBrick(Brick brick) {
		brick.HitEvent -= OnBallHit;
		brick.NoLivesLeft -= OnBrickNoLivesLeft;
		_level.Destroy(brick);
	}

	private void SubscribeInput() =>
		_input.ShotEvent += Throw;

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