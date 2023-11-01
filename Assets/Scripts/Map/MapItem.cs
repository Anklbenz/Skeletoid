using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapItem : MonoBehaviour
{
    [SerializeField] private bool isUnlocked, isCompleted;
    [Range(0, 3)] [SerializeField] private int starsCount;
    [SerializeField] private int worldIndex;
    [SerializeField] private Image firstStarImage, secondStarImage, thirdStarImage, lockImage;
    [SerializeField] private TMP_Text levelNumberText, rankText;
    [SerializeField] private Transform content;
    [SerializeField] private Button startButton;
    [SerializeField] private Image startButtonImage;
    [SerializeField] private Material highLightMaterial;
    public event Action<int> StartEvent;

    public bool isHighlight {
        set => startButtonImage.material = value ? highLightMaterial : null;
    } 

    public Transform dustParticlesTransform => lockImage.transform;

    public int levelsCount;

    private void Awake() {
        startButton.onClick.AddListener(StartClickNotify);
        levelNumberText.text = (worldIndex + 1).ToString("D2");
        isLevelUnlocked = isUnlocked;
        levelStarsCount = starsCount;
        isLevelCompleted = isCompleted;
    }

    private void StartClickNotify() {
        StartEvent?.Invoke(worldIndex);
    }

    public bool isLevelUnlocked {
        set
        {
            lockImage.enabled = !value;
            levelNumberText.enabled = value;
            content.gameObject.SetActive(value);
        }
    }

    public int levelStarsCount {
        set
        {
            firstStarImage.enabled = value >= 1;
            secondStarImage.enabled = value >= 2;
            thirdStarImage.enabled = value >= 3;
        }
    }

    public bool isLevelCompleted {
        get => isCompleted;
        set
        {
            var rank = value ? "30" : "?";
            rankText.text = $"Rank \n{rank}";
            isCompleted = value;
        }
    }
}