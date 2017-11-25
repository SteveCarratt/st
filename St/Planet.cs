using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace St
{
    public class Planet
    {
        private readonly Tile[] _tiles;

        public Planet(params Tile[] tiles)
        {
            _tiles = tiles;
        }

        public IEnumerable<Quantity> Output()
        {
            return _tiles.SelectMany(x => x.Output());
        }

        public int UnemployedPopCount => _tiles.Count(t => t.HasUnemployed);

        public Planet Copy()
        {
            return new Planet(_tiles.Select(t=>t.Copy()).ToArray());
        }

        public IEnumerable<Quantity> Maintenance()
        {
            return _tiles.SelectMany(t => t.Maintenance());
        }
    }
}
