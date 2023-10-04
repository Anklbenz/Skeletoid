
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
		var prefab = BuildMapObject();
		PrefabUtility.SaveAsPrefabAssetAndConnect(prefab, localPath, InteractionMode.UserAction);
		DestroyImmediate(prefab);
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
		CreateWallsAndFloorColliders(container.transform);
		return container;
	}

	private DeadZone CreateDeadZone(Transform transformParent) {
		var deadZone = CreateBoxColliderObject(gizmosDrawer.deadZoneCenter, gizmosDrawer.wallSizeFront, transformParent, true, "DeadZone");
		return deadZone.AddComponent<DeadZone>();
	}

	private void CreateWallsAndFloorColliders(Transform transformParent) {
		var leftWall =CreateBoxColliderObject(gizmosDrawer.leftWallCenter, gizmosDrawer.wallSizeSides, transformParent, false, "WallLeft");
		leftWall.isStatic = true;
		var rigidBody = leftWall.AddComponent<Rigidbody>();
		rigidBody.useGravity = false;
		rigidBody.isKinematic = true;
		
		var rightWall =CreateBoxColliderObject(gizmosDrawer.rightWallCenter, gizmosDrawer.wallSizeSides, transformParent, false, "WallRight");
        rigidBody = rightWall.AddComponent<Rigidbody>();
        rigidBody.useGravity = false;
        rigidBody.isKinematic = true;
        
		var frontWall = CreateBoxColliderObject(gizmosDrawer.frontWallCenter, gizmosDrawer.wallSizeFront, transformParent, false, "WallFront");
		var floor = CreateBoxColliderObject(gizmosDrawer.floorCenter, gizmosDrawer.floorSize, transformParent, false, "Floor");
	}

	private GameObject CreateBoxColliderObject(Vector3 position, Vector3 size, Transform parentTransform = null, bool isTrigger = false, string objName = "object" ) {
		var box = new GameObject(objName);
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