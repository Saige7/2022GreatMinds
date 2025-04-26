namespace BloomFilterAssignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BloomFilter<int> bloomFilter = new BloomFilter<int>(50);

            bloomFilter.Insert(20);
            bloomFilter.Insert(30);
            bloomFilter.Insert(2);
            bloomFilter.Insert(3);
            bloomFilter.Insert(6);
            bloomFilter.Insert(14);

            Console.WriteLine(bloomFilter.ProbablyContains(6));
        }
    }
}
