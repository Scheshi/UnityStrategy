using Abstractions;
using Commands;
using Core.Buildings;
using Input;
using UI.Model;
using UI.Presenter;
using UI.View;
using UnityEngine;
using Utils;
using Zenject;


public class GameStarter : MonoBehaviour
{
    [SerializeField] private ProducePanelView producePanelView;
    [SerializeField] private TopPanelView topPanelView;
    [SerializeField] private ControlPanelView control;
    [SerializeField] private InfoPanelView info;
    [SerializeField] private TestBuilding startBuilding;
    [SerializeField] private TestBuilding secondBuilding;
    private InputController _input;
    private BuildingController _startBuildingController;
    private Presenter _presenter;
    private TopPresenter _topPresenter;
    private ProducePresenter _producePresenter;

    [Inject]
    private void InjectDependency(ScriptableModel<ISelectableItem> selectable, ScriptableModel<Vector3> position, ScriptableModel<IAttackable> target, ControlModel model, ProduceModel produceModel)
    {
        _input = new InputController(selectable, position, target);
        _presenter = new Presenter(control, info, selectable, position, target, model, produceModel);
        _producePresenter = new ProducePresenter(producePanelView, new ProducePanel(selectable));
    }

    [Inject]
    private void InjectDependency(ITimeModel timeModel)
    {
        _topPresenter = new TopPresenter(topPanelView, timeModel);
    }

    private void Start()
    {
        _input.Init();
        _startBuildingController = new BuildingController(startBuilding);
        startBuilding.SetExecutors(new ProduceUnitCommandExecutor(startBuilding), new SpawnPointExecutor(startBuilding));
    }

    private void OnDestroy()
    {
        _presenter.Dispose();
        _input.Dispose();
    }
}