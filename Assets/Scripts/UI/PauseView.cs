using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseView : AnimatedView {
	[SerializeField] private Button resumeButton, restartButton, quitButton;
	[SerializeField] private TMP_Text currentLevelText, totalLevelsText;
	public event Action ContinueEvent, RestartEvent, QuitEvent;

	private void Awake() {
		resumeButton.onClick.AddListener(OnResumeClick);
		restartButton.onClick.AddListener(OnRestartClick);
		quitButton.onClick.AddListener(OnQuitClick);
	}
	
	public void SetCurrentLevelNumber(int current, int total) {
		currentLevelText.text = current.ToString();
		totalLevelsText.text = total.ToString();
	}
	
	private void OnResumeClick() =>
			ContinueEvent?.Invoke();
	private void OnRestartClick() =>
			RestartEvent?.Invoke();
	private void OnQuitClick() =>
			QuitEvent?.Invoke();
}