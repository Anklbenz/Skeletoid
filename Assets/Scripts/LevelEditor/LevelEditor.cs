using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class LevelEditor : MonoBehaviour
{
	[SerializeField] private Transform parent;
	[SerializeField] private GizmosDrawer gizmosDrawer;
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
			if (!child.TryGetComponent<Brick>(out var brick)) continue;
			var brickCopy = Instantiate(brick, brick.transform.position, brick.transform.rotation, container.transform);
			level.bricks.Add(brickCopy);
		}

		level.paddleOrigin = AddPaddleOriginToContainer(container.transform);
		level.deadZone = CreateDeadZone(container.transform);
		return container;
	}

	private DeadZone CreateDeadZone(Transform transformParent) {
		var deadZone = new GameObject("DeadZone");
		
		var boxCollider = deadZone.AddComponent<BoxCollider>();
		boxCollider.isTrigger = true;
		
		deadZone.transform.position = gizmosDrawer.deadZoneCenter;
		deadZone.transform.localScale = gizmosDrawer.deadZoneSize;
		deadZone.transform.SetParent(transformParent);

		return deadZone.AddComponent<DeadZone>();
	}

	private Transform AddPaddleOriginToContainer(Transform container) {
		var paddleOrigin = new GameObject("paddleOrigin");
		paddleOrigin.transform.position = gizmosDrawer.paddleCenter;
		paddleOrigin.transform.SetParent(container.transform);
		return paddleOrigin.transform;
	}

	public void Clear() {
		for (var count = parent.transform.childCount - 1; count >= 0; count--)
			DestroyImmediate(parent.transform.GetChild(count).gameObject);
	}
}