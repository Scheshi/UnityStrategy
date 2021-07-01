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
        private Vector3 _from;
        public event Action OnEndPath = () => { };

        public MoveCommand(Vector3 to)
        {
            _to = to;
        }

        public void SetFrom(Vector3 from)
        {
            _from = from;
        }

        public async void Move(NavMeshAgent agent)
        {
            while (true)
            {
                await MoveTo(agent, _to);
                await MoveTo(agent, _from);
            }
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