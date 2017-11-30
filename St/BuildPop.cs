using System.Collections.Generic;
using System.Linq;

namespace St
{
    public class BuildPop : PlanetVisitor
    {
        private readonly Planet _originalPlanet;
        private readonly Dictionary<Tile, IEnumerable<Quantity>> _tiles = new Dictionary<Tile, IEnumerable<Quantity>>();

        public BuildPop(Planet originalPlanet)
        {
            _originalPlanet = originalPlanet;
        }

        public override void Accept(Tile tile)
        {
            var tileState = tile.Memento();
            var initial = _originalPlanet.Output;
            tile.Populate(Population.Worker);
            _tiles[tile] = Quantity.Subtract(_originalPlanet.Output, initial);
            tile.Restore(tileState);
        }

        public void Build()
        {
            _tiles.OrderByDescending(x => Quantity.Score(x.Value)).First().Key.Populate(Population.Worker);
        }
    }
}
