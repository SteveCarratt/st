using System;
using System.Collections.Generic;

namespace St
{
    public class PlayerAction
    {
        public string Name { get; }
        private readonly Func<Game, IEnumerable<Quantity>> _action;

        public PlayerAction(string name, Func<Game, IEnumerable<Quantity>> action)
        {
            Name = name;
            _action = action;
        }

        public IEnumerable<Quantity> Invoke(Game game)
        {
            return _action.Invoke(game);
        }
    }
}