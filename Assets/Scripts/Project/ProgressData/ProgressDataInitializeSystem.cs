public class ProgressDataInitializeSystem
{
	private readonly WorldsConfig _worldsConfig;
	private readonly ProgressData _progressData;

	public ProgressDataInitializeSystem(WorldsConfig worldsConfig, ProgressData progressData) {
		_worldsConfig = worldsConfig;
		_progressData = progressData;
	}

	public void Initialize() {
		InitializeWordsInfoArray();
		SetFirstWorldUnlocked();
	}

	private void SetFirstWorldUnlocked() =>
		_progressData.worldsInfo[0].isUnlocked = true;
	
	private void InitializeWordsInfoArray() {
		var wordsCount = _worldsConfig.wordsCount;
		var worldsInfo = new WorldInfo[wordsCount];
		for (var i = 0; i < wordsCount; i++)
			worldsInfo[i] = new WorldInfo() { levelsCount = _worldsConfig.GetLevelCountByWordIndex(i) };
		_progressData.worldsInfo = worldsInfo;
	}
}