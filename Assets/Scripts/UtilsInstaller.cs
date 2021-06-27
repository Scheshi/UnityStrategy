using Abstractions;
using Commands.Creators;
using UI.Model;
using UI.Presenter;
using UI.View;
using UnityEngine;
using Zenject;


namespace Utils
{
    public class UtilsInstaller: MonoInstaller
    {
        [SerializeField] private ControlPanelView controlPanelView;
        [SerializeField] private InfoPanelView infoPanelView;
        [SerializeField] private ScriptableModel<ISelectableItem> model;
        [SerializeField] private AssetCollection collection;
        [SerializeField] private ScriptableModel<Vector3> position;
        
        public override void InstallBindings()
        {
            Container.Bind<ScriptableModel<ISelectableItem>>().FromInstance(model).AsSingle();
            Container.Bind<AssetCollection>().FromInstance(collection).AsSingle();
            Container.Bind<ScriptableModel<Vector3>>().FromInstance(position).AsSingle();
            Container.Bind<DiContainer>().WithId("Utils").FromInstance(Container).AsSingle();
            Container.Bind<ControlModel>().AsSingle();
            Container.Bind<CommandCreator<ICreateUnitCommand>>().To<ProduceUnitCommandCreator>().AsSingle();
            Container.Bind<CommandCreator<IAttackCommand>>().To<AttackCommandCreator>().AsSingle();
            Container.Bind<CommandCreator<IMoveCommand>>().To<MoveCommandCreator>().AsSingle();
            Container.Bind<CommandCreator<ICancelCommand>>().To<CancelCommandCreator>().AsSingle();
            
            var presenter = new Presenter(infoPanelView, controlPanelView, model);
            Container.Inject(presenter);
            Container.Bind<Presenter>().FromInstance(presenter).AsSingle();
        }
    }
}