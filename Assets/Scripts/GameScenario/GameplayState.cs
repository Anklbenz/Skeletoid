using UnityEngine;
using System;
public class GameplayState : State {
	private readonly IInput _input;
	private readonly CurrentGameplayData _currentGameplayData;
	private readonly BallSystem _ballSystem;
	private readonly ParticlesFactory _particlesFactory;

	private Ball ball => _currentGameplayData.ball;
	private DeadZone deadZone => _currentGameplayData.deadZone;
	
	public GameplayState(StateSwitcher stateSwitcher, CurrentGameplayData currentGameplayData, BallSystem ballSystem, ParticlesFactory particlesFactory, IInput input) : base(stateSwitcher) {
		_currentGameplayData = currentGameplayData;
		_input = input;
		_ballSystem = ballSystem;
		_particlesFactory = particlesFactory;
	}

	public override void Enter() {
		_input.Enabled = true;
		_ballSystem.SetBallOnReadyPosition();
		ball.OnCollisionEvent += OnBrickHit;

		deadZone.DeadZoneReachedEvent += OnDeadZoneReached;
		
		foreach (var brick in _currentGameplayData.bricks) {
			//brick.HitEvent += OnBrickHit;
			brick.HitPointsOutEvent += OnBrickHitPointOut;
		}
	}

	private void OnDeadZoneReached(Vector3 obj) {
		
		//ExitToLoseState show 
		_ballSystem.SetBallOnReadyPosition();
	}

	private void OnBrickHit(Vector3 obj) {
		_particlesFactory.PlayCollision(obj);
		//throw new System.NotImplementedException();
	}

	private void OnBrickHitPointOut(Brick sender) {
		_particlesFactory.PlayDestroy(sender.transform.position);
		DestroyBrick(sender);

		if (_currentGameplayData.bricks.Count <= 0)
			Debug.Log("BrickIsOver");
	}

	private void DestroyBrick(Brick brick) {
		if (!_currentGameplayData.bricks.Remove(brick))
			throw new Exception($"Brick list do not contains {brick.name}");
		
		brick.HitEvent -= OnBrickHit;
		brick.HitPointsOutEvent -= OnBrickHitPointOut;
		UnityEngine.Object.Destroy(brick.gameObject);
	}
}
