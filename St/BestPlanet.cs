using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace St
{
    public class BestPlanet
    {
        private readonly Planet _planet;
        private readonly Building[] _optimiseForBuildings;

        public BestPlanet(Planet planet, params Building[] optimiseForBuildings)
        {
            _planet = planet;
            _optimiseForBuildings = optimiseForBuildings;
        }

        public string Summary()
        {
            var result = new StringBuilder();
            result.AppendLine(BuildPlanet(_planet.Copy(), _optimiseForBuildings));

            foreach (var building in _optimiseForBuildings)
            {
                var planet = BuildAll(_planet, building);

                result.AppendLine(BuildPlanet(planet, _optimiseForBuildings.Except(new[] { building })));
            }

            return result.ToString();
        }

        private static string BuildPlanet(Planet planet, IEnumerable<Building> buildings)
        {
            var result = new StringBuilder();

            var commands = Enumerable.Empty<ICommand>();
            foreach (var building in buildings)
            {
                commands = commands.Union(Commands(planet, building));
            }

            commands = commands.Union(Commands(planet, Building.MineralProcessingPlantI));
            commands = commands.Union(Commands(planet, Building.EnergyGrid));
            commands = commands.Union(Commands(planet, Building.PlanetaryAdministration));


            while (commands.Any(c => c.Score > 0))
            {
                var bestcommand = commands.Where(c => c.Score > 0).OrderByDescending(c => c.Score).First();
                result.AppendLine(bestcommand.PrettyPrint());
                result.AppendLine();
                bestcommand.Execute();
            }

            result.AppendLine($"{planet.Output - planet.Maintenance}\n{planet.PrettyPrint()}");

            return result.ToString();
        }

        private static IEnumerable<ICommand> Commands(Planet planet, Building alternative)
        {
            var buildAll = new BuildAllTilesVisitor(planet, alternative);

            return buildAll.Commands;
        }

        private static Planet BuildAll(Planet planet, Building building)
        {
            var result = planet.Copy();

            var buildAllMines = new BuildAllTilesVisitor(result, building);
            buildAllMines.Execute();

            return result;
        }
    }

}
