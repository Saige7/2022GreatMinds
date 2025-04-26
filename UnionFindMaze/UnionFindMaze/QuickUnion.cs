
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnionFindMaze
{
    internal class QuickUnion<T> where T : IEquatable<T>
    {
        private int[] parents;
        private Dictionary<Vertex<T>, int> map;

        public QuickUnion(List<Vertex<T>> items)
        {
            parents = new int[items.Count];
            map = new Dictionary<Vertex<T>, int>();

            for (int i = 0; i < items.Count; i++)
            {
                parents[i] = i;
                map.Add(items[i], i);
            }
        }

        public int Find(Vertex<T> p)
        {
            int current = map[p];

            while (parents[current] != current)
            {
                current = parents[current];
            }

            return current;
        }
        public bool Union(Vertex<T> p, Vertex<T> q)
        {
            if (AreConnected(p, q))
            {
                return false;
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
         * QuickUnion:
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
                4, 5, 2, 3, 4, 0, 6
         */
    }
}
