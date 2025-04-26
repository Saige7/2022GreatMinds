namespace UnweightedUnDirectedGraphAssignment
{


    internal class Program
    {
        public void BackTrack(int c, List<int> x)
        {
            if (c == 10) return;

            x.Add(c);
            for (int i = 0; i < 10; i++)
            {
                BackTrack(c + 1, x);
                x.RemoveAt(x.Count - 1);
            }
        }
        static void Main(string[] args)
        {
            Vertex<int> startNode = new Vertex<int>(6);
            Vertex<int> vertexA = new Vertex<int>(2);
            Vertex<int> vertexB = new Vertex<int>(7);
            Vertex<int> vertexC = new Vertex<int>(1);
            Vertex<int> vertexD = new Vertex<int>(4);
            Vertex<int> vertexE = new Vertex<int>(9);   
            Vertex<int> vertexF = new Vertex<int>(10);

            Graph<int> graph = new Graph<int>();

            graph.AddVertex(startNode);
            graph.AddVertex(vertexA);
            graph.AddVertex(vertexB);
            graph.AddVertex(vertexC);
            graph.AddVertex(vertexD);
            graph.AddVertex(vertexE);
            graph.AddVertex(vertexF);

            graph.AddEdge(startNode, vertexA);
            graph.AddEdge(startNode, vertexB);
            graph.AddEdge(vertexA, vertexC);
            graph.AddEdge(vertexA, vertexD);
            graph.AddEdge(vertexB, vertexE);
            graph.AddEdge(startNode, vertexF);
            graph.AddEdge(vertexD, vertexE);


            List<Vertex<int>> breadthFirstTraversal = graph.BreadthFirstTraversal();

            Console.WriteLine("Breadth First Traversal: ");
            for (int i = 0; i < breadthFirstTraversal.Count; i++)
            {
                Console.WriteLine(breadthFirstTraversal[i].Value);
            }

            List<Vertex<int>> depthFirstTraversal = graph.DepthFirstTraversal();

            Console.WriteLine("\nDepth First Traversal: ");
            for (int i = 0; i < depthFirstTraversal.Count; i++)
            {
                Console.WriteLine(depthFirstTraversal[i].Value);
            }

            depthFirstTraversal = graph.RecursiveDepthFirst();

            Console.WriteLine("\nRecursive Depth First Traversal: ");
            for (int i = 0; i < depthFirstTraversal.Count; i++)
            {
                Console.WriteLine(depthFirstTraversal[i].Value);
            }

            Stack<Vertex<int>> SSSPTraversal = graph.SingleSourceShortestPathBFS(vertexF, vertexD);
            int count = SSSPTraversal.Count;

            Console.WriteLine("\nSingle Source Shortest Path BFS: ");
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(SSSPTraversal.Pop().Value);    
            }

            /*
             *         6
             *   2     7    10
             *  1 4  --  9
             */

            //BFS : 6 2 7 10 1 4 9
            //DFS : 6 2 1 4 7 9 10
        }
    }
}