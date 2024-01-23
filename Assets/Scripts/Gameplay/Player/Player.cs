using System;
using UnityEngine;
using Zenject;
public sealed class Player : Obstacle, IPauseSensitive, IPaddle {
	private const float MAX_REFLECT_ANGLE = 55;

	[SerializeField] private BoxCollider boxCollider;
	[SerializeField] private Transform ballParent;
	[SerializeField] private GameObject ballHolder;
	[SerializeField] private Transform skeletonHead;
	[SerializeField] private AnimationPlayer animationPlayer;
	[SerializeField] private InertialMotor inertialMotor;
	
	public event Action<Vector3> HitOnPaddleEvent;
	public InertialMotor motor => inertialMotor;
	public Transform skeletonHeadTransform => skeletonHead;
	public Transform aimTarget { set => animationPlayer.SetAimTarget(value); }
	public bool isBallHolderActive {
		get => ballHolder.activeInHierarchy;
		set => ballHolder.SetActive(value);
	}
	public Transform ballTransform => ballParent;
	private bool _isActive = true;

	[Inject]
	public void Constructor(IInput input) =>
			inertialMotor.Initialize(input);

	protected override void Reflect(IBall ball, Collision collision) {
		var collisionPoint = collision.contacts[0].point;
		var collisionNormal = collision.contacts[0].normal;

		if (CouldSpecialReflectionBePerformed(collisionNormal))
			ball.direction = GetDirectionDependsOnLocalPaddleHitPoint(collisionPoint);
		else
			ball.Reflect(-collisionNormal);

		HitOnPaddleEvent?.Invoke(collisionPoint);
	}

	public void SetPause(bool isPaused) {
		_isActive = !isPaused;
		inertialMotor.SetPause(isPaused);

		if (isPaused)
			AnimateCharacter(0);
	}

	public void FixedUpdate() {
		if (!_isActive) return;

		inertialMotor.FixedTick();
		AnimateCharacter(inertialMotor.velocityX);
	}

	// Hit in opposite direction
	private bool CouldSpecialReflectionBePerformed(Vector3 hitNormal) =>
			Vector3.Dot(transform.forward, hitNormal) < 0;

	public Vector3 GetDirectionDependsOnLocalPaddleHitPoint(Vector3 collisionPoint) {
		var colliderMinX = boxCollider.center.x - boxCollider.size.x / 2;
		var colliderMaxX = boxCollider.center.x + boxCollider.size.x / 2;

		var hitPointLocal = transform.InverseTransformPoint(collisionPoint);
		var lerpX = Mathf.InverseLerp(colliderMinX, colliderMaxX, hitPointLocal.x);
		var angle = Mathf.Lerp(-MAX_REFLECT_ANGLE, MAX_REFLECT_ANGLE, lerpX);

		return (Quaternion.AngleAxis(angle, Vector3.up) * transform.forward);
	}

	private void AnimateCharacter(float rigidbodyVelocityX) =>
			animationPlayer.MoveDirection(rigidbodyVelocityX);
}