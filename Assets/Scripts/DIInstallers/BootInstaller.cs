using Zenject;

public class BootInstaller : MonoInstaller, IInitializable
{
	
	public override void InstallBindings() {
		Container.Bind<IInitializable>().FromInstance(this).AsSingle();
	}

	/*
	public override void Start() {
		
	}
	*/

	public void Initialize() {
		var loaderService = Container.Resolve<SceneLoaderService>();
		loaderService.GoToGameplayScene();
	}
}
