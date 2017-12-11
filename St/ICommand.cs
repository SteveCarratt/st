using System.Collections.Generic;

namespace St
{
    public interface ICommand
    {
        void Execute();
        void Undo();
        IEnumerable<Quantity> Cost { get; }
        IEnumerable<Quantity> Increase { get; }
        decimal Score { get; }
    }
}