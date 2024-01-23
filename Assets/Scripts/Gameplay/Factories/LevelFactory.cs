public sealed class LevelFactory {
	private readonly IFactory _factory;
	private readonly GameplayConfig _config;
	private readonly WorldsConfig _worldsConfig;

	public LevelFactory(GameplayConfig config, WorldsConfig worldsConfig, IFactory factory) {
		_config = config;
		_worldsConfig = worldsConfig;
		_factory = factory;
	}

	public Level CreateLevel(int wordIndex) {
		var mapPrefab = _worldsConfig.GetLevelPrefab(wordIndex);
		var map = _factory.Create(mapPrefab.levelPrefab);
		CreateEnvironment(map, mapPrefab);
		CreatePlayer(map);
		CreateBall(map);
		return map;
	}
	
	private void CreateEnvironment(Level map, LevelTemplate mapPrefab) =>
		map.environment = _factory.Create(mapPrefab.environmentPrefab, map.transform);
	
	private void CreateBall(Level map) {
		var ball = _factory.Create(_config.ballPrefab, map.transform);
	//	ball.speed = _config.ballSpeed;
		ball.damage = _config.ballDamage;
		ball.permissibleAngle = _config.permissibleAngle;
		ball.correctionStep = _config.correctionStep;
		map.ball = ball;
	}
	private void CreatePlayer(Level map) {
		map.player = _factory.Create(_config.playerPrefab, map.transform, map.paddleOrigin.position);
		map.player.motor.maxSpeed = _config.paddleSpeed;
		map.player.motor.accelerationStep = _config.paddleAcceleration;
	}
}