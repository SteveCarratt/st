using System.Collections.Generic;

namespace St
{
    public interface ICommand
    {
        void Execute();
        IEnumerable<Quantity> Cost { get; }
        IEnumerable<Quantity> Increase { get; }
        decimal Score { get; }
    }
}