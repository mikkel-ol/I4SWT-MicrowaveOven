using System;

namespace Microwave.Core.Interfaces
{
    public interface IDoor
    {
        event EventHandler Opened;
        event EventHandler Closed;

        void Open();
        void Close();
    }
}
