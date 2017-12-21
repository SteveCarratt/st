using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public ResourceVector Output => _surface.Output * _surface.PlanetaryModifier;

        public Planet Copy()
        {
            return new Planet(_surface.Copy());
        }

        public ResourceVector Maintenance => _surface.Maintenance;
        public string PrettyOutput() => _surface.PrettyOutput();

        public void Visit(PlanetVisitor visitor)
        {
            _surface.Visit(visitor);
        }

        public bool Has(Building building) => _surface.Has(building);

        public string TileRepresentation(Tile targetTile) => _surface.TileRepresentation(targetTile);

        public string PrettyPrint()
        {
            return $"{Output-Maintenance}\n{_surface.PrettyPrint()}";
        }

        public int Count(Building building) => _surface.Count(building);
    }
}
