using System;
using System.Threading.Tasks;
using Abstractions;
using UnityEngine;
using UnityEngine.AI;


namespace Commands
{
    public class MoveCommand : IMoveCommand
    {
        private Vector3 _to;

        public MoveCommand(Vector3 to)
        {
            _to = to;
        }

        public async void Move(NavMeshAgent agent)
        {
            await MoveTo(agent, _to);
        }

        private async Task MoveTo(NavMeshAgent agent, Vector3 to)
        {
            agent.SetDestination(_to);
            while (Mathf.Abs(agent.transform.position.x - _to.x) < 0.1f &&
                   Mathf.Abs(agent.transform.position.z - _to.z) < 0.1f)
            {
                await Task.Yield();
            }
        }
    }
}