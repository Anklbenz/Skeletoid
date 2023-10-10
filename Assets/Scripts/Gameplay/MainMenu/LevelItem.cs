using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelItem : MonoBehaviour
{
    [SerializeField] private bool isUnlocked;
    [Range(0, 3)] [SerializeField] private int starsCount;
    [SerializeField] private int levelNumber;
    [SerializeField] private Image firstStarImage, secondStarImage, thirdStarImage, lockImage;
    [SerializeField] private TMP_Text levelNumberText;

    public bool isLevelUnlocked {
        set
        {
            lockImage.enabled = !value;
            levelNumberText.enabled = value;
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

    private void OnValidate() {
        levelNumberText.text = levelNumber.ToString("D2");
        isLevelUnlocked = isUnlocked;
        levelStarsCount = starsCount;
    }
}
