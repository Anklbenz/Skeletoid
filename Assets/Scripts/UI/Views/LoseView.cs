using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public class LoseView : AnimatedView {
	[SerializeField] private Button showAdsButton, restartButton, quitButton;
	[SerializeField] private TMP_Text someStatText;
	public event Action ShowAdsEvent, RestartEvent, QuitEvent;

	public bool restartEnabled {
		get => restartButton.interactable;
		set => restartButton.interactable = value;
	}

	public void Set(string info) {
		someStatText.text = info;
	}

	private void Awake() {
		showAdsButton.onClick.AddListener(ShowAdsNotify);
		restartButton.onClick.AddListener(OnRestartClick);
		quitButton.onClick.AddListener(OnQuitClick);
	}
	private void ShowAdsNotify() =>
			ShowAdsEvent?.Invoke();

	private void OnQuitClick() =>
			QuitEvent?.Invoke();

	private void OnRestartClick() =>
			RestartEvent?.Invoke();
}