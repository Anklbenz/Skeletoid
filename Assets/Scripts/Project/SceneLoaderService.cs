using UnityEngine.SceneManagement;

public class SceneLoaderService {
	private readonly SceneIndexesStorage _indexesStorage;
	
	public SceneLoaderService(SceneIndexesStorage indexesStorage) {
		_indexesStorage = indexesStorage;
	}

	public void GoToLevelSelectScene() =>
			SceneManager.LoadScene(_indexesStorage.levelSelectIndex, LoadSceneMode.Single);

	public void GoToGameplayScene() =>
			SceneManager.LoadScene(_indexesStorage.coreSessionIndex, LoadSceneMode.Single);
}