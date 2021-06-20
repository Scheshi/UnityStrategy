using Core.Buildings;
using Input;
using UnityEngine;


public class GameStarter : MonoBehaviour
{
    [SerializeField] private TestBuilding startBuilding;
    private InputView _input;
    private BuildingController _startBuildingController;

    private void Awake()
    {
        _input = new GameObject("Input").AddComponent<InputView>();
        _input.Init();
        _startBuildingController = new BuildingController(startBuilding);
    }
}