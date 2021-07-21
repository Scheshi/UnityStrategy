using System.Threading;
using Abstractions;
using UniRx;
using UnityEngine;
using UnityEngine.AI;


namespace Commands
{
    public class AttackCommand: IAttackCommand
    {
        private NavMeshAgent _agent;
        private IAttackable _target;
        private IAttacker _attacker;
        private AttackOperation _currentAttackOperation;

        private Vector3 _attackerPosition;
        private Vector3 _targetPosition;

        private Subject<Vector3> _subjectPosition = new Subject<Vector3>();
        private Subject<IAttackable> _subjectAttackable = new Subject<IAttackable>();
        private Subject<bool> _subjectCancellable = new Subject<bool>();

        public AttackCommand(IAttackable target)
        {
            if (target == null) return;
            _target = target;
            Observable.EveryUpdate().Subscribe(item =>
            {
                _attackerPosition = _attacker.Transform.position;
                _targetPosition = _target.Transform.position;
            });
            _subjectPosition.ObserveOn(Scheduler.MainThread).Subscribe(Move);
            _subjectAttackable.ObserveOn(Scheduler.MainThread).Subscribe(Attack);
        }

        public void SetDependency(IAttacker attacker, NavMeshAgent navMeshAgent)
        {
            _attacker = attacker;
            _agent = navMeshAgent;
        }

        public void StartAttack()
        {
            _currentAttackOperation = new AttackOperation(this);
        }

        private void Move(Vector3 position)
        {
            _agent.SetDestination(position);
        }
        
        private void Attack(IAttackable attackable)
        {
            
            _agent.ResetPath();
            attackable.TakeDamage(_attacker.Damage);
        }

        public void Cancel()
        {
            _subjectCancellable.OnNext(true);
        }

        class AttackOperation
        {
            private AttackCommand _command;
            private bool _isContinue = true;
            public AttackOperation(AttackCommand command)
            {
                _command = command;
                Thread thread = new Thread(AttackRoutine);
                thread.Start();
            }

            private void AttackRoutine()
            {
                if (_command._attacker == null ||
                    _command._target == null) return;
                _command._subjectCancellable.ObserveOn(Scheduler.CurrentThread).Subscribe(Cancellable);
                while (_isContinue)
                {
                    if ((_command._attackerPosition - _command._targetPosition).sqrMagnitude <
                        _command._attacker.Range * _command._attacker.Range)
                    {
                        _command._subjectAttackable.OnNext(_command._target);
                    }
                    else
                    {
                        _command._subjectPosition.OnNext(_command._targetPosition);
                    }

                    Thread.Sleep((int) _command._attacker.CoolDown * 1000);
                }
            }

            private void Cancellable(bool isContinue)
            {
                _isContinue = isContinue;
            }
        }
    }
}