using System.Collections;
using System.Net.Sockets;

namespace HashMapAssignment
{
    class HashMap<TKey, TValue> : IDictionary<TKey, TValue>
    {
        public LinkedList<(TKey Key, TValue Value)>[] buckets;
        public int Count { get; set; }
        public bool IsReadOnly => false;
        public HashMap(int count)
        {
            Count = count;
            buckets = new LinkedList<(TKey, TValue)>[count];
        }

        public void Add(TKey key, TValue value)
        {
            int bucket = key.GetHashCode() % buckets.Length;

            if (buckets[bucket] == null)
            {
                LinkedList<(TKey Key, TValue Value)> bucketLinkedList = new LinkedList<(TKey Key, TValue Value)>();
                bucketLinkedList.AddFirst((key, value));
                buckets[bucket] = bucketLinkedList;
            }
            else
            {
                if (ContainsKey(key))
                {
                    throw new Exception("key already exists");
                }

                buckets[bucket].AddLast((key, value));
            }

            Count++;
        }
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            buckets = new LinkedList<(TKey Key, TValue Value)>[Count];
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            int bucket = item.Key.GetHashCode() % buckets.Length;

            if (buckets[bucket] == null)
            {
                return false;
            }
            if (buckets[bucket].Contains((item.Key, item.Value)))
            {
                return true;
            }

            return false;
        }

        public bool ContainsKey(TKey key)
        {
            int bucket = key.GetHashCode() % buckets.Length;

            if (buckets[bucket] == null)
            {
                return false;
            }

            LinkedListNode<(TKey Key, TValue Value)> current = buckets[bucket].First;
            while (current != null && !current.Value.Key.Equals(key))
            {
                current = current.Next;
            }
            if (current == null)
            {
                return false;
            }

            return true;
        }

