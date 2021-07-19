using System;
using UI.Model;
using UI.View;
using UniRx;


public class ProducePresenter
{
    private ProducePanelView _producePanel;
    private ProducePanel _model;
    private IDisposable _productionQueueAddCt;
    private IDisposable _productionQueueRemoveCt;
    private IDisposable _productionQueueReplaceCt;
    private IDisposable _cancelButtonCts;

    public ProducePresenter(ProducePanelView view, ProducePanel model)
    {
        _producePanel = view;
        _model = model;
        model.Producer.Subscribe(unitProducer =>
        {
            _productionQueueAddCt?.Dispose();
            _productionQueueRemoveCt?.Dispose();
            _productionQueueReplaceCt?.Dispose();
            _cancelButtonCts?.Dispose();
	
            view.Clear();
            _producePanel.gameObject.SetActive(unitProducer != null);
	
            if (unitProducer != null)
            {
                _productionQueueAddCt = unitProducer.Queue
                    .ObserveAdd()
                    .Subscribe(addEvent => view.SetTask(addEvent.Value, addEvent.Index));

                _productionQueueRemoveCt = unitProducer.Queue
                    .ObserveRemove()
                    .Subscribe(removeEvent => view.SetTask(null, removeEvent.Index));

                _productionQueueReplaceCt = unitProducer.Queue
                    .ObserveReplace()
                    .Subscribe(replaceEvent => view.SetTask(replaceEvent.NewValue, replaceEvent.Index));

                _cancelButtonCts = view.CancelButtonClicks.Subscribe(unitProducer.Cancel);

                for (int i = 0; i < unitProducer.Queue.Count; i++)
                {
                    view.SetTask(unitProducer.Queue[i], i);
                }
            }
        });
    }
}
