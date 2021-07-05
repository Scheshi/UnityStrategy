using Abstractions;
using Zenject;


namespace Commands.Creators
{
    public sealed class AttackCommandCreator: CommandCreatorWithCancelled<IAttackCommand, IAttackable>
    {
        [Inject]
        private AttackCommandCreator(ScriptableModel<IAttackable> model)
        {
            SetAwaitable(model);
        }

        protected override IAttackCommand GetCommand(IAttackable target) => new AttackCommand(target);
    }
}