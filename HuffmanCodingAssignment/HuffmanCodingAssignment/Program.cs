namespace HuffmanCodingAssignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Huffman huffman = new Huffman("ligedhlh;");
            Console.WriteLine(huffman.CompressionVariableLength());
            Console.WriteLine(huffman.DecompressionVariableLength(huffman.CompressionVariableLength()));
            Console.WriteLine(huffman.CompressionFixedLength());
            Console.WriteLine(huffman.DecompressionFixedLength(huffman.CompressionFixedLength()));
        }
    }
}
