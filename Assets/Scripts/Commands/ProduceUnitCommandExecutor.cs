using System;
using Abstractions;
using UniRx;
using UnityEngine;


namespace Commands
{
    public class ProduceUnitCommandExecutor : CommandExecutorBase<ICreateUnitCommand>
    {
        public IObservable<float> CurrentUnitProduce { get; } = new ReactiveProperty<float>();
        public IObservable<IUnit> CurrentUnit { get; } = new ReactiveProperty<IUnit>();
        public IReactiveCollection<IUnit> UnitsQueue { get; } = new ReactiveCollection<IUnit>();
        public Vector3 SpawnPosition { get;}

        public ProduceUnitCommandExecutor(Vector3 spawnPosition)
        {
            SpawnPosition = spawnPosition;
        }
        
        protected override void ExecuteTypeCommand(ICreateUnitCommand command)
        {
            command.SpawnPosition = SpawnPosition;
            command.InstantiateUnit();
        }
    }
}