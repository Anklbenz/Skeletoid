using UnityEngine;

public class BallSystem {
	private readonly CurrentGameplayData _gameplayData;
	private Ball ball => _gameplayData.ball;
	private Transform ballOrigin => _gameplayData.ballOrigin;

	public BallSystem(CurrentGameplayData gameplayData, IInput input) {
		_gameplayData = gameplayData;
		input.Shot += Throw;
	}
	private void Throw() {
		ball.SetDirection(new Vector3(0, 0, 1f));
		ball.isActive = true;
	}

	public void SetBallOnReadyPosition() {
		ball.isActive = false;
		ball.transform.position = ballOrigin.position;
	}
}