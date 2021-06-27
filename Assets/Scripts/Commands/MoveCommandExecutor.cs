using Abstractions;
using UnityEngine;

namespace Commands
{
    public class MoveCommandExecutor: CommandExecutorBase<IMoveCommand>
    {
        private GameObject _gameObjectMoving;
        public MoveCommandExecutor(GameObject gameObjectMoving)
        {
            _gameObjectMoving = gameObjectMoving;
        }
        
        protected override void ExecuteTypeCommand(IMoveCommand command)
        {
            command.Move(_gameObjectMoving.transform);
        }
    }
}