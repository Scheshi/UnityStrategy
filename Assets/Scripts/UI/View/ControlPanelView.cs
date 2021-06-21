using UnityEngine;
using UnityEngine.UI;


namespace UI.View
{
    public class ControlPanelView: MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private Text textName;
        [SerializeField] private Slider healthBar;

        private void Start()
        {
            Reset();
        }
        
        public void SetInfo(Sprite itemIcon, string itemName, float healthPercent)
        {
            icon.gameObject.SetActive(true);
            textName.gameObject.SetActive(true);
            healthBar.gameObject.SetActive(true);
            icon.sprite = itemIcon;
            textName.text = itemName;
            healthBar.value = healthPercent;
        }

        public void SetInfo(Sprite itemIcon, string itemName, int health, int maxHealth)
        {
            icon.gameObject.SetActive(true);
            textName.gameObject.SetActive(true);
            healthBar.gameObject.SetActive(true);
            icon.sprite = itemIcon;
            textName.text = itemName;
            healthBar.value = (float) health / maxHealth;
        }

        public void Reset()
        {
            icon.gameObject.SetActive(false);
            textName.gameObject.SetActive(false);
            healthBar.gameObject.SetActive(false);
        }
    }
}