using UnityEngine;

public class SoundsPlayer {
	private const float MIN_PINCH = 0.8f;
	private const float MAX_PINCH = 1.2f;

	private readonly SoundConfig _soundConfig;
	private PoolObjects<Sound> _poolOfSound;
	private Transform _soundsParent;

	private float randomPinchValue => Random.Range(MIN_PINCH, MAX_PINCH);

	public SoundsPlayer(SoundConfig soundConfig) =>
			_soundConfig = soundConfig;

	public void Initialize() {
		_soundsParent = new GameObject(nameof(_soundsParent)).transform;
		_poolOfSound = new PoolObjects<Sound>(_soundConfig.soundPrefab, _soundConfig.soundsPoolAmount, true, _soundsParent);
	}

	public void PlayWoodHit() {
		var sound = _poolOfSound.GetFreeElement();
		sound.PlayAndDisable(_soundConfig.woodHitClip, randomPinchValue);
	}

	public void PlaySteelHit() {
		var sound = _poolOfSound.GetFreeElement();
		sound.PlayAndDisable(_soundConfig.steelHitClip, randomPinchValue);
	}

	public void PlayPaddleHit() {
		var sound = _poolOfSound.GetFreeElement();
		sound.PlayAndDisable(_soundConfig.paddleHitClip, randomPinchValue);
	}

	public void PlaySkeletonHit() {
		var sound = _poolOfSound.GetFreeElement();
		sound.PlayAndDisable(_soundConfig.skeletonHitClip, randomPinchValue);
	}

	public void PlayWallHit() {
		var sound = _poolOfSound.GetFreeElement();
		sound.PlayAndDisable(_soundConfig.wallHitClip, randomPinchValue);
	}
}
