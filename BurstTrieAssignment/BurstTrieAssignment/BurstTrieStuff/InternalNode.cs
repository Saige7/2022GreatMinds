using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurstTrieAssignment.BurstTrieStuff
{
    class InternalNode : BurstNode
    {
        public BurstNode[] Array;
        public InternalNode(BurstTrie parent) : base(parent)
        {
            Array = new BurstNode[parent.Max - parent.Min];
        }

        public override int Count => throw new NotImplementedException();

        public override BurstNode Insert(string value, int index)
        {
            if (value[index] < ParentTrie.Min || value[index] > ParentTrie.Max)
            {
                throw new ArgumentException("invalid :[");
            }

            if (value[index] == null)
            {
                if (Array[0] == null)
                {
                    BurstNode node = new ContainerNode(ParentTrie);
                    node.Insert(value, index);
                    Array[0] = node;
                }
                else
                {
                    Array[0] = Array[0].Insert(value, index + 1);
                }
            }
            else if (Array[value[index] - ParentTrie.Min] == null)
            {
                BurstNode node = new ContainerNode(ParentTrie);
                node.Insert(value, index);
                Array[value[index] - ParentTrie.Min] = node;
            }
            else
            {
                Array[value[index] - ParentTrie.Min] = Array[value[index] - ParentTrie.Min].Insert(value, index + 1);
            }

            return this;
        }

        public override BurstNode Remove(string value, int index, out bool success)
        {
            if (value[index] == null)
            {
                Array[0] = Array[0].Remove(value, index, out success);
            }

            Array[value[index]] = Array[value[index]].Remove(value, index + 1, out success);
            return this;
        }

        public override BurstNode Search(string prefix, int index)
        {
            if (index == prefix.Length)
            {
                return this;
            }

            return Array[prefix[index]].Search(prefix, index + 1);
        }

        internal override void GetAll(List<string> output)
        {
            for (int i = 0; i < Array.Length; i++)
            {
                Array[i].GetAll(output);
            }
        }
    }
}
