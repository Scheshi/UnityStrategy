using System;
using Abstractions;
using UniRx;
using Utils;

namespace UI.Model
{
    public class ProducePanel
    {
        private ReactiveProperty<IUnitProducer> _producer = new ReactiveProperty<IUnitProducer>();
        public IObservable<IUnitProducer> Producer => _producer;
        private ScriptableModel<ISelectableItem> _selectableModel;
        
        public ProducePanel(ScriptableModel<ISelectableItem> selectableModel)
        {
            _selectableModel = selectableModel;
            selectableModel.OnChangeValue += SelectableModelOnChangeValue;
        }

        private void SelectableModelOnChangeValue()
        {
            if (_selectableModel.CurrentValue.Executors.Has(out ICommandExecutor executor, x => x is IUnitProducer))
            {
                _producer.Value = executor as IUnitProducer;
            }
            else
            {
                _producer.Value = null;
            }
        }
    }
}