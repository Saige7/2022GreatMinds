using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace HillClimberAssignment
{
    internal class HillClimber
    {
        private string TargetString;
        private string OutputString;

        public HillClimber(string targetString, string outputString)
        {
            TargetString = targetString;
            OutputString = outputString;
        }

        private string Mutate()
        {
            Random randomIndex = new Random();
            Random randomAlteration = new Random();
            int index = randomIndex.Next(0, OutputString.Length);
            int alter = randomAlteration.Next(2);
            string mutatedString = "";

            if (alter == 0)
            {
                alter = -1;
            }

            for (int i = 0; i < OutputString.Length; i++)
            {
                if (i == index)
                {
                    if ((int)(OutputString[i]) + alter > 126 || (int)(OutputString[i]) + alter < 32)
                    {
                        alter *= -1;
                    }
                    mutatedString += (char)((int)(OutputString[i]) + alter);
                }
                else
                {
                    mutatedString += (char)OutputString[i];
                }
            }

            return mutatedString;
        }
        private float MeanAbsoluteError(string mutatedString)
        {
            int sum = 0;
            for (int i = 0; i < TargetString.Length; i++)
            {
                sum += Math.Abs(TargetString[i] - mutatedString[i]);               
            }
            return (float)sum / (TargetString.Length + mutatedString.Length);
        }
        public void RunHillClimber()
        {
            float initialError = MeanAbsoluteError(OutputString);
            float error = initialError;

            while (error != 0)
            {
                Console.Write($"{OutputString} => {TargetString} ({error})\n");
                string mutatedString = Mutate();
                error = MeanAbsoluteError(mutatedString);
                if (error < initialError)
                {
                    OutputString = mutatedString;
                    initialError = error;
                }              
            }
            Console.Write($"{OutputString} => {TargetString} ({error})\n");
        }
    }
}
