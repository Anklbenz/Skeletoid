using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseView : AnimatedView {
	[SerializeField] private Button resumeButton, restartButton, quitButton;

	public event Action RestartEvent, QuitEvent, ResumeEvent;

	private void Awake() {
		resumeButton.onClick.AddListener(OnResumeClick);
		restartButton.onClick.AddListener(OnRestartClick);
		quitButton.onClick.AddListener(OnQuitClick);
	}

	private void OnResumeClick() =>
			ResumeEvent?.Invoke();
	private void OnRestartClick() =>
			RestartEvent?.Invoke();
	private void OnQuitClick() =>
			QuitEvent?.Invoke();
}