public class ProgressSystem
{
	public int worldsCount => _progressData.worldsInfo.Length;
	public int currentWorldIndex => _currentWorldIndexWorldIndex;
	public int currentWorldLevelsCount => _progressData.worldsInfo[_currentWorldIndexWorldIndex].levelsCount;
	public int currentCoinsCount => _progressData.currentCoinsWallet.count;
	public int totalCoinsCount => _progressData.totalCoinsWallet.count;
	public int currentLevelIndex => _progressData.levelsHolder.currentIndex;
	public bool hasLives => _progressData.lives.count > 0;
	public int livesCount => _progressData.lives.count;
	public int starsCount => _progressData.statsWallet.count;
	public Level currentLevel { get; set; }
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
			worldData[i] = new WorldData()
			{
				levelsCount = _worldsConfig.GetWorldByIndex(i).levelsCount
			};
		_progressData.worldsInfo = worldData;
	}

	private void SetFirstWorldUnlocked() =>
		_progressData.worldsInfo[0].isUnlocked = true;

	public void SetWorld(int index) {
		_currentWorldIndexWorldIndex = index;
		_progressData.levelsHolder = new LevelsHolder(_progressData.worldsInfo[index].levelsCount);
	}

	public WorldData GetWordInfoByIndex(int index) =>
		_progressData.worldsInfo[index];

	public bool TrySetNextLevel() =>
		_progressData.levelsHolder.TryMoveNext();

	public void AddLife(int count = 1) =>
		_progressData.lives.Increase(count);

	public void SpendLife() =>
		_progressData.lives.Decrease();

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
		if (hasNextWorld) {
			nextWorld.isUnlocked = true;
			nextWorld.freshUnlockedTrigger = true;
		}
	}

	public void SetStars(int stars) {
		var currentWorldStars = currentWorldData.starsCount;
		if(stars< currentWorldStars ) return;
		
		var earnedStars = stars - currentWorldStars;
		currentWorldData.starsCount = stars;
		IncreaseStarsCount(earnedStars);
	}

	public void SetLevelTime(float time) {
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