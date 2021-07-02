using System;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

namespace Abstractions
{
    public interface ICommand{}

    public interface IBuildingCommand: ICommand
    {
        GameObject BuildPrefab();
    }

    public interface ICreateUnitCommand : ICommand
    {
        Vector3 SpawnPosition { get; set; }
        void InstantiateUnit();
    }

    public interface IAttackCommand : ICommand
    {
        void Attack(Vector3 ownerPosition);
    }
    public interface ICancelCommand: ICommand{}

    public interface IMoveCommand : ICommand
    {
        void Move(NavMeshAgent agent);
    }

    public interface IPatrolCommand : ICommand
    {
        void SetStartPosition(Vector3 startPosition);
        
        void Patrol(NavMeshAgent movingTransform);
    }



    public interface ICommandExecutor
    {
        public Type CommandType { get; }
        void Execute(ICommand command);
    }
    

    public abstract class CommandExecutorBase<T> : ICommandExecutor where T: ICommand
    {
        public Type CommandType => typeof(T);

        public void Execute(ICommand command) => ExecuteTypeCommand((T) command);

        protected abstract void ExecuteTypeCommand(T command);
    }
}