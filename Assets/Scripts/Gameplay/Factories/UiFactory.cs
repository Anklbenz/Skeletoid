using UnityEngine;

public class UiFactory {
	private readonly IFactory _factory;
	private readonly UiFactoryConfig _config;
	private Transform _parent;

	public UiFactory(UiFactoryConfig config, IFactory factory) {
		_config = config;
		_factory = factory;
	}

	public void Initialize() =>
			_parent = _factory.Create(_config.canvasPrefab).transform;

	public LoseView CreateLoseView() =>
			CreateStretched(_config.loseViewPrefab);

	public WinView CreateWinView() =>
			CreateStretched(_config.winViewPrefab);

	public PauseView CreatePauseView() =>
			CreateStretched(_config.pauseViewPrefab);

	public HudView CreateHudView() =>
			CreateStretched(_config.hudViewPrefab);


	private T CreateStretched<T>(T prefab) where T : Component {
		var view = _factory.Create<T>(prefab, _parent);
		var rectTransform = (RectTransform)view.transform;
		rectTransform.offsetMax = Vector2.zero;
		rectTransform.offsetMin = Vector2.zero;
		return view;
	}
}