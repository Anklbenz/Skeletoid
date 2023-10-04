using UnityEngine;
using Zenject;

public sealed class Player : MonoBehaviour, IPauseSensitive
{
	[SerializeField] private Transform ballParent;
	[SerializeField] private Rigidbody paddleRigidbody;
	[SerializeField] private PaddleAnimationPlayer paddleAnimationPlayer;
	public Transform ballTransform => ballParent;
	public float maxSpeed { get; set; }
	public float accelerationStep { get; set; }

	private IInput _input;
	private bool _isActive = true;
	private float _currentSpeed;
	private Vector3 _moveVector, _velocityVector;

	[Inject]
	public void Constructor(IInput input) {
		_input = input;
		_input.HorizontalAxisChangedEvent += SetHorizontalAxisValue;
	}

	private void SetHorizontalAxisValue(float xValue) =>
		_moveVector = new Vector3(xValue, 0, 0);

	public void SetPause(bool isPaused) =>
		_isActive = isPaused;

	public void FixedUpdate() {
		if (!_isActive) return;

		InertialMove();
		AnimateCharacter();
	}

	private void InertialMove() {
		// if move vector changed, but !=0 remember _velocityVector
		if (_velocityVector != _moveVector && _moveVector != Vector3.zero)
			_velocityVector = _moveVector;

		//if _moveVector == 0 braking slowly until speed >0 , else accelerate slowly until _speed< maxPaddleSpeed => change slowly _speed variable
		if (_moveVector.x == 0)
			_currentSpeed = _currentSpeed > 0 ? _currentSpeed - accelerationStep : 0;
		else
			_currentSpeed = _currentSpeed < maxSpeed ? _currentSpeed + accelerationStep : maxSpeed;

		//_velocityVector always != 0, but _speed can be == 0
		paddleRigidbody.velocity = _velocityVector * _currentSpeed;
	}

	private void AnimateCharacter() =>
		paddleAnimationPlayer.MoveDirection(paddleRigidbody.velocity.x);

	public void OnDestroy() =>
		_input.HorizontalAxisChangedEvent -= SetHorizontalAxisValue;
}