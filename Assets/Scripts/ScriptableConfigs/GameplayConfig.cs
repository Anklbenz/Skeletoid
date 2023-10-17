using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameplayConfig", menuName = "Configs/GameplayConfig")]
public class GameplayConfig : ScriptableObject {

	
	[Header("Skull")]
	[SerializeField] private Ball ball;
	[SerializeField] private int damage;
	[SerializeField] private float skullStartSpeed = 5;

	[Header("Paddle")]
	[SerializeField] private Player player;
	[SerializeField] private float paddleMaxSpeed = 4;
	[SerializeField] private float paddleAccelerationStep = 0.4f;

	[Header("BallLauncher")] 
	[SerializeField] private BallLaunchSystem _ballLaunchSystem; 

	public int skullDamage => damage;
	public float paddleSpeed => paddleMaxSpeed;
	public float paddleAcceleration => paddleAccelerationStep;
	
	public float ballSpeed => skullStartSpeed;
	public Ball ballPrefab => ball;
	public Player playerPrefab => player;
	public BallLaunchSystem ballLaunchSystemPrefab => _ballLaunchSystem;


}