using System.Collections.Generic;

namespace TrieAssignment
{
    class Node
    {
        public char Letter;
        public Dictionary<char, Node> Children;
        public bool isAWord;

        public Node(char letter)
        {
            Letter = letter;
            Children = new Dictionary<char, Node>();
            isAWord = false;
        }
    }
    class Trie
    {
        public Node root = new Node(default);

        public void Insert(string givenWord)
        {
            Node current = root;
            for(int i = 0; i < givenWord.Length; i++)
            {
                if (current.Children.ContainsKey(givenWord[i]))
                {
                    current = current.Children[givenWord[i]];
                    continue;
                }
                Node nodeToInsert = new Node(givenWord[i]);
                current.Children.Add(givenWord[i], nodeToInsert);
                current = current.Children[givenWord[i]];
            }
            current.isAWord = true;
        }
        public bool Remove(string givenWord)
        {
            if (Contains(givenWord) == false)
            {
                return false;
            }
            
            Node current = SearchForNode(givenWord);
            current.isAWord = false;
            return true;
        }
        public bool Contains(string givenWord)
        {
            Node current = root;
            for(int i = 0; i < givenWord.Length; i++)
            {
                if (current.Children.ContainsKey(givenWord[i]))
                {
                    current = current.Children[givenWord[i]];
                    continue;
                }
                return false;
            }
            return true;
        }
        public Node SearchForNode(string givenWord)
        {
            if (Contains(givenWord) == false)
            {
                throw new Exception("this word does not exist");
            }

            Node current = root;
            for (int i = 0; i < givenWord.Length; i++)
            {
                current = current.Children[givenWord[i]];
            }
            return current;
        }
        public List<string> GetAllMatchingPrefix(string givenPrefix)
        {
            if (Contains(givenPrefix) == false)
            {
                throw new Exception("this prefix does not exist");
            }

            List<string> results = new List<string>();
            
            Node current = SearchForNode(givenPrefix);

            AllMatchingThePrefix(current, results, givenPrefix);

            return results;
        }

        public void AllMatchingThePrefix(Node current, List<string> results, string givenPrefix)
        {
            if(current == null)
            {
                return;
            }

            for(int i = 0; i < current.Children.Count; i++)
            {
                AllMatchingThePrefix(current.Children.ElementAt(i).Value, results, givenPrefix + current.Children.ElementAt(i).Key);
            }

            if(current.isAWord == true)
            {
                results.Add(givenPrefix);
            }
        }

    }

    internal class Program
    {
        static Dictionary<string,string> ParseFile(string[] file, ref List<string> words, ref List<string> definitions)
        {
            for (int i = 1; i < file.Length - 1; i++)
            {
                string[] splitLine = new string[2];
                splitLine = file[i].Split(": ");

                splitLine[0] = splitLine[0].Remove(0, 3);
                splitLine[0] = splitLine[0].Remove(splitLine[0].Length - 1, 1);
                splitLine[1] = splitLine[1].Remove(0, 1);
                splitLine[1] = splitLine[1].Remove(splitLine[1].Length - 2, 2);

                words.Add(splitLine[0]);
                definitions.Add(splitLine[1]);
            }

            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            for (int i = 0; i < definitions.Count; i++)
            {
                dictionary.Add(words[i], definitions[i]);
            }

            return dictionary;
        }

        static List<string> AllSimilarWords(Trie trie, string givenWord)
        {
            List<string> similarWords = new List<string>();
            for (int i = 0; i < givenWord.Length - (givenWord.Length / 3); i++)
            {
                string currentPrefix = givenWord.Remove(givenWord.Length - i);
                if(!trie.Contains(currentPrefix))
                {
                    continue;
                }
                List<string> matchingPrefix = trie.GetAllMatchingPrefix(currentPrefix);
                for (int index = 0; index < matchingPrefix.Count; index++)
                {
                    if (!similarWords.Contains(matchingPrefix[index]))
                    {
                        similarWords.Add(matchingPrefix[index]);
                    }
                }
            }

            return similarWords;
        }
        
        static void Main(string[] args)
        {
            //Trie trie = new Trie();
            //trie.Insert("he");
            //trie.Insert("heed");
            //trie.Insert("heep");
            //trie.Insert("heeps");
            //trie.Insert("hello");
            //trie.Insert("hell");
            //trie.Insert("hey");
            //trie.Insert("help");
            //trie.Insert("cat");
            //trie.SearchForNode("help");
            //trie.Contains("cat");
            //trie.Remove("help");

            //List<string> matchingThePrefix = trie.GetAllMatchingPrefix("he");
            //for(int i = 0; i < matchingThePrefix.Count; i++)
            //{
            //    Console.WriteLine(matchingThePrefix[i]);
            //}


            //word to test wth: irregardless
            string[] file = System.IO.File.ReadAllLines(@"C:\Users\saige.kumar\Downloads\fulldictionary.json");
            List<string> words = new List<string>();
            List<string> definitions = new List<string>();
            Trie wordsInDictionary = new Trie();
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            dictionary = ParseFile(file, ref words, ref definitions);

            for (int i = 0; i < words.Count; i++)
            {
                wordsInDictionary.Insert(words[i]);
            }

            Console.WriteLine("Enter a word: ");
            string givenWord = Console.ReadLine();

            if(dictionary.ContainsKey(givenWord))
            {
                Console.WriteLine();
                Console.WriteLine(dictionary[givenWord]);
            }
            else
            {
                List<string> similarWords = AllSimilarWords(wordsInDictionary, givenWord);
                Console.WriteLine("\nThat word does not exist!");
                if (similarWords.Count >= 1)
                {
                    Console.WriteLine("Here are some similar words with their definitions:");
                }
                for (int i = 0; i < similarWords.Count; i++)
                {
                    Console.WriteLine($"{similarWords[i]}: {dictionary[similarWords[i]]}");
                }
            }

        }
    }
}