using System;
using UnityEngine;

public class GameplaySystem {
	private const string NAME = "GameplayLevel";

	public event Action<Vector3> BallCollisionEvent, BrickDestroyedEvent;
	public event Action AllBricksDestroyedEvent, DeadZoneReachedEvent;
	private Vector3 ballDefaultPosition => _paddle.ballTransform.position;

	private readonly IInput _input;
	private readonly IFactory _factory;
	private readonly GameplayConfig _gameplayConfig;

	private Ball _ball;
	private Level _level;
	private Paddle _paddle;
	private GameObject _gameplayLevel;
	private Environment _environment;

	public GameplaySystem(GameplayConfig gameplayConfig, IFactory factory, IInput input) {
		_gameplayConfig = gameplayConfig;
		_factory = factory;
		_input = input;
	}

	public void Initialize() {
		var mapPrefab = _gameplayConfig.GetMapPrefab(0);
		_gameplayLevel = new GameObject(NAME);

		_environment = _factory.Get<Environment>(mapPrefab.environment,_gameplayLevel.transform);
		_level = _factory.Get<Level>(mapPrefab.level,_gameplayLevel.transform);
		_paddle = _factory.Get<Paddle>(_gameplayConfig.paddlePrefab,_gameplayLevel.transform, _level.paddleOrigin.position );
		_ball = _factory.Get<Ball>(_gameplayConfig.ballPrefab);
		
		GetReady();
		SubscribeGameplayEvents();
		SubscribeInput();
	}
	private void SubscribeInput() {
		_input.Shot += Start;
	}

	public void Start() {
		_ball.isActive = true;
		_ball.SetDirection(new Vector3(0, 0, 1f));
	}

	public void GetReady() {
		_ball.isActive = false;
		_ball.transform.position = ballDefaultPosition;
	}

	private void OnDeadZoneReached() =>
		DeadZoneReachedEvent?.Invoke();

	private void OnBallHit(Vector3 hitPosition) =>
		BallCollisionEvent?.Invoke(hitPosition);

	private void OnBrickHitPointOut(Brick sender) {
		BrickDestroyedEvent?.Invoke(sender.transform.position);
		DestroyAndUnsubscribeBrick(sender);

		if (_level.bricks.Count <= 0)
			AllBricksDestroyedEvent?.Invoke();
	}

	private void DestroyAndUnsubscribeBrick(Brick brick) {
		brick.HitEvent -= OnBallHit;
		brick.HitPointsOutEvent -= OnBrickHitPointOut;
		UnityEngine.Object.Destroy(brick.gameObject);
	}

	private void SubscribeGameplayEvents() {
		foreach (var brick in _level.bricks)
			brick.HitPointsOutEvent += OnBrickHitPointOut;

		_level.deadZone.DeadZoneReachedEvent += OnDeadZoneReached;
		_ball.OnCollisionEvent += OnBallHit;
	}
}