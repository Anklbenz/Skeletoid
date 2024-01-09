using Cinemachine;
using UnityEngine;

public class CameraZoom 
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
	
	private readonly CameraZoomConfig _zoomConfig;
	private CinemachineVirtualCamera _mainCamera;
	private CinemachineVirtualCamera _zoomedCamera;

	public CameraZoom(CameraZoomConfig zoomConfig) {
		_zoomConfig = zoomConfig;
	}
	
	public void InitializeCameras(CinemachineVirtualCamera mainCamera, CinemachineVirtualCamera zoomedCamera) {
		_mainCamera = mainCamera;
		_mainCamera.transform.position = _zoomConfig.mainPosition;
		_mainCamera.transform.rotation =Quaternion.Euler( _zoomConfig.mainRotation);
		_mainCamera.m_Lens.FieldOfView = _zoomConfig.mainCameraFOV;
		
		_zoomedCamera = zoomedCamera;
		_zoomedCamera.transform.position = _zoomConfig.zoomedPosition;
		_zoomedCamera.transform.rotation = Quaternion.Euler(_zoomConfig.zoomedRotation);
		_zoomedCamera.m_Lens.FieldOfView = _zoomConfig.zoomedCameraFOV;
		_zoomedCamera.enabled = false;
	}
	
}
