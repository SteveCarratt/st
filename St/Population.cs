using System;

namespace St
{
    public class Population
    {
        private readonly Func<ResourceVector, ResourceVector> _work;
        private readonly string _name;

        private Population(Func<ResourceVector, ResourceVector> work, string name)
        {
            _work = work;
            _name = name;
        }

        public static readonly Population NoWorker = new Population(r => ResourceVector.Empty, nameof(NoWorker));
        public static readonly Population Worker = new Population(r => r, nameof(Worker));

        public ResourceVector Maintenance => ResourceVectorBuilder.RVB.Food(1).Vector;

        public ResourceVector Work(ResourceVector resources)
        {
            return _work.Invoke(resources);
        }

        public override string ToString() => _name;
    }
}