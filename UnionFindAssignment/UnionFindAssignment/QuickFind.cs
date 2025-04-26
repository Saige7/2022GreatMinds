using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnionFindAssignment
{
    internal class QuickFind<T>
    {
        private int[] sets;
        private Dictionary<T, int> map;

        public QuickFind(List<T> items)
        {
            sets = new int[items.Count];
            map = new Dictionary<T, int>();

            for (int i = 0; i < sets.Length; i++)
            {
                sets[i] = i;
                map.Add(items[i], i);
            }
        }

        public int Find(T p)
        {
            return sets[map[p]];
        }
        public bool Union(T p, T q)
        {
            if (AreConnected(p, q))
            {
                return false;
            }

            for (int i = 0; i < sets.Length; i++)
            {
                if (sets[i] == Find(p))
                {
                    sets[i] = Find(q);
                }
            }
            return true;
        }
        public bool AreConnected(T p, T q)
        {
            if (Find(p) == Find(q))
            {
                return true;
            }
            return false;
        }
    }
}
