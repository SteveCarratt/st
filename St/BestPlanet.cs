using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace St
{
    public class BestPlanet
    {
        private readonly Planet _planet;
        private readonly Building[] _allowedBuildings;

        public BestPlanet(Planet planet, params Building[] allowedBuildings)
        {
            _planet = planet;
            _allowedBuildings = allowedBuildings;
        }

        public string Summary()
        {
            new PopulateAllTiles(_planet, Population.Robot).Execute();

            var mineralPlanet = Optimisefor(Building.MiningNetworkI);
            var energyPlanet = Optimisefor(Building.PowerPlantI);

            return $"Minerals:\n{mineralPlanet.Output-mineralPlanet.Maintenance}\n{mineralPlanet.PrettyPrint()}" +
                   $"Energy:\n{energyPlanet.Output-energyPlanet.Maintenance}\n{energyPlanet.PrettyPrint()}";
        }

        private Planet Optimisefor(Building building)
        {
            var buildAllMines = new BuildAllTilesVisitor(_planet, building);
            buildAllMines.Execute();

            var buildPlanetaryAdministration = new BuildAllTilesVisitor(_planet, Building.PlanetaryAdministration).BestCommand;
            buildPlanetaryAdministration.Execute();
            var result = _planet.Copy();
            buildPlanetaryAdministration.Undo();
            buildAllMines.Undo();

            return result;
        }
    }

}
