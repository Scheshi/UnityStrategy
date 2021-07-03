using System;
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
    [SerializeField] private ControlPanelView control;
    [SerializeField] private InfoPanelView info;
    [SerializeField] private TestBuilding startBuilding;
    [SerializeField] private TestBuilding secondBuilding;
    private InputController _input;
    private BuildingController _startBuildingController;
    private Presenter _presenter;

    [Inject]
    private void InjectDependency(ScriptableModel<ISelectableItem> selectable, ScriptableModel<Vector3> position, ScriptableModel<IAttackable> target, ControlModel model, ProduceModel produceModel)
    {
        _input = new InputController(selectable, position, target);
        _presenter = new Presenter(control, info, selectable, position, target, model, produceModel);
    }

    private void Start()
    {
        _input.Init();
        _startBuildingController = new BuildingController(startBuilding);
        startBuilding.SetExecutors(new ProduceUnitCommandExecutor(startBuilding.transform.position + Vector3.forward * 10));
    }

    private void Update()
    {
        _input.Update();
    }

    private void OnDestroy()
    {
        _presenter.Dispose();
        _input.Dispose();
    }
}