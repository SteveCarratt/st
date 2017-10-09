namespace St
{
    public class Tile
    {
        private readonly Resources _resources = new Resources(energy: 0, minerals: 0);
        private Population _population = Population.NoWorker;
        public Tile(Resources resources) => _resources = resources;
        public Tile() { }

        public Resources Resources() => _population.Work(_resources);

        public void Populate(Population population) => _population = population;
    }
}