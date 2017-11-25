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

        public IEnumerable<Quantity> Output()
        {
            return _population.Work(_building.Produce(_baseResources));
        }

        public IEnumerable<Quantity> Maintenance()
        {
            return _population.Input().Union(_building.Maintenance());
        }

        public void Populate(Population population)
        {
            _population = population;
        }
        public void Construct(Building building)
        {
            _building = building;
        }

        public bool HasUnemployed => _population != Population.NoWorker && _building == Building.None;

        public Tile Copy()
        {
            return new Tile(_baseResources, _building, _population);
        }
    }
}