using System;
using System.Collections.Generic;
using static St.Unit;

namespace St
{
    public class Population
    {
        private readonly Func<IEnumerable<Quantity>, IEnumerable<Quantity>> _work;

        private Population(Func<IEnumerable<Quantity>, IEnumerable<Quantity>> work)
        {
            _work = work;
        }

        public static readonly Population NoWorker = new Population(r => new Quantity[0]);
        public static readonly Population Worker = new Population(r => r);

        public IEnumerable<Quantity> Input() => new[] {Food.Points(-1)};

        public IEnumerable<Quantity> Work(IEnumerable<Quantity> resources)
        {
            return _work.Invoke(resources);
        }
    }
}