using Abstractions;
using UnityEngine;
using Zenject;


namespace Commands.Creators
{
    public class PatrolCommandCreator: CommandCreatorWithCancelled<IPatrolCommand, Vector3>
    {
        [Inject]
        private PatrolCommandCreator(ScriptableModel<Vector3> position)
        {
            SetAwaitable(position);
        }

        protected override IPatrolCommand GetCommand(Vector3 result) => new PatrolCommand(result);
    }
}