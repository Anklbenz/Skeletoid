
using UnityEditor;
using UnityEngine;

public class LevelEditor : MonoBehaviour {
	[SerializeField] private Transform parent;
	[SerializeField] private GizmosDrawer gizmosDrawer;
	[SerializeField] private string path = "Assets/Levels/";
	[SerializeField] private string fileName = "Level";
	[SerializeField] private string fileExtension = ".prefab";

	public void CreatePrefab() {
		var localPath = AssetDatabase.GenerateUniqueAssetPath($"{path}{fileName}{fileExtension}");
		var prefab = BuildMapObject();
		PrefabUtility.SaveAsPrefabAssetAndConnect(prefab, localPath, InteractionMode.UserAction);
		DestroyImmediate(prefab);
		Debug.Log($"Level on path {localPath} created");
	}

	private GameObject BuildMapObject() {
		var container = new GameObject(fileName);
		var level = container.AddComponent<Level>();

		foreach (Transform child in parent) {
			if (!child.TryGetComponent<Brick>(out var brick)) continue;
			var brickCopy = Instantiate(brick, brick.transform.position, brick.transform.rotation, container.transform);
			level.bricks.Add(brickCopy);
		}

		level.paddleOrigin = AddPaddleOriginToContainer(container.transform);
		level.deadZone = CreateDeadZone(container.transform);
		level.walls = CreateWall(container.transform);
		CreateFloor(container.transform);
		return container;
	}

	private DeadZone CreateDeadZone(Transform transformParent) {
		var deadZone = CreateObjectWithComponents(gizmosDrawer.deadZoneCenter, gizmosDrawer.wallSizeFront, transformParent, true, "DeadZone");
		return deadZone.AddComponent<DeadZone>();
	}

	private Wall[] CreateWall(Transform transformParent) {
		var left = CreateObjectWithComponents(gizmosDrawer.leftWallCenter, gizmosDrawer.wallSizeSides, transformParent, false, "WallLeft");
		var leftWall = left.AddComponent<Wall>();
		//	leftWall.isStatic = true;
		/*var rigidBody = leftWall.AddComponent<Rigidbody>();
		rigidBody.useGravity = false;
		rigidBody.isKinematic = true;*/

		var right = CreateObjectWithComponents(gizmosDrawer.rightWallCenter, gizmosDrawer.wallSizeSides, transformParent, false, "WallRight");
		var rightWall = right.AddComponent<Wall>();

		//	rightWall.isStatic = true;
		/*rigidBody = rightWall.AddComponent<Rigidbody>();
		rigidBody.useGravity = false;
		rigidBody.isKinematic = true;*/

		var front = CreateObjectWithComponents(gizmosDrawer.frontWallCenter, gizmosDrawer.wallSizeFront, transformParent, false, "WallFront");
		var frontWall = front.AddComponent<Wall>();

		//	frontWall.isStatic = true;

		var back = CreateObjectWithComponents(gizmosDrawer.backWallCenter, gizmosDrawer.wallSizeFront, transformParent, false, "WallBack");
		back.SetActive(false);
		var backWall = back.AddComponent<Wall>();
		//	frontWall.isStatic = true;

		return new[] {leftWall, rightWall, frontWall, backWall};
	}

	private void CreateFloor(Transform transformParent) {
		var floor = CreateObjectWithComponents(gizmosDrawer.floorCenter, gizmosDrawer.floorSize, transformParent, false, "Floor");
		floor.AddComponent<Floor>();
	}


	private GameObject CreateObjectWithComponents(Vector3 position, Vector3 size, Transform parentTransform = null, bool isTrigger = false, string objName = "object") {
		var box = new GameObject(objName);
		box.isStatic = true;

		var boxCollider = box.AddComponent<BoxCollider>();
		boxCollider.isTrigger = isTrigger;
		box.transform.position = position;
		box.transform.localScale = size;
		box.transform.SetParent(parentTransform);
		return box;
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