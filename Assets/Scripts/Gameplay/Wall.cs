using System;
using UnityEngine;

public class Wall : Obstacle {
	public event Action WallHitEvent;
	private IBall _ball;
	protected override void Reflect(IBall ball, Collision collision) {
		_ball = ball;
		ball.Reflect(-collision.contacts[0].normal);
	}
	protected override void OnCollisionEnter(Collision collision) {
		base.OnCollisionEnter(collision);
		WallHitEvent?.Invoke();
	}

	private void OnCollisionExit(Collision other) =>
			_ball = null;

	private void OnCollisionStay(Collision collision) =>
			_ball?.Reflect(-collision.contacts[0].normal);
}