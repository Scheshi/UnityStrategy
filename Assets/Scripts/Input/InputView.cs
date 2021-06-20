using Abstractions;
using UI.Model;
using UnityEngine;


namespace Input
{
    public class InputView : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        private SelectableModel _model;

        public void Init()
        {
            mainCamera = Camera.main;
            _model = Resources.Load<SelectableModel>("Config/SelectableModel");
        }
        
        private void Update()
        {
            if (UnityEngine.Input.GetButtonDown("Fire1"))
            {
                if (Physics.Raycast(mainCamera.ScreenPointToRay(UnityEngine.Input.mousePosition), out var hit))
                {
                    if (hit.collider.gameObject.layer != LayerMask.NameToLayer("UI"))
                    {
                        ISelectableItem selectable = hit.collider.gameObject.GetComponent<ISelectableItem>();
                        selectable?.Select();
                        _model.SelectItem(selectable);
                    }
                }
            }
        }
    }
}
