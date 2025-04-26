namespace BucketSortAssignment
{
    internal class Program
    {
        static void BucketSort<T>(KeyValuePair<int, T>[] dataset, int bucketCount)
        {
            int max = dataset.Max(x => x.Key);
            int min = dataset.Min(x => x.Key);

            int increment = (max - min + 1) / bucketCount;
            List<int> increments = new List<int>();
            for (int i = 1; i <= bucketCount; i++)
            {
                increments.Add((min - 1) + (increment * i));
            }

            List<KeyValuePair<int, T>>[] buckets = new List<KeyValuePair<int, T>>[bucketCount];
            for (int i = 0; i < buckets.Length; i++)
            {
                buckets[i] = new List<KeyValuePair<int, T>>();
            }

            int whichBucket = 0;

            for (int i = 0; i < dataset.Length; i++)
            {
                for (int j = 0; j < increments.Count; j++)
                {
                    if (dataset[i].Key < increments[j])
                    {
                        whichBucket = j;
                    }
                }
                buckets[whichBucket].Add(dataset[i]);
            }

            for (int i = 0; i < buckets.Length; i++)
            {
                InsertionSort(buckets[i]);
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
        static void InsertionSort<T>(List<KeyValuePair<int, T>> bucket)
        {
            for (int i = 1; i < bucket.Count; i++)
            {
                int j = i;
                while (j > 0 && bucket[j].Key < bucket[j - 1].Key)
                {
                    (bucket[j], bucket[j - 1]) = (bucket[j - 1], bucket[j]);
                    j--;
                }
            }
        }
        static void Main(string[] args)
        {
            KeyValuePair<int, string>[] dataset = { new KeyValuePair<int, string>(1001, "a"), new KeyValuePair<int, string>(1001, "b"), new KeyValuePair<int, string>(1006, "mnopqr"),
                new KeyValuePair<int, string>(1002, "cd"), new KeyValuePair<int, string>(1001, "e"), new KeyValuePair<int, string>(1003, "fgh"),
                new KeyValuePair<int, string>(1001, "i"), new KeyValuePair<int, string>(1005, "jklmn"), new KeyValuePair<int, string>(1004, "opqr")};

            Console.WriteLine("Key    Value");
            for (int i = 0; i < dataset.Length; i++)
            {
                Console.WriteLine($"{dataset[i].Key}   {dataset[i].Value}");
            }

            Console.WriteLine();
            BucketSort<string>(dataset, 3);

            Console.WriteLine("Key    Value");
            for (int i = 0; i < dataset.Length; i++)
            {
                Console.WriteLine($"{dataset[i].Key}   {dataset[i].Value}");
            }
        }
    }
}
