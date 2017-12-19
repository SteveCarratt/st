using System.Collections.Generic;
using System.Linq;

namespace St
{
    public class BuildBuilding : PlanetVisitor
    {
        private readonly Planet _planet;
        private readonly Dictionary<Tile, ResourceVector> _tiles = new Dictionary<Tile, ResourceVector>();


        public BuildBuilding(Planet planet)
        {
            _planet = planet;
        }

        public override void Accept(Tile tile)
        {
            var validBuildings = Building.BasicBuildings;

            var diffs = new Dictionary<Building, ResourceVector>();
            foreach (var validBuilding in validBuildings)
            {
                var initalOutput = _planet.Output;
                tile.Construct(validBuilding);
                diffs[validBuilding] = _planet.Output - initalOutput;
            }

            _tiles[tile] = diffs.OrderByDescending(x => x.Value.Score).First().Value;
        }

        public void Build()
        {
            var bestTile = _tiles.OrderByDescending(x=>x.Value.Score).First();
        }
    }
}
