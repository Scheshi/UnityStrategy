using System;
using System.Collections.Generic;
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
        private List<ICommand> _commandQueue;
        private int _currentHealth;
        public ICommandExecutor[] Executors { get; private set; }
        public Transform Transform => transform;
        public event Action OnSelect;
        public string Name => itemName;
        public int CurrentHealth => _currentHealth;
        public int MaxHealth => maxHealth;
        public void Damage(int point)
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
        }

        public ICommandQueue CommandQueue { get; } = new UnitCommandQueue();
        public GameObject GameObject => gameObject;
    }
}