using System;
using System.Collections.Generic;
using System.Linq;
using static St.ResourceVector;

namespace St
{
    public class Tile
    {
        private Population _population = Population.NoWorker;
        private readonly ResourceVector _baseResources;
        private Building _building = Building.None;

        public Tile(ResourceVector baseResources)
        {
            _baseResources = baseResources;
        }

        public Tile() : this(Empty)
        {
        }

        private Tile(ResourceVector baseResources, Building building, Population population) : this(baseResources)
        {
            _building = building;
            _population = population;
        }

        public ResourceVector Output(ResourceVector adjacencyBonus) => _population.Work(_building.Output(_baseResources + adjacencyBonus));
        public ResourceVector Output() => Output(Empty);

        public ResourceVector Maintenance => _population.Upkeep + _building.Maintenance;

        public ResourceVector AdjacencyBonus => _building.AdjacenyBonus;

        public void Populate(Population population)
        {
            _population = population;
        }
        public void Construct(Building building)
        {
            _building = building;
        }

        public bool HasUnemployed => !_population.Equals(Population.NoWorker) && _building.Equals(Building.None);
        public ResourceMask PlanetaryModifier => _building.PlanetaryModifier;

        public IEnumerable<ICommand> Options(Planet planet)
        {
            if (!HasUnemployed)
                return new[] {new BuildPopCommand(this, Population.Worker, planet)};

            return Building.AvailableBuildings(planet).Select(b => new BuildBuildingCommand(this, planet, b));
        }

        public Tile Copy() => new Tile(_baseResources, _building, _population);

        public object Memento() => this.Copy();

        public void Restore(object state)
        {
            if (!(state is Tile other)) throw new ArgumentException("Param was not a tile", nameof(state));
            this._building = other._building;
            this._population = other._population;
        }

        public bool Has(Building building) => _building == building;

        public string PrettyPrint()
        {
            return $"({_population.ShortName}|{_building.ShortName})";
        }
    }
}