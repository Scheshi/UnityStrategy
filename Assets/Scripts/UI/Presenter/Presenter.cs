using System;
using Abstractions;
using UI.Model;
using UI.View;


namespace UI.Presenter
{
    public class Presenter: IDisposable
    {
        private ScriptableModel<ISelectableItem> _selectable;
        private InfoPanelView _info;
        private ControlPanelView _control;
        private  ControlModel _model;

        public Presenter(ControlPanelView control, InfoPanelView info, ScriptableModel<ISelectableItem> selectable, ControlModel model)
        {
            _selectable = selectable;
            _selectable.OnChangeValue += OnChangeItem;
            _info = info;
            _control = control;
            _info.Reset();
            _model = model;
            _control.OnClick += _model.OnClick;
        }



        private void OnChangeItem()
        {
            if (_selectable.CurrentValue != null)
            {
                _info.SetInfo(_selectable.CurrentValue.Icon, _selectable.CurrentValue.Name, _selectable.CurrentValue.CurrentHealth, _selectable.CurrentValue.MaxHealth);
                SetButtons(_selectable.CurrentValue);
            }
            else
            {
                _info.Reset();
                _control.ClearButtons();
            }
        }

        private void SetButtons(ISelectableItem item)
        {
            _control.ClearButtons();
            _control.SetButtons(item.Executors);
        }


        public void Dispose()
        {
            _control.OnClick -= _model.OnClick;
            _selectable.OnChangeValue -= OnChangeItem;
            _control = null;
            _info = null;
            _selectable = null;
        }
    }
}