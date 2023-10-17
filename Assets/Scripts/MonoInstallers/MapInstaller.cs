using UnityEngine;
using Zenject;

public class MapInstaller : MonoInstaller, IInitializable
{
   [SerializeField] private MapItem[] items;

   public override void InstallBindings() {
      InstallMapSystem();
      InstallInitializeForThis();
 
   }

   private void InstallInitializeForThis() {
      Container.Bind<IInitializable>().FromInstance(this).AsSingle();
   }


   
   private void InstallMapSystem() {
      Container.Bind<WordMapSystem>().AsSingle();
   }

   public void Initialize() {

      
      var mapSystem = Container.Resolve<WordMapSystem>();
      mapSystem.Initialize(items);
   }
}