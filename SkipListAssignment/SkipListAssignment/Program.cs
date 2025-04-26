using System.Collections;

namespace SkipListAssignment
{
    class Node<T>
    {
        public T Value;
        public int Height;
        public Node<T> Next;
        public Node<T> Down;

        public Node(T value)
        {
            Value = value;

            Height = 1;
        }
    }
    class SkipList<T> : ICollection<T> where T : IComparable<T>
    {
        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }
        public void Clear()
        {
            throw new NotImplementedException();
        }
        public bool IsReadOnly => throw new NotImplementedException();



        Random random;
        public SkipList(Random random)
        {
            this.random = random;
        }

        public int Count { get; set; }
        public Node<T> head = new Node<T>(default);

        private int ChooseRandomHeight()
        {
            int height = 1;
            
            int headsOrTails = random.Next(0, 2);
            while (headsOrTails != 1 && height < head.Height + 1)
            {
                height++;
                headsOrTails = random.Next(0, 2);
            }

            if(height == head.Height + 1)
            {
                head.Height++;
                Node<T> newNode = new Node<T>(default);
                newNode.Height = head.Height - 1;
                newNode.Down = head.Down;
                head.Down = newNode;
                newNode.Next = head.Next;
                head.Next = null;
            }

            return height;
        }
        public void Add(T givenValue)
        {
            int randomHeight = ChooseRandomHeight();
            Node<T> current = head;

            if(Contains(givenValue) == true)
            {
                throw new Exception("no duplicates");
            }

            while(current.Height != randomHeight)
            {
                current = current.Down;
            }

            Node<T> nodeToInsert = new Node<T>(givenValue);
            nodeToInsert.Height = 1;
            for (int i = 1; i < randomHeight; i++)
            {
                nodeToInsert.Height++;
                Node<T> newNode = new Node<T>(nodeToInsert.Value);
                newNode.Height = nodeToInsert.Height - 1;               
                newNode.Down = nodeToInsert.Down;
                nodeToInsert.Down = newNode;
            }

            while (current.Next != null && current.Next.Value.CompareTo(givenValue) < 0)
            {
                current = current.Next; 
            }

            if (current.Next != null)
            {
                nodeToInsert.Next = current.Next;
            }
            current.Next = nodeToInsert;

            Node<T> saveOriginalCurrent = new Node<T>(default);
            while(current.Next.Height != 1 && current.Height != 1)
            { 
                current = current.Down;
                nodeToInsert = nodeToInsert.Down;
                saveOriginalCurrent = current;

                while(current.Next != null && current.Next.Value.CompareTo(nodeToInsert.Value) < 0)
                {
                    current = current.Next;
                }

                if(current.Next == null)
                {
                    current.Next = nodeToInsert;
                }
                else if(current.Next != null && !(current.Next.Value.CompareTo(nodeToInsert.Value) < 0))
                {
                    nodeToInsert.Next = current.Next;
                    current.Next = nodeToInsert;
                }

                current = saveOriginalCurrent;
            }
        }
        public bool Remove(T givenValue)
        {
            Node<T> current = head; 

            if(Contains(givenValue) == false)
            {
                throw new Exception("value is not in Skip List");
            }

            while(current.Next.Value.CompareTo(givenValue) != 0)
            {
                if(current.Next.Next == null)
                {
                    if(head.Down == null)
                    {
                        return false;
                    }
                    current = head.Down;
                    continue;
                }
                current = current.Next;
            }
            
            if (current.Next.Height == 1 && current.Height == 1)
            {
                current.Next = current.Next.Next;
                return true;
            }

            while (current.Next != null && current.Next.Height != 1 && current.Height != 1)
            {
                current.Next = current.Next.Next;
                current = current.Down;
                current.Next = current.Next.Next;
            }

            return true;
        }
        public bool Contains(T givenValue)
        {
            Node<T> current = head;

            while (current.Next == null)
            {
                if(current.Height == 1)
                {
                    return false;
                }
                current = current.Down;
            }

            while(current.Next.Value.CompareTo(givenValue) != 0)
            {
                if(current.Next.Next == null)
                {
                    if(current.Down == null)
                    {
                        return false;
                    }
                    current = current.Down;
                    continue;
                }
                current = current.Next;
            }
            return true;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            // Node<int> sentinel = new Node<int>(default) { Height = 1 };
            // sentinel = new Node<int>(default) { Height = sentinel.Height + 1, Down = sentinel };

            // Node<int> two = new Node<int>(2) { Height = 1 };


            //two  = new Node<int>(two.Value) { Height = two.Height + 1, Down = two };

            // Node<int> currSentinel = sentinel;
            // Node<int> currTwo = two;

            // currSentinel.Next = currTwo;
            // currSentinel = currSentinel.Down;
            // currTwo = currTwo.Down;
            // currSentinel.Next = currTwo;

            Random random = new Random(3);
            SkipList<int> list = new SkipList<int>(random);

            list.Add(17);
            list.Add(4);
            list.Add(24);
            list.Add(13);
            list.Add(-2);
            list.Add(8);
            list.Add(1);

            list.Remove(13);

            Console.WriteLine(list.head.Next.Value); 
            Console.WriteLine(list.head.Down.Next.Next.Value);  
            Console.WriteLine(list.head.Down.Down.Next.Next.Value); 
        }
    }
}