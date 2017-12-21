using System.Collections.Generic;
using static St.ResourceVectorBuilder;

namespace St
{
    public class Building
    {
        private readonly int _maxPlanetCount;

        public static readonly Building None = new Building(
            nameof(None),
            "N/A",
            ResourceVector.Empty,
            ResourceVector.Empty,
            ResourceVector.Empty);

        public static readonly Building PowerPlantI = new Building(nameof(PowerPlantI), "PP1",
            RVB.Energy(2).Vector,
            ResourceVector.Empty,
            RVB.Mineral(60).Vector);

        public static readonly Building PowerPlantII = new Building(nameof(PowerPlantII), "PP2",
            RVB.Energy(3).Vector,
            ResourceVector.Empty,
            RVB.Mineral(90).Vector);

        public static readonly Building PowerPlantIII = new Building(nameof(PowerPlantIII), "PP3",
            RVB.Energy(4).Vector,
            ResourceVector.Empty,
            RVB.Mineral(120).Vector);

        public static readonly Building EnergyGrid = new Building(nameof(EnergyGrid), "EG",
            RVB.Energy(2).Vector,
            ResourceVector.Empty,
            RVB.Mineral(150).Vector,
            RVB.Energy(1.1).Mask, 1);

        public static readonly Building BasicMine = new Building(nameof(BasicMine), "BM.",
            RVB.Mineral(1).Vector,
            RVB.Energy(0.5).Vector,
            RVB.Mineral(30).Vector);

        public static readonly Building MiningNetworkI = new Building(nameof(MiningNetworkI), "MN1",
            RVB.Mineral(2).Vector,
            RVB.Energy(1).Vector,
            RVB.Mineral(60).Vector);

        public static readonly Building MiningNetworkII = new Building(nameof(MiningNetworkII), "MN2",
            RVB.Mineral(3).Vector,
            RVB.Energy(1.5).Vector,
            RVB.Mineral(90).Vector);

        public static readonly Building MiningNetworkIII = new Building(nameof(MiningNetworkIII), "MN3",
            RVB.Mineral(4).Vector,
            RVB.Energy(2).Vector,
            RVB.Mineral(120).Vector);

        public static readonly Building MineralProcessingPlantI = new Building(nameof(MineralProcessingPlantI), "MPI",
            RVB.Mineral(2).Vector,
            RVB.Energy(2).Vector,
            RVB.Mineral(150).Vector,
            RVB.Mineral(1.1).Mask, 1);

        public static readonly Building BasicScienceLab =
            new Building(nameof(BasicScienceLab),
                "BSL",
                RVB.Physics(1).Society(1).Engineering(1).Vector,
                RVB.Energy(1).Vector,
                RVB.Mineral(60).Vector);

        public static readonly Building UplinkNode = new Building(nameof(UplinkNode), "UpN",
            RVB.Unity(1).Vector,
            RVB.Energy(1).Vector,
            RVB.Mineral(100).Vector);

        public static readonly Building PlanetaryAdministration = new Building(nameof(PlanetaryAdministration), "PlA",
            RVB.Energy(4).Unity(1).Vector,
            RVB.Energy(1).Vector,
            RVB.Mineral(350).Vector,
            RVB.Energy(1).Mineral(1).Food(1).Vector, 1);

        public static readonly IEnumerable<Building> BasicBuildings =
            new[] {PowerPlantI, BasicMine, BasicScienceLab, UplinkNode};

        private readonly string _name;
        private readonly ResourceVector _output;

        private Building(string name, string shortname, ResourceVector output, ResourceVector maintenance,
            ResourceVector cost, ResourceVector adjaceny)
        {
            _name = name;
            ShortName = shortname;
            _output = output;
            Maintenance = maintenance;
            Cost = cost;
            AdjacenyBonus = adjaceny;
        }

        private Building(string name, string shortname, ResourceVector output, ResourceVector maintenance,
            ResourceVector cost) : this(name, shortname, output, maintenance, cost, ResourceVector.Empty)
        {
        }

        private Building(string name, string shortname, ResourceVector output, ResourceVector maintenance, ResourceVector cost, ResourceVector.ResourceMask planetaryModifier, int maxPlanetCount) : this(name, shortname, output,
            maintenance, cost, ResourceVector.Empty)
        {
            _maxPlanetCount = maxPlanetCount;
            PlanetaryModifier = planetaryModifier;
        }

        private Building(string name, string shortname, ResourceVector output, ResourceVector maintenance, ResourceVector cost, ResourceVector adjacency, int maxPlanetCount) : this(name, shortname, output, maintenance, cost, adjacency)
        {
            _maxPlanetCount = maxPlanetCount;
        }

        public ResourceVector.ResourceMask PlanetaryModifier { get; } = ResourceVector.ResourceMask.IdentityMask;

        public ResourceVector Cost { get; }

        public ResourceVector AdjacenyBonus { get; }

        public ResourceVector Maintenance { get; }

        public string ShortName { get; }

        public ResourceVector Output(ResourceVector baseResources)
        {
            return (baseResources + _output) * _output.Mask;
        }

        public override string ToString()
        {
            return _name;
        }

        public static IEnumerable<Building> AvailableBuildings(Planet planet)
        {
            if (!planet.Has(PlanetaryAdministration))
                return new[] {PlanetaryAdministration};

            return BasicBuildings;
        }

        public bool IsValid(Planet planet)
        {
            return _maxPlanetCount == 0 || planet.Count(this) < _maxPlanetCount;
        }
    }
}