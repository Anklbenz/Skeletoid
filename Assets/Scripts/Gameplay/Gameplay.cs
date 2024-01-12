using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public sealed class Gameplay : IPauseSensitive, IDisposable {
	private const float BALL_OFFSET_LIMIT_AXIS_X = 0.1f;

	public event Action<Brick> BeforeBrickDestroyedEvent;
	public event Action AllBricksDestroyedEvent, LoseEvent;
	public GameplayState state { get; private set; }
	private Ball ball => _level.ball;
	private Player player => _level.player;
	private List<Brick> bricksList => _level.bricks;
	private List<Brick> junkList => _level.junk;
	private List<Enemy> enemiesList => _level.enemies;

	private readonly BonusSystem _bonusSystem;
	private readonly BallLaunch _ballLaunch;
	private readonly BallSpeedIncrease _ballSpeedIncrease;
	private readonly BotNavigationSystem _botNavigationSystem;
	private readonly ILevelEvents _levelEvents;
	private readonly IInput _input;
	private Level _level;

	public Gameplay(
			BallLaunch ballLaunch,
			BonusSystem bonusSystem,
			BallSpeedIncrease ballSpeedIncrease,
			BotNavigationSystem botNavigationSystem,
			ILevelEvents levelEvents,
			IInput input) {
	
		_ballLaunch = ballLaunch;
		_bonusSystem = bonusSystem;
		_ballSpeedIncrease = ballSpeedIncrease;
		_botNavigationSystem = botNavigationSystem;
		_levelEvents = levelEvents;
		_input = input;
		SubscribeEvents();
	}

	public void SetNewLevel(Level level) {
		_level = level;
		Restart();
	}
	private void Throw() {
		if (state == GameplayState.InPlay) return;
		state = GameplayState.InPlay;

		_ballLaunch.Throw();
	}

	public void Restart() {
		state = GameplayState.ReadyToPlay;
        
		_ballLaunch.Reload();
		ball.isActive = false;
		//important set paddle position previsosley than ball position
		SetPaddleToDefaultPosition();
		SetBallToDefaultPosition();
	    SetBallDefaultSpeed();
		player.aimTarget = ball.transform;
	}

	public void SetPause(bool isPaused) {
		player.SetPause(isPaused);
		ball.SetPause(isPaused);
		_bonusSystem.SetPause(isPaused);
		_botNavigationSystem.SetPause(isPaused);
	}

	private void OnDeadZoneReached() {
		state = GameplayState.Lose;
		//If ball count ==1, for may ball in game
		LoseEvent?.Invoke();
	}

	private void OnDestroyed(Brick sender) {
		BeforeBrickDestroyedEvent?.Invoke(sender);

		if (sender.required) {
			DestroyAndUnsubscribeBrick(sender);
			NotifyIfAllBricksDestroyed();
		}
		else {
			DestroyAndUnsubscribeJunk(sender);
		}
	}
	private void NotifyIfAllBricksDestroyed() {
		if (_level.brickCount <= 0)
			AllBricksDestroyedEvent?.Invoke();
	}

	private void DestroyAndUnsubscribeJunk(Brick junk) {
		junkList.Remove(junk);
		UnityEngine.Object.Destroy(junk.gameObject);
	}

	private void DestroyAndUnsubscribeBrick(Brick brick) {
		bricksList.Remove(brick);
		UnityEngine.Object.Destroy(brick.gameObject);
	}
	private void SetPaddleToDefaultPosition() =>
			player.transform.position = _level.paddleOrigin.position;
	private void SetBallToDefaultPosition() {
		var ballPosition = player.ballTransform.position;
		ballPosition.x = Random.Range(ballPosition.x - BALL_OFFSET_LIMIT_AXIS_X, ballPosition.x + BALL_OFFSET_LIMIT_AXIS_X);

		ball.transform.position = ballPosition;
	}

	private void SetBallDefaultSpeed() =>
			_ballSpeedIncrease.SetDefault();
	private void IncreaseBallSpeed() =>
			_ballSpeedIncrease.Increase();

	private void SubscribeEvents() {
		_input.ShotEvent += Throw;
		_levelEvents.DeadZoneReachedEvent += OnDeadZoneReached;
		_levelEvents.BrickDestroyedEvent += OnDestroyed;
		_levelEvents.AnyHitEvent += IncreaseBallSpeed;

	}
	private void UnSubscribeInput() {
		_input.ShotEvent -= Throw;
		_levelEvents.DeadZoneReachedEvent -= OnDeadZoneReached;
		_levelEvents.BrickDestroyedEvent -= OnDestroyed;
		_levelEvents.AnyHitEvent -= IncreaseBallSpeed;
	}

	public void Dispose() =>
			UnSubscribeInput();
}