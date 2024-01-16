using UnityEngine;
using Cysharp.Threading.Tasks;
using Zenject;
public class StoneBackWall : MonoBehaviour, IPauseSensitive {
	private const int BEFORE_PAUSE_TIME = 1800;
	private const int AFTER_PAUSE_TIME = 1000;

	[SerializeField] private ParticleSystem stoneParticles;

	public Wall wall;
	private bool _isActive;
	private Timer _timer;

	[Inject]
	public void Construct(Timer timer) =>
			_timer = timer;
	public void Activate(float duration) {
		if (_isActive) return;
		_isActive = true;

		wall.gameObject.SetActive(true);
		PlayAppearEffect();

		_timer.StartWithAlarm(duration);
		_timer.AlarmEvent += Deactivate;
	}

	private void Deactivate() {
		_timer.AlarmEvent -= Deactivate;

		PlayDisappearEffect();
		wall.gameObject.SetActive(false);
		_isActive = false;
	}
	private async void PlayDisappearEffect() {
		//Show wall disappear
		stoneParticles.Play();
		await UniTask.Delay(AFTER_PAUSE_TIME);
		//stop Particles
		stoneParticles.Stop();
	}
	private async void PlayAppearEffect() {
		//Show wall appear particles
		stoneParticles.Play();
		await UniTask.Delay(BEFORE_PAUSE_TIME);
		//Pause particles On Wall ready
		stoneParticles.Pause();
	}
	public void SetPause(bool isPaused) {
		if (_timer.isRunning && isPaused)
			_timer.Pause();
		else
			_timer.Run();
	}
}