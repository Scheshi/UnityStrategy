using System;
using System.Collections.Generic;
using System.Linq;
using Abstractions;
using Commands;
using UnityEngine;
using Utils;
using Zenject;


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
        [SerializeField] private Team team;
        private Material _outlineMaterial;
        private Material[] _defaultMaterials;
        private Meshes[] _meshes;
        private int _currentHealth;

        public ICommandExecutor[] Executors { get; private set; }
        public ICommandQueue CommandQueue { get; private set; }
        public Transform Transform => transform;
        public Vector3 UnitSpawnPosition { get; set; }
        public event Action OnSelect = () => { };
        public string Name => itemName;
        public int CurrentHealth => _currentHealth;
        public int MaxHealth => maxHealth;
        public void TakeDamage(int point)
        {
            _currentHealth -= point;
        }

        [Inject]
        private void Inject()
        {
            
        }

        public Sprite Icon => icon;

        private void Start()
        {
            UnitSpawnPosition = transform.position;
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
            ICommandExecutor executor = executors.FirstOrDefault(x => x.CommandType == typeof(ICreateUnitCommand));
            ICommandExecutor spawnExecutor = executors.First(x => x.CommandType == typeof(ISpawnPointCommand));
            if (executor != null)
            {
                CommandQueue = new BuildingCommandQueue(executor as CommandExecutorBase<ICreateUnitCommand>, spawnExecutor as CommandExecutorBase<ISpawnPointCommand>);
            }
        }


        public Team Team => team;
        public void SetTeam(Team team)
        {
            this.team = team;
        }
    }
}