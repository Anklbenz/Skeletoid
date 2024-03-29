using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameplayConfig", menuName = "Configs/GameplayConfig")]
public class GameplayConfig : ScriptableObject {
	[Header("Skull")]
	[SerializeField] private Ball ball;
	[SerializeField] private int damage;
	[SerializeField] private float startSpeed = 3.5f;
	[SerializeField] private float boostMultiplier = 1.003f;

	[Header("Reflect settings")]
	[SerializeField] private float permissibleAngleValue = 20;
	[SerializeField] private float correctionStepValue = 2;

	[Header("Paddle")] [SerializeField] private Player player;
	[SerializeField] private float paddleMaxSpeed = 4;
	[SerializeField] private float paddleAccelerationStep = 0.4f;

	[Header("LoseSystem")]
	[SerializeField] private int showingAdsCount;
	[SerializeField] private int delayBeforeWinSystemActivateValue = 300;

	[Header("LoseState")]
	[SerializeField] private int lookAtSkeletonDelay = 700;

	[Header("GameplayHint")]
	[SerializeField] private float gameplayHintDelaySeconds = 700;

	public int delayBeforeLookAtSkeleton => lookAtSkeletonDelay;
	public float gameplayHintDelay => gameplayHintDelaySeconds;
	public int delayBeforeWinSystemActivate => delayBeforeWinSystemActivateValue;
	public int showingAdsNumber => showingAdsCount;
	public int ballDamage => damage;
	public float paddleSpeed => paddleMaxSpeed;
	public float paddleAcceleration => paddleAccelerationStep;
	public float permissibleAngle => permissibleAngleValue;
	public float correctionStep => correctionStepValue;
	public float ballSpeed => startSpeed;
	public float ballBoostMultiplier => boostMultiplier;
	public Ball ballPrefab => ball;
	public Player playerPrefab => player;
}