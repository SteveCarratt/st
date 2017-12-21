using System;
using System.Collections.Generic;
using System.Linq;

namespace St
{
    public class BuildAllTilesVisitor : PlanetVisitor
    {
        private readonly Planet _planet;
        private readonly Building _building;
        private readonly List<ICommand> _commands = new List<ICommand>();

        public BuildAllTilesVisitor(Planet planet, Building building)
        {
            _planet = planet;
            _building = building;
            _planet.Visit(this);
        }

        public override void Accept(Tile tile)
        {
            _commands.Add(new BuildBuildingCommand(tile, _planet, _building, Population.Robot));
        }

        public IEnumerable<ICommand> Commands => _commands;


        public void Execute()
        {
            _commands.ForEach(c => c.Execute());
        }
    }
}