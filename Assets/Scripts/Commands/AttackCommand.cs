using Abstractions;
using UnityEngine;


namespace Commands
{
    public class AttackCommand: IAttackCommand
    {
        private IAttackable _target;
        private IAttacker _attacker;

        public AttackCommand(IAttackable target)
        {
            _target = target;
        }

        public void SetAttacker(IAttacker attacker)
        {
            _attacker = attacker;
        }
        
        public void Attack()
        {
            if ((_attacker.Transform.position - _target.Transform.position).sqrMagnitude > 64)
            {
                Debug.Log("Юнит слишком далеко для атаки");
                return;
            }
            Debug.Log("Успешная атака");
            _target.TakeDamage(10);
        }

        public void Cancel()
        {
            //
        }
    }
}