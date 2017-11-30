using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace St
{
    public class BuildPop
    {
        private readonly Planet _originalPlanet;
        private readonly Dictionary<Tile, IEnumerable<Quantity>> _tiles = new Dictionary<Tile, IEnumerable<Quantity>>();

        public BuildPop(Planet originalPlanet)
        {
            _originalPlanet = originalPlanet;
        }

        public void AcceptTile(Tile tile)
        {
            var initial = _originalPlanet.Output();
            tile.Populate(Population.Worker);
            _tiles[tile] = Quantity.Subtract(_originalPlanet.Output(), initial);
        }

        public void Build()
        {
            _tiles.OrderByDescending(x => Quantity.Score(x.Value)).First().Key.Populate(Population.Worker);
        }
    }
}
