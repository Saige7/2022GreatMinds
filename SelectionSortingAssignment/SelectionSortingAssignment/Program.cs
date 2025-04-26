namespace SelectionSortingAssignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Selection Sort 
            SelectionSort<int> Sort = new SelectionSort<int>();
            Random randomInt = new Random();
            int saveToSort = 0;

            for (int i = 0; i < 7; i++)
            {
                int addToList = randomInt.Next(1, 101);
                Sort.listToSort.Add(addToList);
                Console.WriteLine(Sort.listToSort[i]);
            }

            Console.WriteLine("__________________");

            Sort.Sorting(saveToSort);

            Sort.View();
        }
    }
}