using UI.Model;
using UnityEngine;
using Zenject;

namespace Utils
{
    [CreateAssetMenu(menuName = "Inject/"+nameof(ScriptableInstaller))]
    public class ScriptableInstaller: ScriptableObjectInstaller
    {
        [SerializeField] private AssetCollection collection;
        [SerializeField] private SelectableModel model;
        public override void InstallBindings()
        {
            Container.Bind<AssetCollection>().FromInstance(collection).AsSingle();
            Container.Bind<SelectableModel>().FromInstance(model).AsSingle();
        }
    }
}