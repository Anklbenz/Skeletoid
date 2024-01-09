using UnityEngine;

[CreateAssetMenu(menuName = "Configs/CameraConfig", fileName = "CameraConfig")]
public class CameraZoomConfig : ScriptableObject {
	[Header("Main")]
	[SerializeField] private Vector3 mainCameraPosition = new (0, 4.1f, -2.8f);
	[SerializeField] private Vector3 mainCameraRotation = new (50, 0, 0);
	[SerializeField] private float mainFOV = 50;
	[Header("Zoomed")]
	[SerializeField] private Vector3 zoomedCameraPosition = new (0, 4.1f, -2.8f);
	[SerializeField] private Vector3 zoomedCameraRotation = new (50, 0, 0);
	[SerializeField] private float zoomedFOV = 27;
	public float mainCameraFOV => mainFOV;
	public float zoomedCameraFOV => zoomedFOV;
	public Vector3 mainPosition => mainCameraPosition;
	public Vector3 mainRotation => mainCameraRotation;
	public Vector3 zoomedPosition => zoomedCameraPosition;
	public Vector3 zoomedRotation => zoomedCameraRotation;
}