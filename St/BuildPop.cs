using System.Collections.Generic;
using System.Linq;

namespace St
{
    public class BuildPop : PlanetVisitor
    {
        private readonly Planet _originalPlanet;
        private readonly Dictionary<Tile, IEnumerable<Quantity>> _tiles = new Dictionary<Tile, IEnumerable<Quantity>>();
        private List<BuildPopCommand> _commands = new List<BuildPopCommand>();


        public BuildPop(Planet originalPlanet)
        {
            _originalPlanet = originalPlanet;
        }

        public override void Accept(Tile tile)
        {
            _commands.Add(new BuildPopCommand(tile, Population.Worker, _originalPlanet));
        }

        public ICommand BestAction() => _commands.OrderByDescending(c => c.Score).First();
    }
}
