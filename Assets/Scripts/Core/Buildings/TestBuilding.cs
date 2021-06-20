using System;
using Abstractions;
using UnityEngine;


namespace Core.Buildings
{
    public class TestBuilding: MonoBehaviour, IBuilding
    {
        [SerializeField] private string itemName;
    
        public event Action OnSelect = () => { };
        public string Name => itemName;

        public void Select()
        {
            OnSelect.Invoke();
        }
    }
}