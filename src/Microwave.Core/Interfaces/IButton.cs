using System;

namespace Microwave.Core.Interfaces
{
    public interface IButton
    {
        event EventHandler Pressed;

        void Press();
    }
}
