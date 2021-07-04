using System.Threading;
using Abstractions;
using Commands.Creators;
using UI.Model;
using UnityEngine;
using Zenject;


namespace Utils
{
    public class UtilsInstaller: MonoInstaller
    {
        [SerializeField] private ScriptableModel<ISelectableItem> model;
        [SerializeField] private AssetCollection collection;
        [SerializeField] private ScriptableModel<Vector3> position;
        [SerializeField] private ScriptableModel<IAttackable> target;
        [SerializeField] private ProduceModel produceModel;
        [SerializeField] private CancellationModel cancellation;
        
        public override void InstallBindings()
        {
            Container.Bind<CancellationModel>().FromInstance(cancellation).AsSingle();
            Container.Bind<ProduceModel>().FromInstance(produceModel).AsSingle();
            Container.Bind<CancellationTokenSource>().WithId("Command").AsSingle();
            Container.Bind<ScriptableModel<ISelectableItem>>().FromInstance(model).AsSingle();
            Container.Bind<AssetCollection>().FromInstance(collection).AsSingle();
            Container.Bind<ScriptableModel<Vector3>>().FromInstance(position).AsSingle();
            //Container.Bind<DiContainer>().FromInstance(Container).AsSingle();
            Container.Bind<ControlModel>().AsSingle();
            Container.Bind<CommandCreator<ICreateUnitCommand>>().To<ProduceUnitCommandCreator>().AsSingle();
            Container.Bind<CommandCreatorWithCancelled<IAttackCommand, IAttackable>>().To<AttackCommandCreator>().AsSingle();
            Container.Bind<CommandCreatorWithCancelled<IMoveCommand, Vector3>>().To<MoveCommandCreator>().AsSingle();
            Container.Bind<CommandCreator<ICancelCommand>>().To<CancelCommandCreator>().AsSingle();
            Container.Bind<CommandCreatorWithCancelled<IPatrolCommand, Vector3>>().To<PatrolCommandCreator>().AsSingle();
            Container.Bind<ScriptableModel<IAttackable>>().FromInstance(target).AsSingle();
        }
    }
}