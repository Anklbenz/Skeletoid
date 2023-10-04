using UnityEngine;
using Zenject;

public sealed class Player : MonoBehaviour, IPauseSensitive
{
	[SerializeField] private Transform ballParent;
	[SerializeField] private Rigidbody paddleRigidbody;
	[SerializeField] private PaddleAnimationPlayer paddleAnimationPlayer;
	public Transform ballTransform => ballParent;
	public float speed { get; set; }
	private IInput _input;
	private Vector3 _moveVector;
	private bool _isActive = true;
	private float _speed;
	private float step = 0.5f;
	private Vector3 vel = new Vector3();
	private float inp;

	[Inject]
	public void Constructor(IInput input) {
		_input = input;
		_input.HorizontalAxisChangedEvent += SetHorizontalAxisValue;
	}

	private void SetHorizontalAxisValue(float xValue) {
		_moveVector = new Vector3(xValue, 0, 0);
		inp = xValue;
	}

	public void SetPause(bool isPaused) =>
		_isActive = isPaused;

	public void FixedUpdate() {
		if (!_isActive) return;

		if (inp > 0)
			vel.x = Mathf.Abs(vel.x) < speed ? vel.x + step : vel.x;
		else if (inp < 0)
			vel.x = Mathf.Abs(vel.x) < speed ? vel.x - step : vel.x;
		else if (inp == 0) {
			if (vel.x < 0)
				vel.x += step;
			else if (vel.x > 0)
				vel.x -= step;
			else if (vel.x-step<0.2f) {
				vel.x = 0;
			}
		}

		var velocity = vel;
		paddleRigidbody.velocity = velocity;
		paddleAnimationPlayer.MoveDirection(velocity.x);
	}

	public void OnDestroy() =>
		_input.HorizontalAxisChangedEvent -= SetHorizontalAxisValue;
}