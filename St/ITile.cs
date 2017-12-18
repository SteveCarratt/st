using System.Collections.Generic;

namespace St
{
    public interface ITile
    {
        IEnumerable<Quantity> Output(IEnumerable<Quantity> adjacencyBonus);
        IEnumerable<Quantity> Output();
        IEnumerable<Quantity> Maintenance { get; }
        IEnumerable<Quantity> AdjacencyBonus { get; }
        bool HasUnemployed { get; }
        void Populate(Population population);
        void Construct(Building building);
        Tile Copy();
        object Memento();
        void Restore(object state);
        Tile Blocked();
    }
}