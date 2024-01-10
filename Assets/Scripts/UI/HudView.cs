using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudView : AnimatedView {
	[Header("HudView")]
	[SerializeField] private Animator coinsAnimator;
	[SerializeField] private string playTrigger = "Play";
	[SerializeField] private TMP_Text skullsCountText;
	[SerializeField] private TMP_Text coinsCountText;
	[SerializeField] private Button pauseButton, muteButton;
	[SerializeField] private Animator trainingAnimator;
	[SerializeField] private string activateTrigger = "activate";
	public Transform coinsTransform => coinsCountText.transform;
	public event Action PauseClickedEvent;

	public string skullsCount {
		get => skullsCountText.text;
		set => skullsCountText.text = $"x {value}";
	}

	public string coinsCount {
		get => coinsCountText.text;
		set => coinsCountText.text = $"x {value}";
	}
	private int playHash => Animator.StringToHash(activateTrigger);

	public void PlayTrainingAnimation() {
		trainingAnimator.SetTrigger(playHash);
	}

	private void Awake() {
		pauseButton.onClick.AddListener(PauseClickNotify);
	}

	private void PauseClickNotify() =>
			PauseClickedEvent?.Invoke();

	public void CoinsAnimationPlay() {
		var closeHash = Animator.StringToHash(playTrigger);
		coinsAnimator.SetTrigger(closeHash);
	}
}