namespace RedBlackTree
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RedBlack<int> redBlackTree = new RedBlack<int>();
            redBlackTree.Add(13);
            redBlackTree.Add(8);
            redBlackTree.Add(17);
            redBlackTree.Add(1);
            redBlackTree.Add(11);
            redBlackTree.Add(15);
            redBlackTree.Add(25);
            redBlackTree.Add(6);
            redBlackTree.Add(22);
            redBlackTree.Add(27);

            redBlackTree.Remove(27);
            redBlackTree.Remove(25);
            redBlackTree.Remove(15);

            redBlackTree.Remove(6);

            Queue<int> queue = redBlackTree.TraversalStart();

            Console.WriteLine("Traversal: ");
            while (queue.Count != 0)
            {
                Console.WriteLine(queue.Dequeue());
            }
        }
    }
}