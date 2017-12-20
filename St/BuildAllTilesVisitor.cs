using System.Collections.Generic;
using System.Linq;

namespace St
{
    public class BuildAllTilesVisitor : PlanetVisitor, ICommand
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
            _commands.Add(new BuildBuildingCommand(tile, _planet, _building));
        }

        public void Execute()
        {
            _commands.ForEach(c=>c.Execute());
        }

        public void Undo()
        {
            _commands.ForEach(c=>c.Undo());
        }

        public ResourceVector Increase
        {
            get
            {
                var initial = _planet.Output;
                Execute();
                var diff = _planet.Output - initial;
                Undo();
                return diff;
            }
        }

        public decimal Score => Increase.Score;
        public string PrettyPrint() => string.Join("\n", _commands.Select(c => c.PrettyPrint()));

        public ICommand BestCommand => _commands.OrderByDescending(c => c.Score).First();
    }
}