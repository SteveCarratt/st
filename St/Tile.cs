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

        private delegate ResourceVector AdjacencyBonusStrategy();
        private AdjacencyBonusStrategy _adjacencyBonusStrategy;
        private static readonly AdjacencyBonusStrategy BlockedAdjacencyBonusStrategy = () => ResourceVector.Empty;
        private readonly AdjacencyBonusStrategy _unblockedAdjacencyBonusStrategy;

        private delegate ResourceVector UpkeepStrategy();

        private UpkeepStrategy _upkeepStrategy;
        private static readonly UpkeepStrategy BlockedUpkeepStrategy = () => ResourceVector.Empty;
        private readonly UpkeepStrategy _unblockedUpkeepStrategy;


        private delegate ResourceVector OutputStrategy(ResourceVector adjacencyBonus);
        private OutputStrategy _outputStrategy;
        private static readonly OutputStrategy BlockedOutputStrategy = bonus => ResourceVector.Empty;
        private readonly OutputStrategy _unblockedOutputStrategy;

        public Tile(ResourceVector baseResources)
        {
            _baseResources = baseResources;
            _unblockedOutputStrategy = bonus => _population.Work(_building.Output(_baseResources + bonus));
            _unblockedUpkeepStrategy = () => _population.Upkeep + _building.Maintenance;
            _unblockedAdjacencyBonusStrategy = () => _building.AdjacenyBonus;
            _outputStrategy = _unblockedOutputStrategy;
            _upkeepStrategy = _unblockedUpkeepStrategy;
            _adjacencyBonusStrategy = _unblockedAdjacencyBonusStrategy;
        }

        public Tile() : this(Empty)
        {
        }

        private Tile(ResourceVector baseResources, Building building, Population population) : this(baseResources)
        {
            _building = building;
            _population = population;
        }

        public ResourceVector Output(ResourceVector adjacencyBonus) => _outputStrategy(adjacencyBonus);
        public ResourceVector Output() => Output(Empty);

        public ResourceVector Maintenance => _upkeepStrategy();

        public ResourceVector AdjacencyBonus => _adjacencyBonusStrategy();

        public Tile Populate(Population population)
        {
            _population = population;
            return this;
        }
        public Tile Construct(Building building)
        {
            _building = building;
            return this;
        }

        public Tile Block()
        {
            _outputStrategy = BlockedOutputStrategy;
            _upkeepStrategy = BlockedUpkeepStrategy;
            return this;
        }

        public Tile Unblock()
        {
            _outputStrategy = _unblockedOutputStrategy;
            _upkeepStrategy = _unblockedUpkeepStrategy;
            return this;
        }

        public bool HasUnemployed => !_population.Equals(Population.NoWorker) && _building.Equals(Building.None);
        public ResourceMask PlanetaryModifier => _building.PlanetaryModifier;

        public IEnumerable<ICommand> Options(Planet planet)
        {
            if (!HasUnemployed)
                return new[] {new BuildPopCommand(this, Population.Worker, planet)};

            return Building.AvailableBuildings(planet).Select(b => new BuildBuildingCommand(this, planet, b, _population));
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