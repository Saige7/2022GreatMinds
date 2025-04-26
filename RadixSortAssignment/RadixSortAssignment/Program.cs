namespace RadixSortAssignment
{
    internal class Program
    {
        static void RadixSort<T>(KeyValuePair<int, T>[] dataset)
        {
            List<KeyValuePair<int, T>>[] buckets = new List<KeyValuePair<int, T>>[10];
            int exponent = 0;
            int iterations = LongestKey(dataset);

            for (int count = 0; count < iterations; count++)
            {
                for (int i = 0; i < buckets.Length; i++)
                {
                    buckets[i] = new List<KeyValuePair<int, T>>();
                }

                for (int i = 0; i < dataset.Length; i++)
                {
                    buckets[(dataset[i].Key / (int)Math.Pow(10, exponent)) % 10].Add(dataset[i]);
                }
                exponent++;

                int count2 = 0;
                for (int i = 0; i < buckets.Length; i++)
                {
                    for (int j = 0; j < buckets[i].Count; j++)
                    {
                        dataset[count2] = buckets[i][j];
                        count2++;
                    }
                }
            }
        }
        static int LongestKey<T>(KeyValuePair<int, T>[] dataset)
        {
            int count = 0;
            string check = "";
            for (int i = 0; i < dataset.Length; i++)
            {
                check = $"{dataset[i].Key}";

                if (check.Length > count)
                {
                    count = check.Length;
                }
            }
            return count;
        }
        
        static void Main(string[] args)
        {
            KeyValuePair<int, string>[] dataset = { new KeyValuePair<int, string>(1011, "a"), new KeyValuePair<int, string>(1011, "b"), new KeyValuePair<int, string>(1061, "mnopqr"),
                new KeyValuePair<int, string>(1021, "cd"), new KeyValuePair<int, string>(1013, "e"), new KeyValuePair<int, string>(1031, "fgh"),
                new KeyValuePair<int, string>(1014, "i"), new KeyValuePair<int, string>(1051, "jklmn"), new KeyValuePair<int, string>(1041, "opqr")};

            Console.WriteLine("Key    Value");
            for (int i = 0; i < dataset.Length; i++)
            {
                Console.WriteLine($"{dataset[i].Key}   {dataset[i].Value}");
            }

            Console.WriteLine();
            RadixSort(dataset);

            Console.WriteLine("Key    Value");
            for (int i = 0; i < dataset.Length; i++)
            {
                Console.WriteLine($"{dataset[i].Key}   {dataset[i].Value}");
            }

        }
    }
}
