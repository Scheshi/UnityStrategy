using System;
using Abstractions;
using UniRx;
using UnityEngine;
using Zenject;


namespace Input
{
    public class InputController: IDisposable
    {
        private Camera _mainCamera;
        private ScriptableModel<ISelectableItem> _selectable;
        private ScriptableModel<Vector3> _position;
        private ScriptableModel<IAttackable> _target;

        public InputController(ScriptableModel<ISelectableItem> selectable, ScriptableModel<Vector3> position, ScriptableModel<IAttackable> target)
        {
            _selectable = selectable;
            _position = position;
            _target = target;
        }
        public void Init()
        {
            _mainCamera = Camera.main;
            var leftClick = Observable.EveryUpdate().Where(_ => UnityEngine.Input.GetButtonDown("Fire1"));
            leftClick.Subscribe(_ =>
            {
                if (Physics.Raycast(_mainCamera.ScreenPointToRay(UnityEngine.Input.mousePosition), out var hit)) 
                {
                    if (!hit.collider.gameObject.CompareTag("NotRaycast"))
                    {
                        ISelectableItem selectable = hit.collider.gameObject.GetComponent<ISelectableItem>();
                        _selectable.SetValue(selectable); 
                    }
                }
            });
            var rightClick = Observable.EveryUpdate().Where(_ => UnityEngine.Input.GetButtonDown("Fire2"));
            rightClick.Subscribe(_ =>
            {
                if (Physics.Raycast(_mainCamera.ScreenPointToRay(UnityEngine.Input.mousePosition), out var hit))
                {
                    if (hit.collider.gameObject.TryGetComponent(out IAttackable target))
                    {
                        _target.SetValue(target);
                    }

                    _position.SetValue(hit.point);
                }
            });
        }

        public void Dispose()
        {
            _selectable = null;
            _position = null;
            _mainCamera = null;
        }
    }
}
