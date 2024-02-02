using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public class HudView : View {
	[Header("HudView")]
	[SerializeField] private Animator coinsAnimator;
	[SerializeField] private string playTrigger = "Play";
	[SerializeField] private TMP_Text skullsCountText, coinsCountText;
	[SerializeField] private Button pauseButton;
	[SerializeField] private AnimatedView hudParent;
	[SerializeField] private View trainingTouchAnimation, trainingKeyboardAnimation;

	public event Action PauseClickedEvent;
	public Transform coinsTransform => coinsCountText.transform;
	private View _currentInputTrainingView;

	public void SetCurrentTrainingAnimation(bool isMobile) {
		_currentInputTrainingView = isMobile ? trainingTouchAnimation : trainingKeyboardAnimation;
	}

	public string skullsCount {
		get => skullsCountText.text;
		set => skullsCountText.text = $"x {value}";
	}

	public string coinsCount {
		get => coinsCountText.text;
		set => coinsCountText.text = $"x {value}";
	}

	public bool isTrainingVisible {
		set {
			if (value)
				_currentInputTrainingView.Open();
			else
				_currentInputTrainingView.Close();
		}
	}
	
	public bool isHudVisible {
		set {
			if (value)
				hudParent.Open();
			else
				hudParent.Close();
		}
		get => hudParent.gameObject.activeInHierarchy;
	}
	private void Awake() =>
			pauseButton.onClick.AddListener(PauseClickNotify);

	private void PauseClickNotify() =>
			PauseClickedEvent?.Invoke();

	public void CoinsAnimationPlay() {
		var closeHash = Animator.StringToHash(playTrigger);
		coinsAnimator.SetTrigger(closeHash);
	}
}