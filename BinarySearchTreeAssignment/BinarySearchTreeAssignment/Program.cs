using System;
using System.Data;
using System.Globalization;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace BinarySearchTreeAssignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BinarySearchTree<int> binarySearchTree = new BinarySearchTree<int>();

            binarySearchTree.Insert(5);
            binarySearchTree.Insert(3);
            binarySearchTree.Insert(8);
            binarySearchTree.Insert(6);
            binarySearchTree.Insert(9);
            binarySearchTree.Insert(1);
            binarySearchTree.Insert(4);

            Queue<int> preOrderQueue = binarySearchTree.PreOrderTraversal();
            Queue<int> inOrderQueue = binarySearchTree.InOrderTraversal();
            Queue<int> postOrderQueue = binarySearchTree.PostOrderTraversal();
            Queue<int> breadthFirstQueue = binarySearchTree.BreadthFirstTraversal();
            Queue<int> preOrderRecursiveQueue = binarySearchTree.PreOrderStart();

            Console.WriteLine("Pre-Order Traversal");
            while (preOrderQueue.Count != 0)
            {
                Console.WriteLine(preOrderQueue.Dequeue());
            }
            Console.WriteLine();

            Console.WriteLine("In-Order Traversal");
            while (inOrderQueue.Count != 0)
            {
                Console.WriteLine(inOrderQueue.Dequeue());
            }
            Console.WriteLine();

            Console.WriteLine("Post-Order Traversal");
            while (postOrderQueue.Count != 0)
            {
                Console.WriteLine(postOrderQueue.Dequeue());
            }
            Console.WriteLine();

            Console.WriteLine("Breadth First Traversal");
            while (breadthFirstQueue.Count != 0)
            {
                Console.WriteLine(breadthFirstQueue.Dequeue());
            }
            Console.WriteLine();

            Console.WriteLine("Pre-Order Recursive Traversal");
            while (preOrderRecursiveQueue.Count != 0)
            {
                Console.WriteLine(preOrderRecursiveQueue.Dequeue());
            }
        }
    }
}