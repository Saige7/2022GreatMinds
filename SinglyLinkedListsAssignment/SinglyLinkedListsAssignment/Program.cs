namespace SinglyLinkedListsAssignment
{
    class Node<T> 
    {
        public T Value;
        public Node<T> Next;

        public Node(T value)
            : this(value, null) { }
        public Node(T value, Node<T> next)
        {
            Value = value;
            Next = next;
        }
    }
    class Comparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            if(x < y)
            {
                return -1;
            }
            else if(x == y)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }

    class LinkedList<T>
    {
        public Node<T> Head;
        public Node<T> Tail;
        public int Count { get; private set; }

        public LinkedList()
        {
        }
      
        public void AddFirst(T givenValue)
        {
            if(Head == null)
            {
                Head = new Node<T>(givenValue);
                Tail = Head;
                Count = 1;
            }
            else
            {
                Node<T> nodeToInsert = new Node<T>(givenValue);
                nodeToInsert.Next = Head;
                Head = nodeToInsert;
                Count++;
            }
        }
        public void AddLast(T givenValue)
        {
            if(Head == null)
            {
                Head = new Node<T>(givenValue);
                Tail = Head;
                Count = 1;
            }
            else
            {
                Tail.Next = new Node<T>(givenValue);
                Tail = Tail.Next;
                Count++;
            }
        }
        public void AddBefore(Node<T> givenNode, T givenValue)
        {
            Node<T> runThroughList = Head;
            bool found = false;
            while(found == false)
            {
                if(givenNode == Head)
                {
                    Node<T> insertedNode = new Node<T>(givenValue);
                    insertedNode.Next = Head;
                    Head = insertedNode;
                    found = true;
                    Count++;
                }              
                else if(runThroughList.Next == givenNode)
                {
                    Node<T> insertedNode = new Node<T>(givenValue);
                    insertedNode.Next = givenNode;
                    Count++;
                    found = true;
                }
                runThroughList = runThroughList.Next;
            }
        }

        public void AddAfter(Node<T> givenNode, T givenValue)
        {
            Node<T> runThroughList = Head;
            bool found = false;
            while(found == false)
            {
                if(givenNode == Tail)
                {
                    Node<T> insertedNode = new Node<T>(givenValue);
                    Tail.Next = insertedNode;
                    Tail = Tail.Next;
                    Count++;
                    found = true;
                }
                else if(runThroughList == givenNode)
                {
                    Node<T> insertedNode = new Node<T>(givenValue);
                    insertedNode.Next = givenNode.Next;
                    givenNode.Next = insertedNode;
                    Count++;
                    found = true;
                }
                runThroughList = runThroughList.Next;
            }
        }

        public bool RemoveFirst()
        {
            if(Head == null)
            {
                return false;
            }
            else
            {
                Head = Head.Next;
                Count--;
                return true;
            }
        }

        public bool RemoveLast()
        {
            Node<T> runThroughList = Head;
            bool found = false;
            if(Head == null)
            {
                return false;
            }
            else
            {
                while (found == false)
                {
                    if (runThroughList.Next == Tail)
                    {
                        runThroughList.Next = null;
                        Tail = runThroughList;
                        found = true;
                    }
                    runThroughList = runThroughList.Next;
                }
                Count--;
                return true;
            }
        }

        public bool Remove(T givenValue)
        {
            Node<T> runThroughList = Head;
            int index = 0;
            if(Head != null && givenValue.Equals(Head.Value) == true)
            {
                RemoveFirst();
                return true;
            }
            while(index < Count - 1)
            {
                if(runThroughList.Next.Value.Equals(givenValue) == true)
                {
                    runThroughList.Next = runThroughList.Next.Next;
                    Count--;
                    return true;
                }
                runThroughList = runThroughList.Next;
                index++;
            }
            return false;
        }
        public void Clear()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }

        public bool Contains(T givenValue)
        {
            Node<T> runThroughList = Head;
            bool found = false;
            int index = 0;
            while (index < Count)
            {
                if (runThroughList.Value.Equals(givenValue) == true)
                {
                    found = true;
                }

                runThroughList = runThroughList.Next;
                index++;
            }
            return found;
        }
        public Node<T> Search(T givenValue)
        {
            Node<T> runThroughList = Head;
            int index = 0;
            while(index < Count)
            {
                if (runThroughList.Value.Equals(givenValue) == true)
                {
                    return runThroughList;
                }
                else
                {
                    runThroughList = runThroughList.Next;
                    index++;
                }
            }
            return null;
        }
        public void SortList(IComparer<T> compare,LinkedList<T> listToSort)
        {
            LinkedList<T> sortedList = new LinkedList<T>();
            Node<T> current = listToSort.Head;
            Node<T> runThroughList = listToSort.Head.Next;
            int count = listToSort.Count;

            for(int j = 0; j < count; j++)
            {
                for (int i = j; i < count; i++)
                {
                    if (runThroughList != null)
                    {
                        if (compare.Compare(current.Value, runThroughList.Value) > 0)
                        {
                            current = runThroughList;
                        }
                        if (runThroughList != listToSort.Tail)
                        {
                            runThroughList = runThroughList.Next;
                        }
                    }
                }
                sortedList.AddLast(current.Value);
                listToSort.Remove(current.Value);
                current = listToSort.Head;
                if (j + 1 < count)
                {
                    runThroughList = listToSort.Head.Next;
                }
            }

            Node<T> runThroughOtherList = sortedList.Head;
            for(int s = 0; s < sortedList.Count; s++)
            {
                listToSort.AddLast(runThroughOtherList.Value);
                runThroughOtherList = runThroughOtherList.Next;
            }

        }

        public void printList(LinkedList<T> listToPrint)
        {
            Node<T> runThroughList = listToPrint.Head;

            for(int i = 0; i < listToPrint.Count; i++)
            {
                Console.WriteLine(runThroughList.Value);
                runThroughList = runThroughList.Next;
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            LinkedList<int> linkedList = new LinkedList<int>();

            Comparer comparer = new Comparer();

            Random randomInt = new Random();
            for (int i = 0; i < 6; i++)
            {
                linkedList.AddLast(randomInt.Next(1, 7));
            }
            linkedList.printList(linkedList);
            Console.WriteLine();
            linkedList.SortList(comparer, linkedList);
            linkedList.printList(linkedList);
        }
    }
}