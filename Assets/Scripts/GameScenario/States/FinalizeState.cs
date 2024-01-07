public class FinalizeState : State {
	private readonly Gameplay _gameplay;
	private readonly SceneLoader _sceneLoader;
	private readonly LevelEventsHandler _levelEventsHandler;
	private readonly LevelVfx _levelVfx;

	public FinalizeState(
			StateSwitcher stateSwitcher,
			LevelEventsHandler levelEventsHandler,
			LevelVfx levelVfx,
			Gameplay gameplay,
			SceneLoader sceneLoader
	) : base(stateSwitcher) {
		_levelEventsHandler = levelEventsHandler;
		_levelVfx = levelVfx;
		_gameplay = gameplay;
		_sceneLoader = sceneLoader;
	}

	public override void Enter() {
		FinalizeInstances();
		GotoWorldMap();
	}

	private void FinalizeInstances() {
		_levelEventsHandler.Clear();
		_levelVfx.UnSubscribe();
		_gameplay.Dispose();
	}
	private void GotoWorldMap() =>
			_sceneLoader.GoToWorldMapScene();
}