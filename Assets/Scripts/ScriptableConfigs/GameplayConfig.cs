using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameplayConfig", menuName = "Configs/GameplayConfig")]
public class GameplayConfig : ScriptableObject
{
	[Header("Skull")] [SerializeField] private Ball ball;
	[SerializeField] private int damage;
	[SerializeField] private float skullStartSpeed = 5;

	[Header("Reflect settings")] [SerializeField]
	private float permissibleAngleValue = 20;

	[SerializeField] private float correctionStepValue = 2;

	[Header("Paddle")] [SerializeField] private Player player;
	[SerializeField] private float paddleMaxSpeed = 4;
	[SerializeField] private float paddleAccelerationStep = 0.4f;

	[Header("LoseSystem")] [SerializeField]
	private int showingAdsCount;

	[SerializeField] private int delayBeforeWinSystemActivateValue = 300;
	[Header("LoseState")] [SerializeField] private int lookAtSkeletonDelay = 700;


	public int delayBeforeLookAtSkeleton => lookAtSkeletonDelay;
	public int delayBeforeWinSystemActivate => delayBeforeWinSystemActivateValue;
	public int showingAdsNumber => showingAdsCount;
	public int skullDamage => damage;
	public float paddleSpeed => paddleMaxSpeed;
	public float paddleAcceleration => paddleAccelerationStep;

	public float permissibleAngle => permissibleAngleValue;
	public float correctionStep => correctionStepValue;

	public float ballSpeed => skullStartSpeed;
	public Ball ballPrefab => ball;
	public Player playerPrefab => player;
}