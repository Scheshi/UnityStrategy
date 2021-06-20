using System;
using Abstractions;
using UnityEngine;


namespace Core.Buildings
{
    public class TestBuilding: MonoBehaviour, IBuilding
    {
        [SerializeField] private string itemName;
        [SerializeField] private int maxHealth;
        private int _currentHealth;
    
        public event Action OnSelect = () => { };
        public string Name => itemName;
        public int CurrentHealth => _currentHealth;
        public int MaxHealth => maxHealth;

        private void Start()
        {
            _currentHealth = maxHealth;
        }
        
        public void Select()
        {
            OnSelect.Invoke();
        }
    }
}