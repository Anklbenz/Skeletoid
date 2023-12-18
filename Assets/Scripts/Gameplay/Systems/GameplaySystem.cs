using System;
using UnityEngine;
using Random = UnityEngine.Random;

public sealed class GameplaySystem : IPauseSensitive, IDisposable
{
	private const float X_BALL_POSITION_RANDOMIZE_LIMIT = 0.1f;
	
	public event Action<Brick> BrickDestroyedEvent;
	public event Action AllBricksDestroyedEvent, DeadZoneReachedEvent;
	public GameplayState state { get; private set; }

	public Level currentLevel => _level;
	private Ball ball => _level.ball;
	private Player player => _level.player;
	private DeadZone deadZone => _level.deadZone;
	
	private readonly BallLaunchSystem _ballLaunchSystem;
	private readonly ComboSystem _comboSystem;

	private readonly IInput _input;
	private Level _level;

	public GameplaySystem(BallLaunchSystem ballLaunchSystem /*ComboSystem comboSystem*/,  IInput input) {
		_ballLaunchSystem = ballLaunchSystem;
	//	_comboSystem = comboSystem;
		_input = input;
	}

	public void SetNewLevel(Level level) {
		if (_level != null)
			RemovePreviousLevel();

		_level = level;
		_ballLaunchSystem.Initialize(ball, player);
		Restart();
		SubscribeLevelEvents();
		SubscribeInput();
	}

	private void RemovePreviousLevel() {
		UnSubscribeLevelEvents();
		UnityEngine.Object.Destroy(_level.gameObject);
	}

	private void Throw() {
		if (state == GameplayState.InPlay) return;
		state = GameplayState.InPlay;

		_ballLaunchSystem.Throw();
	}

	public void Restart() {
		state = GameplayState.ReadyToPlay;

		_ballLaunchSystem.Reload();
		ball.isActive = false;
		//important set paddle position previsosley than ball position
		SetPaddleToDefaultPosition();
		SetBallToDefaultPosition();
		//
		player.aimTarget = ball.transform;
	}

	public void SetPause(bool isPaused) {
		player.SetPause(isPaused);
		ball.SetPause(isPaused);
	}

	private void OnDeadZoneReached() {
		DeadZoneReachedEvent?.Invoke();
		state = GameplayState.PlayEnded;
	}

	/*private void OnBallHit(Vector3 hitPosition) =>
		BallCollisionEvent?.Invoke(hitPosition);*/
	
	private void OnBrickHit(Vector3 obj) {
//		throw new NotImplementedException();
	}
	private void OnBrickNoLivesLeft(Brick sender) {
		BrickDestroyedEvent?.Invoke(sender);
		DestroyAndUnsubscribeBrick(sender);

		if (_level.brickCount <= 0)
			AllBricksDestroyedEvent?.Invoke();
	}

	private void DestroyAndUnsubscribeBrick(Brick brick) {
		brick.HitEvent -= OnBrickHit;
		_level.Destroy(brick);
	}

	private void SubscribeInput() =>
		_input.ShotEvent += Throw;

	private void SubscribeLevelEvents() {
		foreach (var brick in _level.bricks) {
			brick.NoLivesLeft += OnBrickNoLivesLeft;
			brick.HitEvent += OnBrickHit;
		}

		deadZone.DeadZoneReachedEvent += OnDeadZoneReached;
	//	ball.OnCollisionEvent += OnBallHit;
	}


	private void UnSubscribeLevelEvents() {
		foreach (var brick in _level.bricks) {
			brick.NoLivesLeft -= OnBrickNoLivesLeft;
			brick.HitEvent -= OnBrickHit;
		}

		deadZone.DeadZoneReachedEvent -= OnDeadZoneReached;
	//	ball.OnCollisionEvent -= OnBallHit;
	}

	private void SetBallToDefaultPosition() {
		var ballPosition = player.ballTransform.position;
		ballPosition.x = Random.Range(ballPosition.x - X_BALL_POSITION_RANDOMIZE_LIMIT, ballPosition.x + X_BALL_POSITION_RANDOMIZE_LIMIT);

		ball.transform.position = ballPosition;
	}

	private void SetPaddleToDefaultPosition() {
		player.transform.position = _level.paddleOrigin.position;
	}

	public void Dispose() {
		UnSubscribeInput();
	}
	private void UnSubscribeInput() =>
		_input.ShotEvent -= Throw;
}