using Abstractions;


namespace Core.Buildings
{
    public class BuildingController: IController
    {
        private IBuilding _view;

        public BuildingController(IBuilding building)
        {
            _view = building;
            _view.OnSelect += OnSelectBuild;
        }

        private void OnSelectBuild()
        {
            //TODO:
        }
    }

    public interface IController
    {
    }
}