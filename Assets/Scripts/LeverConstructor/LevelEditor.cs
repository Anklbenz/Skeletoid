using System;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEditor;
using UnityEngine;

public class LevelEditor : MonoBehaviour {
#if UNITY_EDITOR
	[SerializeField] private Transform parent;
	[SerializeField] private GizmosDrawer gizmosDrawer;
	[SerializeField] private StoneBackWall stoneBackWall;
	[SerializeField] private NavMeshSurface navMeshSurfacePrefab;
	[SerializeField] private LayerMask navigationLayer;
	
	[SerializeField] private string path = "Assets/Levels/";
	[SerializeField] private string fileName = "Level";
	[SerializeField] private string bricksParentName = "Bricks";
	[SerializeField] private string junkParentName = "Junk";
	[SerializeField] private string enemiesParentName = "Enemies";
	[SerializeField] private string fileExtension = ".prefab";
	private string levelFileName => $"{fileName}[{DateTime.Now.ToShortTimeString()}]";

	public void CreatePrefab() {
		var localPath = AssetDatabase.GenerateUniqueAssetPath($"{path}{fileName}{fileExtension}");
		var prefab = BuildMapObject();
		PrefabUtility.SaveAsPrefabAssetAndConnect(prefab, localPath, InteractionMode.UserAction);
		DestroyImmediate(prefab);
		Debug.Log($"Level on path {localPath} created");
	}
//Create enemies
	private GameObject BuildMapObject() {
		var container = new GameObject(levelFileName);
		var brickContainer = new GameObject(bricksParentName);
		var junkContainer = new GameObject(junkParentName);
		var enemiesContainer = new GameObject(enemiesParentName);
		brickContainer.transform.SetParent(container.transform);
		junkContainer.transform.SetParent(container.transform);
		enemiesContainer.transform.SetParent(container.transform);
		
		var level = container.AddComponent<Level>();

		level.bricks = CreateBricksOrJunk(brickContainer.transform, true).ToList();
		level.junk = CreateBricksOrJunk(junkContainer.transform, false).ToList();

		level.paddleOrigin = AddPaddleOriginToContainer(container.transform);
		level.deadZone = CreateDeadZone(container.transform);

		level.walls = CreateWalls(container.transform);

		level.backWall = CreateStoneBackWall(container.transform);
		level.backWall.wall = level.walls[3];

		level.floor = CreateFloor(container.transform);
		level.navMeshSurface = CreateNavMeshSurface(container.transform);
		return container;
	}

	private NavMeshSurface CreateNavMeshSurface(Transform container) {
		return (NavMeshSurface)PrefabUtility.InstantiatePrefab(navMeshSurfacePrefab, container);
	}

	private List<Enemy> CreateEnemies(Transform container) {
		List<Enemy> bricks = new();

		return bricks;
	}
	
	private Brick[] CreateBricksOrJunk(Transform container, bool isBrick) {
		List<Brick> bricks = new();
		foreach (Transform child in parent) {
			if (!child.TryGetComponent<Brick>(out var brick) && brick.required != isBrick) continue;
			var brickCopy = CreateBrick(container, brick);
			bricks.Add(brickCopy);
		}
		return bricks.ToArray();
	}
	
	private static Brick CreateBrick(Transform container, Brick brick) {
		var prefab = PrefabUtility.GetCorrespondingObjectFromSource(brick);
		var brickCopy = (Brick)PrefabUtility.InstantiatePrefab(prefab, container);
		brickCopy.transform.position = brick.transform.position;
		brickCopy.transform.rotation = brick.transform.rotation;
		return brickCopy;
	}

	private StoneBackWall CreateStoneBackWall(Transform transformParent) {
		var backWall = (StoneBackWall)PrefabUtility.InstantiatePrefab(stoneBackWall, transformParent);
		backWall.transform.position = gizmosDrawer.backWallCenter;
		return backWall;
	}

	private DeadZone CreateDeadZone(Transform transformParent) {
		var deadZone = CreateObjectWithComponents(gizmosDrawer.deadZoneCenter, gizmosDrawer.wallSizeFront, transformParent, true, "DeadZone");
		return deadZone.AddComponent<DeadZone>();
	}

	private Wall[] CreateWalls(Transform transformParent) {
		var left = CreateObjectWithComponents(gizmosDrawer.leftWallCenter, gizmosDrawer.wallSizeSides, transformParent, false, "WallLeft");
		var leftWall = left.AddComponent<Wall>();

		var right = CreateObjectWithComponents(gizmosDrawer.rightWallCenter, gizmosDrawer.wallSizeSides, transformParent, false, "WallRight");
		var rightWall = right.AddComponent<Wall>();

		var front = CreateObjectWithComponents(gizmosDrawer.frontWallCenter, gizmosDrawer.wallSizeFront, transformParent, false, "WallFront");
		var frontWall = front.AddComponent<Wall>();

		var back = CreateObjectWithComponents(gizmosDrawer.backWallCenter, gizmosDrawer.wallSizeFront, transformParent, false, "WallBack");
		var backWall = back.AddComponent<Wall>();
		back.SetActive(false);

		return new[] {leftWall, rightWall, frontWall, backWall};
	}

	private Floor CreateFloor(Transform transformParent) {
		var floorGameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
		floorGameObject.transform.position = gizmosDrawer.floorCenter; //  CreateObjectWithComponents(gizmosDrawer.floorCenter, gizmosDrawer.floorSize, transformParent, false, "Floor");
		floorGameObject.transform.localScale = gizmosDrawer.floorSize;
		floorGameObject.transform.SetParent(transformParent);
		floorGameObject.layer = navigationLayer;
		
		var meshRenderer = floorGameObject.GetComponent<MeshRenderer>();

		var floor = floorGameObject.AddComponent<Floor>();
		floor.meshRenderer = meshRenderer;

		return floor;
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
	#endif
}