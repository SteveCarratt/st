using System.Collections.Generic;

namespace St
{
    public interface ICommand
    {
        void Execute();
        void Undo();
        ResourceVector Increase { get; }
        decimal Score { get; }
    }
}