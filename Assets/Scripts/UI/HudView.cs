using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public class HudView : View {
	[Header("HudView")]
	[SerializeField] private Animator coinsAnimator;
	[SerializeField] private string playTrigger = "Play";
	[SerializeField] private TMP_Text skullsCountText, coinsCountText;
	[SerializeField] private Button pauseButton, muteButton;
	[SerializeField] private Animator trainingAnimator;
	[SerializeField] private string activateTrigger = "activate";
//	[SerializeField] private int trainingAnimationDurationMilliseconds = 3600;
	[SerializeField] private AnimatedView hudParent, trainingAnimationParent;
	[SerializeField] private Button trainingRejectButton;

	public event Action PauseClickedEvent, TrainRejectedEvent;
	public Transform coinsTransform => coinsCountText.transform;
//	public int trainingDuration => trainingAnimationDurationMilliseconds;

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
				trainingAnimationParent.Open();
			else
				trainingAnimationParent.Close();
		}

		get => trainingAnimationParent.gameObject.activeInHierarchy;
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
	//private int playHash => Animator.StringToHash(activateTrigger);

	/*public void StartTrainingAnimation() {
		trainingAnimationParent
		
	//	trainingAnimator.SetTrigger(playHash);
	}*/

	private void Awake() {
		pauseButton.onClick.AddListener(PauseClickNotify);
		trainingRejectButton.onClick.AddListener(TrainingRejectNotify);
	}
	private void TrainingRejectNotify() =>
			TrainRejectedEvent?.Invoke();
	private void PauseClickNotify() =>
			PauseClickedEvent?.Invoke();

	public void CoinsAnimationPlay() {
		var closeHash = Animator.StringToHash(playTrigger);
		coinsAnimator.SetTrigger(closeHash);
	}
}