using System;
using System.Collections.Generic;
using System.Linq;
using Abstractions;
using Commands;
using UnityEngine;


namespace Core.Buildings
{
    public class Unit: MonoBehaviour, IUnit
    {
        [SerializeField] private string itemName;
        [SerializeField] private int maxHealth;
        [SerializeField] private Sprite icon;
        [SerializeField] private float damage;
        private List<ICommand> _commandQueue;
        private int _currentHealth;
        public ICommandExecutor[] Executors { get; private set; }
        public Transform Transform => transform;
        public event Action OnSelect;
        public string Name => itemName;
        public int CurrentHealth => _currentHealth;
        public int MaxHealth => maxHealth;
        public void TakeDamage(int point)
        {
            _currentHealth -= point;
        }

        public Sprite Icon => icon;

        public void Select()
        {
            //
        }

        public void Unselect()
        {
            //
        }

        public void SetExecutors(params ICommandExecutor[] executors)
        {
            Executors = executors;
            ICommandExecutor moveExecutor = executors.FirstOrDefault(x => x.CommandType == typeof(IMoveCommand));
            ICommandExecutor attackExecutor = executors.FirstOrDefault(x => x.CommandType == typeof(IAttackCommand));
            ICommandExecutor patrolExecutor = executors.FirstOrDefault(x => x.CommandType == typeof(IPatrolCommand));
            CommandQueue = new UnitCommandQueue(moveExecutor as CommandExecutorBase<IMoveCommand>, patrolExecutor as CommandExecutorBase<IPatrolCommand>, attackExecutor as CommandExecutorBase<IAttackCommand>);
        }

        public ICommandQueue CommandQueue { get; private set; }
        public GameObject GameObject => gameObject;
        public float Damage => damage;
    }
}