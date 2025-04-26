namespace InsertionSortingAssignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Insertion Sort

            Random randomInt = new Random();
            List<int> listToSort = new List<int>();

            for(int i = 0; i < 9; i++)
            {
                int addToList = randomInt.Next(1, 101);
                listToSort.Add(addToList);
                Console.WriteLine(listToSort[i]);
            }

            for(int i = 0; i < listToSort.Count; i++)
            {

            }
        }
    }
}