using System;
using System.Collections.Generic;
using System.Linq;
using Abstractions;
using Commands;
using UnityEngine;
using Utils;


namespace Core.Buildings
{
    public class Unit: MonoBehaviour, IUnit
    {
        [SerializeField] private string itemName;
        [SerializeField] private int maxHealth;
        [SerializeField] private Sprite icon;
        [SerializeField] private int damage;
        [SerializeField] private float attackRange;
        [SerializeField] private float attackCooldown;
        [SerializeField] private int visionRange;
        private List<ICommand> _commandQueue;
        private int _currentHealth;
        private Team _team;
        public ICommandExecutor[] Executors { get; private set; }
        public Transform Transform => transform;
        public event Action OnSelect;
        public string Name => itemName;
        public int CurrentHealth => _currentHealth;
        public int MaxHealth => maxHealth;
        public void TakeDamage(int point)
        {
            _currentHealth -= point;
            if (_currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            UnitManager.RegisterUnit(this);
        }

        private void OnDestroy()
        {
            UnitManager.UnregisterUnit(this);
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
        public void SetTeam(Team team)
        {
            _team = team;
        }

        public int VisionRange => visionRange;
        public int Damage => damage;
        public float Range => attackRange;
        public float CoolDown => attackCooldown;
        public Vector3 CurrentPosition { get; private set; }

        private void Update()
        {
            CurrentPosition = transform.position;
        }

        public Team Team => _team;
    }
}