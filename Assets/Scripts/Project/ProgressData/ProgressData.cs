using System;

public class ProgressData
{
	public Wallet lives = new(2);
	public Wallet totalCoins = new(0);
	public Wallet currentCoins = new(0);


	public WorldInfo[] worldsInfo;
	public int current => currentWorldIndex;
	private int currentWorldIndex = 0;
	public LevelsHolder levelsHolder;

	public void SetWorld(int index) {
		currentWorldIndex = index;
		levelsHolder = new LevelsHolder(worldsInfo[index].levelsCount);
	}

	public void SetCurrentCompleted() {
		worldsInfo[currentWorldIndex].isCompleted = true;
	}
	
	// отдельная система с методом Set Word Info
	public ProgressData(WorldsConfig config) {
		SetWordsLevelCount(config);
	}
// отдельная система с методом Set Word Info
	private void SetWordsLevelCount(WorldsConfig config) {
		var wordsCount = config.wordsCount;
		worldsInfo = new WorldInfo[wordsCount];
		for (var i = 0; i < wordsCount; i++)
			worldsInfo[i] = new WorldInfo() { levelsCount = config.GetLevelCountByWordIndex(i) };
	
		worldsInfo[0].isUnlocked = true;
	}
}