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

        public static Population NoWorker => new Population(r => new Quantity[0]);
        public static Population Worker => new Population(r => r);

        public override bool Equals(object obj)
        {
            return obj is Population population &&
                   EqualityComparer<Func<IEnumerable<Quantity>, IEnumerable<Quantity>>>.Default.Equals(_work, population._work);
        }

        public override int GetHashCode()
        {
            return -474499317 + EqualityComparer<Func<IEnumerable<Quantity>, IEnumerable<Quantity>>>.Default.GetHashCode(_work);
        }

        public IEnumerable<Quantity> Input() => new[] {Food.Points(-1)};

        public IEnumerable<Quantity> Work(IEnumerable<Quantity> resources)
        {
            return _work.Invoke(resources);
        }
    }
}