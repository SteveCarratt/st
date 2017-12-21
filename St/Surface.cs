using System.Collections.Generic;
using System.Linq;
using System.Text;
using static St.ResourceVector;
using static St.ResourceVector.ResourceMask;

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
        public ResourceVector Output => _rows.Any() ? _rows.Aggregate(Empty, (l,r)=> l+r.Output) : Empty;
        public ResourceVector Maintenance => _rows.Any() ? _rows.Aggregate(Empty, (l, r) => l + r.Maintenance) : Empty;
        public ResourceMask PlanetaryModifier => _rows.Any()? _rows.Aggregate(IdentityMask, (res, r)=> res+r.PlanetaryModifier) : IdentityMask;
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

        public string TileRepresentation(Tile targetTile)
        {
            var sb = new StringBuilder();
            
            foreach (var surfaceRow in _rows)
            {
                sb.AppendLine(surfaceRow.TileRepresentation(targetTile));
            }
            return sb.ToString();
        }

        public string PrettyPrint()
        {
            var sb = new StringBuilder();

            foreach (var surfaceRow in _rows)
            {
                sb.AppendLine(surfaceRow.PrettyPrint());
            }
            return sb.ToString();
        }

        public string PrettyOutput()
        {
            var sb = new StringBuilder();

            foreach (var surfaceRow in _rows)
            {
                sb.AppendLine(surfaceRow.PrettyOutput());
            }
            return sb.ToString();
        }


        public int Count(Building building) => _rows.Sum(r => r.Count(building));
    }
}
