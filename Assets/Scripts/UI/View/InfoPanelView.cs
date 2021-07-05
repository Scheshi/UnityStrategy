using UnityEngine;
using UnityEngine.UI;


namespace UI.View
{
    public class InfoPanelView: MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private Text textName;
        [SerializeField] private Slider healthBar;
        [SerializeField] private Slider produceSlider;

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

        public void EndProduce()
        {
            produceSlider.gameObject.SetActive(false);
        }

        public void SetValueProduce(float value)
        {
            if (produceSlider.gameObject.activeSelf)
            {
                produceSlider.value = produceSlider.maxValue - value;
            }
        }

        public void StartProduce()
        {
            produceSlider.gameObject.SetActive(true);
        }
    }
}