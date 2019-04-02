using System;
using Microwave.Core.Interfaces;

namespace Microwave.Core.Boundary
{
    public class Door : IDoor
    {
        public event EventHandler Opened;
        public event EventHandler Closed;

        public void Close()
        {
            Closed?.Invoke(this, System.EventArgs.Empty);
        }

        public void Open()
        {
            Opened?.Invoke(this, System.EventArgs.Empty);
        }
    }
}
