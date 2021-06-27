using System;
using Abstractions;
using UI.Model;
using UI.View;
using Utils;
using Zenject;


namespace UI.Presenter
{
    public class Presenter: IDisposable
    {
        private SelectableModel _selectable;
        private InfoPanelView _info;
        private ControlPanelView _control;
        private  ControlModel _model;


        public Presenter(InfoPanelView info, ControlPanelView control, SelectableModel selectable)
        {
            _selectable = selectable;
            _selectable.SubscriptionOnSelect(OnChangeItem);
            _info = info;
            _control = control;
            _info.Reset();
        }

        [Inject]
        private void InjectModel(ControlModel model)
        {
            _model = model;
            _control.OnClick += _model.OnClick;
        }

        

        private void OnChangeItem(ISelectableItem item)
        {
            if (item != null)
            {
                _info.SetInfo(null, item.Name, item.CurrentHealth, item.MaxHealth);
                SetButtons(item);
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
            _selectable.UnsubscriptionOnSelect(OnChangeItem);
            _control = null;
            _info = null;
            _selectable = null;
        }
    }
}