using System;

namespace Microwave.Core.Interfaces
{
    public interface ITimer
    {
        int TimeRemaining { get; }
        event EventHandler Expired;
        event EventHandler TimerTick;

        void Start(int time);
        void Stop();
    }
}
