public class BallLaunchSystem {
	private const int THROW_POWER_IMPULSE = 4;
	private Ball _ball;
	private ILaunch _launchSite;

	public void Initialize(Ball ball, ILaunch specialReflect) {
		_ball = ball;
		_launchSite = specialReflect;
	}

	public void Throw() {
		_ball.FallOnFloorEvent += OnFallOnFloor;

		var throwDirection = _launchSite.GetDirectionDependsOnLocalPaddleHitPoint(_ball.transform.position);
		_launchSite.isBallHolderActive = false;
		_ball.direction = throwDirection;
		_ball.Throw(throwDirection * THROW_POWER_IMPULSE);
	}

	private void OnFallOnFloor() {
		_ball.FallOnFloorEvent -= OnFallOnFloor;
		_ball.isActive = true;
	}

	public void Reload() {
		_launchSite.isBallHolderActive = true;
	}
}