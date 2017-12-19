using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace St
{
    public class SurfaceRow : IEnumerable
    {
        private readonly SurfaceRow _higherRow;
        private SurfaceRow _lowerRow;
        private readonly int _offsetFromHigher;
        private readonly Tile[] _tiles;

        public SurfaceRow(params Tile[] tiles)
        {
            _tiles = tiles;
        }

        public SurfaceRow(SurfaceRow higherRow, int offsetFromHigher, params Tile[] tiles)
        {
            higherRow.LowerRow(this);
            _higherRow = higherRow;
            _offsetFromHigher = offsetFromHigher;
            _tiles = tiles;
        }

        private void LowerRow(SurfaceRow lowerRow)
        {
            _lowerRow = lowerRow;
        }

        public ResourceVector Output => _tiles.Aggregate(ResourceVector.Empty, (total,tile) => total + tile.Output(AdjacencyBonusFor(tile)) );
        public ResourceVector Maintenance => _tiles.Aggregate(ResourceVector.Empty, (total, tile) => total + tile.Maintenance);
        public int UnemployedCount => _tiles.Count(t => t.HasUnemployed);

        public int TileCount => _tiles.Length;
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
            return AdjacentTiles(target).Aggregate(ResourceVector.Empty, (total, adjacentTile) => total + adjacentTile.AdjacencyBonus);
        }

        private IEnumerable<Tile> AdjacentTiles(Tile tile)
        {
            var index = Array.IndexOf(_tiles, tile);

            if (index > 0)
                yield return _tiles[index - 1];

            if (index < _tiles.Length - 1)
                yield return _tiles[index + 1];

            var higherIndex = index + _offsetFromHigher;
            if (_higherRow != null && higherIndex < _higherRow._tiles.Length && higherIndex >= 0)
                yield return _higherRow._tiles[higherIndex];

            var lowerIndex = index - _lowerRow?._offsetFromHigher;
            if (lowerIndex.HasValue && lowerIndex.Value >= 0 && lowerIndex.Value < _lowerRow._tiles.Length)
                yield return _lowerRow._tiles[lowerIndex.Value];
        }

        public SurfaceRow Copy()
        {
            return new SurfaceRow(_tiles.Select(t=>t.Copy()).ToArray());
        }

        public bool Has(Building building) => _tiles.Any(t => t.Has(building));
    }
}