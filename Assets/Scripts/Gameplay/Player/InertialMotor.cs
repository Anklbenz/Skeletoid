using System;
using UnityEngine;

public class InertialMotor : MonoBehaviour, IPauseSensitive {
	[SerializeField] private Rigidbody paddleRigidbody;
	public float maxSpeed { get; set; }
	public float accelerationStep { get; set; }

	public float velocityX => paddleRigidbody.velocity.x;

	private float _currentSpeed;
	private Vector3 _moveVector, _velocityVector, _rigidbodyVelocityBeforePaused;
	private IInput _input;

	public void Initialize(IInput input) {
		_input = input;
		_input.HorizontalAxisChangedEvent += OnInput;
	}

	public void FixedTick() {
		MoveInertial();
	}

	private void OnInput(float xValue) =>
			_moveVector = new Vector3(xValue, 0, 0);

	private void MoveInertial() {
		// if move vector changed, but !=0 remember _velocityVector
		if (_velocityVector != _moveVector && _moveVector != Vector3.zero)
			_velocityVector = _moveVector;

		//if _moveVector == 0 braking slowly until speed >0 , else accelerate slowly until _speed< maxPaddleSpeed => change slowly _speed variable
		if (_moveVector.x == 0)
			_currentSpeed = _currentSpeed > 0 ? _currentSpeed - accelerationStep : 0;
		else
			_currentSpeed = _currentSpeed < maxSpeed ? _currentSpeed + accelerationStep : maxSpeed;

		//_velocityVector always != zero, but _speed can be == 0
		paddleRigidbody.velocity = _velocityVector * _currentSpeed;
	}

	public void OnDestroy() =>
			_input.HorizontalAxisChangedEvent -= OnInput;
	public void SetPause(bool isPaused) {
		paddleRigidbody.isKinematic = isPaused;
	}
}

