using System;
using static St.ResourceVector.ResourceMask;
using static St.ResourceVectorBuilder;

namespace St
{
    public class Population
    {
        private readonly ResourceVector.ResourceMask _outputMask;
        private readonly string _name;

        public static readonly Population NoWorker = new Population(NullMask, ResourceVector.Empty, nameof(NoWorker));
        public static readonly Population Worker = new Population(IdentityMask, RVB.Food(1).Vector, nameof(Worker));
        public static readonly Population Robot = new Population(RVB.Mineral(1.2).Mask, RVB.Energy(1).Vector, nameof(Robot));

        private Population(ResourceVector.ResourceMask outputMask, ResourceVector upkeep, string name)
        {
            _outputMask = outputMask;
            Upkeep = upkeep;
            _name = name;
        }

        public ResourceVector Upkeep { get; }

        public ResourceVector Work(ResourceVector resources)
        {
            return resources * _outputMask;
        }

        public override string ToString() => _name;
        public string ShortName => _name.Substring(0,3);
    }
}