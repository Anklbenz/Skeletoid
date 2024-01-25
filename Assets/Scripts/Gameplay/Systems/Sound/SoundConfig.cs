using UnityEngine;

[CreateAssetMenu(menuName = "Configs/SoundConfig", fileName = "SoundConfig", order = 0)]
public class SoundConfig : ScriptableObject {
	[SerializeField] private Sound sound;
	[SerializeField] private int poolAmount;
	[SerializeField] private AudioClip
			woodHit,
			steelHit,
			skeletonHit,
			wallHit,
			paddleHit;

	public AudioClip woodHitClip => woodHit;
	public AudioClip steelHitClip => steelHit;
	public AudioClip skeletonHitClip => skeletonHit;
	public AudioClip wallHitClip => wallHit;
	public AudioClip paddleHitClip => paddleHit;

	public Sound soundPrefab => sound;
	public int soundsPoolAmount => poolAmount;
}