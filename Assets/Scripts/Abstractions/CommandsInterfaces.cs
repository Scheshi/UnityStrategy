using System;
using UnityEngine;

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
        void Attack();
    }
    public interface ICancelCommand: ICommand{}

    public interface IMoveCommand : ICommand
    {
        event Action OnEndPath;
        void Move(Transform transform);
    }



    public interface ICommandExecutor
    {
        void Execute(ICommand command);
    }
    
    public abstract class CommandExecutorBase<T> : ICommandExecutor where T: ICommand
    {
        public Type CommandType => typeof(T);

        public void Execute(ICommand command) => ExecuteTypeCommand((T) command);

        protected abstract void ExecuteTypeCommand(T command);


    }
}