using UnityEngine;
using Zenject;
public sealed class Player : MonoBehaviour, IPauseSensitive, ISpecialReflect, ILaunch {
	private const float MAX_REFLECT_ANGLE = 70;
	[SerializeField] private BoxCollider boxCollider;
	[SerializeField] private Transform ballParent;
	[SerializeField] private Rigidbody paddleRigidbody;
	[SerializeField] private AnimationPlayer animationPlayer;
	[SerializeField] private GameObject ballHolder;

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
	private Vector3 _moveVector, _velocityVector;

	[Inject]
	public void Constructor(IInput input) {
		_input = input;
		_input.HorizontalAxisChangedEvent += OnInput;
	}

	private void OnInput(float xValue) =>
			_moveVector = new Vector3(xValue, 0, 0);

	public void SetPause(bool isPaused) =>
			_isActive = !isPaused;

	public void FixedUpdate() {
		if (!_isActive) return;

		InertialMove();
		AnimateCharacter();
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

		//_velocityVector always != 0, but _speed can be == 0
		paddleRigidbody.velocity = _velocityVector * _currentSpeed;
	}

	private void AnimateCharacter() =>
			animationPlayer.MoveDirection(paddleRigidbody.velocity.x);

	public void OnDestroy() =>
			_input.HorizontalAxisChangedEvent -= OnInput;
}