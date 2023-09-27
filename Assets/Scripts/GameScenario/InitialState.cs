using System.Linq;

public class InitialState : State {
	private readonly GameplaySystem _gameplaySystem;
	private readonly ParticlesService _particlesService;
	private CoinService _coinService;
	
	public InitialState(StateSwitcher stateSwitcher, GameplaySystem gameplaySystem, ParticlesService particlesService, CoinService coinService) : base(stateSwitcher) {
		_gameplaySystem = gameplaySystem;
		_particlesService = particlesService;
		_coinService = coinService;
	}

	public override void Enter() {
		Initialize();
		SwitchToGameplay();
	}
	private void Initialize() {
		_gameplaySystem.Initialize();
		_particlesService.Initialize();
		_coinService.Initialize();
	}

	private void SwitchToGameplay() {
		switcher.SetState<GameState>();
	}
}