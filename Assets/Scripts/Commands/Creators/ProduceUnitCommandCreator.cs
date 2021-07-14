using System;
using Abstractions;
using UnityEngine;
using Utils;
using Zenject;


namespace Commands.Creators
{
    public sealed class ProduceUnitCommandCreator: CommandCreator<ICreateUnitCommand>
    {
        private AssetCollection _collection;
        
        [Inject] private ProduceModel _produceModel;
        
        public ProduceUnitCommandCreator()
        {
            _collection = Resources.Load<AssetCollection>("Config/Collection");
        }

        protected override void CreateCommand(Action<ICreateUnitCommand> onCallBack, bool isComplete = false)
        {
            onCallBack?.Invoke(_collection.InjectAsset(new ProduceUnitCommand(_produceModel)));
        }
    }
}