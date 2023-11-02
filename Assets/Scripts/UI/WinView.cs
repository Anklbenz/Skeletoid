using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinView : AnimatedView
{
	[SerializeField] private Button continueButton;
	[SerializeField] private TMP_Text coinsEarnedText, currentLevelText, totalLevelsText;
	public event Action ContinueEvent;

	public void SetCoinsCount(int coins) =>
		coinsEarnedText.text = coins.ToString("D2");

	public void SetCurrentLevelNumber(int current, int total) {
		//	currentLevelText.text = current.ToString();
	//	totalLevelsText.text = total.ToString();
	}

	private void Awake() =>
		continueButton.onClick.AddListener(OnContinueClick);

	private void OnContinueClick() =>
		ContinueEvent?.Invoke();
}
