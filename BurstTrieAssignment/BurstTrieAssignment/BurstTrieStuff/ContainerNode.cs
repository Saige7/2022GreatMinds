using BurstTrieAssignment.BSTStuff;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurstTrieAssignment.BurstTrieStuff
{
    class ContainerNode : BurstNode
    {
        public BinarySearchTree<string> Container;

        public ContainerNode(BurstTrie parent) : base(parent)
        {
            Container = new BinarySearchTree<string>();
        }

        //int a => Container.Count;
        public override int Count
        {
            get
            {
                return Container.Count;
            }
        }

        public override BurstNode Insert(string value, int index)
        {
            if (Container.Count <= 5)
            {
                Container.Insert(value);
                return this;
            }

            BurstNode node = new InternalNode(ParentTrie);
            Queue<string> queue = Container.BreadthFirstTraversal();
            for (int i = 0; i < Container.Count; i++)
            {
                node.Insert(queue.Dequeue(), index);
            }

            return node;
        }

        public override BurstNode Remove(string value, int index, out bool success)
        {
            if(Container.Count == 0)
            {
                success = false;
                return null;
            }

            Container.Delete(Container.Search(value));
            success = true;

            return this;
        }

        public override BurstNode Search(string prefix, int index)
        {
            Container.Search(prefix);
            return this;
        }

        internal override void GetAll(List<string> output)
        {
            Queue<string> queue = Container.InOrderTraversal();
            output.AddRange(queue);     
        }
    }
}
