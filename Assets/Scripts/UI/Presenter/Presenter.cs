using System;
using Abstractions;
using UI.Model;
using UI.View;
using UnityEngine;


namespace UI.Presenter
{
    public class Presenter: IDisposable
    {
        private SelectableModel _selectable;
        private InfoPanelView _info;
        private ControlPanelView _control;
        private readonly ControlModel _model = new ControlModel();
        
        
        public Presenter(InfoPanelView info, ControlPanelView control)
        {
            _selectable = Resources.Load<SelectableModel>("Config/SelectableModel");
            _selectable.SubscriptionOnSelect(OnChangeItem);
            _info = info;
            _control = control;
            _info.Reset();
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
            _control = null;
            _info = null;
            _selectable = null;
        }
    }
}