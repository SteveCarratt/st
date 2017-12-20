using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static St.ResourceVector;

namespace St
{
    public class SurfaceRow : IEnumerable
    {
        private readonly SurfaceRow _higherRow;
        private SurfaceRow _lowerRow;
        private readonly int _rowoffset;
        private readonly Tile[] _tiles;

        public SurfaceRow(params Tile[] tiles)
        {
            _tiles = tiles;
        }

        public SurfaceRow(int rowoffset, params Tile[] tiles)
        {
            _rowoffset = rowoffset;
            _tiles = tiles;
        }

        private SurfaceRow(SurfaceRow higherRow, int rowoffset, params Tile[] tiles)
        {
            higherRow.LowerRow(this);
            _higherRow = higherRow;
            _rowoffset = rowoffset;
            _tiles = tiles;
        }

        private void LowerRow(SurfaceRow lowerRow)
        {
            _lowerRow = lowerRow;
        }

        public ResourceVector Output => _tiles.Aggregate(Empty, (total,tile) => total + tile.Output(AdjacencyBonusFor(tile)) );
        public ResourceVector Maintenance => _tiles.Aggregate(Empty, (total, tile) => total + tile.Maintenance);
        public int UnemployedCount => _tiles.Count(t => t.HasUnemployed);

        public int TileCount => _tiles.Length;

        public ResourceMask PlanetaryModifier => _tiles.Aggregate(ResourceMask.IdentityMask, (res, tile) => res + tile.PlanetaryModifier);

        public IEnumerable<ICommand> Options(Planet planet) => _tiles.SelectMany(t => t.Options(planet));

        public IEnumerator GetEnumerator()
        {
            return _tiles.GetEnumerator();
        }

        public void Visit(PlanetVisitor visitor)
        {
            foreach (var tile in _tiles)
            {
                visitor.Accept(tile);
            }
        }

        private ResourceVector AdjacencyBonusFor(Tile target)
        {
            return AdjacentTiles(target).Aggregate(Empty, (total, adjacentTile) => total + adjacentTile.AdjacencyBonus);
        }

        private IEnumerable<Tile> AdjacentTiles(Tile tile)
        {
            var index = Array.IndexOf(_tiles, tile);

            if (index > 0)
                yield return _tiles[index - 1];

            if (index < _tiles.Length - 1)
                yield return _tiles[index + 1];

            var higherIndex = index + (_rowoffset-_higherRow?._rowoffset);
            if (higherIndex.HasValue && higherIndex.Value < _higherRow._tiles.Length && higherIndex.Value >= 0)
                yield return _higherRow._tiles[higherIndex.Value];

            var lowerIndex = index + (_rowoffset - _lowerRow?._rowoffset);
            if (lowerIndex.HasValue && lowerIndex.Value >= 0 && lowerIndex.Value < _lowerRow._tiles.Length)
                yield return _lowerRow._tiles[lowerIndex.Value];
        }

        public SurfaceRow Copy()
        {
            return new SurfaceRow(_rowoffset, _tiles.Select(t=>t.Copy()).ToArray());
        }

        public SurfaceRow AppendRow(int offset, params Tile[] tiles)
        {
            return new SurfaceRow(this, _rowoffset + offset, tiles);
        }

        public SurfaceRow AppendRow(params Tile[] tiles) => AppendRow(0, tiles);

        public bool Has(Building building) => _tiles.Any(t => t.Has(building));

        public string TileRepresentation(Tile targetTile)
        {
            var sb = new StringBuilder();
            sb.Append(new string('_', _rowoffset*3));
            foreach (var tile in _tiles)
            {
                sb.Append(targetTile == tile ? "(x)" : "(o)");
            }
            return sb.ToString();
        }

        public string PrettyPrint()
        {
            var sb = new StringBuilder();
            sb.Append(new string('_', _rowoffset * 9));
            foreach (var tile in _tiles)
            {
                sb.Append(tile.PrettyPrint());
            }
            return sb.ToString();
        }
    }
}