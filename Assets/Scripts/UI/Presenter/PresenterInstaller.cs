using UI.View;
using UnityEngine;
using Zenject;

namespace UI.Presenter
{
    public class PresenterInstaller: MonoInstaller
    {
        [SerializeField] private ControlPanelView controlPanelView;
        [SerializeField] private InfoPanelView infoPanelView;

        public override void InstallBindings()
        {
            Container.Bind<Presenter>().FromInstance(new Presenter(infoPanelView, controlPanelView)).AsSingle();
        }
    }
}