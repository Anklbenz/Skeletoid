using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudView : AnimatedView
{
	[SerializeField] private TMP_Text skullsCountText;
	[SerializeField] private TMP_Text coinsCountText;
	[SerializeField] private Button  pauseButton,muteButton;
	public Transform coinsTransform => coinsCountText.transform;
	public event Action PauseClickedEvent;

	public string skullsCount {
		get => skullsCountText.text;
		set => skullsCountText.text = value;
	}

	public string coinsCount {
		get => coinsCountText.text;
		set => coinsCountText.text = value;
	}

	private void Awake() =>
		pauseButton.onClick.AddListener(PauseClickNotify);

	private void PauseClickNotify() =>
		PauseClickedEvent?.Invoke();
}