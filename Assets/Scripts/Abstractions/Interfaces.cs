using System;
using UnityEngine;
using Utils;

namespace Abstractions
{
    public interface IHealthOwner
    {
        int CurrentHealth { get; }
        int MaxHealth { get; }
    }

    public interface IPositionOwner
    {
        Vector3 CurrentPosition { get; }
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
        public int VisionRange { get; }
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

    public interface IBuilding : ISelectableItem, ITeamOwner
    {
        Vector3 UnitSpawnPosition { get; set; }
    }
    
    public interface ITeamOwner
    {
        Team Team { get; }
        public void SetTeam(Team team);
    }

    public interface IUnit : ISelectableItem, IAttacker, IPositionOwner, ITeamOwner
    {
        
        GameObject GameObject { get; }
    }
}
