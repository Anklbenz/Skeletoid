using Zenject;

public class SpawnSphere : MonoInstaller{
	public override void InstallBindings() {
		BindGameObjectFactory();
	}
	private void BindGameObjectFactory() {
		Container.Bind<ResourcesLoader>().To<ResourcesLoader>().AsSingle();
		Container.Bind<IGameObjectFactory>().To<GameObjectFactory>().AsSingle();
		Container.Bind<IGameObjectFactoryByPath>().To<Factory>().AsSingle();
	}

}
