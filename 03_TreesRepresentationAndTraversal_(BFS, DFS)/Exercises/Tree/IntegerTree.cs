namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class IntegerTree : Tree<int>, IIntegerTree
    {
        public IntegerTree(int key, params Tree<int>[] children)
            : base(key, children)
        {
        }

        public IEnumerable<IEnumerable<int>> GetPathsWithGivenSum(int sum)
        {
            var leaves = this.BfsWithResultKeys(t => t.Children.Count == 0);
            ICollection<ICollection<int>> paths = new List<ICollection<int>>();

            foreach (var leaf in leaves)
            {
                List<int> leafPath = new List<int>();
                var node = leaf;

                while (node != null)
                {
                    leafPath.Add(node.Key);

                    if (node.Parent == null)
                    {
                        break;
                    }
                    
                    node = node.Parent;
                }

                if (leafPath.Sum() == sum)
                {
                    leafPath.Reverse();
                    paths.Add(leafPath);
                }
            }

            return paths;
        }

        public IEnumerable<Tree<int>> GetSubtreesWithGivenSum(int sum)
        {
            var roots = new List<Tree<int>>();

            this.SubtreesSumDFS(this, roots, sum);

            return roots;
        }

        private int SubtreesSumDFS(Tree<int> node, List<Tree<int>> roots, int targetSum)
        {
            var currSum = node.Key;

            foreach (var child in node.Children)
            {
                currSum += SubtreesSumDFS(child, roots, targetSum);
            }

            if (currSum == targetSum)
            {
                roots.Add(node);
            }

            return currSum;
        }
    }
}
