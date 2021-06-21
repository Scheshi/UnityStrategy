using Core.Buildings;
using Input;
using UI.Presenter;
using UnityEngine;


public class GameStarter : MonoBehaviour
{
    [SerializeField] private TestBuilding startBuilding;
    [SerializeField] private TestBuilding secondBuilding;
    private InputView _input;
    private BuildingController _startBuildingController;
    private Presenter _presenter;

    private void Awake()
    {
        _input = new GameObject("Input").AddComponent<InputView>();
        _input.Init();
        _startBuildingController = new BuildingController(startBuilding);
        //_secondBuildingController = new BuildingController(secondBuilding);
        _presenter = new Presenter();
    }
}