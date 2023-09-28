using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller, IInitializable
{
	[SerializeField] private GameplayConfig gameplayConfig;
	[SerializeField] private LoseConfig loseConfig;
	[SerializeField] private ParticlesConfig particlesConfig;
	[SerializeField] private CoinServiceConfig coinsConfig;
	[SerializeField] private WinView winViewPrefab;

	public override void InstallBindings() {
		InstallDiFactory();
		InstallGameplaySystem();
		InstallProgressData();
		InstallLoseSystem();
		InstallWinSystem();
		InstallParticlesService();
		InstallCoinService();
		InstallGameScenario();
		InstallInitializeForThis();
	}

	private void InstallProgressData() {
		Container.Bind<ProgressData>().AsSingle();
	}

	private void InstallLoseSystem() {
		Container.Bind<LoseConfig>().FromInstance(loseConfig).AsSingle();
		Container.Bind<LoseSystem>().AsSingle();
	}
	
	private void InstallWinSystem() {
		Container.Bind<WinView>().FromInstance(winViewPrefab).AsSingle();
		//Container.Bind<ProgressData>().AsSingle();
		Container.Bind<WinSystem>().AsSingle();
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
		Container.Bind<LoseState>().AsSingle();
		Container.Bind<WinState>().AsSingle();
		Container.Bind<GameScenario>().AsSingle();
	}

	private void InstallDiFactory() =>
		Container.Bind<IFactory>().To<DiFactory>().AsSingle();

	public void Initialize() {
		var gameScenario = Container.Resolve<GameScenario>();
		gameScenario.Start();
	}
}