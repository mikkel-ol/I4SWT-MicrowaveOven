namespace Microwave.Core.Interfaces
{
    public interface IDisplay
    {
        void ShowTime(int minutes, int seconds);
        void ShowPower(int power);
        void Clear();
    }
}
