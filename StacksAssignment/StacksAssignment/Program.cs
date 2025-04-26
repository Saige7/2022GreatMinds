using System.Data.Common;

namespace StacksAssignment
{
    internal class Program
    {
        class Stack<T>
        {
            public int Count { get; private set; }
            private LinkedList<T> data = new LinkedList<T>();

            public Stack()
            {
            }

            public void Push(T value)
            {
                data.AddFirst(value);
            }
            public T Pop()
            {
                if(data.First == null)
                {
                    throw new Exception("stack was empty");
                }
                T firstValue = data.First.Value;
                data.RemoveFirst();
                return firstValue;
            }
            public T Peek()
            {
                if(data.First == null)
                {
                    throw new Exception("stack was empty");
                }
                T firstValue = data.First.Value;
                return firstValue;
            }
        }
        class ArrayStack<T>
        {
            public int Count { get; private set; }
            private T[] data;

            public ArrayStack(int capacity = 10)
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
            public void Push(T value)
            {
                if(Count > data.Length)
                {
                    Resize(Count*2);
                }
                data[Count] = value;
                Count++;

            }
            public T Pop()
            {
                if(Count == 0)
                {
                    throw new Exception("Stack is empty");
                }
                T firstValue = data[Count - 1];
                Count--;
                return firstValue;
            }
            public T Peek()
            {
                if(Count == 0)
                {
                    throw new Exception("Stack is empty");
                }
                return data[Count - 1];
            }

        }
        static void Main(string[] args)
        {
            Stack<int> stack = new Stack<int>();
            ArrayStack<int> arrayStack = new ArrayStack<int>();

            stack.Push(1);
            stack.Push(2);
            stack.Pop();
            stack.Push(3);
            Console.WriteLine(stack.Peek());

            arrayStack.Push(1);
            arrayStack.Push(2);
            arrayStack.Pop();
            Console.WriteLine(arrayStack.Peek());
        }
    }
}