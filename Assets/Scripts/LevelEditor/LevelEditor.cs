using UnityEditor;
using UnityEngine;

public class LevelEditor : MonoBehaviour
{
	[SerializeField] private Transform parent;
	[SerializeField] private string path = "Assets/Levels/";
	[SerializeField] private string fileName = "Level";
	[SerializeField] private string fileExtension = ".prefab";

	public void CreatePrefab() {
		var localPath = AssetDatabase.GenerateUniqueAssetPath($"{path}{fileName}{fileExtension}");
		var prefab = BuildGameObject();
		PrefabUtility.SaveAsPrefabAssetAndConnect(prefab, localPath, InteractionMode.UserAction);
		DestroyImmediate(prefab);
	}

	private GameObject BuildGameObject() {
		var container = new GameObject(fileName);
		var level = container.AddComponent<Level>();

		foreach (Transform child in parent) {
			if (child.TryGetComponent<Brick>(out var brick)) {
				var brickCopy = Instantiate(brick, brick.transform.position, brick.transform.rotation, container.transform);
				level.bricks.Add(brickCopy);
				continue;
			}

			if (!child.TryGetComponent<Paddle>(out var paddle)) continue;
			
			var paddleOrigin = new GameObject("paddleOrigin");
			paddleOrigin.transform.position = paddle.transform.position;
			paddleOrigin.transform.SetParent(container.transform);
			level.paddleOrigin = paddleOrigin.transform;
		}

		return container;
	}

	public void Clear() {
		for (var count = parent.transform.childCount - 1; count >= 0; count--)
			DestroyImmediate(parent.transform.GetChild(count).gameObject);
	}
}