using System;
using Microwave.Core.Interfaces;

namespace Microwave.Core.Boundary
{
    public class Button : IButton
    {
        public event EventHandler Pressed;

        public void Press()
        {
            Pressed?.Invoke(this, System.EventArgs.Empty);
        }
    }
}
