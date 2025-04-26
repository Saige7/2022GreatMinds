namespace QuickSortAssignment
{
    internal class Program
    {
        /*
        static int SumUpTo(int num)
        {
            if (num <= 0)
            {
                return 0;
            }

            return num + SumUpTo(num - 1);
        }

        static int Fibonacci(int num)
        {
            if (num <= 2)
            {
                return 1;
            }

            return Fibonacci(num - 1) + Fibonacci(num - 2);
        }

        static int SmallestInList(List<int> inputedList, int index)
        {
            if (index >= inputedList.Count)
            {
                return int.MaxValue;
            }

            int smallest = SmallestInList(inputedList, index + 1);
            if (smallest < inputedList[index])
            {
                return smallest;
            }
            else
            {
                return inputedList[index];
            }
        }
        static int UsersSmallestInList(List<int> inputedList)
        {
            return SmallestInList(inputedList, 0);
        }

        static int NumberOfEvens(List<int> inputedList, int index)
        {
            if (index >= inputedList.Count)
            {
                return 0;
            }

            int result = NumberOfEvens(inputedList, index + 1);
            if (inputedList[index] % 2 == 0)
            {
                result++;
            }
            return result;
        }
        static int UsersNumOfEvens(List<int> inputedList)
        {
            return NumberOfEvens(inputedList, 0);
        }

        static bool SortedOrNot(List<int> inputedList, int index)
        {
            if (index >= inputedList.Count - 1)
            {
                return true;
            }

            bool check = SortedOrNot(inputedList, index + 1);
            if (inputedList[index + 1] < inputedList[index])
            {
                return false;
            }
            if (check == false)
            {
                return false;
            }
            return check;
        }
        static bool UsersSortedOrNot(List<int> inputedList)
        {
            return SortedOrNot(inputedList, 0);
        }

        ^^^Recursion Practice
         */

        static void LomutoPartition<T>(T[] inputArray, int Start, int End) where T : IComparable<T>
        {
            if (End - 1 <= Start)
            {
                return;
            }

            int pivot = End - 1;
            int wall = Start - 1;
            T saveForSwap;

            for (int i = Start; i < End; i++)
            {
                if (inputArray[i].CompareTo(inputArray[pivot]) < 0)
                {
                    saveForSwap = inputArray[wall + 1];
                    inputArray[wall + 1] = inputArray[i];
                    inputArray[i] = saveForSwap;
                    wall++;
                }
            }

            saveForSwap = inputArray[wall + 1];
            inputArray[wall + 1] = inputArray[pivot];
            inputArray[pivot] = saveForSwap;
            pivot = wall + 1;

            LomutoPartition(inputArray, Start, pivot);
            LomutoPartition(inputArray, pivot + 1, End);

            return;
        }
        static void HoarePartition<T>(T[] inputArray, int Start, int End) where T : IComparable<T>
        {
            if(End - 1 < Start)
            {
                return;
            }
            
            T pivot = inputArray[Start];
            int right = End - 1;
            int left = Start;

            while (right >= left)
            {
                if (inputArray[left].CompareTo(pivot) < 0)
                {
                    left++;
                }
                else if (inputArray[right].CompareTo(pivot) > 0)
                {
                    right--;
                }
                else
                {
                    T saveForSwap = inputArray[left];
                    inputArray[left] = inputArray[right];
                    inputArray[right] = saveForSwap;

                    right--;
                    left++;
                }
            }

            if(right == left)
            {
                HoarePartition(inputArray, Start, right);
                HoarePartition(inputArray, left, End);
            }
            else
            {
                HoarePartition(inputArray, Start, right + 1);
                HoarePartition(inputArray, left, End);
            }
        }
        static void Main(string[] args)
        {
            int[] inputArray = { 5, 4, 3, 6, 2, 1, 7 };
            Console.WriteLine("Lomuto: ");
            LomutoPartition<int>(inputArray, 0, inputArray.Length);
            for (int i = 0; i < inputArray.Length; i++)
            {
                Console.WriteLine(inputArray[i]);
            }

            Console.WriteLine();
            Console.WriteLine("Hoare: ");

            HoarePartition<int>(inputArray, 0, inputArray.Length);
            for (int i = 0; i < inputArray.Length; i++)
            {
                Console.WriteLine(inputArray[i]);
            }
        }
    }
}