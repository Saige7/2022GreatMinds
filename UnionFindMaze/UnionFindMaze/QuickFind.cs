using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnionFindMaze
{
    internal class QuickFind<T> where T : IEquatable<T>
    {
        private int[] sets;
        private Dictionary<Vertex<T>, int> map;

        public QuickFind(List<Vertex<T>> items)
        {
            sets = new int[items.Count];
            map = new Dictionary<Vertex<T>, int>();

            for (int i = 0; i < items.Count; i++)
            {
                sets[i] = i;
                map.Add(items[i], i);
            }
        }

        public int Find(Vertex<T> p)
        {
            return sets[map[p]];
        }
        public bool Union(Vertex<T> p, Vertex<T> q)
        {
            if (AreConnected(p, q))
            {
                return false;
            }

            for(int i = 0; i < sets.Length; i++)
            {
                if (sets[i] == Find(p))
                {
                    sets[i] = Find(q);
                }
            }
            return true;
        }
        public bool AreConnected(Vertex<T> p, Vertex<T> q)
        {
            if (Find(p) == Find(q))
            {
                return true;
            }
            return false;
        }

        /*
         * QuickFind:
                a, b, c, d, e, f, g
                0, 1, 2, 3, 4, 5, 6

                Union a and e
                a, b, c, d, e, f, g
                4, 1, 2, 3, 4, 5, 6

                Union b and f
                a, b, c, d, e, f, g
                4, 5, 2, 3, 4, 5, 6

                Union f and a
                a, b, c, d, e, f, g
                4, 4, 2, 3, 4, 4, 6
         */
    }
}
