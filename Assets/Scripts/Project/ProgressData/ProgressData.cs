using System;

public class ProgressData
{
	public Wallet lives = new(2);
	public Wallet totalCoins = new(0);
	public Wallet currentCoins = new(0);

	public WorldInfo[] worldsInfo { get; set; }
	public LevelsHolder levelsHolder { get; private set; }
	public int current => _currentWorldIndex;

	private int _currentWorldIndex = 0;

	public void SetWorld(int index) {
		_currentWorldIndex = index;
		levelsHolder = new LevelsHolder(worldsInfo[index].levelsCount);
	}

	public void SetCurrentCompleted() {
		worldsInfo[_currentWorldIndex].isCompleted = true;
		SetUnlockedNextAfterCompleted();
	}

	private void SetUnlockedNextAfterCompleted() {
		if (worldsInfo.Length <= _currentWorldIndex + 1)
			worldsInfo[_currentWorldIndex + 1].isUnlocked = true;
	}
}