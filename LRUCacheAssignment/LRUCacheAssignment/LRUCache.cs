using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRUCacheAssignment
{
    public interface ICache<TKey, TValue>
    {
        bool TryGetValue(TKey key, out TValue value);
        void Put(TKey key, TValue value);
    }
    class LRUCache<TKey, TValue> : ICache<TKey, TValue> 
    {
        public DoublyLinkedList<TValue> doublyLinkedList;
        public Dictionary<TKey, Node<TValue>> hashmap;
        
        public LRUCache()
        {
            doublyLinkedList = new DoublyLinkedList<TValue>();
            hashmap = new Dictionary<TKey, Node<TValue>>();
        }

        public void Put(TKey key, TValue value)
        {
            if (hashmap.ContainsKey(key))
            {
                hashmap[key].Value = value;
                doublyLinkedList.MoveToHead(value);
            }
            else
            {
                Node<TValue> nodeToInsert = new Node<TValue>(value);

                doublyLinkedList.AddFirst(value);
                hashmap.Add(key, doublyLinkedList.Find(value));
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (hashmap.ContainsKey(key))
            {
                doublyLinkedList.MoveToHead(hashmap[key].Value);
                value = hashmap[key].Value;
                return true;
            }

            value = default(TValue);
            return false;
        }
    }
}
