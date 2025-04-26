using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace LRUCacheAssignment
{
    class DoublyLinkedList<T> 
    {
        public Node<T> Head;
        public Node<T> Tail;
        public int Count;

        public DoublyLinkedList()
        {
            Count = 0;
        }
        
        public void MoveToHead(T value)
        {
            Node<T> current = Find(value);

            current.Previous.Next = current.Next;
            current.Next.Previous = current.Previous;
            current.Previous = Tail;
            current.Next = Head;
            Head.Previous = current;
            Head = current;
        }
        public Node<T> Find(T value)
        {

            Node<T> current = Head;
            while (!current.Value.Equals(value) && current != null)
            {
                current = current.Next;
            }

            if(current == null)
            {
                return null;
            }
            
            return current;
        }
        public void AddFirst(T value)
        {
            Node<T> nodeToInsert = new Node<T>(value);

            if (Head == null)
            {
                Head = nodeToInsert;
                Tail = Head;
            }
            else
            {
                nodeToInsert.Next = Head;
                Head.Previous = nodeToInsert;
                Head = nodeToInsert;
                Tail.Next = nodeToInsert;
                nodeToInsert.Previous = Tail;
            }

            Count++;
        }
        public void AddLast(T value)
        {
            Node<T> nodeToInsert = new Node<T>(value);

            if (Head == null)
            {
                Head = nodeToInsert;
                Tail = Head;
            }
            else
            {
                Tail.Next = nodeToInsert;
                nodeToInsert.Previous = Tail;
                Tail = nodeToInsert;
                Head.Previous = nodeToInsert;
                nodeToInsert.Next = Head;
            }

            Count++;
        }
        public void AddBefore(Node<T> node, T value)
        {
            Node<T> nodeToInsert = new Node<T>(value);
            Node<T> current = Head;

            if (node == null)
            {
                throw new Exception("error");
            }
            else if (node == Head)
            {
                AddFirst(value);
            }

            while(current != node)
            {
                current = current.Next;
            }

            current.Previous.Next = nodeToInsert;
            nodeToInsert.Previous = current.Previous;
            nodeToInsert.Next = current;
            current.Previous = nodeToInsert;

            Count++;
        }
        public void AddAfter(Node<T> node, T value)
        {
            Node<T> nodeToInsert = new Node<T>(value);
            Node<T> current = Head;

            if (node == null)
            {
                throw new Exception("error");
            }
            else if (node == Tail)
            {
                AddLast(value);
            }

            while (current != node)
            {
                current = current.Next;
            }

            nodeToInsert.Next = current.Next;
            current.Next.Previous = nodeToInsert;
            current.Next = nodeToInsert;
            nodeToInsert.Previous = current;

            Count++;
        }
        public bool RemoveFirst()
        {
            if (Head == null)
            {
                return false;
            }


            Tail.Next = Head.Next;
            Head = Head.Next;
            Head.Previous = Tail;


            Count--;
            
            return true;
        }
        public bool RemoveLast()
        {
            if (Head == null)
            {
                return false;
            }

            Head.Previous = Tail.Previous;
            Tail = Tail.Previous;
            Tail.Next = Head;

            Count--;

            return true;
        }
        public bool Remove(T value)
        {
            Node<T> current = Head;

            while (!current.Value.Equals(value) && current != null)
            {
                current = current.Next;
            }

            if (current == null)
            {
                return false;
            }

            current.Previous.Next = current.Next;
            current.Next.Previous = current.Previous;

            Count--;

            return true;
        }
        public bool isEmpty()
        {
            if(Count == 0)
            {
                return true;
            }
            return false;
        }
        public void printList(DoublyLinkedList<T> listToPrint)
        {
            Node<T> current = listToPrint.Head;

            for (int i = 0; i < listToPrint.Count; i++)
            {
                Console.WriteLine(current.Value);
                current = current.Next;
            }
        }

    }
}