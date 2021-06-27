using Commands;
using Core.Buildings;
using Input;
using UI.Presenter;
using UnityEngine;
using Zenject;


public class GameStarter : MonoBehaviour
{
    [SerializeField] private TestBuilding startBuilding;
    [SerializeField] private TestBuilding secondBuilding;
    private InputView _input;
    private BuildingController _startBuildingController;
    [Inject] private Presenter _presenter;
    [Inject(Id = "Utils")] private DiContainer _container;

    private void Awake()
    {
       _input = new GameObject("Input").AddComponent<InputView>();
        _startBuildingController = new BuildingController(startBuilding);
        startBuilding.SetExecutors(new ProduceUnitCommandExecutor(startBuilding.transform.position + Vector3.forward * 10));
        //_secondBuildingController = new BuildingController(secondBuilding);
    }

    private void Start()
    {
        _container.Inject(_input);
        _input.Init();
    }
}