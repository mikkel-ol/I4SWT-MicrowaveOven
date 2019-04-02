using Microwave.Core.Interfaces;

namespace Microwave.Core.Boundary
{
    public class Output : IOutput
    {
        public void OutputLine(string line)
        {
            System.Console.WriteLine(line);
        }
        
    }
}