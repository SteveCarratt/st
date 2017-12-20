using System.Collections.Generic;

namespace St
{
    public class BuildBuildingCommand : ICommand
    {
        private readonly Tile _targetTile;
        private readonly Planet _planet;
        private readonly Building _building;
        private object _initialState;

        public BuildBuildingCommand(Tile targetTile, Planet planet, Building building)
        {
            _targetTile = targetTile;
            _planet = planet;
            _building = building;
        }

        public void Execute()
        {
            _initialState = _targetTile.Memento();
            _targetTile.Construct(_building);
        }

        public void Undo()
        {
            _targetTile.Restore(_initialState);
        }

        public ResourceVector Increase
        {
            get
            {
                var initial = _planet.Output;
                Execute();
                var diff = _planet.Output - initial;
                Undo();
                return diff;
            }
        }

        public decimal Score => Increase.Score;
        public string PrettyPrint() => ToString();
        public override string ToString() => $"Bulding {_building} on \n{_planet.TileRepresentation(_targetTile)}\n of {_planet} yields {Score} - {Increase.PrettyPrint()}";
    }
}