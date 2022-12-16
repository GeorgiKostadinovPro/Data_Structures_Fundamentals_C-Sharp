namespace _02.LowestCommonAncestor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
        where T : IComparable<T>
    {
        public BinaryTree(
            T value,
            BinaryTree<T> leftChild,
            BinaryTree<T> rightChild)
        {
            this.Value = value;
            this.LeftChild = leftChild;
            this.RightChild = rightChild;
            if (leftChild != null)
            {
                this.LeftChild.Parent = this;
            }

            if (rightChild != null)
            {
                this.RightChild.Parent = this;
            }
        }

        public T Value { get; set; }

        public BinaryTree<T> LeftChild { get; set; }

        public BinaryTree<T> RightChild { get; set; }

        public BinaryTree<T> Parent { get; set; }

        public T FindLowestCommonAncestor(T first, T second)
        {
            BinaryTree<T> firstNode = this.FindWithBfs(first, this);
            BinaryTree<T> secondNode = this.FindWithBfs(second, this);

            if (firstNode == null || secondNode == null)
            {
                throw new InvalidOperationException();
            }

            var firstNodeAncestors = this.FindAncestors(firstNode);
            var secondNodeAncestors = this.FindAncestors(secondNode);

            return firstNodeAncestors.Intersect(secondNodeAncestors).First();
        }

        private Queue<T> FindAncestors(BinaryTree<T> root)
        {
            var result = new Queue<T>();

            var currNode = root;

            while (currNode != null)
            {
                result.Enqueue(currNode.Value);

                currNode = currNode.Parent;
            }

            return result;
        }

        private BinaryTree<T> FindWithBfs(T element, BinaryTree<T> root)
        {
            var queue = new Queue<BinaryTree<T>>();

            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                var currNode = queue.Dequeue();

                if (element.Equals(currNode.Value))
                {
                    return currNode;
                }

                if (currNode.LeftChild != null)
                {
                    queue.Enqueue(currNode.LeftChild);
                }
                        
                if(currNode.RightChild != null)
                {
                    queue.Enqueue(currNode.RightChild);
                }
            }

            return null;
        }
    }
}
