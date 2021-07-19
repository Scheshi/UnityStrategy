using System;
using System.Linq;
using Abstractions;
using UI.Model;
using UI.View;
using UnityEngine;
using Utils;


namespace UI.Presenter
{
    public class Presenter: IDisposable
    {
        private ScriptableModel<ISelectableItem> _selectable;
        private ScriptableModel<Vector3> _position;
        private ScriptableModel<IAttackable> _attackable;
        private InfoPanelView _info;
        private ControlPanelView _control;
        private ControlModel _model;
        private ProduceModel _produceModel;
        private ICommandQueue _currentQueue;
        private bool _isPending;


        public Presenter(ControlPanelView control, InfoPanelView info, ScriptableModel<ISelectableItem> selectable, ScriptableModel<Vector3> position, ScriptableModel<IAttackable> attackModel, ControlModel model, ProduceModel produceModel)
        {
            info.EndProduce();
            _selectable = selectable;
            _selectable.OnChangeValue += OnChangeItem;
            _info = info;
            _control = control;
            _info.Reset();
            _model = model;
            _control.OnClick += OnClick;
            _control.OnCancel += _model.OnCancelCommands;
            _position = position;
            _attackable = attackModel;
            _produceModel = produceModel;
            _position.OnChangeValue += OnChangePosition;
            _attackable.OnChangeValue += OnChangeTarget;
        }

        private void OnClick(ICommandExecutor arg1, ICommandQueue arg2)
        {
            _model.OnClick(arg1, arg2);
            _isPending = true;
        }

        private void OnChangePosition()
        {
            if (!_isPending)
            {
                _model.CreateCommand(
                    _selectable.CurrentValue.Executors.FirstOrDefault(x => x.CommandType == typeof(IMoveCommand)),
                    _currentQueue, true);
            }
            else
            {
                _isPending = false;
            }
        }

        private void OnChangeTarget()
        {
            if (!_isPending)
            {
                _model.CreateCommand(
                    _selectable.CurrentValue.Executors.FirstOrDefault(x => x.CommandType == typeof(IAttackCommand)),
                    _currentQueue, true);
            }
            else
            {
                _isPending = false;
            }
        }


        private void OnChangeItem()
        {
            if (_selectable.CurrentValue != null)
            {
                _info.SetInfo(_selectable.CurrentValue.Icon, _selectable.CurrentValue.Name, _selectable.CurrentValue.CurrentHealth, _selectable.CurrentValue.MaxHealth);
                SetButtons(_selectable.CurrentValue);
                _currentQueue = _selectable.CurrentValue.CommandQueue;
            }
            else
            {
                _info.Reset();
                _control.ClearButtons();
            }
            _model.OnCancelCommandCreators();
        }

        private void SetButtons(ISelectableItem item)
        {
            _control.ClearButtons();
            _control.SetButtons(_selectable.CurrentValue.CommandQueue, item.Executors);
        }


        public void Dispose()
        {
            _control.OnClick -= _model.OnClick;
            _selectable.OnChangeValue -= OnChangeItem;
            _attackable.OnChangeValue -= OnChangeTarget;
            _position.OnChangeValue -= OnChangePosition;
            _produceModel = null;
            _control = null;
            _info = null;
            _selectable = null;
            _attackable = null;
            _position = null;
        }
    }
}