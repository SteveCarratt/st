using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace St
{
    public class Tile
    {
        private readonly Quantity[] _baseResources;
        private Population _population = Population.NoWorker;
        private Building _building = Building.None;

        public Tile(params Quantity[] baseResources)
        {
            _baseResources = baseResources;
        }

        private Tile(Quantity[] baseResources, Building building, Population population) : this(baseResources)
        {
            _building = building;
            _population = population;
        }

        public IEnumerable<Quantity> Output => _population.Work(_building.Produce(_baseResources)).ToArray();

        public IEnumerable<Quantity> Maintenance() => _population.Input.Union(_building.Maintenance);

        public void Populate(Population population)
        {
            _population = population;
        }
        public void Construct(Building building)
        {
            _building = building;
        }

        public bool HasUnemployed => !_population.Equals(Population.NoWorker) && _building.Equals(Building.None);

        public Tile Copy() => new Tile(_baseResources, _building, _population);

        public object Memento() => this.Copy();

        public void Restore(object state)
        {
            if (!(state is Tile other)) throw new ArgumentException("Param was not a tile", nameof(state));
            this._building = other._building;
            this._population = other._population;
        }
    }
}