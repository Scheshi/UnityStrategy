using System.Collections;
using Abstractions;
using UnityEngine;

namespace Commands
{
    public class PatrolCommandExecutor: CommandExecutorBase<IPatrolCommand>
    {
        private Transform _ownerTransform;
        
        
        public PatrolCommandExecutor(Transform ownerTransform)
        {
            _ownerTransform = ownerTransform;
        }
        
        protected override void ExecuteTypeCommand(IPatrolCommand command)
        {
            command.SetStartPosition(_ownerTransform.position);
            _ownerTransform.gameObject.GetComponent<MonoBehaviour>().StartCoroutine(PatrolCoroutine(command));
        }
        
        private IEnumerator PatrolCoroutine(IPatrolCommand command)
        {
            while (true)
            {
                command.Patrol(_ownerTransform);
                yield return null;
            }
        }
    }
}