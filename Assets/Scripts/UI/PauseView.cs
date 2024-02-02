using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseView : AnimatedView {
	[SerializeField] private Button resumeButton, restartButton, quitButton;
	[SerializeField] private Slider volumeSlider;

	public event Action<float> VolumeChangedEvent; 
	public event Action ContinueEvent, RestartEvent, QuitEvent;

	private void Awake() {
		resumeButton.onClick.AddListener(OnResumeClick);
		restartButton.onClick.AddListener(OnRestartClick);
		quitButton.onClick.AddListener(OnQuitClick);
		volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
	}
	private void OnVolumeChanged(float volumeValue) =>
			VolumeChangedEvent?.Invoke(volumeValue);
	private void OnResumeClick() =>
			ContinueEvent?.Invoke();
	private void OnRestartClick() =>
			RestartEvent?.Invoke();
	private void OnQuitClick() =>
			QuitEvent?.Invoke();
}