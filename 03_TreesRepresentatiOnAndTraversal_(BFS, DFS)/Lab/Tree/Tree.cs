namespace Tree
{
    using System;
    using System.Collections.Generic;

    public class Tree<T> : IAbstractTree<T>
    {
        private T value;
        private Tree<T> parentNode;
        private readonly IList<Tree<T>> children;

        public Tree(T value)
        {
            this.value = value;
            this.children = new List<Tree<T>>();
        }

        public Tree(T value, params Tree<T>[] children)
            : this(value)
        {
            foreach (Tree<T> child in children)
            {
                child.parentNode = this;
                this.children.Add(child);
            }
        }

        public IEnumerable<T> OrderBfs()
        {
            Queue<Tree<T>> nodes = new Queue<Tree<T>>();
            ICollection<T> result = new List<T>();

            nodes.Enqueue(this);

            while (nodes.Count > 0)
            {
                Tree<T> node = nodes.Dequeue();
                result.Add(node.value);

                foreach (Tree<T> child in node.children)
                {
                    nodes.Enqueue(child);
                }
            }

            return result;
        }

        public IEnumerable<T> OrderDfs()
        {
            Stack<Tree<T>> nodes = new Stack<Tree<T>>();
            Stack<T> result = new Stack<T>();

            nodes.Push(this);

            while (nodes.Count > 0)
            {
                Tree<T> node = nodes.Pop();

                foreach (Tree<T> child in node.children)
                {
                    nodes.Push(child);
                }

                result.Push(node.value);
            }

            return result;
        }

                
        public void AddChild(T parentKey, Tree<T> child)
        {
            Tree<T> parentNode = this.FindNodeWithBfs(parentKey);

            if (parentNode == null)
            {
                throw new ArgumentNullException();
            }

            parentNode.children.Add(child);
            child.parentNode = parentNode;
        }

        public void RemoveNode(T nodeKey)
        {
            Tree<T> nodeToRemove = this.FindNodeWithBfs(nodeKey);

            if (nodeToRemove == null)
            {
                throw new ArgumentNullException();
            }

            Tree<T> parentNode = nodeToRemove.parentNode;

            if (parentNode == null)
            {
                throw new ArgumentException();
            }

            parentNode.children.Remove(nodeToRemove);
            nodeToRemove.parentNode = null;
        }

        public void Swap(T firstKey, T secondKey)
        {
            Tree<T> firstNode = this.FindNodeWithBfs(firstKey);
            Tree<T> secondNode = this.FindNodeWithBfs(secondKey);

            if (firstNode == null)
            {
                throw new ArgumentNullException();
            }

            if (secondKey == null)
            {
                throw new ArgumentNullException();
            }

            Tree<T> firstParentNode = firstNode.parentNode;

            if (firstParentNode == null)
            {
                throw new ArgumentException();
            }

            Tree<T> secondParentNode = secondNode.parentNode;

            if (secondParentNode == null)
            {
                throw new ArgumentException();
            }

            int firstNodeIdx = firstParentNode.children.IndexOf(firstNode);
            int secondNodeIdx = secondParentNode.children.IndexOf(secondNode);

            firstParentNode.children[firstNodeIdx] = secondNode;
            secondNode.parentNode = firstParentNode;
            secondParentNode.children[secondNodeIdx] = firstNode;
            firstNode.parentNode = secondParentNode;
        }

        private void Dfs(Tree<T> node, ICollection<T> result) 
        {
            foreach (Tree<T> child in node.children)
            {
                this.Dfs(child, result);
            }  
            
            result.Add(node.value);
        } 

        private Tree<T> FindNodeWithBfs(T parentKey)
        {
            Queue<Tree<T>> nodes = new Queue<Tree<T>>();

            nodes.Enqueue(this);

            while (nodes.Count > 0)
            {
                Tree<T> node = nodes.Dequeue();

                if (node.value.Equals(parentKey))
                {
                    return node;
                }

                foreach (Tree<T> child in node.children)
                {
                    nodes.Enqueue(child);
                }
            }

            return null;
        }
    }
}
