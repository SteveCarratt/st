using System;
using System.Collections.Generic;
using static St.Unit;

namespace St
{
    public class Population
    {
        private readonly Func<IEnumerable<Quantity>, IEnumerable<Quantity>> _work;
        private readonly string _name;

        private Population(Func<IEnumerable<Quantity>, IEnumerable<Quantity>> work, string name)
        {
            _work = work;
            _name = name;
        }

        public static readonly Population NoWorker = new Population(r => new Quantity[0], nameof(NoWorker));
        public static readonly Population Worker = new Population(r => r, nameof(Worker));
        public IEnumerable<Quantity> Cost { get; set; }

        public IEnumerable<Quantity> Input() => new[] {Food.Points(-1)};

        public IEnumerable<Quantity> Work(IEnumerable<Quantity> resources)
        {
            return _work.Invoke(resources);
        }

        public override string ToString() => _name;
    }
}