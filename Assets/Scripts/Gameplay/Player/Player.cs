using System;
using UnityEngine;
using Zenject;
public sealed class Player : Obstacle, IPauseSensitive, ILaunch {
	private const float MAX_REFLECT_ANGLE = 70;
	public event Action<Vector3> HitOnPaddleEvent;

	[SerializeField] private BoxCollider boxCollider;
	[SerializeField] private Rigidbody paddleRigidbody;
	[SerializeField] private Transform ballParent;
	[SerializeField] private GameObject ballHolder;
	[SerializeField] private Transform skeletonHead;
	[SerializeField] private AnimationPlayer animationPlayer;
	public Transform skeletonHeadTransform => skeletonHead;
	public Transform aimTarget { set => animationPlayer.SetAimTarget(value); }
	public bool isBallHolderActive {
		get => ballHolder.activeInHierarchy;
		set => ballHolder.SetActive(value);
	}
	public Transform ballTransform => ballParent;
	public float maxSpeed { get; set; }
	public float accelerationStep { get; set; }

	private IInput _input;
	private bool _isActive = true;
	private float _currentSpeed;
	private Vector3 _moveVector, _velocityVector, _rigidbodyVelocityBeforePaused;

	[Inject]
	public void Constructor(IInput input) {
		_input = input;
		_input.HorizontalAxisChangedEvent += OnInput;
	}

	protected override void Reflect(IBall ball, Collision collision) {
		var collisionPoint = collision.contacts[0].point;
		var collisionNormal = collision.contacts[0].normal;
		
		if (CouldSpecialReflectionBePerformed(collisionPoint, collisionNormal))
			ball.direction = GetDirectionDependsOnLocalPaddleHitPoint(collisionPoint);
		else
			ball.Reflect(-collisionNormal);

		HitOnPaddleEvent?.Invoke(collisionPoint);
	}

	private void OnInput(float xValue) =>
			_moveVector = new Vector3(xValue, 0, 0);

	public void SetPause(bool isPaused) {
		_isActive = !isPaused;
		paddleRigidbody.isKinematic = isPaused;

		if (isPaused)
			AnimateCharacter(0);
	}

	public void FixedUpdate() {
		if (!_isActive) return;

		InertialMove();
		AnimateCharacter(paddleRigidbody.velocity.x);
	}

	// Hit in opposite direction
	public bool CouldSpecialReflectionBePerformed(Vector3 hitPoint, Vector3 hitNormal) =>
			Vector3.Dot(transform.forward, hitNormal) < 0;


	public Vector3 GetDirectionDependsOnLocalPaddleHitPoint(Vector3 collisionPoint) {
		var colliderMinX = boxCollider.center.x - boxCollider.size.x / 2;
		var colliderMaxX = boxCollider.center.x + boxCollider.size.x / 2;

		var hitPointLocal = transform.InverseTransformPoint(collisionPoint);
		var lerpX = Mathf.InverseLerp(colliderMinX, colliderMaxX, hitPointLocal.x);
		var angle = Mathf.Lerp(-MAX_REFLECT_ANGLE, MAX_REFLECT_ANGLE, lerpX);

		return (Quaternion.AngleAxis(angle, Vector3.up) * transform.forward);
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

		//_velocityVector always != zero, but _speed can be == 0
		paddleRigidbody.velocity = _velocityVector * _currentSpeed;
	}

	private void AnimateCharacter(float rigidbodyVelocityX) =>
			animationPlayer.MoveDirection(rigidbodyVelocityX);

	public void OnDestroy() =>
			_input.HorizontalAxisChangedEvent -= OnInput;
}