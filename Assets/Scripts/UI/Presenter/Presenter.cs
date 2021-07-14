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

        
        public Presenter(ControlPanelView control, InfoPanelView info, ScriptableModel<ISelectableItem> selectable, ScriptableModel<Vector3> position, ScriptableModel<IAttackable> attackModel, ControlModel model, ProduceModel produceModel)
        {
            info.EndProduce();
            _selectable = selectable;
            _selectable.OnChangeValue += OnChangeItem;
            _info = info;
            _control = control;
            _info.Reset();
            _model = model;
            _control.OnClick += _model.OnClick;
            _control.OnCancel += _model.OnCancelCommands;
            _position = position;
            _attackable = attackModel;
            _produceModel = produceModel;
            _produceModel.OnStartProduce += OnStartProduce;
            _produceModel.OnEndProduce += OnEndProduce;
            _produceModel.OnChangeValue += OnChangeProduceValue;
            _position.OnChangeValue += OnChangePosition;
            _attackable.OnChangeValue += OnChangeTarget;
        }

        private void OnChangePosition()
        {
            _model.CreateCommand(_selectable.CurrentValue.Executors.FirstOrDefault(x => x.CommandType == typeof(IMoveCommand)), true);
        }

        private void OnChangeTarget()
        {
            _model.CreateCommand(_selectable.CurrentValue.Executors.FirstOrDefault(x => x.CommandType == typeof(IAttackCommand)), true);
        }

        private void OnStartProduce()
        {
            _info.StartProduce();
        }

        private void OnChangeProduceValue()
        {
            _info.SetValueProduce(_produceModel.CurrentValue);
        }

        private void OnEndProduce()
        {
            _info.EndProduce();
        }
        

        private void OnChangeItem()
        {
            if (_selectable.CurrentValue != null)
            {
                _info.SetInfo(_selectable.CurrentValue.Icon, _selectable.CurrentValue.Name, _selectable.CurrentValue.CurrentHealth, _selectable.CurrentValue.MaxHealth);
                SetButtons(_selectable.CurrentValue);
                if (_selectable.CurrentValue.Executors != null && _produceModel.CurrentValue > 0)
                {
                    ICommandExecutor executor =
                        _selectable.CurrentValue.Executors.FirstOrDefault(x =>
                            x.CommandType == typeof(ICreateUnitCommand));
                    if (executor != null)
                    {
                        _info.StartProduce();
                        _info.SetValueProduce(_produceModel.CurrentValue);
                    }
                    else
                    {
                        _info.EndProduce();
                    }
                }
                else
                {
                    _info.EndProduce();
                }
            }
            else
            {
                _info.Reset();
                _control.ClearButtons();
                _info.EndProduce();
            }
            _model.OnCancelCommandCreators();
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
            _attackable.OnChangeValue -= OnChangeTarget;
            _position.OnChangeValue -= OnChangePosition;
            _produceModel.OnStartProduce -= OnStartProduce;
            _produceModel.OnEndProduce -= OnEndProduce;
            _produceModel.OnChangeValue -= OnChangeProduceValue;
            _produceModel = null;
            _control = null;
            _info = null;
            _selectable = null;
            _attackable = null;
            _position = null;
        }
    }
}