namespace _02.BinarySearchTree
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class BinarySearchTree<T> : IBinarySearchTree<T> where T : IComparable
    {
        private class Node
        {
            public Node(T value)
            {
                this.Value = value;
            }

            public T Value { get; }
            public Node Left { get; set; }
            public Node Right { get; set; }
        }

        private Node root;

        private BinarySearchTree(Node node)
        {
            this.PreOrderCopy(node);
        }

        public BinarySearchTree()
        {
        }

        public void Insert(T element)
        {
            this.root = this.Insert(element, this.root);
        }

        public bool Contains(T element)
        {
            Node current = this.FindElement(element);

            return current != null;
        }

        public void EachInOrder(Action<T> action)
        {
            this.EachInOrder(this.root, action);
        }

        public IBinarySearchTree<T> Search(T element)
        {
            Node current = this.FindElement(element);

            return new BinarySearchTree<T>(current);
        }

        public void Delete(T element)
        {
            throw new NotImplementedException();
        }

        public void DeleteMax()
        {
            if (this.root == null)
            {
                throw new InvalidOperationException("The stack is empty!");
            }

            Node root = this.DeleteMaxNode(this.root);
            this.root = root;
        }

        public void DeleteMin()
        {
            if (this.root == null)
            {
                throw new InvalidOperationException("The stack is empty!");
            }

            Node root = this.DeleteMinNode(this.root);
            this.root = root;
        }

        public int Count()
        {
            return this.CountNodes(root);
        }

        public int Rank(T element)
        {
            return this.NodeRank(element, this.root);
        }

        public T Select(int rank)
        {
            Node node = this.SelectNode(this.root, rank);

            if (node == null)
            {
                throw new InvalidOperationException();
            }

            return node.Value;
        }

        public T Ceiling(T element)
        {
            return this.Select(this.Rank(element) + 1);
        }

        public T Floor(T element)
        {
            return this.Select(this.Rank(element) - 1);
        }

        public IEnumerable<T> Range(T startRange, T endRange)
        {
            Queue<T> elementsInRange = new Queue<T>();

            this.GetRange(this.root, startRange, endRange, elementsInRange);

            return elementsInRange;
        }
        
        private Node DeleteMinNode(Node node)
        {
            if (node.Left == null)
            {
                return node.Right;
            }

            node.Left = this.DeleteMinNode(node.Left);

            return node;
        }
        
        private Node DeleteMaxNode(Node node)
        {
            if (node.Right == null)
            {
                return node.Left;
            }

            node.Right = this.DeleteMaxNode(node.Right);

            return node;
        }
        
        private int CountNodes(Node node)
        {
            if (node == null)
            {
                return 0;
            }

            return 1 + this.CountNodes(node.Left) + this.CountNodes(node.Right);
        }
        
        private int NodeRank(T element, Node node)
        {
            if (node == null)
            {
                return 0;
            }

            if (element.CompareTo(node.Value) == -1)
            {
                return this.NodeRank(element, node.Left);
            }
            else if (element.CompareTo(node.Value) == 1)
            {
                return 1 + this.CountNodes(node.Left) + this.NodeRank(element, node.Right);
            }

            return this.CountNodes(node.Left);
        }

        private Node SelectNode(Node node, int rank)
        {
            if (node == null)
            {
                return null;
            }

            int leftCount = this.CountNodes(node.Left);

            if (leftCount == rank)
            {
                return node;
            }

            if (leftCount > rank)
            {
                return this.SelectNode(node.Left, rank);
            }
            else
            {
                return this.SelectNode(node.Right, rank - (leftCount + 1));
            }
        }

        private void GetRange(Node node, T startRange, T endRange, Queue<T> elementsInRange)
        {
            if (node == null)
            {
                return;
            }

            bool nodeInLowerRange = startRange.CompareTo(node.Value) == -1;
            bool nodeInUpperRange = endRange.CompareTo(node.Value) == 1;

            if (nodeInLowerRange)
            {
                this.GetRange(node.Left, startRange, endRange, elementsInRange);
            }

            if (startRange.CompareTo(node.Value) <= 0
                && endRange.CompareTo(node.Value) >= 0)
            {
                elementsInRange.Enqueue(node.Value);
            }

            if (nodeInUpperRange)
            {
                this.GetRange(node.Right, startRange, endRange, elementsInRange);
            }
        }

        private Node FindElement(T element)
        {
            Node current = this.root;

            while (current != null)
            {
                if (current.Value.CompareTo(element) > 0)
                {
                    current = current.Left;
                }
                else if (current.Value.CompareTo(element) < 0)
                {
                    current = current.Right;
                }
                else
                {
                    break;
                }
            }

            return current;
        }

        private void PreOrderCopy(Node node)
        {
            if (node == null)
            {
                return;
            }

            this.Insert(node.Value);
            this.PreOrderCopy(node.Left);
            this.PreOrderCopy(node.Right);
        }

        private Node Insert(T element, Node node)
        {
            if (node == null)
            {
                node = new Node(element);
            }
            else if (element.CompareTo(node.Value) < 0)
            {
                node.Left = this.Insert(element, node.Left);
            }
            else if (element.CompareTo(node.Value) > 0)
            {
                node.Right = this.Insert(element, node.Right);
            }

            return node;
        }

        private void EachInOrder(Node node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }

            this.EachInOrder(node.Left, action);
            action(node.Value);
            this.EachInOrder(node.Right, action);
        }
    }
}
