namespace Problem03.Queue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Queue<T> : IAbstractQueue<T>
    {
        private class Node
        {
            public Node(T value)
            {
                this.Value = value;
            }

            public T Value { get; set; }

            public Node Tail { get; set; }
        }

        private Node head;

        public int Count { get; set; }

        public void Enqueue(T item)
        {
            Node node = new Node(item);

            if (this.head == null)
            {
                this.head = node;
            }
            else
            {
                Node currentItem = this.head;

                while (currentItem != null)
                {
                    if (currentItem.Tail == null)
                    {
                        currentItem.Tail = node;
                        break;
                    }

                    currentItem = currentItem.Tail;
                }
            }
            
            this.Count++;
        }

        public T Dequeue()
        {
            this.CheckForEmptyQueue();

            T value = this.head.Value;

            if (this.head.Tail == null)
            {
                this.head = null;
            }
            else 
            {
                Node newHead = this.head.Tail;
                this.head = newHead;
            }

            this.Count--;

            return value;
        }

        public T Peek()
        {
            this.CheckForEmptyQueue();

            return this.head.Value;
        }

        public bool Contains(T item)
        {
            Node currentItem = this.head;

            while (currentItem != null)
            {
                if (currentItem.Value.Equals(item))
                {
                    return true;
                }

                currentItem = currentItem.Tail;
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node currentItem = this.head;

            while (currentItem != null)
            {
                yield return currentItem.Value;

                currentItem = currentItem.Tail;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void CheckForEmptyQueue()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Queue is empty.");
            }
        }
    }
}