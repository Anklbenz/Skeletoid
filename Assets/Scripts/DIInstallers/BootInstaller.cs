using Zenject;

public class BootInstaller : MonoInstaller
{
	public override void Start() {
		var loaderService = Container.Resolve<SceneLoaderService>();
		loaderService.GoToGameplayScene();
	}
}
