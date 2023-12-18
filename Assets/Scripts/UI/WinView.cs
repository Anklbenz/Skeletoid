using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinView : AnimatedView {
	[SerializeField] private Button continueButton;
	[SerializeField] private TMP_Text levelTimeText;
	[SerializeField] private GameObject star1Object, star2Object, star3Object;
	[SerializeField] private int starsAppearDelay;

	public event Action ContinueEvent;
	
	public string levelTime {
		set => levelTimeText.text =value;
	}
	
	public async void ShowStarsCount(int starsCount) {
		if (starsCount >= 1)
			await ShowStar(star1Object);
		if (starsCount >= 2)
			await ShowStar(star2Object);
		if (starsCount >= 3)
			await ShowStar(star3Object);
	}

	private async UniTask ShowStar(GameObject starGameObject) {
		await UniTask.Delay(starsAppearDelay);
		if(starGameObject == null) return;
		
		starGameObject.SetActive(true);
	}

	private void Awake() {
		continueButton.onClick.AddListener(OnContinueClick);
		star1Object.SetActive(false);
		star2Object.SetActive(false);
		star3Object.SetActive(false);
	}

	private void OnContinueClick() =>
			ContinueEvent?.Invoke();
}