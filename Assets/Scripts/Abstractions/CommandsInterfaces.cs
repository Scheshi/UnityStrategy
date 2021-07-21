using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Abstractions
{
    public interface ICommand
    {
        void Cancel();
    }

    public interface IBuildingCommand: ICommand
    {
        GameObject BuildPrefab();
    }

    public interface ICreateUnitCommand : ICommand
    {
        Vector3 SpawnPosition { get; set; }
        float ProductionTime { get; }
        Sprite Icon { get; }
        GameObject UnitPrefab { get;}
        string UnitName { get; }
    }

    public interface IAttackCommand : ICommand
    {
        void SetDependency(IAttacker attacker, NavMeshAgent agent);
        void StartAttack();
    }
    public interface ICancelCommand: ICommand{}

    public interface IMoveCommand : ICommand
    {
        Task Move(NavMeshAgent agent);
    }

    public interface IPatrolCommand : ICommand
    {
        void SetStartPosition(Vector3 startPosition);
        
        void Patrol(NavMeshAgent movingTransform);
    }



    public interface ICommandExecutor
    {
        public Type CommandType { get; }
        Task TryExecute(ICommand command);
    }
    

    public abstract class CommandExecutorBase<T> : ICommandExecutor<T> where T: ICommand
    {
        public Type CommandType => typeof(T);

        public async Task TryExecute(ICommand command)
        {
            if (command is T currentCommand)
            {
                await ExecuteTypeCommand(currentCommand);
            }
        }

        protected abstract Task ExecuteTypeCommand(T command);
    }
}