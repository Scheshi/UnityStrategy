using Abstractions;
using UnityEngine;


namespace Input
{
    public class InputView : MonoBehaviour
    {
        [SerializeField]private Camera mainCamera;

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
                    ISelectableItem selectable = hit.collider.gameObject.GetComponent<ISelectableItem>();
                    selectable?.Select();
                }
            }
        }
    }
}
