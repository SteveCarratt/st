using System.Linq;

namespace St
{
    public class Surface
    {
        private readonly SurfaceRow[] _rows;

        public Surface(SurfaceRow[] rows)
        {
            _rows = rows;
        }

        public int TileCount => _rows.Sum(x => x.TileCount);
    }
}
