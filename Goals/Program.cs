// See https://aka.ms/new-console-template for more information
using System.IO;

namespace GoalsProgram
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // dddd Represents the full name of the day (Monday, Tuesday, etc), dd Represents the day of the month as a number from 01 through 31,
            // MMMM Full month name (e.g. December), yyyy year
            // must be declared as a string since the final product is a string
            string localTime = DateTime.Now.ToString("dddd, dd MMMM, yyyy");
            bool saveConfirmation = false;
            int maxLength = 256;
            string goal;
            //Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) is the C# way to get the %AppData% folder
            string? path = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}/goals.txt";

            Console.WriteLine("Goals Application!");
            Console.WriteLine();
            ConsoleKey previousGoalsResponse;

            do
            {
                //will continue to show the prompt until either Y or N is entered
                Console.WriteLine("Would you like to view all previous goals? (Y/n)");
                Console.WriteLine("------------------------------------------------");
                previousGoalsResponse = Console.ReadKey(false).Key;
                if (previousGoalsResponse != ConsoleKey.Enter)
                    Console.WriteLine();
            } while (previousGoalsResponse != ConsoleKey.Y && previousGoalsResponse != ConsoleKey.N);

            //provided the response is "Y", will add all the lines to the "lines" array, loop through them, tab and write line
            if(previousGoalsResponse == ConsoleKey.Y)
            {
                string[] lines = File.ReadAllLines(path);

                Console.WriteLine();
                Console.WriteLine("Previous goals: ");
                foreach (string line in lines)
                {
                    Console.WriteLine("\t" + line);
                }
            }

            do
            {
                Console.WriteLine();
                Console.WriteLine("Please enter your goal for today:");
                Console.WriteLine("---------------------------------");
                goal = Console.ReadLine();
                if(goal.Length >= maxLength)
                    goal = goal.Substring(0, maxLength);
                Console.WriteLine();
                Console.WriteLine($"[For {localTime}, your goal is:]");
                Console.WriteLine(goal);
                Console.WriteLine();

                ConsoleKey response;
                do
                {
                    Console.WriteLine("[Save Changes (Y/n)]");
                    response = Console.ReadKey(false).Key;
                    if (response != ConsoleKey.Enter)
                        Console.WriteLine();
                } while (response != ConsoleKey.Y && response != ConsoleKey.N);
                saveConfirmation = response == ConsoleKey.Y;
            } while (!saveConfirmation);

            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(goal);
            }
            Console.ReadLine();
        }
    }
}
