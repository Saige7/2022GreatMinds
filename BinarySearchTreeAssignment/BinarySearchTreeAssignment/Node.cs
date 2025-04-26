namespace BinarySearchTreeAssignment
{
    class Node<T>
    {
        public T Value;
        public Node<T> left;
        public Node<T> right;

        public Node(T value)
        {
            Value = value;
        }
    }
}