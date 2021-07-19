using Abstractions;
using UnityEngine;


namespace Commands.Creators
{
    public class SpawnPointCommandCreator: CommandCreatorWithCancelled<ISpawnPointCommand, Vector3>
    {
        protected override ISpawnPointCommand GetCommand(Vector3 result)
        {
            return new SpawnPointCommand(result);
        }
    }
}