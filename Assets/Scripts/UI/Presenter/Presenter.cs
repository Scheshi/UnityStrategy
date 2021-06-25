using Abstractions;
using UI.Model;
using UI.View;
using UnityEngine;

namespace UI.Presenter
{
    public class Presenter
    {
        private SelectableModel _model;
        private InfoPanelView _info;
        private ControlPanelView _control;

        public Presenter()
        {
            _model = Resources.Load<SelectableModel>("Config/SelectableModel");
            _model.SubscriptionOnSelect(OnChangeItem);
            _info = Object.FindObjectOfType<InfoPanelView>();
            _control = Object.FindObjectOfType<ControlPanelView>();
            _info.Reset();
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
            }
        }

        private void SetButtons(ISelectableItem item)
        {
            _control.ClearButtons();
            _control.SetButtons(item.Executors);
        }
    }
}