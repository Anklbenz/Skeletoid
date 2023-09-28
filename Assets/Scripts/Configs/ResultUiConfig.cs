using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GameplayUIConfig",menuName = "Configs/GameplayUIConfig")]
public class ResultUiConfig : ScriptableObject {
	[SerializeField] private Canvas parentedCanvas;
	[SerializeField] private LoseView loseView;
	[SerializeField] private PauseView pauseView;
	[SerializeField] private WinView winView;

	public Canvas canvasPrefab => parentedCanvas;
	public LoseView loseViewPrefab => loseView;
	public PauseView pauseViewPrefab => pauseView;
	public WinView winViewPrefab => winView;
}
