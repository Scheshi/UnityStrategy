using Abstractions;
using UnityEngine;


namespace Commands
{
    public class AttackCommand: IAttackCommand
    {
        private IAttackable _target;

        public AttackCommand(IAttackable target)
        {
            _target = target;
        }
        
        public void Attack(Vector3 ownerPosition)
        {
            if ((ownerPosition - _target.Transform.position).sqrMagnitude > 64)
            {
                Debug.Log("Юнит слишком далеко для атаки");
                return;
            }
            Debug.Log("Успешная атака");
            _target.Damage(10);
        }

        public void Cancel()
        {
            //
        }
    }
}