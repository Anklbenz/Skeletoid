public class LevelsHolder {
	private readonly int _maxLevelIndex;
	private int _currentLevel;

	public int current => _currentLevel;
	
	public LevelsHolder(int levelsCount) {
		_maxLevelIndex = levelsCount - 1;
	}
   
	public bool TryMoveNext() {
		if (_currentLevel >= _maxLevelIndex)
			return false;

		_currentLevel++;
		return true;
	}
	
}