using UnityEngine.SceneManagement;

public class SceneLoaderService {
	private readonly SceneIndexes _indexes;
	
	public SceneLoaderService(SceneIndexes indexes) {
		_indexes = indexes;
	}

	public void GoToLevelSelectScene() =>
			SceneManager.LoadScene(_indexes.levelSelectIndex, LoadSceneMode.Single);

	public void GoToGameplayScene() =>
			SceneManager.LoadScene(_indexes.coreSessionIndex, LoadSceneMode.Single);
}