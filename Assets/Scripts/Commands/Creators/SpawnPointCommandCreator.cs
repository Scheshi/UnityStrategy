using Abstractions;
using UnityEngine;
using Zenject;


namespace Commands.Creators
{
    public class SpawnPointCommandCreator: CommandCreatorWithCancelled<ISpawnPointCommand, Vector3>
    {
        [Inject]
        private SpawnPointCommandCreator(ScriptableModel<Vector3> selectable)
        {
            SetAwaitable(selectable);
        }
        
        protected override ISpawnPointCommand GetCommand(Vector3 result)
        {
            return new SpawnPointCommand(result);
        }
    }
}