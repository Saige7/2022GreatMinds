using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnionFindAssignment
{
    internal class QuickUnion<T>
    {
        private int[] parents;
        private Dictionary<T, int> map;

        public QuickUnion(List<T> items)
        {
            parents = new int[items.Count];
            map = new Dictionary<T, int>();

            for(int i = 0; i < items.Count; i++)
            {
                parents[i] = i;
                map.Add(items[i], i);
            }
        }

        public int Find(T p)
        {
            int currentIndex = map[p];
            while (parents[currentIndex] != currentIndex)
            {
                currentIndex = parents[currentIndex];
            }

            return parents[currentIndex];
        }
        public bool Union(T p, T q)
        {
            if (AreConnected(p, q))
            {
                return false;
            }

            parents[map[p]] = map[q];

            return true;
        }
        public bool AreConnected(T p, T q)
        {
            if(Find(p) == Find(q))
            {
                return true;
            }

            return false;
        }
    }
}
