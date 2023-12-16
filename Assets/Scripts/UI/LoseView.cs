using TMPro;
using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LoseView : AnimatedView {
	[SerializeField] private Button showAdsButton, continueButton, quitButton;
	[SerializeField] private int lifeAppearDelay = 550;
	[SerializeField] private int lifeDisappearDelay = 1000;
	[SerializeField] private TMP_Text livesCountText, lifeChangeText ;
	public event Action ShowAdsClickedEvent, ContinueClickedEvent, QuitClickedEvent;

	public bool continueWithAdsButtonVisible {
		set => showAdsButton.gameObject.SetActive(value);
	}

	public bool continueVisible {
		set => continueButton.gameObject.SetActive(value);
	}

	public int livesCount {
		set => livesCountText.text = value.ToString("D2");
	}

	private void Awake() {
		showAdsButton.onClick.AddListener(ShowAdsNotify);
		continueButton.onClick.AddListener(OnRestartClick);
		quitButton.onClick.AddListener(OnQuitClick);
	}

	public async void SetLivesWithAnimation(int countBefore, int countAfter, string changeText) {
		livesCount = countBefore;
		lifeChangeText.text = changeText;
		await UniTask.Delay(lifeAppearDelay);
		livesCount = countAfter;
		
		lifeChangeText.gameObject.SetActive(true);
		await UniTask.Delay(lifeDisappearDelay);
		lifeChangeText.gameObject.SetActive(false);
	}

	private void ShowAdsNotify() =>
			ShowAdsClickedEvent?.Invoke();

	private void OnQuitClick() =>
			QuitClickedEvent?.Invoke();

	private void OnRestartClick() =>
			ContinueClickedEvent?.Invoke();
			
}