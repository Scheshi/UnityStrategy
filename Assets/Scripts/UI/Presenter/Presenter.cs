using System;
using Abstractions;
using Commands;
using UI.Model;
using UI.View;
using UnityEngine;
using Utils;
using Object = UnityEngine.Object;


namespace UI.Presenter
{
    public class Presenter: IDisposable
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
            _control.OnClick += OnClick;
        }

        private void OnClick(ICommandExecutor executor)
        {
            if (executor is CommandExecutorBase<ICreateUnitCommand> unitCreater)
            {
                AssetCollection collection = Resources.Load<AssetCollection>("Config/Collection");
                unitCreater.Execute(collection.InjectAsset(new ProduceUnitCommand()));
                Resources.UnloadAsset(collection);
            }
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
            _control.OnClick -= OnClick;
            _control = null;
            _info = null;
            _model = null;
        }
    }
}