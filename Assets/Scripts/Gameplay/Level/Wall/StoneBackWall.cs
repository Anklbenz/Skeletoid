using UnityEngine;
using Cysharp.Threading.Tasks;
public class StoneBackWall : MonoBehaviour {
	private const int BEFORE_PAUSE_TIME = 1800;
	private const int AFTER_PAUSE_TIME = 1000;
	
	[SerializeField] private ParticleSystem stoneParticles;
	public Wall wall;
	private bool _isShowed;

	public async void Show() {
		stoneParticles.Play();
		gameObject.SetActive(true);
		await UniTask.Delay(BEFORE_PAUSE_TIME);
		stoneParticles.Pause();
	}

	public async void Hide() {
		stoneParticles.Play();
		await UniTask.Delay(AFTER_PAUSE_TIME);
		gameObject.SetActive(false);
		stoneParticles.Stop();
	}
}