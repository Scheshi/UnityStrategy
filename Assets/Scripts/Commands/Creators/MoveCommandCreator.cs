using Abstractions;
using UnityEngine;
using Zenject;


namespace Commands.Creators
{
    public sealed class MoveCommandCreator: CommandCreatorWithCancelled<IMoveCommand, Vector3>
    {
        [Inject]
        private MoveCommandCreator(ScriptableModel<Vector3> position)
        {
            SetAwaitable(position);
        }
        
        protected override IMoveCommand GetCommand(Vector3 result) => new MoveCommand(result, CancellationModel);
    }
}