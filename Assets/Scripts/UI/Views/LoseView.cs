using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public class LoseView : AnimatedView
{
	[SerializeField] private Button showAdsButton, restartButton, quitButton;
	[SerializeField] private TMP_Text skullsCountText;
	public event Action ShowAdsEvent, RestartEvent, QuitEvent;

	public bool showAdsButtonVisible {
		set => showAdsButton.gameObject.SetActive(value);
	}

	public bool restartInteractable {
		set => restartButton.interactable = value;
	}

	private void Awake() {
		showAdsButton.onClick.AddListener(ShowAdsNotify);
		restartButton.onClick.AddListener(OnRestartClick);
		quitButton.onClick.AddListener(OnQuitClick);
	}

	public void SetSkullsCount(int skulls) =>
		skullsCountText.text = skulls.ToString("D2");

	private void ShowAdsNotify() =>
		ShowAdsEvent?.Invoke();

	private void OnQuitClick() =>
		QuitEvent?.Invoke();

	private void OnRestartClick() =>
		RestartEvent?.Invoke();
}