using UnityEngine;
using Zenject;

namespace Utils
{
    public class ScriptableInstaller: ScriptableObjectInstaller
    {
        [SerializeField] private AssetCollection collection;
        public override void InstallBindings()
        {
            Container.Bind<AssetCollection>().FromInstance(collection).AsSingle();
        }
    }
}