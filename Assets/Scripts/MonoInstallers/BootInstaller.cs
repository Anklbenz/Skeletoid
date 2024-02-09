using UnityEngine;
using YG;
using Zenject;

public class BootInstaller : MonoInstaller, IInitializable {
	[SerializeField] private YandexGame yandexGame;
	public override void InstallBindings() {
		Container.Bind<IInitializable>().FromInstance(this).AsSingle();
	}

	public void Initialize() {
		var advSDk = Container.Resolve<AdvSDK>();
		advSDk.Initialize(yandexGame);

		var loaderService = Container.Resolve<SceneLoader>();
		loaderService.GoToWorldMapScene();
	}
}