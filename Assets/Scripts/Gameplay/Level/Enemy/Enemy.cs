using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy : Damageble, IPauseSensitive {
	private const float MIN_DISTANCE = 0.2f;
	private const int LOW_PRIORITY_VALUE = 15;
	private const int HIGH_PRIORITY_VALUE = 50;

	[SerializeField] private NavMeshAgent navMeshAgent;
	[SerializeField] private Animator animator;
	[SerializeField] private Vector2 speedInterval;

	public event Action<Enemy> NoLivesLeft;
	public event Action<Vector3> HitEvent, DamagedEvent;
	private SkeletonAnimator _skeletonAnimator;

	public bool isPathComplete => (!navMeshAgent.pathPending && !navMeshAgent.hasPath) || navMeshAgent.remainingDistance < MIN_DISTANCE;

	public Vector3 destination {
		set => navMeshAgent.SetDestination(value);
	}
	private void Awake() {
		_skeletonAnimator = new SkeletonAnimator(transform, animator);

		navMeshAgent.updateRotation = false;
		navMeshAgent.avoidancePriority = Random.Range(LOW_PRIORITY_VALUE, HIGH_PRIORITY_VALUE);
		navMeshAgent.speed = Random.Range(speedInterval.x, speedInterval.y);
	}

	protected override void Reflect(IBall ball, Collision collision) {
		ball.Reflect(-collision.contacts[0].normal);
		base.Hit(ball.damage);
	}
	protected override void OnDamaged() =>
			DamagedEvent?.Invoke(transform.position);
	protected override void OnHit() =>
			HitEvent?.Invoke(transform.position);
	protected override void OnDestroyed() =>
			NoLivesLeft?.Invoke(this);
	public void SetPause(bool isPaused) =>
			navMeshAgent.isStopped = isPaused;
	private void FixedUpdate() =>
			_skeletonAnimator.Move(navMeshAgent.desiredVelocity);

#if UNITY_EDITOR
	private void OnDrawGizmos() =>
			Gizmos.DrawSphere(navMeshAgent.destination, 0.1f);
#endif
}