using System;
using Abstractions;
using UnityEngine;
using UnityEngine.AI;


namespace Commands
{
    public class MoveCommand: IMoveCommand
    {
        private Vector3 _to;
        public event Action OnEndPath = () => { };
        public MoveCommand(Vector3 to)
        {
            _to = to;
        }
        
        public void Move(NavMeshAgent agent)
        {
            agent.SetDestination(_to);
        }
    }
}