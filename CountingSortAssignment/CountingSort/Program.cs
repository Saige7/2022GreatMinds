namespace CountingSort
{
    internal class Program
    {
        static void CountingSort<T>(KeyValuePair<int, T>[] dataset)
        {
            int max = dataset.Max(x => x.Key);
            int min = dataset.Min(x => x.Key);

            List<KeyValuePair<int, T>>[] buckets = new List<KeyValuePair<int, T>>[(max - min) + 1];
            for (int i = 0; i < buckets.Length; i++)
            {
                buckets[i] = new List<KeyValuePair<int, T>>();
            }

            for (int i = 0; i < dataset.Length; i++)
            {
                buckets[dataset[i].Key - min].Add(dataset[i]);
            }

            int count = 0;
            for (int i = 0; i < buckets.Length; i++)
            {
                for (int j = 0; j < buckets[i].Count; j++)
                {
                    dataset[count] = buckets[i][j];
                    count++;
                }
            }
        }
        static void Main(string[] args)
        {
            KeyValuePair<int, string>[] dataset = { new KeyValuePair<int, string>(1001, "a"), new KeyValuePair<int, string>(1001, "b"), new KeyValuePair<int, string>(1006, "mnopqr"), 
                new KeyValuePair<int, string>(1002, "cd"), new KeyValuePair<int, string>(1001, "e"), new KeyValuePair<int, string>(1003, "fgh"), 
                new KeyValuePair<int, string>(1001, "i"), new KeyValuePair<int, string>(1003, "jkl")};

            Console.WriteLine("Key    Value");
            for (int i = 0; i < dataset.Length; i++)
            {
                Console.WriteLine($"{dataset[i].Key}   {dataset[i].Value}");
            }

            Console.WriteLine();
            CountingSort<string>(dataset);

            Console.WriteLine("Key    Value");
            for (int i = 0; i < dataset.Length; i++)
            {
                Console.WriteLine($"{dataset[i].Key}   {dataset[i].Value}");
            }
        }
    }
}
