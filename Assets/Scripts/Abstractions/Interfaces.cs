using System;
using UnityEngine;

namespace Abstractions
{
    public interface IHealthOwner
    {
        int CurrentHealth { get; }
        int MaxHealth { get; }
    }

    public interface ITransformOwner
    {
        Transform Transform { get; }
    }

    public interface IAttackable : IHealthOwner, ITransformOwner
    {
        void Damage(int point);
    }
    
    public interface ISelectableItem: IAttackable
    {
        ICommandExecutor[] Executors { get; }
        Transform Transform { get; }
        event Action OnSelect;
        string Name { get; }
        
        Sprite Icon { get; }
        void Select();
        void Unselect();
        void SetExecutors(params ICommandExecutor[] executors);
    }

    public interface IBuilding : ISelectableItem
    {
        
    }

    public interface IUnit : ISelectableItem
    {
        GameObject GameObject {get};
    }
}
