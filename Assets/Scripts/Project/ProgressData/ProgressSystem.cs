using System;
using UnityEngine;

public class ProgressSystem
{
	public int worldsCount => _progressData.worldsInfo.Length;
	public int currentWorldIndex => _currentWorldIndexWorldIndex;
	public int currentCoinsCount => _progressData.currentCoinsWallet.count;
	public int totalCoinsCount => _progressData.totalCoinsWallet.count;
	public int keysCount => _progressData.keysWallet.count;
	public int livesCount => _progressData.lives.count;
	public int starsCount => _progressData.statsWallet.count;

	public long lastSpendTime {
		get => _progressData.lastKeySpendTimeSeconds;
		set => _progressData.lastKeySpendTimeSeconds = value;
	}

	public Level currentLevel { get; set; }

	public int lastUnlockedWorldIndex {
		get
		{
			for (var i = 0; i < _progressData.worldsInfo.Length; i++)
				if (!_progressData.worldsInfo[i].isUnlocked)
					return i - 1;

			return _progressData.worldsInfo.Length - 1;
		}
	}

	private WorldData currentWorldData => _progressData.worldsInfo[_currentWorldIndexWorldIndex];
	private WorldData nextWorld => _progressData.worldsInfo[_currentWorldIndexWorldIndex + 1];
	private bool hasNextWorld => _progressData.worldsInfo.Length > _currentWorldIndexWorldIndex + 1;
	private int _currentWorldIndexWorldIndex;

	private readonly WorldsConfig _worldsConfig;
	private readonly ProgressData _progressData;

	public ProgressSystem(WorldsConfig worldsConfig, ProgressData progressData) {
		_worldsConfig = worldsConfig;
		_progressData = progressData;
		InitializeWordsFromConfig();
		SetFirstWorldUnlocked();
	}

	private void InitializeWordsFromConfig() {
		var wordsCount = _worldsConfig.wordsCount;
		var worldData = new WorldData[wordsCount];

		for (var i = 0; i < wordsCount; i++)
			worldData[i] = new WorldData();
	    _progressData.worldsInfo = worldData;
	}

	private void SetFirstWorldUnlocked() =>
		_progressData.worldsInfo[0].isUnlocked = true;

	public void SetWorld(int index) {
		_currentWorldIndexWorldIndex = index;
	}
	
	public Vector3 GetCurrentTimeLimits() {
		var world = _worldsConfig.GetWorldByIndex(_currentWorldIndexWorldIndex);
		return new Vector3(world.firstStarSeconds, world.secondStarSeconds, world.thirdStarSeconds);
	}

	public WorldData GetWordInfoByIndex(int index) =>
		_progressData.worldsInfo[index];

	public void SetCurrentWorldLives() {
		var lives =_worldsConfig.GetWorldByIndex(_currentWorldIndexWorldIndex).lives;
		_progressData.lives.Reset();
		_progressData.lives.Increase(lives);
	}
	
	public int AddLife(int count = 1) {
		_progressData.lives.Increase(count);
		return _progressData.lives.count;
	}

	public int SpendLife() {
		_progressData.lives.Decrease();
		return _progressData.lives.count;
	}

	public void SpendKey() =>
		_progressData.keysWallet.Decrease();

	public void IncreaseKey(int count=1) =>
		_progressData.keysWallet.Increase(count);

	public void IncreaseCurrentCoins(int count) =>
		_progressData.currentCoinsWallet.Increase(count);

	public void DecreaseTotalCoins(int count) =>
		_progressData.totalCoinsWallet.Decrease(count);

	public void IncreaseStarsCount(int count) =>
		_progressData.statsWallet.Increase(count);

	public void DecreaseStarsCount(int count) =>
		_progressData.statsWallet.Decrease(count);

	public void SetCurrentLevelCompleted() {
		currentWorldData.isCompleted = true;

		if (!hasNextWorld || nextWorld.isUnlocked) return;
		nextWorld.isUnlocked = true;
		nextWorld.freshUnlocked = true;
	}

	public void SetCurrentLevelStars(int stars) {
		var currentWorldStars = currentWorldData.starsCount;
		if (stars < currentWorldStars) return;

		var earnedStars = stars - currentWorldStars;
		currentWorldData.starsCount = stars;
		IncreaseStarsCount(earnedStars);
	}

	public void SetCurrentLevelTime(float time) {
		if (currentWorldData.bestCompletedTime > time)
			currentWorldData.bestCompletedTime = time;
	}

	public void ApplyCurrentCoins() {
		var currentScore = _progressData.currentCoinsWallet.count;
		_progressData.totalCoinsWallet.Increase(currentScore);
		_progressData.currentCoinsWallet.Reset();
	}

	public void ResetCurrentCoins() =>
		_progressData.currentCoinsWallet.Reset();
}