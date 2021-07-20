using Abstractions;
using Zenject;


namespace Commands.Creators
{
    public sealed class AttackCommandCreator: CommandCreatorWithCancelled<IAttackCommand, IAttackable>
    {
        private IAttacker _currentAttacker;
        public AttackCommandCreator(IAttacker attacker)
        {
            _currentAttacker = attacker;
        }

        [Inject]
        private void InjectDependies(ScriptableModel<IAttackable> model)
        {
            SetAwaitable(model);
        }

        protected override IAttackCommand GetCommand(IAttackable target) => new AttackCommand(target);
    }
}