using System.Linq;

public class InitialState : State
{
	private readonly UiFactory _uiFactory;
	private readonly CameraSystem _cameraSystem;
	private readonly ParticlesService _particlesService;
	private readonly FlyingCoinService _flyingCoinService;
	private readonly StarsSystem _starsSystem;
	private readonly Lose _lose;
	private readonly Win _win;
	private readonly HudSystem _hudSystem;
	private readonly PauseUiSystem _pauseUiSystem;

	public InitialState(
		StateSwitcher stateSwitcher,
		UiFactory uiFactory,
		CameraSystem cameraSystem,
		Lose lose,
		Win win,
		PauseUiSystem pauseUiSystem,
		HudSystem hudSystem,
		ParticlesService particlesService,
		FlyingCoinService flyingCoinService,
			StarsSystem starsSystem
	) : base(stateSwitcher) {

		_uiFactory = uiFactory;
		_cameraSystem = cameraSystem;
		_particlesService = particlesService;
		_flyingCoinService = flyingCoinService;
		_starsSystem = starsSystem;
		_lose = lose;
		_win = win;
		_pauseUiSystem = pauseUiSystem;
		_hudSystem = hudSystem;
	}

	public override void Enter() {
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

		_particlesService.Initialize();
		_flyingCoinService.Initialize();
		_cameraSystem.Initialize();
		
	}

	private void GotoLevelInitialize() {
		switcher.SetState<InitializeLevelState>();
	}
}