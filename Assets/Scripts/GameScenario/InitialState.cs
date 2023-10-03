using System.Linq;

public class InitialState : State {
	private readonly UiFactory _uiFactory;
	private readonly ParticlesService _particlesService;
	private readonly FlyingCoinService _flyingCoinService;
	private readonly LoseSystem _loseSystem;
	private readonly WinSystem _winSystem;
	private readonly HudSystem _hudSystem;
	private readonly PauseUiSystem _pauseUiSystem;

	public InitialState(StateSwitcher stateSwitcher, UiFactory uiFactory, LoseSystem loseSystem, WinSystem winSystem, PauseUiSystem pauseUiSystem,HudSystem hudSystem, ParticlesService particlesService, FlyingCoinService flyingCoinService) : base(stateSwitcher) {
		_uiFactory = uiFactory;
		_particlesService = particlesService;
		_flyingCoinService = flyingCoinService;
		_loseSystem = loseSystem;
		_winSystem = winSystem;
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
		_loseSystem.Initialize(loseView);
		var winView = _uiFactory.CreateWinView();
		_winSystem.Initialize(winView);

		var pauseView = _uiFactory.CreatePauseView();
		pauseView.ForceClose();
		_pauseUiSystem.Initialize(pauseView);
		
		_particlesService.Initialize();
		_flyingCoinService.Initialize();
      
	}
	private void GotoLevelInitialize() {
		switcher.SetState<InitializeLevelState>();
	}
}