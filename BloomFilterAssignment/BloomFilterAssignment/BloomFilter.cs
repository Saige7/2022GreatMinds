using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloomFilterAssignment
{
    internal class BloomFilter<T>
    {
        public int Count;
        private HashSet<Func<T, int>> hashFuncs;
        private bool[] bits;

        public BloomFilter(int count)
        {
            Count = count;
            hashFuncs = new HashSet<Func<T, int>>();
            bits = new bool[count];
        }

        public void LoadHashFunc(Func<T, int> hashFunc)
        {
            hashFuncs.Add(hashFunc);
        }
        public void Insert(T item)
        {
            List<int> indices = GetIndices(item);

            for (int i = 0; i < bits.Length; i++)
            {
                if (indices.Contains(i))
                {
                    bits[i] = true;
                    indices.Remove(i);
                }
            }
        }

        private List<int> GetIndices(T item)
        {
            List<int> indices = new List<int>();

            if (hashFuncs.Count != 0)
            {
                foreach (var func in hashFuncs)
                {
                    indices.Add(func(item) % Count);
                }
            }
            else
            {
                indices.Add(HashFuncOne(item) % Count);
                indices.Add(HashFuncTwo(item) % Count);
                indices.Add(HashFuncThree(item) % Count);
            }

            return indices;
        }

        public bool ProbablyContains(T item)
        {
            List<int> indices = GetIndices(item);

            for (int i = 0; i < bits.Length; i++)
            {
                if (indices.Contains(i))
                {
                    if (!bits[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private int HashFuncOne(T item)
        {
            return item.GetHashCode();
        }
        private int HashFuncTwo(T item)
        {
            string dummyString = "dummystring";
            return (dummyString, item).GetHashCode();
        }
        private int HashFuncThree(T item)
        {
            int hash = 17;

            hash *= (HashFuncOne(item), HashFuncTwo(item)).GetHashCode();

            return hash;

        }
    }
}
