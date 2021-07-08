using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View
{
    public class TopPanelView: MonoBehaviour
    {
        [SerializeField] private Text timeText;
        [SerializeField] private Button menuButton;

        public string Time
        {
            set => timeText.text = value;
        }

        public IObservable<Unit> MenuButton => menuButton.onClick.AsObservable();
    }
}