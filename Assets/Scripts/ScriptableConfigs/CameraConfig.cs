using Cinemachine;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/CameraConfig", fileName = "CameraConfig")]
public class CameraConfig : ScriptableObject

{
	[SerializeField] private CinemachineVirtualCamera mainCamera;
	[SerializeField] private CinemachineVirtualCamera zoomedCamera;
	[SerializeField] private Vector3 mainCameraPosition = new Vector3(0,4.1f,-2.8f);
	[SerializeField] private Vector3 mainCameraRotation = new Vector3(50,0,0);
	[SerializeField] private Vector3 zoomedCameraPosition=new Vector3(0,4.1f,-2.8f);
	[SerializeField] private Vector3 zoomedCameraRotation=new Vector3(50,0,0);
	[SerializeField] private float zoomedFOV = 27;
	[SerializeField] private float cameraShakeIntensityValue;
	[SerializeField] private int shakeDurationMillisecondsValue;

	public float zoomedCameraFOV => zoomedFOV;
	public CinemachineVirtualCamera mainCameraPrefab => mainCamera;
	public Vector3 mainPosition => mainCameraPosition;
	public Vector3 mainRotation => mainCameraRotation;
	public Vector3 zoomedPosition => zoomedCameraPosition;
	public Vector3 zoomedRotation => zoomedCameraRotation;
	public CinemachineVirtualCamera zoomedCameraPrefab => zoomedCamera;
	public int cameraShakeDurationMilliseconds => shakeDurationMillisecondsValue;
	public float cameraShakeIntensity => cameraShakeIntensityValue;
}
