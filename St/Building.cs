using System.Collections.Generic;
using System.Linq;
using static St.Unit;

namespace St
{
    public class Building
    {
        public static readonly Building None = new Building(nameof(None), Turn.s(0), new Quantity[0], new Quantity[0]);

        public static readonly Building PowerPlantI = new Building(nameof(PowerPlantI),
                                                                   Turn.s(200),
                                                                   new[] {Energy.Points(2)},
                                                                   new Quantity[0],
                                                                   Mineral.Points(-60));

        public static readonly Building BasicMine = new Building(nameof(BasicMine),
                                                                 Turn.s(120),
                                                                 new[] {Mineral.Points(1)},
                                                                 new[] {Energy.Points(-0.5)},
                                                                 Mineral.Points(-30));

        public static readonly Building BasicScienceLab =
            new Building(nameof(BasicScienceLab),
                         Turn.s(200),
                         new[] {Physics.Points(1), Biology.Points(1), Engineering.Points(1)},
                         new[] {Energy.Points(-1)},
                         Mineral.Points(-60));

        public static readonly Building UplinkNode = new Building(nameof(UplinkNode),
                                                                  Turn.s(172),
                                                                  new[] {Unity.Points(2)},
                                                                  new[] {Energy.Points(-1)},
                                                                  Mineral.Points(-100));

        public static readonly IEnumerable<Building> BasicBuildings =
            new[] {PowerPlantI, BasicMine, BasicScienceLab, UplinkNode};

        private readonly Quantity[] _produce;
        private readonly Quantity[] _maintenance;
        private readonly Quantity[] _cost;
        private readonly string _name;
        private readonly Quantity _buildTime;

        private Building(
            string name,
            Quantity buildTime,
            Quantity[] produce,
            Quantity[] maintenance,
            params Quantity[] cost)
        {
            _name = name;
            _buildTime = buildTime;
            _produce = produce;
            _maintenance = maintenance;
            _cost = cost;
        }

        public IEnumerable<Quantity> Produce(Quantity[] tileResources)
        {
            return new List<Quantity>(tileResources.Union(_produce));
        }

        public override string ToString() => _name;

        public IEnumerable<Quantity> Cost => _cost;

        public IEnumerable<Quantity> Maintenance => _maintenance; 
    }
}