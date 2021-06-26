using Abstractions;
using Commands;
using Utils;
using Zenject;


namespace UI.Model
{
    public class ControlModel
    {
        [Inject] private AssetCollection _assetCollection;
        
        public void OnClick(ICommandExecutor executor)
        {
            if (executor is CommandExecutorBase<ICreateUnitCommand> unitCreater)
            {
                unitCreater.Execute(_assetCollection.InjectAsset(new ProduceUnitCommand()));
            }
        }
    }
}