using UnityEngine;

[CreateAssetMenu(fileName = "ScenesIndexes", menuName = "Configs/ScenesIndexesConfig")]
public class SceneIndexes : ScriptableObject {
	[SerializeField] private int levelSelectSceneIndex;
	[SerializeField] private int coreSessionSceneIndex;

	public int levelSelectIndex => levelSelectSceneIndex;
	public int coreSessionIndex => coreSessionSceneIndex;
}
