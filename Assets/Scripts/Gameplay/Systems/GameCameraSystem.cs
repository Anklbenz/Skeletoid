using Cinemachine;
using Cysharp.Threading.Tasks;

public class GameCameraSystem 
{
   private readonly CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;

   public GameCameraSystem(CinemachineVirtualCamera camera) {
      _cinemachineBasicMultiChannelPerlin = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
   }

   public async void Shake(float intensity, int durationMilliseconds) {
      _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
      await UniTask.Delay(durationMilliseconds);
      _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
   }
}
