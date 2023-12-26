using UnityEngine;

public class InitialState : State
{
	private readonly UiFactory _uiFactory;
	private readonly FlyingService _flyingService;
	private readonly Lose _lose;
	private readonly Win _win;
	private readonly HudSystem _hudSystem;
	private readonly LevelVfx _levelVfx;
	private readonly Pause _pause;
	private Camera _cameraMain;

	public InitialState(
		StateSwitcher stateSwitcher,
		UiFactory uiFactory,
		Lose lose,
		Win win,
		Pause pause,
		HudSystem hudSystem,
		LevelVfx levelVfx,
		FlyingService flyingService) : base(stateSwitcher) {

		_uiFactory = uiFactory;
		_levelVfx = levelVfx;
		_flyingService = flyingService;
		_lose = lose;
		_win = win;
		_pause = pause;
		_hudSystem = hudSystem;
		_levelVfx = levelVfx;
	}

	public override void Enter() {
		_cameraMain = Camera.main;
		Initialize();
		GotoLevelInitialize();
	}

	private void Initialize() {
		_uiFactory.Initialize();
		var hudView = _uiFactory.CreateHudView();
		_hudSystem.Initialize(hudView);
		var loseView = _uiFactory.CreateLoseView();
		_lose.Initialize(loseView);
		var winView = _uiFactory.CreateWinView();
		_win.Initialize(winView);

		var pauseView = _uiFactory.CreatePauseView();
		pauseView.ForceClose();
		_pause.Initialize(pauseView);

		_levelVfx.Initialize();

		_flyingService.Initialize();
		var coinsPosition = _hudSystem.coinsTargetTransform.position;
		_flyingService.destination = _cameraMain.ScreenToWorldPoint(new Vector3(coinsPosition.x, coinsPosition.y, _cameraMain.nearClipPlane));

	}

	private void GotoLevelInitialize() {
		switcher.SetState<InitializeLevelState>();
	}
}