using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurstTrieAssignment.BurstTrieStuff
{
    abstract class BurstNode
    {
        internal BurstTrie ParentTrie;
        public abstract int Count { get; }
        public BurstNode root;

        protected BurstNode(BurstTrie parent)
        {
            ParentTrie = parent;
        }

        public abstract BurstNode Insert(string value, int index);

        public abstract BurstNode Remove(string value, int index, out bool success);

        public abstract BurstNode Search(string prefix, int index);

        internal abstract void GetAll(List<string> output);

    }
}
