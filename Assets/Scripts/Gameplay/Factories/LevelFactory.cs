using System;
using UnityEngine;

public sealed class LevelFactory {
	private readonly IFactory _factory;
	private readonly GameplayConfig _config;

	public LevelFactory(GameplayConfig config, IFactory factory) {
		_config = config;
		_factory = factory;
	}

	public Level CreateLevel(int wordIndex, int levelIndex) {
		if (!_config.LevelPrefabExists(wordIndex, levelIndex))
			throw new Exception($"World {wordIndex} Level {levelIndex} not exists check gameplayConfig/Worlds");

		var mapPrefab = _config.GetLevelPrefab(wordIndex, levelIndex);
		var map = _factory.Create<Level>(mapPrefab.levelPrefab);
		map.environment = _factory.Create<Environment>(mapPrefab.environmentPrefab, map.transform);
	
		map.player = _factory.Create<Player>(_config.playerPrefab, map.transform, map.paddleOrigin.position);
		map.player.maxSpeed = _config.paddleSpeed;
		map.player.accelerationStep = _config.paddleAcceleration;
		
		map.ball = _factory.Create<Ball>(_config.ballPrefab, map.transform);
		map.ball.speed = _config.ballSpeed;
		map.ball.damage = _config.skullDamage;
		return map;
	}
}