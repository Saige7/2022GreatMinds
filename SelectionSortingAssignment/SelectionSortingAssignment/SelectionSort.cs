using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelectionSortingAssignment
{
    internal class SelectionSort<T> where T : IComparable 
    {
        public List<T> listToSort = new List<T>();

        public void Sorting(T saveToSwap)
        {            
            for(int current = 0; current < listToSort.Count; current++)
            {
                int smallestValueIndex = current;
                saveToSwap = listToSort[current];

                for(int i = current; i < listToSort.Count; i++)
                {
                    if (listToSort[smallestValueIndex].CompareTo(listToSort[i]) > 0)
                    {
                        smallestValueIndex = i;
                    }    
                }

                listToSort[current] = listToSort[smallestValueIndex];
                listToSort[smallestValueIndex] = saveToSwap;
            }
        }

        public void View()
        {
            for(int i = 0; i < listToSort.Count; i++)
            {
                Console.WriteLine(listToSort[i]);
            }
        }
    }
}
