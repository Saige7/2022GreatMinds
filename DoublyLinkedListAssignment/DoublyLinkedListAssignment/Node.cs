namespace DoublyLinkedListAssignment
{
    class Node<T>
    {
        public Node<T> Next;
        public Node<T> Previous;
        public T Value;

        public Node(T value)
        {
            Value = value;
            Next = null;
            Previous = null;
        }
    }
}