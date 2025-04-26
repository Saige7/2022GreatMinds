namespace DoublyLinkedListAssignment
{

    internal class Program
    {
        static void Main(string[] args)
        {
            DoublyLinkedList<int> doublyLinkedList = new DoublyLinkedList<int>();

            for (int i = 0; i < 6; i++)
            {
                doublyLinkedList.AddLast(i);
            }

            Console.WriteLine(doublyLinkedList.isEmpty());

            doublyLinkedList.printList(doublyLinkedList);

        }
    }
}