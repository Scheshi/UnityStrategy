using System.Collections;
using Abstractions;
using UnityEngine;

namespace Commands
{
    public class MoveCommandExecutor: CommandExecutorBase<IMoveCommand>
    {
        private GameObject _gameObjectMoving;
        private bool _pathEnd = false;
        private Coroutine _moveCoroutine;
        
        public MoveCommandExecutor(GameObject gameObjectMoving)
        {
            _gameObjectMoving = gameObjectMoving;
        }
        
        protected override void ExecuteTypeCommand(IMoveCommand command)
        {
            var mono = _gameObjectMoving.GetComponent<MonoBehaviour>();
            _pathEnd = false;
            _moveCoroutine = mono.StartCoroutine(MoveCoroutine(command));
            command.OnEndPath += () =>
            {
                _pathEnd = true;
                mono.StopCoroutine(_moveCoroutine);
            };
        }

        private IEnumerator MoveCoroutine(IMoveCommand command)
        {
            while (!_pathEnd)
            {
                command.Move(_gameObjectMoving.transform);
                yield return null;
            }
        }
    }
}