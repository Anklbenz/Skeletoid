using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class SceneLoader {
	private const int FADE_IN_MILLISECONDS = 250;

	private readonly SceneIndexes _indexes;
	private FadeView _view;

	public SceneLoader(SceneIndexes indexes) {
		_indexes = indexes;
	}

	public void InitializeView(FadeView view) {
		_view = view;
	}

	public void GoToWorldMapScene() {
		SwitchSceneWithAnimation(_indexes.mainMenuIndex);
	}

	public void GoToGameplayScene() {
		SwitchSceneWithAnimation(_indexes.gamePlayIndex);
	}
	private async void SwitchSceneWithAnimation(int sceneIndex) {
		_view.Open();
		await UniTask.Delay(FADE_IN_MILLISECONDS);

		var gameplaySceneLoadTask = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
		while (!gameplaySceneLoadTask.isDone)
			await UniTask.Yield();

		_view.Close();
	}
}