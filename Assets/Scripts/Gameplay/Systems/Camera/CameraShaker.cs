using Cinemachine;
using Cysharp.Threading.Tasks;

public class CameraShaker {
	private readonly CameraShakeConfig _config;
	private CinemachineVirtualCamera _mainCamera;
	private CinemachineVirtualCamera _zoomedCamera;
	private CinemachineBasicMultiChannelPerlin _basicMultiChannelPerlin;

	public CameraShaker(CameraShakeConfig config) {
		_config = config;
	}
	
	public void InitializeCamera(CinemachineVirtualCamera mainCamera) {
		_mainCamera = mainCamera;
		_basicMultiChannelPerlin = _mainCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
	}
	
	public async void Shake() {
		_basicMultiChannelPerlin.m_AmplitudeGain = _config.cameraShakeIntensity;
		await UniTask.Delay(_config.cameraShakeDurationMilliseconds);
		_basicMultiChannelPerlin.m_AmplitudeGain = 0;
	}
}
