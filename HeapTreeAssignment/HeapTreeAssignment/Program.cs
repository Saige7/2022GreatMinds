using System.Runtime.InteropServices;

namespace HeapTreeAssignment
{
    internal class Program
    {
        public class HeapTree<T> where T : IComparable<T>
        {
            public List<T> heap = new List<T>();

            public void HeapifyUp(int givenIndex)
            {
                while (givenIndex >= 0)
                {
                    int parentIndex = (givenIndex - 1) / 2;

                    if (heap[givenIndex].CompareTo(heap[parentIndex]) < 0)
                    {
                        T saveToSwap = heap[givenIndex];
                        heap[givenIndex] = heap[parentIndex];
                        heap[parentIndex] = saveToSwap;
                        givenIndex = parentIndex;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            public void Insert(T givenValue)
            {
                heap.Add(givenValue);
                HeapifyUp(heap.Count - 1);
            }
            public void HeapifyDown(int givenIndex)
            {
                while(givenIndex <= heap.Count)
                {
                    int leftIndex = givenIndex * 2 + 1;
                    int rightIndex = givenIndex * 2 + 2;
                    T saveToSwap;

                    if (rightIndex < heap.Count)
                    {
                        if (heap[leftIndex].CompareTo(heap[rightIndex]) < 0 && heap[givenIndex].CompareTo(heap[leftIndex]) > 0)
                        {
                            saveToSwap = heap[givenIndex];
                            heap[givenIndex] = heap[leftIndex];
                            heap[leftIndex] = saveToSwap;
                            givenIndex = leftIndex;
                        }
                        else if (heap[givenIndex].CompareTo(heap[rightIndex]) > 0)
                        {
                            saveToSwap = heap[givenIndex];
                            heap[givenIndex] = heap[rightIndex];
                            heap[rightIndex] = saveToSwap;
                            givenIndex = rightIndex;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            public T Pop()
            {
                T root = heap[0];
                
                heap[0] = heap[heap.Count - 1];
                HeapifyDown(0);
                heap.RemoveAt(heap.Count - 1);
                return root;
            }
            public List<T> HeapSort(List<T> listToSort)
            {
                List<T> sortedList = new List<T>();

                for(int i = 0; i < listToSort.Count; i++)
                {
                    Insert(listToSort[i]);
                }
                for (int i = 0; i < listToSort.Count; i++)
                {
                    T poppedValue = Pop();
                    sortedList.Add(poppedValue);
                }

                return sortedList;
            }
        }
        static void Main(string[] args)
        {
            HeapTree<int> heapTree = new HeapTree<int> ();
            //heapTree.Insert(13);
            //heapTree.Insert(1);
            //heapTree.Insert(2);
            //heapTree.Insert(9);
            //heapTree.Insert(10);
            //heapTree.Insert(3);
            //heapTree.Pop();

            //for(int i = 0; i < heapTree.heap.Count; i++)
            //{
            //    Console.WriteLine(heapTree.heap[i]);
            //}

            Console.WriteLine();

            List<int> randomNum = new List<int>();
            Random random = new Random();
            for(int i = 0; i < 1000; i++)
            {
                randomNum.Add(random.Next(0, 1000));
            }

            List<int> sortedList = new List<int>();
            sortedList = heapTree.HeapSort(randomNum);

            for(int i = 0; i < sortedList.Count - 1; i++)
            {
                if (sortedList[i] > sortedList[i + 1])
                {
                    throw new Exception("error");
                }
            }
        }
    }
}