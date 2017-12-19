using System.Collections.Generic;
using System.Linq;

namespace St
{
    public class Surface
    {
        private readonly SurfaceRow[] _rows;

        public Surface(params SurfaceRow[] rows)
        {
            _rows = rows;
        }

        public int TileCount => _rows.Sum(x => x.TileCount);
        public IEnumerable<Quantity> Output => _rows.Any() ? _rows.Select(x => x.Output).Aggregate(Quantity.Add) : Enumerable.Empty<Quantity>();
        public IEnumerable<Quantity> Maintenance => _rows.Any() ? _rows.Select(x=>x.Maintenance).Aggregate(Quantity.Add): Enumerable.Empty<Quantity>();

        public int UnemployedCount => _rows.Sum(t => t.UnemployedCount);
        public bool HasUnemployed => UnemployedCount > 0;

        public IEnumerable<ICommand> Options(Planet planet) => _rows.SelectMany(r => r.Options(planet));

        public void Visit(PlanetVisitor visitor)
        {
            foreach (var row in _rows)
            {
                row.Visit(visitor);
            }
        }

        public Surface Copy()
        {
            return new Surface(_rows.Select(r=>r.Copy()).ToArray());
        }

        public bool Has(Building building) => _rows.Any(r => r.Has(building));
    }
}
