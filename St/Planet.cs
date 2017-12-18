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

        public IEnumerable<Quantity> Output => _surface.Output;

        public Planet Copy()
        {
            return new Planet(_surface.Copy());
        }

        public IEnumerable<Quantity> Maintenance => _surface.Maintenance;

        public void Visit(PlanetVisitor visitor)
        {
            _surface.Visit(visitor);
            
        }
    }
}
