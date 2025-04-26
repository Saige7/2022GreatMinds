using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffmanCodingAssignment
{
    internal class Huffman
    {
        public string Data;
        public Dictionary<char, int> leaves;
        public Dictionary<char, string> codes;
        public Dictionary<string, char> codesOppositeWay;
        public Node root;
        public int length;
        public Huffman(string data)
        {
            Data = data;
            length = 0;
            leaves = new Dictionary<char, int>();
            codesOppositeWay = new Dictionary<string, char>();
            codes = BuildCodes();
            root = BuildTree();
        }

        public Node BuildTree()
        {
            for (int i = 0; i < Data.Length; i++)
            {
                if (leaves.ContainsKey(Data[i]))
                {
                    leaves[Data[i]]++;
                    continue;
                }

                leaves.Add(Data[i], 1);
            }

            PriorityQueue<Node, int> priorityQueue = new PriorityQueue<Node, int>();

            foreach (var keyValuePair in leaves)
            {
                Node node = new Node(keyValuePair.Key);
                node.weight = leaves[keyValuePair.Key];
                priorityQueue.Enqueue(node, leaves[keyValuePair.Key]);
            }

            while (priorityQueue.Count > 1)
            {
                Node parent = new Node('\0');
                parent.Left = priorityQueue.Dequeue();
                parent.Right = priorityQueue.Dequeue();
                parent.weight = parent.Left.weight + parent.Right.weight;
                parent.Left.Parent = parent;
                parent.Right.Parent = parent;

                priorityQueue.Enqueue(parent, parent.weight);
            }

            return priorityQueue.Dequeue();
        }
        public Dictionary<char, string> GetDirections()
        {
            Dictionary<char, string> directionsOfLeaves = new Dictionary<char, string>();
            Node current = root;
            Node parent = current;
            List<Node> list = new List<Node>();
            string directions = "";

            if (leaves.Count == 1)
            {
                directionsOfLeaves.Add(root.Value, "0");
            }

            while (directionsOfLeaves.Count < leaves.Count)
            {
                if (current.Value != '\0' && !directionsOfLeaves.ContainsKey(current.Value))
                {
                    directionsOfLeaves.Add(current.Value, directions);
                }

                if (current.Left != null && !list.Contains(current.Left))
                {
                    parent = current;
                    directions += "0";
                    current = current.Left;
                    list.Add(current);
                }
                else if ((current.Left == null || list.Contains(current.Left)) && current.Right != null && !list.Contains(current.Right))
                {
                    directions += "1";
                    parent = current;
                    current = current.Right;
                    list.Add(current);
                }
                else if ((current.Left == null && current.Right == null) || (list.Contains(current.Left) && list.Contains(current.Right)))
                {
                    current = current.Parent;
                    directions = directions.Remove(directions.Length - 1);
                }     
            }

            return directionsOfLeaves;
        }
        public string CompressionVariableLength()
        {
            Dictionary<char, string> directions = GetDirections();
            string result = "";

            for (int i = 0; i < Data.Length; i++)
            {
                result += directions[Data[i]];
            }

            return result;
        }
        public string DecompressionVariableLength(string compressed)
        {
            Node current = root;
            string result = "";

            for (int i = 0; i < compressed.Length; i++)
            {
                if (compressed[i] == '0' && current.Left != null)
                {
                    current = current.Left;
                }
                else if(current.Right != null)
                {
                    current = current.Right;
                }

                if (current.Value != '\0')
                {
                    result += current.Value;
                    current = root;
                }
            }

            return result;
        }

        public Dictionary<char, string> BuildCodes()
        {
            Dictionary<char, string> codes = new Dictionary<char, string>();

            for (int i = 0; i < Data.Length; i++)
            {
                if (codes.ContainsKey(Data[i]))
                {
                    continue;
                }
                codes.Add(Data[i], Convert.ToString(i, 2));

            }

            length = codes[Data[Data.Length - 1]].Length;
            for (int i = 0; i < Data.Length; i++)
            {
                while (codes[Data[i]].Length < length)
                {
                    codes[Data[i]] = "0" + codes[Data[i]];
                }
                if (codesOppositeWay.ContainsKey(codes[Data[i]]))
                {
                    continue;
                }
                codesOppositeWay.Add(codes[Data[i]], Data[i]);
            }

            return codes;
        }

        public string CompressionFixedLength()
        {
            string result = "";

            for (int i = 0; i < Data.Length; i++)
            {
                result += codes[Data[i]];
            }

            return result;
        }
        public string DecompressionFixedLength(string compressed)
        {
            string code = "";
            string result = "";
            int count = 1;

            for (int i = 0; i < compressed.Length; i++)
            {
                code += compressed[i];
                if (count % length == 0)
                {
                    result += codesOppositeWay[code];
                    code = "";
                }
                count++;
            }

            return result;
        }

    }
}
