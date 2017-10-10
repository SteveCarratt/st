using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using static St.Unit;
namespace St
{
    public class Game
    {
        private readonly IEnumerable<Planet> _planets;
        private Quantity _gameTime = Turn.s(0);
        private readonly Dictionary<Quantity, List<Action>> _callbacks = new Dictionary<Quantity, List<Action>>();
        private IEnumerable<Quantity> _resources = new Quantity[]{};
        private readonly List<string> _actionHistory = new List<string>();

        public Game(IEnumerable<Planet> planets)
        {
            _planets = planets;
            _resources = new[] { Energy.Points(100), Mineral.Points(200), Food.Points(100), Influence.Points(100) };
        }

        public Game() : this(new List<Planet>())
        {
        }

        private Game(List<string> actionHistory, IEnumerable<Quantity> resources, IEnumerable<Planet> planets, Quantity gameTime, Dictionary<Quantity, List<Action>> callbacks) : this(planets)
        {
            _actionHistory = actionHistory;
            _resources = resources;
            _gameTime = gameTime;
            _callbacks = callbacks;
        }

        public void Ticks(Quantity amount)
        {
            for (var t = Turn.s(0); t <= amount; t++)
            {
                Tick();
            }
        }

        public void RegisterTimer(Quantity turns, Action callback)
        {
            var callTime = turns+_gameTime;
            if (_callbacks.ContainsKey(callTime)) _callbacks[callTime].Add(callback);
            else _callbacks[callTime] = new List<Action>(){callback};
        }


        public void Tick()
        {
            AddResources(ResourceChange);
            _gameTime++;
            if (!_callbacks.ContainsKey(_gameTime)) return;
            foreach (var action in _callbacks[_gameTime])
            {
                action.Invoke();
            }
            _callbacks.Remove(_gameTime);
        }

        private IEnumerable<Quantity> ResourceChange
        {
            get { return Quantity.Add(_planets.SelectMany(x => x.Output()), _planets.SelectMany(x=>x.Maintenance())); }
        }

        public IEnumerable<Game> Games(Quantity turns)
        {
            if (turns < Turn.s(0)) throw new ArgumentException("Number of turns cannot be negative", nameof(turns));
            if (turns == Turn.s(0)) return new[] {this};
            var games = _planets.SelectMany(x => x.AvailableActions(_resources)).Select(a =>
            {
                var g = this.Copy();
                g.TakeAction(a);
                return g;
            }).ToArray();
            foreach (var game in games)
            {
                game.Tick();
            }
            return games.SelectMany(g => g.Games(turns - Turn.s(1))).ToArray();
        }

        private void TakeAction(PlayerAction playerAction)
        {
            _actionHistory.Add(playerAction.Name);
            AddResources(playerAction.Invoke(this));
        }

        private Game Copy()
        {
            return new Game(new List<string>(_actionHistory), new List<Quantity>(_resources), _planets.Select(p=>p.Copy()).ToArray(), _gameTime, new Dictionary<Quantity, List<Action>>(_callbacks));
        }

        private void AddResources(IEnumerable<Quantity> resources)
        {
            _resources = Quantity.Add(_resources, resources);
        }

        public override string ToString()
        {
            return "History: " + FormatHistory() + Environment.NewLine + "Current: " + FomatRes(_resources) + Environment.NewLine + "Change: " + FomatRes(ResourceChange);
        }

        private string FormatHistory()
        {
            return _actionHistory.Aggregate((l, r) => l+','+r);
        }

        private string FomatRes(IEnumerable<Quantity> resources)
        {
            return resources.Aggregate(string.Empty, (l, r) => l.ToString() + ", " + r.ToString()).Trim(',');
        }
    }
}