using System.Linq;

public class InitialState : State {
	private readonly GameplaySystem _gameplaySystem;
	private readonly ParticlesService _particlesService;
	private readonly CoinService _coinService;
	private readonly LoseSystem _loseSystem;
	private readonly WinSystem _winSystem;
	
	public InitialState(StateSwitcher stateSwitcher, GameplaySystem gameplaySystem, LoseSystem loseSystem, WinSystem winSystem, ParticlesService particlesService, CoinService coinService) : base(stateSwitcher) {
		_gameplaySystem = gameplaySystem;
		_particlesService = particlesService;
		_coinService = coinService;
		_loseSystem = loseSystem;
		_winSystem = winSystem;
	}

	public override void Enter() {
		Initialize();
		SwitchToGameplay();
	}
	private void Initialize() {
		
		// Create canvas - distribute to others here
		_gameplaySystem.Initialize();
		_particlesService.Initialize();
		_coinService.Initialize();
		_loseSystem.Initialize();
		_winSystem.Initialize();
	}

	private void SwitchToGameplay() {
		switcher.SetState<GameState>();
	}
}