using System.Linq;
using UnityEngine;

public class InitialState : State {
	private readonly UiFactory _uiFactory;
	private readonly CameraSystem _cameraSystem;
	private readonly ParticlesPlayer _particlesPlayer;
	private readonly FlyingService _flyingService;
	private readonly StarsSystem _starsSystem;
	private readonly Lose _lose;
	private readonly Win _win;
	private readonly HudSystem _hudSystem;
	private readonly PauseUiSystem _pauseUiSystem;
	private Camera _cameraMain;

	public InitialState(
			StateSwitcher stateSwitcher,
			UiFactory uiFactory,
			CameraSystem cameraSystem,
			Lose lose,
			Win win,
			PauseUiSystem pauseUiSystem,
			HudSystem hudSystem,
			ParticlesPlayer particlesPlayer,
			FlyingService flyingService,
			StarsSystem starsSystem) : base(stateSwitcher) {

		_uiFactory = uiFactory;
		_cameraSystem = cameraSystem;
		_particlesPlayer = particlesPlayer;
		_flyingService = flyingService;
		_starsSystem = starsSystem;
		_lose = lose;
		_win = win;
		_pauseUiSystem = pauseUiSystem;
		_hudSystem = hudSystem;
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
		_pauseUiSystem.Initialize(pauseView);

		_particlesPlayer.Initialize();
		_cameraSystem.Initialize();
	    
		_flyingService.Initialize();
		var coinsPosition = _hudSystem.coinsTargetTransform.position;
		_flyingService.destination = _cameraMain.ScreenToWorldPoint(new Vector3(coinsPosition.x, coinsPosition.y, _cameraMain.nearClipPlane));

	}

	private void GotoLevelInitialize() {
		switcher.SetState<InitializeLevelState>();
	}
}