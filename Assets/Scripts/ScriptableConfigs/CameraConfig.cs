using UnityEngine;

[CreateAssetMenu(menuName = "Configs/CameraConfig", fileName = "CameraConfig")]
public class CameraConfig : ScriptableObject
{
   	[SerializeField] private float cameraShakeIntensityValue;
   	[SerializeField] private int shakeDurationMillisecondsValue;
   
   	public int cameraShakeDurationMilliseconds => shakeDurationMillisecondsValue; 
   	public float cameraShakeIntensity => cameraShakeIntensityValue;
}
