using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller {
	[SerializeField] private StorageConfig storageConfig;

	public override void InstallBindings() {
		InstallFactory();
		InstallResourcesFactory();
		InstallGameStates();
		Container.Bind<GameScenario>().AsSingle().NonLazy();
	}

	private void InstallFactory() {
		Container.Bind<ResourcesLoader>().To<ResourcesLoader>().AsSingle();
		Container.Bind<IFactory>().To<Factory>().AsSingle();
	}
	
	private void InstallResourcesFactory() {
		Container.Bind<IResourcesFactory>().To<ResourcesFactory>().AsSingle();
	}
	
	private void InstallGameStates() {
		Container.Bind<StorageConfig>().FromInstance(storageConfig);
		Container.Bind<StateSwitcher>().AsSingle();
		Container.Bind<InitializeState>().AsSingle();
	}
	
	public override void Start() {
		
	}
}