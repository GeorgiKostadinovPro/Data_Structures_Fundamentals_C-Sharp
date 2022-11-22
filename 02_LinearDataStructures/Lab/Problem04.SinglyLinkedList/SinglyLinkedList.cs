namespace Problem04.SinglyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class SinglyLinkedList<T> : IAbstractLinkedList<T>
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

        private Node first;

        public int Count { get; set; }

        public void AddFirst(T item)
        {
            Node node = new Node(item);

            if (this.first == null)
            {
                this.first = node;
            }
            else
            {
                node.Next = this.first;
                this.first = node;
            }

            this.Count++;
        }

        public void AddLast(T item)
        {
            Node node = new Node(item);

            if (this.first == null)
            {
                this.first = node;
            }
            else
            {
                Node currentItem = this.first;

                while (currentItem != null)
                {
                    if (currentItem.Next == null)
                    {
                        currentItem.Next = node;
                        break;
                    }

                    currentItem = currentItem.Next;
                }
            }

            this.Count++;
        }

        public T GetFirst()
        {
            this.CheckForEmptyList();

            return this.first.Value;
        }

        public T GetLast()
        {
            this.CheckForEmptyList();

            T value = default(T);

            if (this.first.Next == null)
            {
                value = this.first.Value;
            }
            else 
            { 
                Node currentItem = this.first;

                while (currentItem != null)
                {
                    if (currentItem.Next == null)
                    {
                        value = currentItem.Value;
                        break;
                    }
                
                    currentItem = currentItem.Next;
                }
            } 

            return value;
        }

        public T RemoveFirst()
        {
            this.CheckForEmptyList();

            T value = this.first.Value;

            if (this.first.Next == null)
            {
                this.first = null;
            }
            else
            {
                Node newFirst = this.first.Next;
                this.first = newFirst;
            }

            this.Count--;
            return value;
        }

        public T RemoveLast()
        {
            this.CheckForEmptyList();

            T value = default(T);

            if (this.first.Next == null)
            {
                value = this.first.Value;
                this.first = null;
            }
            else
            {
                Node currentItem = this.first;
                Node prevItem = null;

                while (currentItem != null)
                {
                    if (currentItem.Next == null)
                    {
                        value = currentItem.Value;
                        prevItem.Next = null;
                        break;
                    }

                    prevItem = currentItem;
                    currentItem = currentItem.Next;
                }
            }

            this.Count--;
            return value;
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            Node currentItem = this.first;

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
                throw new InvalidOperationException("List is empty.");
            }
        }
    }
}