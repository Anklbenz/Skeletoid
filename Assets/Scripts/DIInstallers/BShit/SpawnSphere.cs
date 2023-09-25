using Zenject;

public class SpawnSphere : MonoInstaller{
	public override void InstallBindings() {
		BindGameObjectFactory();
	}
	private void BindGameObjectFactory() {
		Container.Bind<ResourcesLoader>().AsSingle();
		Container.Bind<IFactory>().To<Factory>().AsSingle();
		Container.Bind<IResourcesFactory>().To<ResourcesFactory>().AsSingle();
	}

}
