using System;

namespace St
{
    public class Population
    {
        private readonly Func<Resources, Resources> _work;
        private Population(Func<Resources, Resources> work) => _work = work;
        public static Population NoWorker => new Population(r => new Resources(energy: 0, minerals: 0));
        public static Population Worker => new Population(r => r);

        public Resources Work(Resources resources) => _work.Invoke(resources);
    }
}