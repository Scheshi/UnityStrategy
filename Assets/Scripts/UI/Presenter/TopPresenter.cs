using System;
using Abstractions;
using UI.View;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.Presenter
{
    public class TopPresenter: IDisposable
    {
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
            Debug.Log("Show menu");
        }

        public void Dispose()
        {
            _topPanel = null;
        }
    }
}