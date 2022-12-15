namespace _02.BinarySearchTree
{
    using System;
    using System.Collections;
    using System.Threading;
    using System.Xml;

    public class BinarySearchTree<T> : IBinarySearchTree<T>
        where T : IComparable<T>
    {
        private class Node 
        {
            public Node(T value)
            {
                this.Value = value;
            }

            public T Value { get; private set; }

            public Node LeftChild { get; set; }

            public Node RightChild { get; set; }
        }

        private Node root;

        private BinarySearchTree(Node node)
        {
            this.PreOrderCopy(node);
        }


        public BinarySearchTree() { }

        public bool Contains(T element)
        {
            var nodeToFind = this.FindNode(element);

            return nodeToFind != null;
        }
        
        public void EachInOrder(Action<T> action)
        {
            this.EachInOrder(action, this.root);
        }

        public void Insert(T element)
        {
            this.root = this.Insert(element, this.root);
        }

        public IBinarySearchTree<T> Search(T element)
        {
            var node = this.FindNode(element);

            if (node == null)
            {
                return null;
            }

            return new BinarySearchTree<T>(node);
        }
        
        private Node Insert(T element, Node node)
        {
            if (node == null)
            {
                node = new Node(element);
            }
            else if (element.CompareTo(node.Value) == -1)
            {
                node.LeftChild = this.Insert(element, node.LeftChild);
            }
            else if (element.CompareTo(node.Value) == 1)
            {
                node.RightChild = this.Insert(element, node.RightChild);
            }

            return node;
        }
        
        private Node FindNode(T element)
        {
            var node = this.root;

            while (node != null)
            {

                if (element.CompareTo(node.Value) == -1)
                {
                    node = node.LeftChild;
                }
                else if (element.CompareTo(node.Value) == 1)
                {
                    node = node.RightChild;
                }
                else
                {
                    break;
                }
            }

            return node;
        }
        private void PreOrderCopy(Node node)
        {
            if (node == null)
            {
                return;
            }

            this.Insert(node.Value);
            this.PreOrderCopy(node.LeftChild);
            this.PreOrderCopy(node.RightChild);
        }
        
        private void EachInOrder(Action<T> action, Node node)
        {
            if (node == null)
            {
                return;
            }
            
            this.EachInOrder(action, node.LeftChild);
            
            action.Invoke(node.Value);

            this.EachInOrder(action, node.RightChild);
        }
    }
}
