namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Xml.Linq;

    public class Tree<T> : IAbstractTree<T>
    {
        private ICollection<Tree<T>> children;
        public Tree(T key, params Tree<T>[] children)
        {
            this.Key = key;
            this.children = new List<Tree<T>>();

            foreach (var child in children)
            {
                this.AddChild(child);
                child.Parent = this;
            }
        }

        public T Key { get; private set; }

        public Tree<T> Parent { get; private set; }

        public IReadOnlyCollection<Tree<T>> Children 
            => (IReadOnlyCollection<Tree<T>>)this.children;

        public void AddChild(Tree<T> child)
        {
            this.children.Add(child);
        }

        public void AddParent(Tree<T> parent)
        {
            this.Parent = parent;
        }

        public string AsString()
        {
            StringBuilder sb = new StringBuilder();

            this.DFSAsString(sb, this, 0);

            return sb.ToString().TrimEnd();
        }

        private void DFSAsString(StringBuilder sb, Tree<T> tree, int indent)
        {
            sb.Append(' ', indent)
                .AppendLine(tree.Key.ToString());

            foreach (Tree<T> child in tree.children)
            {
                this.DFSAsString(sb, child, indent + 2);
            }
        }

        public IEnumerable<T> GetInternalKeys()
        {
            return this.BfsWithResultKeys(t => t.Parent != null && t.children.Count > 0)
                .Select(t => t.Key);
               
        }

        public IEnumerable<T> GetLeafKeys()
        {
            return this.BfsWithResultKeys(t => t.children.Count == 0)
                .Select(t => t.Key);
        }

        protected IEnumerable<Tree<T>> BfsWithResultKeys(Predicate<Tree<T>> predicate)
        {
            Queue<Tree<T>> queue = new Queue<Tree<T>>();
            List<Tree<T>> result = new List<Tree<T>>();

            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                Tree<T> node = queue.Dequeue();

                if (predicate.Invoke(node))
                {
                    result.Add(node);
                }

                foreach (Tree<T> child in node.children)
                {
                    queue.Enqueue(child);
                }
            }

            return result;
        }

        public T GetDeepestKey()
        {
            return this.GetDeepestNode().Key;
        }

        private Tree<T> GetDeepestNode()
        {
            var leaves = this.BfsWithResultKeys(t => t.children.Count == 0);

            Tree<T> deepestNode = null;
            int maxDepth = 0;

            foreach (Tree<T> leaf in leaves)
            {
                int depth = this.GetLeafDepth(leaf);

                if (depth > maxDepth)
                {
                    maxDepth = depth;
                    deepestNode = leaf;
                }
            }

            return deepestNode;
        }

        private int GetLeafDepth(Tree<T> leaf)
        { 
            int depth = 0;
            Tree<T> node = leaf;

            while (node.Parent != null)
            {
                depth++;

                node = node.Parent;
            }

            return depth;
        }

        public IEnumerable<T> GetLongestPath()
        {
            Tree<T> deepestNode = this.GetDeepestNode();
            Tree<T> node = deepestNode;
            List<T> nodes = new List<T>();

            while (node != null)
            {
                nodes.Add(node.Key);

                if (node.Parent == null)
                {
                    break;
                }
                
                node = node.Parent;
            }

            nodes.Reverse();

            return nodes;
        }
    }
}
