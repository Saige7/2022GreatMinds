namespace SortedDoublyLinkedListAssignment
{

    class SortedDoublyLinkedList<T> where T : IComparable<T>
    {
        public int Count = 0;
        public Node<T> Head = new Node<T>(default);

        public void Insert(T givenValue)
        {
            Node<T> current = Head.Next;
            Node<T> previous = Head;
            Node<T> nodeToInsert = new Node<T>(givenValue);

            while (current != null && current.Value.CompareTo(givenValue) < 0)
            {
                previous = current;
                current = current.Next;
            }

            nodeToInsert.Next = current;
            nodeToInsert.Previous = previous;
            previous.Next = nodeToInsert;
            if (current != null)
            {
                current.Previous = nodeToInsert;
            }

            Count++;
        }
        public bool Delete(T givenValue)
        {
            Node<T> current = Head.Next;
            Node<T> previous = Head;

            while (current.Next != null && current.Value.CompareTo(givenValue) != 0)
            {
                previous = current;
                current = current.Next;
            }

            if (current.Value.CompareTo(givenValue) != 0)
            {
                return false;
            }

            if (current.Next != null)
            {
                current.Next.Previous = previous;
            }

            previous.Next = current.Next;

            Count--;

            return true;
        }
        public void Print()
        {
            Node<T> current = Head.Next;
            Console.WriteLine();
            while (current.Next != null)
            {
                Console.WriteLine(current.Value);
                current = current.Next;
            }
            Console.WriteLine(current.Value);
        }
    }

}