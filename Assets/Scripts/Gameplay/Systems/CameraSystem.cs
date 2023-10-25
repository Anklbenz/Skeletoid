using Cinemachine;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CameraSystem
{
   public Transform zoomedLookAt {
      set => _zoomedCamera.LookAt = value;
   }

   public bool zoomedIsActive {
      set
      {
         _mainCamera.enabled = (!value);
         _zoomedCamera.enabled = value;
      }
   }

   public Transform zoomedCamTransform => _zoomedCamera.transform;

   private readonly CameraConfig _config;
   private CinemachineVirtualCamera _mainCamera;
   private CinemachineVirtualCamera _zoomedCamera;
   private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;

   public CameraSystem(CameraConfig config) {
      _config = config;
   }
   
   public void Initialize() {
      _mainCamera = Object.Instantiate(_config.mainCameraPrefab, _config.mainPosition, Quaternion.Euler(_config.mainRotation));
      _cinemachineBasicMultiChannelPerlin = _mainCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

      _zoomedCamera = Object.Instantiate(_config.zoomedCameraPrefab, _config.zoomedPosition, Quaternion.Euler(_config.zoomedRotation));
    
      _zoomedCamera.m_Lens.FieldOfView = _config.zoomedCameraFOV;
      _zoomedCamera.enabled = false;

   }
   public void Initialize(CinemachineVirtualCamera mainCamera) {
      _mainCamera = mainCamera;
      _cinemachineBasicMultiChannelPerlin = _mainCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
   }
   public async void Shake() {
      _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = _config.cameraShakeIntensity;
      await UniTask.Delay(_config.cameraShakeDurationMilliseconds);
      _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
   }
}
