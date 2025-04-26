namespace GenericPractice_ShopingList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ShopingList<string> shopingList = new ShopingList<string>();
            bool exit = false;


            while(exit == false)
            {
                Console.WriteLine("Enter a to add to list, b to remove from list, c to view list, d to exit");
                string usersReply = Console.ReadLine();
                Console.WriteLine();

                if (usersReply == "a")
                {
                    Console.WriteLine("Enter what you would like to add");
                    string addToList = Console.ReadLine();
                    shopingList.Add(addToList);
                    Console.WriteLine();
                }
                else if (usersReply == "b")
                {
                    Console.WriteLine("Enter what you would like to remove");
                    string removeFromList = Console.ReadLine();
                    shopingList.Remove(removeFromList);
                    Console.WriteLine();
                }
                else if (usersReply == "c")
                {
                    shopingList.View();
                    Console.WriteLine();
                }
                else
                {
                    exit = true;
                }
            }
            

        }
    }
}