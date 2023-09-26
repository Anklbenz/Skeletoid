using UnityEngine;
using System;
public class GameplayState : State {
	private readonly IInput _input;
	private readonly CurrentGameplayData _currentGameplayData;
	private readonly BallSystem _ballSystem;
	private readonly ParticlesPlayer _particlesPlayer;
	
	public GameplayState(StateSwitcher stateSwitcher, CurrentGameplayData currentGameplayData, BallSystem ballSystem, ParticlesPlayer particlesPlayer, IInput input) : base(stateSwitcher) {
		_currentGameplayData = currentGameplayData;
		_input = input;
		_ballSystem = ballSystem;
	}

	public override void Enter() {
		_input.Enabled = true;
		_ballSystem.SetBallOnReadyPosition();
		
		foreach (var brick in _currentGameplayData.bricks) {
			brick.HitEvent += OnBrickHit;
			brick.HitPointsOutEvent += OnBrickHitPointOut;
		}
	}
	private void OnBrickHit(Vector3 obj) {
		Debug.Log("Hit");
		//throw new System.NotImplementedException();
	}

	private void OnBrickHitPointOut(Brick sender) {
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
