using Microsoft.VisualBasic;

using System.Drawing;
using System.Text;
using System.Text.Json;

namespace WeightedDirectedGraphsAssignment
{
    public struct AirportEdge
    { 
        public string Start { get; set; }
        public string End { get; set; }
        public int Distance { get; set; }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            Graph<int> graph = new Graph<int>();
            PathFinding<int> pathFinding = new PathFinding<int>(graph);

            Vertex<int> startNode6 = new Vertex<int>(6);
            Vertex<int> vertex2 = new Vertex<int>(2);
            Vertex<int> vertex7 = new Vertex<int>(7);
            Vertex<int> vertex1 = new Vertex<int>(1);
            Vertex<int> vertex4 = new Vertex<int>(4);
            Vertex<int> vertex9 = new Vertex<int>(9);
            Vertex<int> vertex10 = new Vertex<int>(10);

            graph.AddVertex(startNode6);
            graph.AddVertex(vertex2);
            graph.AddVertex(vertex7);
            graph.AddVertex(vertex1);
            graph.AddVertex(vertex4);
            graph.AddVertex(vertex9);
            graph.AddVertex(vertex10);

            graph.AddEdge(startNode6, vertex2, -3);
            graph.AddEdge(startNode6, vertex7, 1);
            graph.AddEdge(startNode6, vertex9, 1);
            graph.AddEdge(startNode6, vertex10, 18);
            graph.AddEdge(vertex2, vertex1, 1);
            graph.AddEdge(vertex2, vertex4, 6);
            graph.AddEdge(vertex7, vertex9, 4);
            graph.AddEdge(vertex9, vertex4, 3);
            graph.AddEdge(vertex4, vertex1, 2);
            graph.AddEdge(vertex1, vertex4, 2);
            graph.AddEdge(vertex4, startNode6, -4);

            Dictionary<Vertex<int>, Point> map = new Dictionary<Vertex<int>, Point>();
            map.Add(startNode6, new Point(3, 0));
            map.Add(vertex2, new Point(1, 1));
            map.Add(vertex7, new Point(3, 1));
            map.Add(vertex10, new Point(5, 1));
            map.Add(vertex1, new Point(0, 2));
            map.Add(vertex4, new Point(2, 2));
            map.Add(vertex9, new Point(4, 2));

            /*
             *         6---,
             *   2     7   |   10
             *  1-4 <-- 9 -' 
             * 
             */

            Stack<Vertex<int>> breadthFirst = graph.BreadthFirst(startNode6, vertex4);
            int count = breadthFirst.Count;
            Console.WriteLine("Breadth-First Search: ");
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(breadthFirst.Pop().Value);
            }

            Stack<Vertex<int>> depthFirst = graph.DepthFirst(startNode6, vertex4);
            count = depthFirst.Count;
            Console.WriteLine("Depth-First Search: ");
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(depthFirst.Pop().Value);
            }

            Stack<Vertex<int>> dijkstrasAlgorithm = pathFinding.DijkstraAlgorithm(startNode6, vertex1);
            count = dijkstrasAlgorithm.Count;
            Console.WriteLine("\nDijkstra's Algorithm: ");
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(dijkstrasAlgorithm.Pop().Value);
            }

            Stack<Vertex<int>> AStarAlgorithm = pathFinding.AStarAlgorithm(startNode6, vertex1, map);
            count = AStarAlgorithm.Count;
            Console.WriteLine("\nA Star Algorithm: ");
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(AStarAlgorithm.Pop().Value);
            }
            Console.WriteLine();
            Console.WriteLine(pathFinding.BellmanFordAlgorithm(startNode6));


            AirportEdge[] edges = (AirportEdge[])JsonSerializer.Deserialize(File.ReadAllText("C:\\Users\\saige.kumar\\Downloads\\AirportProblemEdges.json"), typeof(AirportEdge[]));
            string[] vertices = (string[])JsonSerializer.Deserialize(File.ReadAllText("C:\\Users\\saige.kumar\\Downloads\\AirportProblemVerticies.json"), typeof(string[]));

            Graph<string> airportGraph = new Graph<string>();
            for (int i = 0; i < vertices.Length; i++)
            {
                airportGraph.AddVertex(new Vertex<string>(vertices[i]));
            }
            for (int i = 0; i < edges.Length; i++)
            {
                airportGraph.AddEdge(airportGraph.Search(edges[i].Start), airportGraph.Search(edges[i].End), edges[i].Distance);
            }

            PathFinding<string> airport = new PathFinding<string>(airportGraph);


            Console.WriteLine("\nStart Airport: ");
            string startAirport = Console.ReadLine();
            Console.WriteLine("End Airport: ");
            string endAirport = Console.ReadLine();

            Stack<Vertex<string>> shortestRoute = airport.DijkstraAlgorithm(airportGraph.Search(startAirport), airportGraph.Search(endAirport));
            count = shortestRoute.Count;
            Console.WriteLine("\nShortest Route: ");
            for (int i = 0; i < count - 1; i++)
            {
                Console.Write($"{shortestRoute.Pop().Value} --> ");
            }
            Console.WriteLine(shortestRoute.Pop().Value);
        }
    }
}