using TMPro;
using UnityEngine;

public class MapHudView : MonoBehaviour,ICoinsTarget
{
   [SerializeField] private TMP_Text coinsText, livesText, startsText, timerText;
   public Transform coinsTargetTransform => coinsText.transform;
   public bool isTimerActive {
      set => timerText.enabled = value;
   }
   
   public string coinsCount {
      set => coinsText.text = value;
   }
   
   public string livesCount {
      set => livesText.text = value;
   }
   
   public string starsCount {
      set => startsText.text = value;
   }

   public string timesLeft {
      set => timerText.text = value;
   }

}