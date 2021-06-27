using System;
using Abstractions;
using Commands;
using Core.Buildings;
using Input;
using UI.Model;
using UI.Presenter;
using UI.View;
using UnityEngine;
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
    private void InjectDependency(ScriptableModel<ISelectableItem> selectable, ScriptableModel<Vector3> position)
    {
        _input = new InputController(selectable, position);
    }

    [Inject]
    private void InjectDependencyPresenter(ScriptableModel<ISelectableItem> item, ControlModel model)
    {
        _presenter = new Presenter(control, info, item, model);
    }
    
    private void Awake()
    {
        //_container.Inject(_input);
        
        
        //_secondBuildingController = new BuildingController(secondBuilding);
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
}