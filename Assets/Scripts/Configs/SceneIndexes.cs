using UnityEngine;

[CreateAssetMenu(fileName = "ScenesIndexes", menuName = "Configs/ScenesIndexesConfig")]
public class SceneIndexes : ScriptableObject {
	[SerializeField] private int mainMenuSceneIndex;
	[SerializeField] private int gamePlaySceneIndex;

	public int mainMenuIndex => mainMenuSceneIndex;
	public int gamePlayIndex => gamePlaySceneIndex;
}
