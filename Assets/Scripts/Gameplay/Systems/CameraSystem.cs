using Cinemachine;
using Cysharp.Threading.Tasks;

public class CameraSystem 
{
   private readonly CameraConfig _config;
   private readonly CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;

   public CameraSystem(CameraConfig config, CinemachineVirtualCamera camera) {
      _config = config;
      _cinemachineBasicMultiChannelPerlin = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
   }

   public async void Shake() {
      _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = _config.cameraShakeIntensity;
      await UniTask.Delay(_config.cameraShakeDurationMilliseconds);
      _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
   }
}
