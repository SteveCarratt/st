using System.Collections.Generic;
using System.Linq;

namespace St
{
    public class Planet
    {
        private readonly Surface _surface;

        public Planet()
        {
            _surface = new Surface();
        }

        public Planet(Surface surface)
        {
            _surface = surface;
        }

        public ResourceVector Output => _surface.Output;

        public Planet Copy()
        {
            return new Planet(_surface.Copy());
        }

        public ResourceVector Maintenance => _surface.Maintenance;
        public IEnumerable<ICommand> Options => _surface.Options(this);
        public bool HasUnemployed => _surface.HasUnemployed;

        public void Visit(PlanetVisitor visitor)
        {
            _surface.Visit(visitor);
            
        }

        public bool Has(Building building) => _surface.Has(building);
    }
}
