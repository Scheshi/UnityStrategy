using Abstractions;
using UnityEngine;

namespace Commands
{
    public interface ISpawnPointCommand: ICommand
    {
        Vector3 Position { get; }
    }
}