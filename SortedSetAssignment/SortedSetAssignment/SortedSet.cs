using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortedSetAssignment
{
    internal class SortedSet<T> : ISortedSet<T>
    {
        private RedBlack<T> tree;
        public IComparer<T> Comparer { get; init; }

        public int Count { get; private set; }

        public SortedSet(IComparer<T> comparer)
        {
            tree = new RedBlack<T>(comparer);
            Count = tree.Count;
            Comparer = comparer;
        }

        public bool Add(T item)
        {
            if (tree.Search(item) != null)
            {
                return false;
            }

            tree.Add(item);
            Count = tree.Count;

            return true;
        }
        public void AddRange(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                Add(item);
            }
        }
        public T Ceiling(T item)
        {
            if (Contains(item))
            {
                return item;
            }

            IEnumerator<T> enumerator = GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (Comparer.Compare(enumerator.Current, item) > 0)
                {
                    return enumerator.Current;
                }
            }

            return default(T);
        }
        public void Clear()
        {
            tree.Clear();
            Count = tree.Count;
        }
        public bool Contains(T item)
        {
            Queue<T> queue = tree.TraversalStart();
            return queue.Contains(item);
        }
        public T Floor(T item)
        {
            if (Contains(item))
            {
                return item;
            }

            IEnumerator<T> enumerator = GetEnumerator();
            T save = default(T);
            while (enumerator.MoveNext())
            {
                if (Comparer.Compare(enumerator.Current, item) < 0)
                {
                    save = enumerator.Current;
                }
            }

            return save;
        }
        public ISortedSet<T> Intersection(ISortedSet<T> other)
        {
            ISortedSet<T> sortedSet = new SortedSet<T>(Comparer);

            foreach (T item in other)
            {
                if (Contains(item))
                {
                    sortedSet.Add(item);
                }
            }

            return sortedSet;
        }
        public T Max()
        {
            Queue<T> queue = tree.TraversalStart();
            return queue.Last();
        }
        public T Min()
        {
            Queue<T> queue = tree.TraversalStart();
            return queue.First();
        }
        public bool Remove(T item)
        {
            if (tree.Search(item) == null)
            {
                return false;
            }

            tree.Remove(item);
            Count = tree.Count;
            return true;
        }
        public ISortedSet<T> Union(ISortedSet<T> other)
        {
            ISortedSet<T> sortedSet = new SortedSet<T>(Comparer);

            sortedSet.AddRange(this);
            sortedSet.AddRange(other);

            return sortedSet;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public IEnumerator<T> GetEnumerator()
        {
            return tree.TraversalStart().GetEnumerator();
        }
    }

    public class NormalComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            return x - y;
        }
    }
    public class ReverseComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            return y - x;
        }
    }
}