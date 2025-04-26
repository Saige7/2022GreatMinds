namespace BTreeAssignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BTree<int> btree = new BTree<int>();
            Random newRandom = new Random(6);
            for (int i = 0; i < 15; i++)
            {
                int val = newRandom.Next(0, 101);
                Console.WriteLine(val);
                btree.Insert(val);
            }
            Console.WriteLine(btree.Search(76, btree.root)!=null);
            ;
        }
    }
}
