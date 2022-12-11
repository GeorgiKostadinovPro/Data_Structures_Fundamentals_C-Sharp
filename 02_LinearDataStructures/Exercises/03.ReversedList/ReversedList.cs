namespace Problem03.ReversedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ReversedList<T> : IAbstractList<T>
    {
        private const int DefaultCapacity = 4;

        private T[] items;

        public ReversedList()
            : this(DefaultCapacity) { }

        public ReversedList(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            this.items = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                this.ValidateIndex(index);

                return this.items[this.Count - index - 1];
            }
            set
            {
                this.ValidateIndex(index);

                this.items[index] = value;
            }
        }

        public int Count { get; private set; }

        public void Add(T item)
        {
            if (this.IsArrayForExpaning())
            {
                this.ExpandArray();
            }

            this.items[this.Count] = item;
            this.Count++;
        }

        public bool Contains(T item)
        {
            return this.IndexOf(item) != -1 ? true : false;
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (item.Equals(this.items[i]))
                {
                    return this.Count - i - 1;
                }
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            this.ValidateIndex(index);

            if (this.IsArrayForExpaning())
            {
                this.ExpandArray();
            }

            for (int i = this.Count; i > this.Count - index; i--)
            {
                this.items[i] = this.items[i - 1];
            }

            this.items[this.Count - index] = item;
            this.Count++;
        }

        public bool Remove(T item)
        {
            int index = this.IndexOf(item);

            if (index == -1)
            {
                return false;
            }

            this.RemoveAt(index);

            return true;
        }

        public void RemoveAt(int index)
        {
            this.ValidateIndex(index);

            for (int i = this.Count - index - 1; i < this.Count - 1; i++)
            {
                this.items[i] = this.items[i + 1];
            }

            this.items[this.Count - 1] = default(T);
            this.Count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = this.Count - 1; i >= 0; i--)
            {
                yield return this.items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException("Index is outside the bounds of the array!");
            }
        }

        private bool IsArrayForExpaning()
        {
            return this.Count == this.items.Length;
        }

        private void ExpandArray()
        {
            T[] itemsCopy = new T[this.items.Length * 2];

            for (int i = 0; i < this.items.Length; i++)
            {
                itemsCopy[i] = this.items[i];
            }

            this.items = itemsCopy;
        }
    }
}