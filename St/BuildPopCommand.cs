using System.Collections.Generic;

namespace St
{
    public class BuildPopCommand : ICommand
    {
        private readonly Tile _targetTile;
        private readonly Population _pop;
        private readonly Planet _planet;
        private object _initialState;

        public BuildPopCommand(Tile targetTile, Population pop, Planet planet)
        {
            _targetTile = targetTile;
            _pop = pop;
            _planet = planet;
        }

        public void Execute()
        {
            _initialState = _targetTile.Memento();
            _targetTile.Populate(_pop);
        }

        public void Undo()
        {
            _targetTile.Restore(_initialState);   
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

        public override string ToString() => $"Bulding {_pop} on {_targetTile} of {_planet} yields {Score} - {Increase.PrettyPrint()}";
    }
}
