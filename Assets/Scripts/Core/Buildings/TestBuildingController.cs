using Abstractions;


namespace Core.Buildings
{
    public class BuildingController
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
}