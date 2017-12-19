using System.Collections.Generic;
using static St.ResourceVectorBuilder;
using static St.Unit;

namespace St
{
    public class Building
    {
        public static readonly Building None = new Building(
            nameof(None),
            ResourceVector.Empty,
            ResourceVector.Empty,
            ResourceVector.Empty);

        public static readonly Building PowerPlantI = new Building(nameof(PowerPlantI),
            RVB.Energy(2).Vector,
            ResourceVector.Empty,
            RVB.Mineral(60).Vector);

        public static readonly Building BasicMine = new Building(nameof(BasicMine),
            RVB.Mineral(1).Vector,
            RVB.Energy(0.5).Vector,
            RVB.Mineral(30).Vector);

        public static readonly Building BasicScienceLab =
            new Building(nameof(BasicScienceLab),
                RVB.Physics(1).Biology(1).Engineering(1).Vector,
                RVB.Energy(1).Vector,
                RVB.Mineral(60).Vector);

        public static readonly Building UplinkNode = new Building(nameof(UplinkNode),
            RVB.Unity(1).Vector,
            RVB.Energy(1).Vector,
            RVB.Mineral(100).Vector);

        public static readonly Building PlanetaryAdministration = new Building(nameof(PlanetaryAdministration),
            RVB.Energy(4).Unity(1).Vector,
            RVB.Energy(1).Vector,
            RVB.Mineral(350).Vector,
            RVB.Energy(1).Mineral(1).Food(1).Vector);

        public static readonly IEnumerable<Building> BasicBuildings =
            new[] {PowerPlantI, BasicMine, BasicScienceLab, UplinkNode};

        private readonly string _name;
        private readonly ResourceVector _output;

        private Building(
            string name,
            ResourceVector output,
            ResourceVector maintenance,
            ResourceVector cost,
            ResourceVector adjaceny)
        {
            _name = name;
            _output = output;
            Maintenance = maintenance;
            Cost = cost;
            AdjacenyBonus = adjaceny;
        }

        private Building(
            string name,
            ResourceVector output,
            ResourceVector maintenance,
            ResourceVector cost) : this(name,  output, maintenance, cost, ResourceVector.Empty)
        {
        }

        public ResourceVector Cost { get; }

        public ResourceVector AdjacenyBonus { get; }

        public ResourceVector Maintenance { get; }

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
    }
}