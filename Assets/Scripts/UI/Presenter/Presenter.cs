using Abstractions;
using UI.Model;
using UI.View;
using UnityEngine;

namespace UI.Presenter
{
    public class Presenter
    {
        private SelectableModel _model;
        private ControlPanelView _view;

        public Presenter()
        {
            _model = Resources.Load<SelectableModel>("Config/SelectableModel");
            _model.SubscriptionOnSelect(OnChangeItem);
            _view = Object.FindObjectOfType<ControlPanelView>();
        }

        public void OnChangeItem(ISelectableItem item)
        {
            if (item != null)
            {
                _view.SetInfo(null, item.Name, item.CurrentHealth, item.MaxHealth);
            }
            else
            {
                _view.Reset();
            }
        }
    }
}