namespace Problem02.DoublyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class DoublyLinkedList<T> : IAbstractLinkedList<T>
    {
        private class Node
        {
            public Node(T value)
            {
                this.Value = value;
            }

            public T Value { get; set; }

            public Node Next { get; set; }

            public Node Prev { get; set; }
        }

        private Node head;
        private Node tail;

        public int Count { get; set; }

        public void AddFirst(T item)
        {
            Node node = new Node(item);

            if (this.head == null)
            {
                this.head = node;
                this.tail = node;
            }
            else
            {
                node.Next = this.head;
                this.head.Prev = node;
                this.head = node;
            }

            this.Count++;
        }

        public void AddLast(T item)
        {
            Node node = new Node(item);

            if (this.tail == null)
            {
                this.head = node;
                this.tail = node;
            }
            else
            {
                node.Prev = this.tail;
                this.tail.Next = node;
                this.tail = node;
            }

            this.Count++;
        }

        public T GetFirst()
        {
            this.CheckForEmptyList();

            return this.head.Value;
        }

        public T GetLast()
        {
            this.CheckForEmptyList();

            return this.tail.Value;
        }

        public T RemoveFirst()
        {
            this.CheckForEmptyList();

            T value = this.head.Value;

            if (this.head == this.tail)
            {
                this.head = null;
                this.tail = null;
            }
            else
            {
                Node newFirst = this.head.Next;
                newFirst.Prev = null;
                this.head = newFirst;
            }

            this.Count--;
            return value;
        }

        public T RemoveLast()
        {
            this.CheckForEmptyList();

            T value = this.tail.Value;

            if (this.head == this.tail)
            {
                this.head = null;
                this.tail = null;
            }
            else
            {
                Node newLast = this.tail.Prev;
                newLast.Next = null;
                this.tail = newLast;
            }

            this.Count--;
            return value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node currentItem = this.head;

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
        
        private void CheckForEmptyList()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("List is empty!");
            }
        }
    }
}