using System;
using System.Collections.Generic;
using Abstractions;
using UnityEngine;


namespace Core.Buildings
{
    public class TestBuilding: MonoBehaviour, IBuilding
    {
        private struct Meshes
        {
            public MeshRenderer Renderer;
            public Material DefaultMaterial;
        }
        [SerializeField] private string itemName;
        [SerializeField] private int maxHealth;
        [SerializeField] private Sprite icon;
        private Material _outlineMaterial;
        private Material[] _defaultMaterials;
        private Meshes[] _meshes;
        private int _currentHealth;

        public ICommandExecutor[] Executors { get; private set; }
        public Transform Transform => transform;
        public event Action OnSelect = () => { };
        public string Name => itemName;
        public int CurrentHealth => _currentHealth;
        public int MaxHealth => maxHealth;
        public void Damage(int point)
        {
            _currentHealth -= point;
        }

        public Sprite Icon => icon;

        private void Start()
        {
            var meshes = GetComponentsInChildren<MeshRenderer>();
            var meshList = new List<Meshes>();
            
            for(int i = 0; i < meshes.Length; i++)
            {
                meshList.Add(new Meshes() {Renderer = meshes[i], DefaultMaterial = meshes[i].material});
            }

            _meshes = meshList.ToArray();
            _outlineMaterial = Resources.Load<Material>("Materials/Outline");
            _currentHealth = maxHealth;
        }
        
        public void Select()
        {
            for (int i = 0; i < _meshes.Length; i++)
            {
                var materials = new Material[2];
                materials[0] = _outlineMaterial;
                materials[1] = _meshes[i].DefaultMaterial;
                _meshes[i].Renderer.materials = materials;
            }
            OnSelect.Invoke();
        }

        public void Unselect()
        {
            for (int i = 0; i < _meshes.Length; i++)
            {
                _meshes[i].Renderer.material = _meshes[i].DefaultMaterial;
            }
        }

        public void SetExecutors(params ICommandExecutor[] executors)
        {
            Executors = executors;
        }
    }
}