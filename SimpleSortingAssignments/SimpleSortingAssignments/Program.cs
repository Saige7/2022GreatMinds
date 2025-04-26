using System;

namespace SimpleSortingAssignments
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Bubble Sort

            List<int> listToSort = new List<int>();
            Random randomInt = new Random();
            int toSwap = 0;
            bool didSwap = false;

            for (int i = 0; i < 6; i++)
            {
                int addToList = randomInt.Next(1, 7);
                listToSort.Add(addToList);
                Console.WriteLine(listToSort[i]);
            }

            Console.WriteLine("-------------");

            for (int j = 0; j < listToSort.Count; j++)
            {
                int current = 0;
                for (int i = 1; i < listToSort.Count; i++)
                {
                    if (listToSort[current] > listToSort[i])
                    {
                        toSwap = listToSort[current];
                        listToSort[current] = listToSort[i];
                        listToSort[i] = toSwap;
                        didSwap = true;
                    }
                    current++;
                }
                if (didSwap == false)
                {
                    break;
                }
            }

            for (int i = 0; i < listToSort.Count; i++)
            {
                Console.WriteLine(listToSort[i]);
            }
        }
    }
}