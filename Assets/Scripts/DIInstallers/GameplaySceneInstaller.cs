using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller {
	[SerializeField] private StorageConfig storageConfig;
	[SerializeField] private KeyboardConfig keyboardConfig; 

	public override void InstallBindings() {
		InstallFactory();
		InstallResourcesFactory();
	//	Container.Bind<Paddle>().AsSingle();
		InstallInitialGameState();
		InstallPlayState();
		InstallGameScenario();
	}
	private void InstallPlayState() {
		Container.Bind<GameplayState>().AsSingle();
	}

	private void InstallGameScenario() {
		Container.Bind<GameScenario>().AsSingle().NonLazy();
	}

	private void InstallFactory() {
		Container.Bind<ResourcesLoader>().To<ResourcesLoader>().AsSingle();
		Container.Bind<IFactory>().To<Factory>().AsSingle();
	}
	
	private void InstallResourcesFactory() {
		Container.Bind<IResourcesFactory>().To<ResourcesFactory>().AsSingle();
	}
	
	private void InstallInitialGameState() {
		Container.Bind<StorageConfig>().FromInstance(storageConfig);
		Container.Bind<StateSwitcher>().AsSingle();
		Container.Bind<InitialState>().AsSingle();
	}
}