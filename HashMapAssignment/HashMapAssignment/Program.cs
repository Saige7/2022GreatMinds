using System.Diagnostics.CodeAnalysis;

namespace HashMapAssignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HashMap<int, string> hashmap = new HashMap<int, string>(3);

            hashmap.Add(5, "hi");
            hashmap.Add(13, "hello");
            hashmap.Add(52, "hey");
            hashmap.ReHash();
            //hashmap.Add(5, "hat");
            hashmap.Remove(5);
            Console.WriteLine(hashmap.Contains(new KeyValuePair<int, string>(13, "hi")));
            Console.WriteLine(hashmap.ContainsKey(13));
            //hashmap.Clear();

            int[] num = { 3, 4, 7, 19, 20, 2, 18, 6, 37, 98, 42, 31, 1, 24};
            for (int i = 0; i < num.Length; i++)
            {
                hashmap.Add(new KeyValuePair<int, string>(num[i], "h"));
            }
            hashmap.ReHash();

            int[] othernum = { 3, 42, 31, 1, 24 };
            for (int i = 0; i < othernum.Length; i++)
            {
                hashmap.Remove(othernum[i]);
            }
            Console.WriteLine(hashmap.Remove(104));

            hashmap[20] = "g";
            Console.WriteLine(hashmap[20]);
            Console.WriteLine("\n");

            string value = "hi";
            Console.WriteLine(hashmap.TryGetValue(20, out value));
            Console.WriteLine(value);
            Console.WriteLine(hashmap.TryGetValue(900, out value));
            Console.WriteLine(value);
            Console.WriteLine();

            IEnumerator<KeyValuePair<int, string>> enumerator = hashmap.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }
        }
    }
}
