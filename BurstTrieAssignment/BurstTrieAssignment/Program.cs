using BurstTrieAssignment.BurstTrieStuff;

namespace BurstTrieAssignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> listToSort = new List<string>();
            listToSort.Add("a");
            listToSort.Add("ace");
            listToSort.Add("add");
            listToSort.Add("b");
            listToSort.Add("bad");
            listToSort.Add("bay");
            listToSort.Add("by");
            listToSort.Add("beds");
            listToSort.Add("dime");
            listToSort.Add("fine");
            listToSort.Add("taco");
            listToSort.Add("under");
            listToSort.Add("turtle");
            listToSort.Add("donkey");
            listToSort.Add("hyrax");
            listToSort.Add("king");
            listToSort.Add("installment");
            listToSort.Add("tourney");
            listToSort.Add("install");
            listToSort.Add("zenith");
            listToSort.Add("can");
            listToSort.Add("ice");
            listToSort.Add("omnipotent");
            listToSort.Add("jail");
            listToSort.Add("sky");
            listToSort.Add("good");
            listToSort.Add("quoka");

            BurstTrie burstTrie = new BurstTrie('a', 'z');
            listToSort = burstTrie.BurstSort(listToSort);

            for (int i = 0; i < listToSort.Count; i++)
            {
                Console.WriteLine(listToSort[i]);
            }
            BurstNode node = burstTrie.root.Search("b", 0);
            Console.WriteLine(node!=null);
        }
    }
}
