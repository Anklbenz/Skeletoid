using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Damageble {
	private const float MIN_DISTANCE = 0.1f;

	[SerializeField] private NavMeshAgent navMeshAgent;

	public event Action<Enemy> NoLivesLeft;
	public event Action<Vector3> HitEvent, DamagedEvent;
	public bool isPathComplete => navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete || navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid || navMeshAgent.remainingDistance > MIN_DISTANCE;

	public void SetNewDestination(Vector3 target) =>
			navMeshAgent.SetDestination(target);

	protected override void Reflect(IBall ball, Collision collision) {
		ball.Reflect(-collision.contacts[0].normal);
		base.Hit(ball.damage);
	}

	protected override void OnDamaged() =>
			DamagedEvent?.Invoke(transform.position);
	protected override void OnHit() =>
			HitEvent?.Invoke(transform.position);
	private void OnDestroy() =>
			NoLivesLeft?.Invoke(this);
}