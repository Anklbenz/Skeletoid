using System;
using TMPro;
using UnityEngine;

public class MapHudView : MonoBehaviour, IFlyingTarget
{
   [SerializeField] private TMP_Text coinsText, livesText, startsText, timerText;
   [SerializeField] private Animator keysAnimator, coinAnimator;
   [SerializeField] private string trigger = "Play";
   [SerializeField] private float speed = 0.8f;
   public Transform coinsTargetTransform => coinsText.transform;

   private bool _isKeyInitialized, _isCoinsInitialized;

   private void Awake() {
      keysAnimator.speed = speed;
   }

   public bool isTimerActive {
      set => timerText.enabled = value;
   }

   public string timeLeft {
      set => timerText.text = value;
   }

   public string coinsCount {
	   set {
		   if (_isCoinsInitialized)
			   coinAnimator.SetTrigger(trigger);
		   else
			   _isCoinsInitialized = true;

		   coinsText.text = value;
	   }
   }

   public string keysCount {
	   set {
		   if (_isKeyInitialized)
			   keysAnimator.SetTrigger(trigger);
		   else
			   _isKeyInitialized = true;
		   
		   livesText.text = value;
	   }
   }

   public string starsCount {
      set => startsText.text = value;
   }

}