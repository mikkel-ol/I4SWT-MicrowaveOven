namespace Microwave.Core.Interfaces
{
    public interface ICookController
    {
        void StartCooking(int power, int time);
        void Stop();
    }
}
