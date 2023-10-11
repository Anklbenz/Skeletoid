using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WordMapItem : MonoBehaviour
{
    [SerializeField] private bool isUnlocked,isCompleted;
    [Range(0, 3)] [SerializeField] private int starsCount;
    [SerializeField] private int levelNumber;
    [SerializeField] private Image firstStarImage, secondStarImage, thirdStarImage, lockImage;
    [SerializeField] private TMP_Text levelNumberText, rankText;
    [SerializeField] private Transform content;

    public bool isLevelUnlocked {
        set
        {
            lockImage.enabled = !value;
            levelNumberText.enabled = value;
            content.gameObject.SetActive( value);
        }
    }

    public int levelStarsCount {
        set
        {
            firstStarImage.enabled = value >= 1 ;
            secondStarImage.enabled = value >= 2;
            thirdStarImage.enabled = value >= 3;
        }
    }

    public bool isLevelCompleted {
        set
        {
            var rank = value ? "30" :"?";
            rankText.text = $"Rank \n{rank}";
        }
    }

    private void OnValidate() {
        levelNumberText.text = levelNumber.ToString("D2");
        isLevelUnlocked = isUnlocked;
        levelStarsCount = starsCount;
        isLevelCompleted = isCompleted;
    }
}
