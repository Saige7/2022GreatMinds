namespace SortedSetAssignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ISortedSet<int> sortedSet = new SortedSet<int>(new ReverseComparer());
            ISortedSet<int> sortedSet2 = new SortedSet<int>(new NormalComparer());
            int[] items = { 1, 2, 3 };
           
            sortedSet.Add(5);
            sortedSet.Add(27);
            sortedSet.Add(13);
            sortedSet.Add(2);
            sortedSet.Add(14);
            sortedSet.Remove(27);
            sortedSet.Remove(5);

            sortedSet2.Add(5);
            sortedSet2.Add(13);
            sortedSet2.Add(11);
            sortedSet2.Add(6);
            sortedSet2.Add(14);
            sortedSet2.Add(2);

            Console.WriteLine(sortedSet.Max());
            Console.WriteLine(sortedSet.Min());
            Console.WriteLine(sortedSet.Contains(1));
            Console.WriteLine(sortedSet.Contains(16));
            Console.WriteLine(sortedSet.Ceiling(14));
            Console.WriteLine(sortedSet.Floor(12));

            ISortedSet<int> sortedSet3 = sortedSet.Union(sortedSet2);
            ISortedSet<int> sortedSet4 = sortedSet.Intersection(sortedSet2);
            sortedSet.AddRange(items);
            foreach (var item in sortedSet)
            {
                Console.WriteLine(item);
            }
            //sortedSet.Clear();
        }
    }
}
