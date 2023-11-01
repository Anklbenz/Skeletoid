using System;
using TMPro;
using UnityEngine;

public class MapHudView : MonoBehaviour, ICoinsTarget
{
   [SerializeField] private TMP_Text coinsText, livesText, startsText, timerText;
   [SerializeField] private Animator keysAnimator;
   [SerializeField] private string trigger = "Play";
   [SerializeField] private float speed = 0.8f;
   public Transform coinsTargetTransform => coinsText.transform;

   private bool _isKeyInitialize;

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
      set => coinsText.text = value;
   }

   public string keysCount {
      set {
         if(_isKeyInitialize)
            keysAnimator.SetTrigger(trigger);
         livesText.text = value;
         _isKeyInitialize = true;
      }
   }

   public string starsCount {
      set => startsText.text = value;
   }

}