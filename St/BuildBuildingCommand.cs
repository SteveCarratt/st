using System;
using System.Collections.Generic;

namespace St
{
    public class BuildBuildingCommand : ICommand
    {
        private readonly Tile _targetTile;
        private readonly Planet _planet;
        private readonly Building _building;
        private readonly Population _pop;
        private object _initialState;

        public BuildBuildingCommand(Tile targetTile, Planet planet, Building building, Population pop)
        {
            _targetTile = targetTile;
            _planet = planet;
            _building = building;
            _pop = pop;
        }

        public void Execute()
        {
            _initialState = _targetTile.Memento();
            Execute(_targetTile, _planet, _building, _pop);
        }

        private static Planet Execute(Tile targetTile, Planet planet, Building building, Population pop)
        {
            if (building.IsValid(planet))
            {
                targetTile.Populate(pop);
                targetTile.Construct(building);
            }
            return planet;
        }

        public void Undo()
        {
            _targetTile.Restore(_initialState);
        }

        public ResourceVector Increase
        {
            get
            {
                var newPlanet = Execute(_targetTile, _planet.Copy(), _building, _pop);
                return (newPlanet.Output - _planet.Maintenance) - (_planet.Output - _planet.Maintenance);
            }
        }

        public decimal Score
        {
            get
            {
                var p = Execute(_targetTile, _planet.Copy(), _building, _pop);
                return (p.Output - p.Maintenance).Score;
            }
        }
        public string PrettyPrint() => ToString();
        public override string ToString() => $"Bulding {_building} on \n{_planet.TileRepresentation(_targetTile)} of {_planet} yields {Score} - {Increase.PrettyPrint()}";
    }
}