using UnityEngine;
using Zenject;

public class BootInstaller : MonoInstaller, IInitializable
{
	public override void InstallBindings() {
		Container.Bind<IInitializable>().FromInstance(this).AsSingle();
	}
	
	public void Initialize() {
		Application.targetFrameRate = 200;
		var loaderService = Container.Resolve<SceneLoader>();
		loaderService.GoToWorldMapScene();
	}
}
