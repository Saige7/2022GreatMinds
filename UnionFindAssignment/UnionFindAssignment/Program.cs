namespace UnionFindAssignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> list = new List<string>();
            list.Add("B");
            list.Add("J");
            list.Add("A");
            list.Add("K");
            list.Add("E");

            QuickFind<string> quickFind = new QuickFind<string>(list);
            QuickUnion<string> quickUnion = new QuickUnion<string>(list);

            Console.WriteLine(quickUnion.Find("B"));
            Console.WriteLine(quickFind.Find("B"));

            Console.WriteLine(quickUnion.Union("A", "B"));
            Console.WriteLine(quickFind.Union("A", "B"));

            Console.WriteLine(quickUnion.AreConnected("A", "B"));
            Console.WriteLine(quickFind.AreConnected("A", "B"));

            Console.WriteLine(quickUnion.Find("A"));
            Console.WriteLine(quickFind.Find("A"));

            Console.WriteLine(quickUnion.Find("K"));
            Console.WriteLine(quickFind.Find("K"));
        }
    }
}
