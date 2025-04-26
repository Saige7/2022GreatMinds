using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Straigh4
{
    internal class Vertex<T> where T : IEquatable<T>
    {
        public T Value;
        public bool Visted;

        public List<Edge<T>> Neighbors { get; set; }

        public int NeighborCount => Neighbors.Count;

        public Vertex(T value)
        {
            Value = value;
            Neighbors = new List<Edge<T>>();
            Visted = false;
        }
    }
}
