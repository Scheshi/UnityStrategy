using Abstractions;
using Commands;
using UI.View;
using Utils;


public class ProducePresenter
{
    private ProducePanelView _producePanel;
    private ProduceModel _model;
    private SelectableModel _selectable;

    public ProducePresenter(ProducePanelView view, SelectableModel selectable)
    {
        _producePanel = view;
        _selectable = selectable;
        _selectable.OnChangeValue += SelectableOnChangeValue;
    }

    private void SelectableOnChangeValue()
    {
        if (_selectable.CurrentValue.Executors.Has(out ICommandExecutor executor,
            x => x.CommandType == typeof(ICreateUnitCommand)))
        {
            
        }
    }
}
