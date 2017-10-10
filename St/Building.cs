using System.Collections.Generic;
using System.Linq;
using static St.Unit;
namespace St
{
    public class Building
    {
        private readonly Quantity[] _bonusPoints;
        public static Building None = new Building();

        private Building(params Quantity[] bonusPoints)
        {
            _bonusPoints = bonusPoints;
        }

        public static Building MiningNetworkI()
        {
            return new Building(Mineral.Points(1));
        }

        public IEnumerable<Quantity> Output(Quantity[] tileResources)
        {
            return new List<Quantity>(tileResources.Union(_bonusPoints));
        }
    }
}