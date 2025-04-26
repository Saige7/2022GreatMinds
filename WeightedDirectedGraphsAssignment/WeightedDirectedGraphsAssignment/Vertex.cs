namespace WeightedDirectedGraphsAssignment
{
    public class Vertex<T>
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