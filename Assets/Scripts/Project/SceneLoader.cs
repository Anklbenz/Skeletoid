using UnityEngine.SceneManagement;

public class SceneLoader {
	private readonly SceneIndexes _indexes;
	
	public SceneLoader(SceneIndexes indexes) {
		_indexes = indexes;
	}

	public void GoToMainMenuScene() =>
			SceneManager.LoadScene(_indexes.mainMenuIndex, LoadSceneMode.Single);

	public void GoToGameplayScene() =>
			SceneManager.LoadScene(_indexes.gamePlayIndex, LoadSceneMode.Single);
}