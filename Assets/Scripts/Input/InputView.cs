using Abstractions;
using UnityEngine;
using Zenject;


namespace Input
{
    public class InputView : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [Inject]private ScriptableModel<ISelectableItem> _selectable;
        [Inject]private ScriptableModel<Vector3> _position;

        public void Init()
        {
            mainCamera = Camera.main;
        }
        
        private void Update()
        {
            if (UnityEngine.Input.GetButtonDown("Fire1"))
            {
                if (Physics.Raycast(mainCamera.ScreenPointToRay(UnityEngine.Input.mousePosition), out var hit)) 
                {
                    if (!hit.collider.gameObject.CompareTag("NotRaycast"))
                    {
                        ISelectableItem selectable = hit.collider.gameObject.GetComponent<ISelectableItem>();
                        _selectable.SetValue(selectable); 
                    }
                }
            }

            if (UnityEngine.Input.GetButtonDown("Fire2"))
            {
                if (Physics.Raycast(mainCamera.ScreenPointToRay(UnityEngine.Input.mousePosition), out var hit)) 
                {
                    
                    _position.SetValue(hit.point);
                }
            }
        }
    }
}
