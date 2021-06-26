using UnityEngine;
using Zenject;

namespace Utils
{
    public class UtilsInstaller: MonoInstaller
    {
        [SerializeField] private SelectableModel model;
        [SerializeField] private AssetCollection collection;
        public override void InstallBindings()
        {
            Container.Bind<SelectableModel>().FromInstance(model).AsSingle();
            Container.Bind<AssetCollection>().FromInstance(collection).AsSingle();
        }
    }
}