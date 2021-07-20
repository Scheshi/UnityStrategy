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
        void TakeDamage(int point);
    }

    public interface IAttacker : ITransformOwner
    {
        public int Damage { get; }
        public float Range { get; }
        public float CoolDown { get; }
    }
    
    public interface ISelectableItem: IAttackable
    {
        ICommandExecutor[] Executors { get; }
        ICommandQueue CommandQueue { get; }
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
        Vector3 UnitSpawnPosition { get; set; }
    }

    public interface IUnit : ISelectableItem, IAttacker
    {
        
        GameObject GameObject { get; }
    }
}
