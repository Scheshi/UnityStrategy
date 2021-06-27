using Abstractions;
using UnityEngine;

namespace Commands
{
    public class AttackCommand: IAttackCommand
    {
        public void Attack()
        {
            Debug.Log(nameof(Attack));
        }
    }
}