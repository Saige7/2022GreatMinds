    namespace LRUCacheAssignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LRUCache<int , int> LRUCache = new LRUCache<int , int>();

            LRUCache.Put(1, 2);
            LRUCache.Put(2, 3);
            LRUCache.Put(3, 4);

            LRUCache.TryGetValue(10, out int value);
        }
    }
}
