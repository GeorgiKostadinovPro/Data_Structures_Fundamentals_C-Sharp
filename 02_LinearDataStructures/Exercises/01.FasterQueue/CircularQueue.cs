namespace Problem01.CircularQueue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class CircularQueue<T> : IAbstractQueue<T>
    {
        private const int DEFAULT_CAPACITY = 4;

        private T[] items;
        private int startIdx;
        private int endIdx;

        public CircularQueue()
            : this(DEFAULT_CAPACITY)
        { }

        public CircularQueue(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }

            this.items = new T[capacity];
        }

        public int Count { get; set; }

        public void Enqueue(T item)
        {
            if (this.IsArrayForExpanding())
            {
                this.ExpandArray();
            }

            this.items[this.endIdx] = item;
            this.endIdx = (this.endIdx + 1) % this.items.Length;
            this.Count++;
        }

        public T Dequeue()
        {
            this.CheckForEmptyQueue();

            T value = this.items[this.startIdx];
            this.startIdx = (this.startIdx + 1) % this.items.Length;
            this.Count--;

            return value;
        }

        public T Peek()
        {
            this.CheckForEmptyQueue();

            return this.items[this.startIdx];
        }

        public T[] ToArray()
        {
            T[] itemsCopy = new T[this.Count];

            for (int i = 0; i < this.Count; i++)
            {
                itemsCopy[i] = this.items[(this.startIdx + i)
                    % this.items.Length];
            }

            return itemsCopy;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                yield return this.items[(this.startIdx + i) 
                    % this.items.Length];
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

        private bool IsArrayForExpanding()
        {
            return this.Count == this.items.Length;
        }

        private void ExpandArray()
        {
            T[] itemsCopy = new T[this.items.Length * 2];

            for (int i = 0; i < this.Count; i++)
            {
                itemsCopy[i] = this.items[(this.startIdx + i)
                    % this.items.Length];
            }

            this.items = itemsCopy;

            this.startIdx = 0;
            this.endIdx = this.Count;
        }
    }
}
