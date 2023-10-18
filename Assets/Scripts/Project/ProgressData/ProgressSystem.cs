public class ProgressSystem
{
	public int worldsCount => _data.worldsInfo.Length;
	public int currentWorldIndex => _currentWorldIndexWorldIndex;
	public int currentWorldLevelsCount => _data.worldsInfo[_currentWorldIndexWorldIndex].levelsCount;
	public int currentCoinsCount => _data.currentCoinsWallet.count;
	public int totalCoinsCount => _data.totalCoinsWallet.count;
	public int currentLevelIndex => _data.levelsHolder.currentIndex;
	public bool hasLives => _data.lives.count > 0;
	public int livesCount => _data.lives.count;
	public int starsCount => _data.statsWallet.count;
	
	
	private WorldData currentWorld => _data.worldsInfo[_currentWorldIndexWorldIndex];
	private WorldData nextWorld => _data.worldsInfo[_currentWorldIndexWorldIndex + 1];
	private bool hasNextWorld => _data.worldsInfo.Length > _currentWorldIndexWorldIndex + 1;
	private int _currentWorldIndexWorldIndex;

	private readonly WorldsConfig _worldsConfig;
	private readonly ProgressData _data;

	public ProgressSystem(WorldsConfig worldsConfig, ProgressData data) {
		_worldsConfig = worldsConfig;
		_data = data;
		InitializeWordsFromConfig();
		SetFirstWorldUnlocked();
	}

	private void InitializeWordsFromConfig() {
		var wordsCount = _worldsConfig.wordsCount;
		var worldsInfo = new WorldData[wordsCount];
		for (var i = 0; i < wordsCount; i++)
			worldsInfo[i] = new WorldData() { levelsCount = _worldsConfig.GetLevelCountByWordIndex(i) };
		_data.worldsInfo = worldsInfo;
	}

	private void SetFirstWorldUnlocked() =>
		_data.worldsInfo[0].isUnlocked = true;

	public void SetWorld(int index) {
		_currentWorldIndexWorldIndex = index;
		_data.levelsHolder = new LevelsHolder(_data.worldsInfo[index].levelsCount);
	}

	public WorldData GetWordInfoByIndex(int index) =>
		_data.worldsInfo[index];

	public bool TrySetNextLevel() =>
		_data.levelsHolder.TryMoveNext();

	public void AddLife() =>
		_data.lives.Increase();

	public void SpendLife() =>
		_data.lives.Decrease();

	public void IncreaseCurrentCoins(int count) =>
		_data.currentCoinsWallet.Increase(count);

	public void DecreaseTotalCoins(int count) =>
		_data.totalCoinsWallet.Decrease(count);

	public void IncreaseStarsCount(int count) =>
		_data.statsWallet.Increase(count);

	public void DecreaseStarsCount(int count) =>
		_data.statsWallet.Decrease(count);
	
	public void SetCurrentLevelCompleted() {
		currentWorld.isCompleted = true;
		if (hasNextWorld) {
			nextWorld.isUnlocked = true;
			nextWorld.freshUnlockedTrigger = true;
		}

		ApplyCurrentCoins();
	}
	
	private void ApplyCurrentCoins() {
		var currentScore = _data.currentCoinsWallet.count;
		_data.totalCoinsWallet.Increase(currentScore);
		_data.currentCoinsWallet.Reset();
	}
}