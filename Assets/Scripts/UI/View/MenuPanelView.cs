using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View
{
    public class MenuPanelView: MonoBehaviour
    {
        [SerializeField] private Button continueButton;

        public IObservable<Unit> ContinueButtonObservable => continueButton.onClick.AsObservable();
    }
}