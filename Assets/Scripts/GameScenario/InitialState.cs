using System.Linq;
using UnityEngine;
public class InitialState : State {
	private readonly IFactory _factory;
	private readonly GameObjectsConfig _gameObjectsConfig;
	private readonly CurrentGameplayData _currentGameplayData;
	
	public InitialState(StateSwitcher stateSwitcher, GameObjectsConfig config, CurrentGameplayData currentGameplayData, IFactory factory) : base(stateSwitcher) {
		_factory = factory;
		_gameObjectsConfig = config;
		_currentGameplayData = currentGameplayData;
	}

	public override void Enter() {
		InitializeSceneObjects();
		SwitchToGameplay();
	}
	private void InitializeSceneObjects() {
		var map = _gameObjectsConfig.GetMapPrefab(0);
		var levelInstance = _factory.Get<Level>(map.level);

		var environmentInstance = _factory.Get<Environment>(map.environment);
		var ballInstance = _factory.Get<Ball>(_gameObjectsConfig.ballPrefab);
		var paddleInstance = _factory.Get<Paddle>(_gameObjectsConfig.paddlePrefab);
		
		paddleInstance.transform.position = levelInstance.paddleOrigin.position;

		_currentGameplayData.bricks = levelInstance.bricks.ToList();
		_currentGameplayData.paddle = paddleInstance;
		_currentGameplayData.ball = ballInstance;
		_currentGameplayData.environment = environmentInstance;
		_currentGameplayData.ballOrigin = paddleInstance.ballTransform;
	}
	
	private void SwitchToGameplay() {
		switcher.SetState<GameplayState>();
	}
}