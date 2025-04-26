namespace QueuesAssignment
{
    internal class Program
    {
        class Queue<T>
        {
            public int Count { get; private set; }
            private LinkedList<T> data = new LinkedList<T>();

            public Queue()
            {
            }

            public void Enqueue(T value)
            {
                data.AddLast(value);
            }
            public T Dequeue()
            {
                if(data == null)
                {
                    throw new Exception("queue is empty");
                }
                T firstValue = data.First.Value;
                data.RemoveFirst();
                Count--;
                return firstValue;
            }
            public T Peek()
            {
                if(data == null)
                {
                    throw new Exception("queue is empty");
                }
                T firstValue = data.First.Value;
                return firstValue;
            }
        }
        class ArrayQueue<T>
        {
            public int Count { get; private set; }
            private T[] data;
            private int Head;
            private int Tail;

            public ArrayQueue(int capacity = 10)
            {
                data = new T[capacity];
            }

            private void Resize(int size)
            {
                T[] resizedArray = new T[size];
                for(int i = 0; i < Count; i++)
                {
                    resizedArray[i] = data[i];
                }
                data = resizedArray;
            }
            public void Enqueue(T value)
            {
                if(Count > data.Length)
                {
                    Resize(Count*2);
                }
                data[Tail] = value;
                Count++;
            }
            public T Dequeue()
            {
                if(Count == 0)
                {
                    throw new Exception("Queue is empty");
                }
                T firstValue = data[Head];
                Count--;
                return firstValue;
            }
            public T Peek()
            {
                if(Count == 0)
                {
                    throw new Exception("Queue is empty");
                }
                return data[Head];
            }


        }
        static void Main(string[] args)
        {
            Queue<int> queue = new Queue<int>();
            ArrayQueue<int> arrayQueue = new ArrayQueue<int>();
            
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Dequeue();
            Console.WriteLine(queue.Peek());

            arrayQueue.Enqueue(3);
            arrayQueue.Enqueue(4);
            arrayQueue.Dequeue();
            Console.WriteLine(arrayQueue.Peek());
        }
    }
}