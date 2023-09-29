using UnityEngine;
[CreateAssetMenu(fileName = "GameplayUIConfig", menuName = "Configs/GameplayUIConfig")]
public class UiFactoryConfig : ScriptableObject {
	[SerializeField] private Canvas parentedCanvas;
	[SerializeField] private LoseView loseView;
	[SerializeField] private PauseView pauseView;
	[SerializeField] private WinView winView;
	[SerializeField] private HudView hudView;

	public Canvas canvasPrefab => parentedCanvas;
	public LoseView loseViewPrefab => loseView;
	public PauseView pauseViewPrefab => pauseView;
	public WinView winViewPrefab => winView;
	public HudView hudViewPrefab => hudView;
}