        public bool Remove(TKey key)
        {
            int bucket = key.GetHashCode() % buckets.Length;

            LinkedListNode<(TKey Key, TValue Value)> current = buckets[bucket].First;            
            while (current != null && !current.Value.Key.Equals(key))
            {
                current = current.Next;
            }

            if (current == null)
            {
                return false;
            }
            buckets[bucket].Remove(current);
            Count--;

            return true;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        public void ReHash()
        {
            LinkedList<(TKey Key, TValue Value)>[] oldBuckets = buckets;
            buckets = new LinkedList<(TKey Key, TValue Value)>[buckets.Length * 2];
            
            for (int i = 0; i < oldBuckets.Length; i++)
            {
                if (oldBuckets[i] == null)
                {
                    continue;
                }

                LinkedListNode<(TKey Key, TValue Value)> current = oldBuckets[i].First;
                while (current != null)
                {
                    Add(current.Value.Key, current.Value.Value);
                    current = current.Next;
                }
            }
        }

        //DO THESE LAST
        //---
        public TValue this[TKey key] 
        {
            get
            {
                int bucket = key.GetHashCode() % buckets.Length;
                if (buckets[bucket] == null)
                {
                    throw new Exception("doesn't exist");
                }

                LinkedListNode<(TKey Key, TValue Value)> current = buckets[bucket].First;
                while (current != null && !current.Value.Key.Equals(key))
                {
                    current = current.Next;
                }

                if (current.Value.Key.Equals(key))
                {
                    return current.Value.Value;
                }

                throw new Exception("doesn't exist");
            }
            set
            {
                int bucket = key.GetHashCode() % buckets.Length;
                if (buckets[bucket] == null)
                {
                    Add(key, value);
                }

                LinkedListNode<(TKey Key, TValue Value)> current = buckets[bucket].First;
                while (current != null && !current.Value.Key.Equals(key))
                {
                    current = current.Next;
                }

                if (current == null)
                {
                    Add(key, value);
                }
                else
                {
                    current.ValueRef.Value = value;
                }
            }
        }
        
        public bool TryGetValue(TKey key, out TValue value)
        {
            int bucket = key.GetHashCode() % buckets.Length;

            if (buckets[bucket] == null)
            {
                value = default;
                return false;
            }

            LinkedListNode<(TKey Key, TValue Value)> current = buckets[bucket].First;
            while (current != null && !current.Value.Key.Equals(key))
            {
                current = current.Next;
            }
            if (current == null)
            {
                value = default;
                return false;
            }

            value = current.Value.Value;
            return true;
        }


        //Do not do yield return, do proper enumeration -Edden
        //https://sharplab.io/#v2:CYLg1APgAgTAjAWAFBQAwAIpwHQGED2ANoQKYDGALgJb4B2AzgNzJqZwAszSLAzJjOgBKJYAGUKAT1IBBAE6yAhhPQg2PADxVaFAHzIA3snTH0WiuloBXALZwuJ09os2Y9k2efWeb4x6vXOI3cnfwBWH3Qg4yg+YTFJGXklAAo/GzgAGkdzfxgstK98kJt2IpybUIBKKPRDJAcHCgALKnpsfzh0AF5POxrGlrbc7s9XfpNm1vabPh7/b3HjSaGSkf9A+oalwenrULWKiIBfZBqY9ABJAFF/EkUKfFlNbR10AHESChubO4UH2WS1U2xjqW2iAHYLCQAO7ob7WX7/ZLLSrHU7Ay7wxGPNTYD5fW73R6A7qvfFYokA1HohznegUWSWShwwl/HGqa6s/7PXQ1fQmRZCETiKQkOSKZTWCTipQRBweMiWeQkbQXWjAEgADzlAoxtL4HlwStkKvM+gA5p9GOgAA6yKgANz+JHQ9CtJ24epM+AARgArcjmTk/Sl4Y2m0noI3K7RcQXnClsgFxEWJCXoKUyiRAsG1QUDKaZpLKHpFiU64we+N8H34IjoACy+AdJAAclqKIDBaDcyZFTGKGqNZqwGAKw16NCqBQyE1kv2Tar1Vqc72QfmwWQFG70HAQButtHF+ZS9Li7s+l611BIQzLCRx7mtzuYPur72jxHT1ndmN37mb3QO8HwPBpnxdHg3zXQ9wycb9z3mR8wUA4CkK2cD0HYKDoIcT84IzM8JV2DYcNpW9GRA/9N23F1Qmw0io1gk8CJ/MI0IaFCKPYj0cMAgAzBRCDdccq3fKB2ExLlHmwYQ3U7Vcth7NcF1NIctRGVARMFatMAkgARVobXwN0u3fJTe0FHiTA9UTzlgKM8wxWABD5QUUwSMVi08eg1hhZJQiyOBUECgL0BgYKwqqcdgwRSkeVeWsfQOaw2nJKSqXHAB6TKYuxWR0D9KhrBGRKdWy4xCusGSSDkwFx0FaEWlIdBkkS7AmxbdtNXkhSGnMgC4AATla30wwHal3ysrZBT4x4SAUWdkidfLpxIYqtG83qHH65ChtSCg1om3MpoaGobKAA===
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return new Enumerator(this);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>
        {
            HashMap<TKey, TValue> hashmap;
            int currentBucketIndex;
            LinkedList<(TKey Key, TValue Value)> currentBucket;
            int currentIndex;

            public KeyValuePair<TKey, TValue> Current { get; private set; }
            object IEnumerator.Current => Current;

            public Enumerator(HashMap<TKey, TValue> hashmap)
            {
                this.hashmap = hashmap;
                currentBucket = hashmap.buckets[0];
                currentIndex = 0;
            }

            public bool MoveNext()
            {
                if (currentIndex == currentBucket.Count)
                {
                    currentIndex = 0;
                    currentBucketIndex++;
                    currentBucket = hashmap.buckets[currentBucketIndex];
                }
                while (currentBucket == null || currentBucket.Count == 0)
                {
                    if (currentBucketIndex == hashmap.buckets.Length - 1)
                    {
                        return false;
                    }
                    currentBucketIndex++;
                    currentBucket = hashmap.buckets[currentBucketIndex];
                }

                LinkedListNode<(TKey Key, TValue Value)> currentNode = currentBucket.First;
                KeyValuePair<TKey, TValue> pair = new KeyValuePair<TKey, TValue>(currentNode.Value.Key, currentNode.Value.Value);

                for (int i = 0; i <= currentIndex; i++)
                {
                    if (currentNode != null)
                    {
                        pair = new KeyValuePair<TKey, TValue>(currentNode.Value.Key, currentNode.Value.Value);
                    }
                    currentNode = currentNode.Next;
                }

                currentIndex++;

                Current = pair;
                return true;
            }

            public void Reset()
            {
                currentBucketIndex = 0;
                currentBucket = hashmap.buckets[0];
            }
            public void Dispose()
            {
            }
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public ICollection<TKey> Keys => throw new NotImplementedException();

        public ICollection<TValue> Values => throw new NotImplementedException();

    }
}
