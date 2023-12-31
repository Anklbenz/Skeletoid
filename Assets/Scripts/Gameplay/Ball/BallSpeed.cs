public class BallSpeed {
	private float _startSpeed;
	private float _boostMultiplier;
	private Ball _ball;

	public BallSpeed(GameplayConfig config) {
		_startSpeed = config.ballSpeed;
		_boostMultiplier = config.ballBoostMultiplier;
	}
	public void Initialize(Ball ball) {
		_ball = ball;
		SetDefault();
	}
	public void Increase() {
		//Because speed increase when hit on paddle sides before run
		if (!_ball.isActive) return;
		_ball.speed *= _boostMultiplier;
	}

	public void SetDefault() =>
			_ball.speed = _startSpeed;
}