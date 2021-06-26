using System;
using Abstractions;
using UnityEngine;
using Utils;


namespace Commands.Creators
{
    public sealed class ProduceUnitCommandCreator: CommandCreator<ICreateUnitCommand>
    {
        private AssetCollection _collection;
        public ProduceUnitCommandCreator()
        {
            _collection = Resources.Load<AssetCollection>("Config/Collection");
        }

        protected override void CreateCommand(Action<ICreateUnitCommand> onCallBack)
        {
            onCallBack?.Invoke(_collection.InjectAsset(new ProduceUnitCommand()));
        }
    }
}