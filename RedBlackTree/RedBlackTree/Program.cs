namespace RedBlackTree
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RedBlackTree<int> redBlackTree = new RedBlackTree<int>();
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

            //redBlackTree.Remove(15);
        }
    }
}