using Abstractions;
using Commands.Creators;
using Zenject;

namespace UI.Model
{
    public class ModelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CommandCreator<ICreateUnitCommand>>().To<ProduceUnitCommandCreator>().AsSingle();
            Container.Bind<CommandCreator<IAttackCommand>>().To<AttackCommandCreator>().AsSingle();
            Container.Bind<CommandCreator<IMoveCommand>>().To<MoveCommandCreator>().AsSingle();
            Container.Bind<CommandCreator<ICancelCommand>>().To<CancelCommandCreator>().AsSingle();
        }
    }
}