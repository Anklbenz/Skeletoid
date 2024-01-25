using Cysharp.Threading.Tasks;
using UnityEngine;

public class Sound : MonoBehaviour {
	[SerializeField] private AudioSource audioSource;

	public AudioSource source => audioSource;
	public bool isEnabled {
		get => gameObject.activeInHierarchy;
		set => gameObject.SetActive(value);
	}
	
	public async void PlayAndDisable(AudioClip clip, float pitch = 1) {
		var clipLengthMilliseconds = (clip.length / Mathf.Abs(audioSource.pitch)) * 1000;
		Play(clip, pitch);
		await UniTask.Delay((int)clipLengthMilliseconds);
		Stop();
	}

	public void PlayLooped(AudioClip clip, float pitch = 1) {
		audioSource.loop = true;
		Play(clip, pitch);
	}

	public void Play(AudioClip clip, float pitch = 1) {
		audioSource.pitch = pitch;
		audioSource.clip = clip;
		audioSource.Play();
	}

	public void Stop() {
		audioSource.Stop();
		audioSource.clip = null;
		audioSource.loop = false;
		audioSource.pitch = 1;

		gameObject.SetActive(false);
	}
}