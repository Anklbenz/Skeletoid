using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller, IInitializable {
	[SerializeField] private GameObjectsConfig gameObjectsConfig;
	[SerializeField] private ParticlesConfig particlesConfig;
	
	public override void InstallBindings() {
		InstallFactoryDI();
	//	InstallFactory();
		InstallParticlesFactory();
		InstallGameScenario();
		InstallInitializeForThis();
	}

	private void InstallParticlesFactory() {
		Container.Bind<ParticlesConfig>().FromInstance(particlesConfig);
		Container.Bind<ParticlesFactory>().AsSingle();
	}

	/*private void InstallFactory() {
		Container.Bind<Factory>().AsSingle();
	}*/

	private void InstallInitializeForThis() =>
		Container.Bind<IInitializable>().FromInstance(this).AsSingle();

	private void InstallGameScenario() {
		Container.Bind<StateSwitcher>().AsSingle();
		Container.Bind<CurrentGameplayData>().AsSingle();
		Container.Bind<BallSystem>().AsSingle();
		Container.Bind<GameObjectsConfig>().FromInstance(gameObjectsConfig);
		Container.Bind<InitialState>().AsSingle();
		Container.Bind<GameplayState>().AsSingle();
		Container.Bind<GameScenario>().AsSingle();
	}
	
	private void InstallFactoryDI() =>
		Container.Bind<IFactory>().To<DIFactory>().AsSingle();
	
	public void Initialize() {
		var gameScenario = Container.Resolve<GameScenario>();
		gameScenario.Start();
	}
}