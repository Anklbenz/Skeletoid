using UnityEngine;

[ExecuteAlways]
public class GizmosDrawer : MonoBehaviour
{
	private const float STEP = 0.5f;

	[SerializeField] private Vector2Int gameFieldSize = new Vector2Int(10, 22);

	[Header("Grid")] [SerializeField] private bool isGridVisible = true;
	[SerializeField] private Color gridColor = Color.white;

	[Header("Walls")] [SerializeField] private bool isWallsVisible = true;
	[SerializeField] private Color wallsColor = Color.white;

	[Header("PaddleZone")] [SerializeField]
	private bool isPaddleZoneVisible = true;

	[SerializeField] private int lineNumber = -3;
	[SerializeField] private Color paddleZoneColor = Color.white;

	[Header("HorizontalCenterLine")] [SerializeField]
	private bool isHorizontalCenterLineVisible = true;

	[SerializeField] private Color horizontalCenterLineColor = Color.white;

	[Header("VerticalCenterLine")] [SerializeField]
	private bool isVerticalCenterLineVisible = true;

	[SerializeField] private Color verticalCenterLineColor = Color.white;

	private Vector3 realSize => new Vector3(gameFieldSize.x, 0, gameFieldSize.y) * STEP;
	private Vector3 sizeExtends => new Vector3(gameFieldSize.x, 0, gameFieldSize.y) / 2;

	private void OnDrawGizmos() {
		if (isGridVisible)
			DrawGrid();

		if (isWallsVisible)
			DrawWalls();

		if (isPaddleZoneVisible)
			DrawPaddleLine();

		if (isHorizontalCenterLineVisible)
			DrawHorizontalCenterLine();

		if (isVerticalCenterLineVisible)
			DrawVerticalCenterLine();
	}

	private void DrawVerticalCenterLine() {
		Gizmos.color = verticalCenterLineColor;

		var origin = (new Vector3(0 - sizeExtends.x, 0, 0)) * STEP;
		var destination = (new Vector3(0 + sizeExtends.x, 0, 0)) * STEP;
		Gizmos.DrawLine(origin, destination);
	}

	private void DrawHorizontalCenterLine() {
		Gizmos.color = horizontalCenterLineColor;

		var origin = (new Vector3(0, 0, 0 - sizeExtends.z)) * STEP;
		var destination = (new Vector3(0, 0, 0 + sizeExtends.z)) * STEP;
		Gizmos.DrawLine(origin, destination);
	}

	private void DrawPaddleLine() {
		Gizmos.color = paddleZoneColor;
		Gizmos.DrawWireCube(new Vector3(0, 0, -lineNumber - STEP / 2), new Vector3(realSize.x, 0, STEP));
	}

	private void DrawWalls() {
		Gizmos.color = wallsColor;
		Gizmos.DrawWireCube(new Vector3(-realSize.x / 2 - STEP / 2, STEP / 2, 0), new Vector3(STEP, STEP, realSize.z));
		Gizmos.DrawWireCube(new Vector3(+realSize.x / 2 + STEP / 2, STEP / 2, 0), new Vector3(STEP, STEP, realSize.z));
		Gizmos.DrawWireCube(new Vector3(0, STEP / 2, -realSize.z / 2 - STEP / 2), new Vector3(realSize.x, STEP, STEP));
	}

	private void DrawGrid() {
		Gizmos.color = gridColor;
		for (int i = 0; i <= gameFieldSize.x; i++) {
			var origin = (new Vector3(i, 0, 0) - sizeExtends) * STEP;
			var destination = (new Vector3(i, 0, gameFieldSize.y) - sizeExtends) * STEP;
			Gizmos.DrawLine(origin, destination);
		}

		for (int i = 0; i <= gameFieldSize.y; i++) {
			var origin = (new Vector3(0, 0, i) - sizeExtends) * STEP;
			var destination = (new Vector3(gameFieldSize.x, 0, i) - sizeExtends) * STEP;
			Gizmos.DrawLine(origin, destination);
		}
	}
}