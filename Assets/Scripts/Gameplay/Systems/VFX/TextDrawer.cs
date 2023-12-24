using UnityEngine;

public class TextDrawer 
{
	private const string PARTICLES_PARENT_NAME = "ParticlesParent";
	
	private readonly TextDrawerConfig _config;
	private PoolObjects<TextHint> _dustDarkPool, _dustBrightPool, _dustCirclePool, _firePool, _waterPool, _grenadePool, _hintPool;
	private GameObject _particlesParent;
	private Camera _camera;
	private int _showTimeMilliseconds;
	public TextDrawer(TextDrawerConfig config) {
		_config = config;
		_showTimeMilliseconds = _config.showTimeMilliseconds;
	}

	public void Initialize() {
		_particlesParent = new GameObject(PARTICLES_PARENT_NAME);
		_hintPool = new PoolObjects<TextHint>(_config.textHintPrefab, _config.textHintPoolSize, true, _particlesParent.transform);
		_camera = Camera.main;
	}

	public void ShowHint(Vector3 position, string hintText) { 
		var hint = _hintPool.GetFreeElement();
		hint.transform.position = position;
		var camTransform = _camera.transform;
		Vector3 lookPos = hint.transform.position - _camera.transform.position ;
	    hint.transform.LookAt( lookPos );
		hint.Show(hintText, _showTimeMilliseconds);
	}

}
