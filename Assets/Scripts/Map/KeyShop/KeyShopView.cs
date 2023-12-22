using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyShopView : AnimatedView {
	private const float NON_INTERACTABLE_ALPHA = 0.3f;
	[SerializeField] private TMP_Text coinsCountText, keysCountText, timeLeftToRecoveryText, goldPriceText;
	[SerializeField] private Button buyForGoldButton, buyForAdsButton, closeButton;
	[SerializeField] private CanvasGroup canvasGroup;

	public event Action BuyForGoldEvent, BuyForAdsEvent, CloseEvent;

	public string coinsCount {
		set => coinsCountText.text = value;
	}

	public string keysCount {
		set => keysCountText.text = value;
	}

	public string timeLeftToRecovery {
		set => timeLeftToRecoveryText.text = value;
	}

	public string goldPrice {
		set => goldPriceText.text = value;
	}

	public bool buyForGoldInteractable {
		set {
			canvasGroup.interactable = value;
			canvasGroup.alpha = value ? 1 : NON_INTERACTABLE_ALPHA;
		}
	}

	private void Awake() {
		buyForAdsButton.onClick.AddListener(BuyForAdsNotify);
		buyForGoldButton.onClick.AddListener(BuyForGoldNotify);
		closeButton.onClick.AddListener(CloseNotify);
	}
	private void BuyForGoldNotify() =>
			BuyForGoldEvent?.Invoke();

	private void BuyForAdsNotify() =>
			BuyForAdsEvent?.Invoke();

	private void CloseNotify() =>
			CloseEvent?.Invoke();
}