using System.Collections.Generic;
using System.Linq;

namespace St
{
    public class PopulateAllTiles : PlanetVisitor, ICommand
    {
        private readonly Planet _planet;
        private readonly Population _worker;
        private readonly List<ICommand> _commands = new List<ICommand>();

        public PopulateAllTiles(Planet planet, Population worker)
        {
            _planet = planet;
            _worker = worker;
            _planet.Visit(this);
        }

        public override void Accept(Tile tile)
        {
            _commands.Add(new BuildPopCommand(tile, _worker, _planet));
        }

        public void Execute()
        {
            _commands.ForEach(c => c.Execute());
        }

        public void Undo()
        {
            _commands.ForEach(c => c.Undo());
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