public class BallLaunch {
	private const int THROW_POWER_IMPULSE = 4;
	private Ball _ball;
	private IPaddle _paddleSite;

	public void Initialize(Ball ball, IPaddle specialReflect) {
		_ball = ball;
		_paddleSite = specialReflect;
	}

	public void Throw() {
		_ball.FallOnFloorEvent += OnFallOnFloor;

		var throwDirection = _paddleSite.GetDirectionDependsOnLocalPaddleHitPoint(_ball.transform.position);
		_paddleSite.isBallHolderActive = false;
		_ball.direction = throwDirection;
		_ball.Throw(throwDirection * THROW_POWER_IMPULSE);
	}

	private void OnFallOnFloor() {
		_ball.FallOnFloorEvent -= OnFallOnFloor;
		_ball.isActive = true;
	}

	public void Reload() {
		_paddleSite.isBallHolderActive = true;
	}
}