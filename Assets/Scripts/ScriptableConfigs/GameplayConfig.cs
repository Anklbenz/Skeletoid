using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameplayConfig", menuName = "Configs/GameplayConfig")]
public class GameplayConfig : ScriptableObject
{
	[Header("Skull")] [SerializeField] private Ball ball;
	[SerializeField] private int damage;
	[SerializeField] private float skullStartSpeed = 5;

	[Header("Paddle")] [SerializeField] private Player player;
	[SerializeField] private float paddleMaxSpeed = 4;
	[SerializeField] private float paddleAccelerationStep = 0.4f;

	[Header("LoseSystem")] [SerializeField]
	private int showingAdsCount;

	[Header("Camera")] 
	[SerializeField] private float cameraShakeIntensityValue;
	[SerializeField] private int shakeDurationMillisecondsValue;
	[SerializeField] private int delayBeforeWinSystemActivateValue = 300;

	public int cameraShakeDurationMilliseconds => shakeDurationMillisecondsValue; 
	public float cameraShakeIntensity => cameraShakeIntensityValue;
	public int delayBeforeWinSystemActivate => delayBeforeWinSystemActivateValue;
	public int showingAdsNumber=>showingAdsCount;
	public int skullDamage => damage;
	public float paddleSpeed => paddleMaxSpeed;
	public float paddleAcceleration => paddleAccelerationStep;

	public float ballSpeed => skullStartSpeed;
	public Ball ballPrefab => ball;
	public Player playerPrefab => player;
}
