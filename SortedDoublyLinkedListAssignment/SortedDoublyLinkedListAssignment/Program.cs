namespace SortedDoublyLinkedListAssignment
{
    internal partial class Program
    {

        static void Main(string[] args)
        {
            SortedDoublyLinkedList<int> list = new SortedDoublyLinkedList<int>();

            list.Insert(4);
            list.Insert(5);
            list.Insert(2);
            list.Insert(-8);
            list.Insert(-2);
            list.Insert(17);
            list.Delete(4);
            list.Delete(-8);
            list.Delete(17);
            list.Insert(5);

            list.Print();
        }
    }
}