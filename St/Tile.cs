using System.Collections;
using System.Collections.Generic;

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

        public IEnumerable<Quantity> Output()
        {
            return _population.Work(_building.Output(_baseResources));
        }

        public IEnumerable<Quantity> Input()
        {
            return _population.Input;
        }

        public void Populate(Population population)
        {
            _population = population;
        }
        public void Construct(Building building)
        {
            _building = building;
        }
    }
}