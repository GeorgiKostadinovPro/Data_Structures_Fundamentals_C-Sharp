namespace Demo
{
    using System;
    using Tree;
    using System.Linq;

    public class Program
    {
        public static void Main(string[] args)
        {
            //string[] input = new string[] { "9 17", "9 4", "9 14", "4 36", "14 53", "14 59", "53 67", "53 73" };

            string[] input = new string[] { "7 19", "7 21", "7 14", "19 1", "19 12", "19 31", "14 23", "14 6" };

            IntegerTreeFactory treeFactory = new IntegerTreeFactory();

            IntegerTree tree = treeFactory.CreateTreeFromStrings(input);

            Console.WriteLine($"Leaf keys: {string.Join(", ", tree.GetLeafKeys())}");
            Console.WriteLine($"Internal keys: {string.Join(", ", tree.GetInternalKeys())}");
            Console.WriteLine($"Deepest key: {string.Join(", ", tree.GetDeepestKey())}");
            Console.WriteLine($"Longest path to the root: {string.Join(", ", tree.GetLongestPath())}");

            var paths = tree.GetPathsWithGivenSum(27);

            foreach (var path in paths)
            {
                 Console.WriteLine($"Paths with sum equal to 27: {string.Join(" ", path)}");
            }

            var subtrees = tree.GetSubtreesWithGivenSum(43);

            
            Console.WriteLine($"Paths with sum equal to 43: {string.Join(" ", subtrees.Select(sb => sb.Key))}");
        }
    }
}
