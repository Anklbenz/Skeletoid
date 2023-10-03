public class LevelsHolder
{
	private readonly int _maxLevelIndex;
	private int _currentLevelIndex;

	public int levelsCount { get; }
	public int currentIndex => _currentLevelIndex;
	public int current => _currentLevelIndex + 1;

	public LevelsHolder(int levelsCount) {
		this.levelsCount = levelsCount;
		_maxLevelIndex = levelsCount - 1;
	}

	public bool TryMoveNext() {
		if (_currentLevelIndex >= _maxLevelIndex)
			return false;

		_currentLevelIndex++;
		return true;
	}
}