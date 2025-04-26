namespace MergeSortAssignment
{
    internal class Program
    {
        static List<T> Merge<T>(List<T> leftList, List<T> rightList) where T : IComparable<T>
        {
            int leftI = 0;
            int rightI = 0;
            List<T> sortedList = new List<T>();
            while (leftI < leftList.Count && rightI < rightList.Count)
            {
                if (leftList[leftI].CompareTo(rightList[rightI]) < 0)
                {
                    sortedList.Add(leftList[leftI]);
                    leftI++;
                }
                else
                {
                    sortedList.Add(rightList[rightI]);
                    rightI++;
                }
            }
            while(leftI < leftList.Count)
            {
                sortedList.Add(leftList[leftI]);
                leftI++;
            }
            while(rightI < rightList.Count)
            {
                sortedList.Add(rightList[rightI]);
                rightI++;
            }

            return sortedList;
        }
        static List<T> MergeSort<T>(List<T> inputList) where T : IComparable<T>
        {
            List<T> sortedList = new List<T>();
            if (inputList.Count < 2)
            {

                return inputList;
            }

            List<T> leftList = new List<T>();
            List<T> rightList = new List<T>();
            int listMidPoint = inputList.Count / 2;

            for (int i = 0; i < listMidPoint; i++)
            {
                leftList.Add(inputList[i]);
            }
            for (int i = listMidPoint; i < inputList.Count; i++)
            {
                rightList.Add(inputList[i]);
            }

            List<T> left = MergeSort(leftList);
            List<T> right = MergeSort(rightList);
            return Merge(left, right);

            /*for(int i = 0; i < sortedList.Count; i++)
            {
                Console.WriteLine(sortedList[i]);
            }*/
        }

        static void doSomething(List<int> a)
        {
            a.Add(5);
            Console.WriteLine($"In Call: {a.Count}");
        }
        static void Main(string[] args)
        {
            List<int> inputList = new List<int>();

            inputList.Add(38);
            inputList.Add(27);
            inputList.Add(43);
            inputList.Add(3);
            inputList.Add(9);
            inputList.Add(82);
            inputList.Add(10);

            List<int> a = new List<int>();
            doSomething(a);
            Console.WriteLine(a.Count);
            List<int> sorted = MergeSort(inputList);
            for(int i = 0; i < sorted.Count; i++)
            {
                Console.WriteLine(sorted[i]);
            }
        }
    }
}