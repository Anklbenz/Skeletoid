using UnityEngine;

[CreateAssetMenu(menuName = "Configs/CameraShakeConfig", fileName = "CameraShakeConfig")]
public class CameraShakeConfig : ScriptableObject {
	[SerializeField] private float cameraShakeIntensityValue;
	[SerializeField] private int shakeDurationMillisecondsValue;

	public int cameraShakeDurationMilliseconds => shakeDurationMillisecondsValue;
	public float cameraShakeIntensity => cameraShakeIntensityValue;
}
