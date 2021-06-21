using System;
using Abstractions;
using UnityEngine;


namespace Core.Buildings
{
    public class TestBuilding: MonoBehaviour, IBuilding
    {
        [SerializeField] private string itemName;
        [SerializeField] private int maxHealth;
        private Material _outlineMaterial;
        private Material[] _defaultMaterials;
        private MeshRenderer _renderer;
        private int _currentHealth;
    
        public event Action OnSelect = () => { };
        public string Name => itemName;
        public int CurrentHealth => _currentHealth;
        public int MaxHealth => maxHealth;

        private void Start()
        {
            _renderer = GetComponent<MeshRenderer>();
            _defaultMaterials = _renderer.materials;
            _outlineMaterial = Resources.Load<Material>("Materials/Outline");
            _currentHealth = maxHealth;
        }
        
        public void Select()
        {
            var material = _renderer.material;
            Material[] materials = new Material[2];
            materials[0] = _outlineMaterial;
            materials[1] = material;
            /*for (int i = 1; i < _renderer.materials.Length; i++)
            {
                materials[i] = _renderer.materials[i-1];
            }*/
            _renderer.materials = materials;
            OnSelect.Invoke();
        }

        public void Unselect()
        {
            _renderer.materials = _defaultMaterials;
        }
    }
}