namespace St
{
    public class SurfaceRow
    {
        private readonly SurfaceRow _higherRow;
        private SurfaceRow _lowerRow;
        private readonly int _offset;
        private readonly Tile[] _tiles;

        public SurfaceRow(params Tile[] tiles)
        {
            _tiles = tiles;
        }

        public SurfaceRow(SurfaceRow higherRow, int offset, params Tile[] tiles)
        {
            higherRow.LowerRow(this);
            _higherRow = higherRow;
            _offset = offset;
            _tiles = tiles;
        }

        private void LowerRow(SurfaceRow lowerRow)
        {
            _lowerRow = lowerRow;
        }

        public int TileCount => _tiles.Length;
    }
}