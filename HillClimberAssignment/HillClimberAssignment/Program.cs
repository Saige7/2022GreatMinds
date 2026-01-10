namespace HillClimberAssignment
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.Write("Enter your target string: ");
            string targetString = Console.ReadLine();

            Random random = new Random();
            string outputString = "";
            for (int i = 0; i < targetString.Length; i++)
            {
                int randomAscii = random.Next(32, 127);
                outputString += (char)randomAscii;
            }

            HillClimber hillClimber = new HillClimber(targetString, outputString);
            hillClimber.RunHillClimber();
            //Console.WriteLine(outputString);
            //Console.WriteLine(hillClimber.Mutate());
            //Console.WriteLine(hillClimber.MeanAbsoluteError(outputString));

        }
    }
}
