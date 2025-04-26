using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurstTrieAssignment.BurstTrieStuff
{
    class BurstTrie
    {
        public readonly char Min;
        public readonly char Max;
        public BurstNode root { get; private set; }

        public BurstTrie(char min, char max)
        {
            Min = min;
            Max = max;
            root = new ContainerNode(null);
        }

        public List<string> BurstSort(List<string> listToSort)
        {
            for (int i = 0; i < listToSort.Count; i++)
            {
                root.Insert(listToSort[i], 0);
            }
            bool success = false;
            root.Remove("install", 0, out success);
            List<string> output = new List<string>();
            root.GetAll(output);

            return output;
        }
    }
}
