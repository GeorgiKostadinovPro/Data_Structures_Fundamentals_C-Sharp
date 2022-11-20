namespace Problem02.Stack
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Stack<T> : IAbstractStack<T>
    {
        private class Node
        {
            public Node(T value)
            {
                this.Value = value;
            }

            public T Value { get; set; }

            public Node Next { get; set; }
        }

        private Node top;

        public int Count { get; set; }

        public void Push(T item)
        {
            Node node = new Node(item);

            if (this.top == null)
            {
                this.top = node;
            }
            else
            {
                node.Next = this.top;
                this.top = node;
            }

            this.Count++;
        }

        public T Pop()
        {
            this.CheckForEmptyStack();

            T value = this.top.Value;

            if (this.top.Next == null)
            {
                this.top = null;
            }
            else 
            { 
                Node newTop = this.top.Next;
                this.top = newTop;
            }

            this.Count--;

            return value;
        }

        public T Peek()
        {
            this.CheckForEmptyStack();

            return this.top.Value;
        }

        public bool Contains(T item)
        {
            Node currentItem = this.top;

            while (currentItem != null)
            {
                if (currentItem.Value.Equals(item))
                {
                    return true;
                }

                currentItem = currentItem.Next;
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node currentItem = this.top;

            while (currentItem != null)
            {
                yield return currentItem.Value;

                currentItem = currentItem.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        
        private void CheckForEmptyStack()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Stack is empty.");
            }
        }
    }
}