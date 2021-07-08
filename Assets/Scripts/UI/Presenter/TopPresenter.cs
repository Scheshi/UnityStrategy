using System;
using Abstractions;
using UI.View;
using UniRx;
using Zenject;
using UnityEngine;
using Object = UnityEngine.Object;


namespace UI.Presenter
{
    public class TopPresenter: IDisposable
    {
        private const string PathToMenuPrefab = "MenuPanel";
        private TopPanelView _topPanel;
        [Inject] private ITimeModel _timeModel;
        
        public TopPresenter(TopPanelView view, ITimeModel time)
        {
            _topPanel = view;
            _timeModel = time;
            _timeModel.TimeTick.Subscribe(i => _topPanel.Time = TimeSpan.FromSeconds(i).ToString());
            _topPanel.MenuButton.Subscribe(_ => OnMenuButtonClick());
        }

        private void OnMenuButtonClick()
        {
            Time.timeScale = 0.0f;
            MenuPanelView view = Object.Instantiate(Resources.Load<MenuPanelView>(PathToMenuPrefab), _topPanel.transform);
            view.ContinueButtonObservable.Subscribe(_ =>
            {
                Time.timeScale = 1.0f;
                Object.Destroy(view.gameObject);
            });
        }

        public void Dispose()
        {
            _topPanel = null;
        }
    }
}