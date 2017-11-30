using System.Collections.Generic;
using System.Linq;

namespace St
{
    public class BuildBuilding : PlanetVisitor
    {
        private readonly Planet _planet;
        private Dictionary<Tile, IEnumerable<Quantity>> _tiles = new Dictionary<Tile, IEnumerable<Quantity>>();


        public BuildBuilding(Planet planet)
        {
            _planet = planet;
        }

        public override void Accept(Tile tile)
        {
            var validBuildings = Building.BasicBuildings;

            var diffs = new Dictionary<Building, IEnumerable<Quantity>>();
            foreach (var validBuilding in validBuildings)
            {
                var initalOutput = _planet.Output;
                tile.Construct(validBuilding);
                diffs[validBuilding] = Quantity.Subtract(_planet.Output, initalOutput);
            }

            _tiles[tile] = diffs.OrderByDescending(x => x.Value.Score()).First().Value;
        }

        public void Build()
        {
            var bestTile = _tiles.OrderByDescending(x=>x.Value.Score()).First();
        }
    }
}
