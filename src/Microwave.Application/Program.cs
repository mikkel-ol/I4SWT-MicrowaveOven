using System;
using Microwave.Core.Boundary;
using Microwave.Core.Controllers;

namespace Microwave.Application
{
    class Program
    {
        static Button startCancelButton = new Button();
        static Button powerButton = new Button();
        static Button timeButton = new Button();
        static Door door = new Door();
        static Output output = new Output();
        static Display display = new Display(output);
        static PowerTube powerTube = new PowerTube(output);
        static Light light = new Light(output);
        static Timer timer = new Timer();
        static CookController cooker = new CookController(timer, display, powerTube);
        static UserInterface ui = new UserInterface(powerButton, timeButton, startCancelButton, door, display, light, cooker);

        static void Main(string[] args)
        {
            // Finish the double association
            cooker.UI = ui;

            Console.WriteLine(
                "Select use case:\n\n" +
                "0. Default, no extension\n" +
                "1. With extension 1\n" +
                "2. With extension 2\n" +
                "3. With extension 3\n" +
                "4. With extension 4\n"
            );
            Console.Write("Choice: ");

            char choice = '.';
            try {
                choice = Console.ReadLine()[0];
            } 
            catch(Exception)
            {
                Console.WriteLine("Wrong input. Exiting..");
                return;
            }

            switch(choice)
            {
                case '0':
                    UseCaseNoExtension();
                    break;

                case '1':
                    UseCaseExtension1();
                    break;

                case '2':
                    UseCaseExtension2();
                    break;

                case '3':
                    UseCaseExtension3();
                    break;

                case '4':
                    UseCaseExtension4();
                    break;

                default:
                    Console.WriteLine("Wrong input. Exiting..");
                    return;
            }

            // Wait
            while (true)
            {
                System.Threading.Thread.Sleep(1000);
            }
        }

        static void UseCaseNoExtension()
        {
            // START USE CASE
            Console.WriteLine("Door opens");
            door.Open();
            Console.WriteLine("Food is put into microwave");
            Console.WriteLine("Door closes");
            door.Close();

            // Loop through Power button presses
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("Power button pressed");
                powerButton.Press();
                System.Threading.Thread.Sleep(100);
            }

            int min = 1;
            for (int i = 0; i < min; i++)
            {
                Console.WriteLine("Time button is pressed");
                timeButton.Press();
            }

            // Start microwave
            Console.WriteLine("\nStart button is pressed");
            startCancelButton.Press();

            // When timer expires, continue use case
            timer.Expired += new EventHandler((o, e) =>
            {
                Console.WriteLine("Food is warmed\n");
                Console.WriteLine("Door opens");
                door.Open();
                Console.WriteLine("Food is taken out of microwave");
                Console.WriteLine("Door closes");
                door.Close();

                Console.WriteLine("\nUse case done");
                return;
            });
        }

        static void UseCaseExtension1()
        {
            // START USE CASE
            Console.WriteLine("Door opens");
            door.Open();
            Console.WriteLine("Food is put into microwave");
            Console.WriteLine("Door closes");
            door.Close();

            // Loop through Power button presses
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("Power button pressed");
                powerButton.Press();
                System.Threading.Thread.Sleep(100);
            }

            // EXTENSION 1
            startCancelButton.Press();

            // DO MORE
        }

        static void UseCaseExtension2()
        {

        }

        static void UseCaseExtension3()
        {

        }

        static void UseCaseExtension4()
        {

        }
    }
}
