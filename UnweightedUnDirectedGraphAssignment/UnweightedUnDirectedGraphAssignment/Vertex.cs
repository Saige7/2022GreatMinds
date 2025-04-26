namespace UnweightedUnDirectedGraphAssignment
{
    class Vertex<T>
    {
        public T Value;
        public List<Vertex<T>> Neighbors { get; set; }
        public int NeighborCount => Neighbors.Count;
        public bool visted;

        public Vertex(T value)
        {
            Value = value;
            Neighbors = new List<Vertex<T>>();
            visted = false;
        }
    }
}