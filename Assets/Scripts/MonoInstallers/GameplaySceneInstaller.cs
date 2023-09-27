using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller, IInitializable {
	[SerializeField] private GameplayConfig gameplayConfig;
	[SerializeField] private ParticlesConfig particlesConfig;
	[SerializeField] private CoinServiceConfig coinsConfig;
	
	public override void InstallBindings() {
		InstallDiFactory();
	   InstallGameplaySystem();
		InstallParticlesService();
		InstallCoinService();
		
		InstallGameScenario();
		InstallInitializeForThis();
	}
	private void InstallCoinService() {
		Container.Bind<CoinServiceConfig>().FromInstance(coinsConfig).AsSingle();
		Container.Bind<CoinService>().AsSingle();
	}
	private void InstallGameplaySystem() {
		Container.Bind<GameplayConfig>().FromInstance(gameplayConfig).AsSingle();
		Container.Bind<GameplaySystem>().AsSingle();
	}

	private void InstallParticlesService() {
		Container.Bind<ParticlesConfig>().FromInstance(particlesConfig);
		Container.Bind<ParticlesService>().AsSingle();
	}
	
	private void InstallInitializeForThis() =>
		Container.Bind<IInitializable>().FromInstance(this).AsSingle();

	private void InstallGameScenario() {
		Container.Bind<StateSwitcher>().AsSingle();
		Container.Bind<InitialState>().AsSingle();
		Container.Bind<GameState>().AsSingle();
		Container.Bind<GameScenario>().AsSingle();
	}
	
	private void InstallDiFactory() =>
		Container.Bind<IFactory>().To<DiFactory>().AsSingle();
	
	public void Initialize() {
		var gameScenario = Container.Resolve<GameScenario>();
		gameScenario.Start();
	}
}